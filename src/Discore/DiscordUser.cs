using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordUser : DiscordIdEntity
    {
        /// <summary>
        /// Gets the name of this user.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Gets the user's 4-digit discord-tag.
        /// </summary>
        public string Discriminator { get; }

        /// <summary>
        /// Gets the user's avatar or null if the user does not have an avatar.
        /// </summary>
        public DiscordCdnUrl? Avatar { get; }

        /// <summary>
        /// Gets whether this account belongs to an OAuth application.
        /// </summary>
        public bool IsBot { get; }

        // TODO: Consider renaming HasTwoFactorAuth to MfaEnabled

        /// <summary>
        /// Gets whether this account has two-factor authentication enabled.
        /// </summary>
        public bool? HasTwoFactorAuth { get; }

        /// <summary>
        /// Gets whether the email on this account is verified.
        /// </summary>
        public bool? IsVerified { get; }

        /// <summary>
        /// Gets the email (if available) of this account.
        /// </summary>
        public string? Email { get; }

        // TODO: Add flags, premium_type, locale

        internal DiscordUser(JsonElement json)
            : base(json)
        {
            string? avatarHash = json.GetProperty("avatar").GetString();
            Avatar = avatarHash != null ? DiscordCdnUrl.ForUserAvatar(Id, avatarHash) : null;

            Username = json.GetProperty("username").GetString();
            Discriminator = json.GetProperty("discriminator").GetString();
            IsBot = json.GetPropertyOrNull("bot")?.GetBoolean() ?? false;
            HasTwoFactorAuth = json.GetPropertyOrNull("mfa_enabled")?.GetBoolean();
            IsVerified = json.GetPropertyOrNull("verified")?.GetBoolean();
            Email = json.GetPropertyOrNull("email")?.GetString();
        }

        public override string ToString()
        {
            return Username;
        }
    }
}

#nullable restore
