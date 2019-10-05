#nullable enable

using System.Text.Json;

namespace Discore
{
    public class DiscordChannelMention : DiscordIdEntity
    {
        /// <summary>
        /// Gets the ID of the guild containing the channel.
        /// </summary>
        public Snowflake GuildId { get; }

        /// <summary>
        /// Gets the channel type.
        /// </summary>
        public DiscordChannelType Type { get; }

        /// <summary>
        /// Gets the name of the channel.
        /// </summary>
        public string Name { get; }

        private DiscordChannelMention(Snowflake id, Snowflake guildId, DiscordChannelType type, string name)
            : base(id)
        {
            GuildId = guildId;
            Type = type;
            Name = name;
        }

        internal static DiscordChannelMention FromJson(JsonElement json)
        {
            return new DiscordChannelMention(
                id: json.GetProperty("id").GetUInt64(),
                guildId: json.GetProperty("guild_id").GetUInt64(),
                type: (DiscordChannelType)json.GetProperty("type").GetInt32(),
                name: json.GetProperty("name").GetString());
        }
    }
}

#nullable restore
