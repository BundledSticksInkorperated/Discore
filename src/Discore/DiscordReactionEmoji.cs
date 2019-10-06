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

        public DiscordReactionEmoji(string name)
        {
            Name = name;
        }

        public DiscordReactionEmoji(string name, Snowflake? id)
        {
            Name = name;
            Id = id;
        }

        public override string ToString()
        {
            return Id.HasValue ? $"{Name}:{Id.Value}" : Name;
        }

        internal static DiscordReactionEmoji FromJson(JsonElement json)
        {
            return new DiscordReactionEmoji(
                id: json.GetProperty("id").GetSnowflakeOrNull(),
                name: json.GetProperty("name").GetString());
        }
    }
}

#nullable restore
