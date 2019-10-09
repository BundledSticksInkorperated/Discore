using System.Linq;
using System.Text.Json;

#nullable enable

namespace Discore
{
    /// <summary>
    /// Direct message channels represent a one-to-one conversation between two users, outside of the scope of guilds.
    /// </summary>
    public sealed class DiscordDMChannel : DiscordChannel
    {
        /// <summary>
        /// Gets the user on the other end of this channel.
        /// </summary>
        public DiscordUser Recipient { get; }

        /// <summary>
        /// The ID of the last message sent in this channel or null if no messages have been sent yet.
        /// </summary>
        public Snowflake? LastMessageId { get; }

        internal DiscordDMChannel(JsonElement json)
            : base(DiscordChannelType.DirectMessage, json)
        {
            // Normal DM should only ever have exactly one recipient
            JsonElement recipientData = json.GetProperty("recipients").EnumerateArray().First();
            Recipient = new DiscordUser(recipientData);

            LastMessageId = json.GetPropertyOrNull("last_message_id")?.GetSnowflakeOrNull();
        }

        public override string ToString()
        {
            return $"DM Channel: {Recipient}";
        }
    }
}

#nullable restore
