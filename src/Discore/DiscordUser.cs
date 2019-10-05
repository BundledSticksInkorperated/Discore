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

        private DiscordUser(Snowflake id,
            string username, 
            string discriminator, 
            DiscordCdnUrl? avatar, 
            bool isBot, 
            bool? hasTwoFactorAuth, 
            bool? isVerified, 
            string? email)
            : base(id)
        {
            Username = username;
            Discriminator = discriminator;
            Avatar = avatar;
            IsBot = isBot;
            HasTwoFactorAuth = hasTwoFactorAuth;
            IsVerified = isVerified;
            Email = email;
        }

        public override string ToString()
        {
            return Username;
        }

        internal static DiscordUser FromJson(JsonElement json)
        {
            Snowflake id = json.GetProperty("id").GetUInt64();

            string? avatarHash = json.GetProperty("avatar").GetString();
            DiscordCdnUrl? avatar = avatarHash != null ? DiscordCdnUrl.ForUserAvatar(id, avatarHash) : null;

            return new DiscordUser(
                id: id,
                username: json.GetProperty("username").GetString(),
                discriminator: json.GetProperty("discriminator").GetString(),
                avatar: avatar,
                isBot: json.GetPropertyOrNull("bot")?.GetBoolean() ?? false,
                hasTwoFactorAuth: json.GetPropertyOrNull("mfa_enabled")?.GetBoolean(),
                isVerified: json.GetPropertyOrNull("verified")?.GetBoolean(),
                email: json.GetPropertyOrNull("email")?.GetString());
        }
    }
}

#nullable restore
