#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordEmbedAuthor
    {
        /// <summary>
        /// Gets the name of the author.
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Gets the url to the author.
        /// </summary>
        public string? Url { get; }

        /// <summary>
        /// Gets the url of an icon of the author.
        /// </summary>
        public string? IconUrl { get; }

        /// <summary>
        /// Gets a proxied url to the icon of the author.
        /// </summary>
        public string? ProxyIconUrl { get; }

        private DiscordEmbedAuthor(string? name, string? url, string? iconUrl, string? proxyIconUrl)
        {
            Name = name;
            Url = url;
            IconUrl = iconUrl;
            ProxyIconUrl = proxyIconUrl;
        }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }

        internal static DiscordEmbedAuthor FromJson(JsonElement json)
        {
            return new DiscordEmbedAuthor(
                name: json.GetPropertyOrNull("name")?.GetString(),
                url: json.GetPropertyOrNull("url")?.GetString(),
                iconUrl: json.GetPropertyOrNull("icon_url")?.GetString(),
                proxyIconUrl: json.GetPropertyOrNull("proxy_icon_url")?.GetString());
        }
    }
}

#nullable restore
