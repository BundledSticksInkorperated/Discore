#nullable enable

using System.Diagnostics;
using System.Text.Json;

namespace Discore
{
    /// <summary>
    /// A <see cref="DiscordDMChannel"/> or a <see cref="DiscordGuildChannel"/>.
    /// </summary>
    public abstract class DiscordChannel : DiscordIdEntity
    {
        /// <summary>
        /// Gets the type of this channel.
        /// </summary>
        public DiscordChannelType Type { get; }

        /// <summary>
        /// Gets whether this channel is a guild channel.
        /// </summary>
        public bool IsGuildChannel => 
               Type == DiscordChannelType.GuildText
            || Type == DiscordChannelType.GuildVoice
            || Type == DiscordChannelType.GuildCategory
            || Type == DiscordChannelType.GuildNews
            || Type == DiscordChannelType.GuildStore;

        private protected DiscordChannel(DiscordChannelType type, JsonElement json)
            : base(json)
        {
            Debug.Assert((DiscordChannelType)json.GetProperty("type").GetInt32() == type);

            Type = type;
        }

        public override string ToString()
        {
            return $"{Type} Channel: {Id}";
        }
    }
}

#nullable restore
