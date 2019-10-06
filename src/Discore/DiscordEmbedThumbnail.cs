#nullable enable

using System.Text.Json;

namespace Discore
{
    /// <summary>
    /// A thumbnail of a <see cref="DiscordEmbed"/>.
    /// </summary>
    public sealed class DiscordEmbedThumbnail
    {
        /// <summary>
        /// Gets the url of the thumbnail.
        /// </summary>
        public string? Url { get; }
        /// <summary>
        /// Gets the proxy url of the thumbnail.
        /// </summary>
        public string? ProxyUrl { get; }
        /// <summary>
        /// Gets the pixel-width of the thumbnail.
        /// </summary>
        public int? Width { get; }
        /// <summary>
        /// Gets the pixel-height of the thumbnail.
        /// </summary>
        public int? Height { get; }

        private DiscordEmbedThumbnail(string? url, string? proxyUrl, int? width, int? height)
        {
            Url = url;
            ProxyUrl = proxyUrl;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return Url ?? base.ToString();
        }

        internal static DiscordEmbedThumbnail FromJson(JsonElement json)
        {
            return new DiscordEmbedThumbnail(
                url: json.GetPropertyOrNull("url")?.GetString(),
                proxyUrl: json.GetPropertyOrNull("proxy_url")?.GetString(),
                width: json.GetPropertyOrNull("width")?.GetInt32(),
                height: json.GetPropertyOrNull("height")?.GetInt32());
        }
    }
}

#nullable restore
