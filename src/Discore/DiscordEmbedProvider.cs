#nullable enable

using System.Text.Json;

namespace Discore
{
    /// <summary>
    /// The web provider of a <see cref="DiscordEmbed"/>.
    /// </summary>
    public sealed class DiscordEmbedProvider
    {
        /// <summary>
        /// Gets the name of this provider.
        /// </summary>
        public string? Name { get; }
        /// <summary>
        /// Gets the url of this provider.
        /// </summary>
        public string? Url { get; }

        private DiscordEmbedProvider(string? name, string? url)
        {
            Name = name;
            Url = url;
        }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }

        internal static DiscordEmbedProvider FromJson(JsonElement json)
        {
            return new DiscordEmbedProvider(
                name: json.GetPropertyOrNull("name")?.GetString(),
                url: json.GetPropertyOrNull("url")?.GetString());
        }
    }
}

#nullable restore
