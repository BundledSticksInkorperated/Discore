using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordGuildTextChannel : DiscordGuildChannel
    {
        // TODO: Add rate_limit_per_user, last_pin_timestamp

        /// <summary>
        /// Gets the topic of this channel.
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// The ID of the last message sent in this channel or null if no messages have been sent yet.
        /// </summary>
        public Snowflake? LastMessageId { get; }

        /// <param name="guildId">If null, the guild ID will be pulled from the <paramref name="json"/>.</param>
        internal DiscordGuildTextChannel(Snowflake? guildId, JsonElement json)
            : base(DiscordChannelType.GuildText, guildId, json)
        {
            Topic = json.GetProperty("topic").GetString();
            LastMessageId = json.GetPropertyOrNull("last_message_id")?.GetSnowflakeOrNull();
        }
    }
}

#nullable restore
