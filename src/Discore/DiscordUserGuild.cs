#nullable enable

using System.Text.Json;

namespace Discore
{
    /// <summary>
    /// A brief version of a guild object.
    /// </summary>
    public sealed class DiscordUserGuild : DiscordIdEntity
    {
        /// <summary>
        /// Gets the name of this guild.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the icon of this guild or null if the guild has no icon set.
        /// </summary>
        public DiscordCdnUrl? Icon { get; }
        /// <summary>
        /// Gets whether the user is the owner of this guild.
        /// </summary>
        public bool IsOwner { get; }
        /// <summary>
        /// Gets the user's enabled/disabled permissions.
        /// </summary>
        public DiscordPermission Permissions { get; }

        private DiscordUserGuild(
            Snowflake id, 
            string name, 
            DiscordCdnUrl? icon, 
            bool isOwner, 
            DiscordPermission permissions)
            : base(id)
        {
            Name = name;
            Icon = icon;
            IsOwner = isOwner;
            Permissions = permissions;
        }

        public override string ToString()
        {
            return Name;
        }

        internal static DiscordUserGuild FromJson(JsonElement json)
        {
            Snowflake id = json.GetProperty("id").GetUInt64();

            string? iconHash = json.GetProperty("icon").GetString();
            DiscordCdnUrl? icon = iconHash != null ? DiscordCdnUrl.ForGuildIcon(id, iconHash) : null;

            return new DiscordUserGuild(
                id: id,
                name: json.GetProperty("name").GetString(),
                icon: icon,
                isOwner: json.GetPropertyOrNull("owner")?.GetBoolean() ?? false,
                permissions: (DiscordPermission)(json.GetPropertyOrNull("permissions")?.GetInt32() ?? 0));
        }
    }
}

#nullable restore
