using System;
using System.Text.Json;

#nullable enable

namespace Discore
{
    /// <summary>
    /// A permission overwrite for a <see cref="DiscordRole"/> or <see cref="DiscordGuildMember"/>.
    /// </summary>
    public sealed class DiscordOverwrite : DiscordIdEntity
    {
        /// <summary>
        /// The ID of the channel this overwrite is for.
        /// </summary>
        public Snowflake ChannelId { get; }

        /// <summary>
        /// The type of this overwrite.
        /// </summary>
        public DiscordOverwriteType Type { get; }
        /// <summary>
        /// The specifically allowed permissions specified by this overwrite.
        /// </summary>
        public DiscordPermission Allow { get; }
        /// <summary>
        /// The specifically denied permissions specified by this overwrite.
        /// </summary>
        public DiscordPermission Deny { get; }

        internal DiscordOverwrite(Snowflake channelId, JsonElement json)
            : base(json)
        {
            ChannelId = channelId;

            Allow = (DiscordPermission)json.GetProperty("allow").GetInt32();
            Deny = (DiscordPermission)json.GetProperty("deny").GetInt32();

            string typeStr = json.GetProperty("type").GetString();
            DiscordOverwriteType type;
            if (Enum.TryParse(typeStr, ignoreCase: true, out type))
                Type = type;
        }

        public override string ToString()
        {
            return $"{Type} Overwrite: {Id}";
        }
    }
}

#nullable restore
