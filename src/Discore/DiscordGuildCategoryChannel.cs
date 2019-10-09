using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordGuildCategoryChannel : DiscordGuildChannel
    {
        /// <param name="guildId">If null, the guild ID will be pulled from the <paramref name="json"/>.</param>
        internal DiscordGuildCategoryChannel(Snowflake? guildId, JsonElement json)
            : base(DiscordChannelType.GuildCategory, guildId, json)
        { }
    }
}

#nullable restore
