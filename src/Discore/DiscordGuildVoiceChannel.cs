using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordGuildVoiceChannel : DiscordGuildChannel
    {
        /// <summary>
        /// Gets the audio bitrate used for this channel.
        /// </summary>
        public int Bitrate { get; }

        /// <summary>
        /// Gets the maximum number of users that can be connected to this channel simultaneously.
        /// </summary>
        public int UserLimit { get; }

        /// <param name="guildId">If null, the guild ID will be pulled from the <paramref name="json"/>.</param>
        internal DiscordGuildVoiceChannel(Snowflake? guildId, JsonElement json)
            : base(DiscordChannelType.GuildVoice, guildId, json)
        {
            Bitrate = json.GetProperty("bitrate").GetInt32();
            UserLimit = json.GetProperty("user_limit").GetInt32();
        }
    }
}

#nullable restore
