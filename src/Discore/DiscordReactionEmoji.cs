#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordReactionEmoji
    {
        /// <summary>
        /// Gets the ID of the emoji (if custom emoji).
        /// </summary>
        public Snowflake? Id { get; }

        /// <summary>
        /// Gets the name of the emoji.
        /// </summary>
        public string Name { get; }

        // TODO: Review usage of these public constructors

        public DiscordReactionEmoji(string name)
        {
            Name = name;
        }

        public DiscordReactionEmoji(string name, Snowflake? id)
        {
            Name = name;
            Id = id;
        }

        internal DiscordReactionEmoji(JsonElement json)
        {
            Id = json.GetProperty("id").GetSnowflakeOrNull();
            Name = json.GetProperty("name").GetString();
        }

        public override string ToString()
        {
            return Id.HasValue ? $"{Name}:{Id.Value}" : Name;
        }
    }
}

#nullable restore
