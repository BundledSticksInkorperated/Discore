using System.Diagnostics;
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

        private DiscordDMChannel(Snowflake id, DiscordUser recipient, Snowflake? lastMessageId) 
            : base(id, DiscordChannelType.DirectMessage)
        {
            Recipient = recipient;
            LastMessageId = lastMessageId;
        }

        public override string ToString()
        {
            return $"DM Channel: {Recipient}";
        }

        internal static DiscordDMChannel FromJson(JsonElement json)
        {
            Debug.Assert((DiscordChannelType)json.GetProperty("type").GetInt32() == DiscordChannelType.DirectMessage);

            // Normal DM should only ever have exactly one recipient
            JsonElement recipientData = json.GetProperty("recipients").EnumerateArray().First();
            var recipient = DiscordUser.FromJson(recipientData);

            return new DiscordDMChannel(
                id: json.GetProperty("id").GetUInt64(),
                recipient: recipient,
                lastMessageId: json.GetPropertyOrNull("last_message_id")?.GetUInt64OrNull());
        }
    }
}

#nullable restore
