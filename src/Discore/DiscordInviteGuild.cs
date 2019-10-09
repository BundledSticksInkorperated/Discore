#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordInviteGuild
    {
        /// <summary>
        /// Gets the ID of the guild this invite is for.
        /// </summary>
        public Snowflake Id { get; }

        /// <summary>
        /// Gets the name of the guild.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the guild splash (or null if none exists).
        /// </summary>
        public DiscordCdnUrl? Splash { get; }

        /// <summary>
        /// Gets the guild icon (or null if none exists).
        /// </summary>
        public DiscordCdnUrl? Icon { get; }

        internal DiscordInviteGuild(JsonElement json)
        {
            Id = json.GetProperty("id").GetSnowflake();
            Name = json.GetProperty("name").GetString();

            string? splashHash = json.GetProperty("splash").GetString();
            Splash = splashHash != null ? DiscordCdnUrl.ForGuildSplash(Id, splashHash) : null;

            string? iconHash = json.GetProperty("icon").GetString();
            Icon = iconHash != null ? DiscordCdnUrl.ForGuildIcon(Id, iconHash) : null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

#nullable restore
