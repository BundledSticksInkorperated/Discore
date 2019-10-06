#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordEmbedFooter
    {
        /// <summary>
        /// Gets the footer text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the url of the footer icon.
        /// </summary>
        public string? IconUrl { get; }

        /// <summary>
        /// Gets a proxied url of the footer icon.
        /// </summary>
        public string? ProxyIconUrl { get; }

        private DiscordEmbedFooter(string text, string? iconUrl, string? proxyIconUrl)
        {
            Text = text;
            IconUrl = iconUrl;
            ProxyIconUrl = proxyIconUrl;
        }

        internal static DiscordEmbedFooter FromJson(JsonElement json)
        {
            return new DiscordEmbedFooter(
                text: json.GetProperty("text").GetString(),
                iconUrl: json.GetPropertyOrNull("icon_url")?.GetString(),
                proxyIconUrl: json.GetPropertyOrNull("proxy_icon_url")?.GetString());
        }
    }
}

#nullable restore
