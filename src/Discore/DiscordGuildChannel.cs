using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace Discore
{
    public abstract class DiscordGuildChannel : DiscordChannel
    {
        /// <summary>
        /// Gets the name of this channel.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the UI ordering position of this channel.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// Gets a dictionary of all permission overwrites associated with this channel.
        /// </summary>
        public IReadOnlyDictionary<Snowflake, DiscordOverwrite> PermissionOverwrites { get; }

        /// <summary>
        /// Gets whether this channel is NSFW (not-safe-for-work).
        /// </summary>
        public bool Nsfw { get; }

        /// <summary>
        /// Gets the ID of the parent category channel or null if the channel is not in a category.
        /// </summary>
        public Snowflake? ParentId { get; }

        /// <summary>
        /// Gets the ID of the guild this channel is in.
        /// </summary>
        public Snowflake GuildId { get; }

        /// <param name="guildId">If null, the guild ID will be pulled from the <paramref name="json"/>.</param>
        internal DiscordGuildChannel(DiscordChannelType type, Snowflake? guildId, JsonElement json)
            : base(type, json)
        {
            GuildId = guildId ?? json.GetProperty("guild_id").GetSnowflake();

            Name = json.GetProperty("name").GetString();
            Position = json.GetProperty("position").GetInt32();
            Nsfw = json.GetProperty("nsfw").GetBoolean();
            ParentId = json.GetProperty("parent_id").GetSnowflakeOrNull();

            JsonElement overwritesData = json.GetProperty("permission_overwrites");
            var overwrites = new Dictionary<Snowflake, DiscordOverwrite>();

            foreach (JsonElement overwriteData in overwritesData.EnumerateArray())
            {
                var overwrite = new DiscordOverwrite(Id, overwriteData);
                overwrites.Add(overwrite.Id, overwrite);
            }

            PermissionOverwrites = overwrites;
        }

        public override string ToString()
        {
            return $"{Type} Channel: {Name}";
        }
    }
}

#nullable restore
