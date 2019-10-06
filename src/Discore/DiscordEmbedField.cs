#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordEmbedField
    {
        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets whether this field should display inline.
        /// </summary>
        public bool IsInline { get; }

        private DiscordEmbedField(string name, string value, bool isInline)
        {
            Name = name;
            Value = value;
            IsInline = isInline;
        }

        public override string ToString()
        {
            return Name;
        }

        internal static DiscordEmbedField FromJson(JsonElement json)
        {
            return new DiscordEmbedField(
                json.GetProperty("name").GetString(),
                json.GetProperty("value").GetString(),
                json.GetPropertyOrNull("inline")?.GetBoolean() ?? false);
        }
    }
}

#nullable restore
