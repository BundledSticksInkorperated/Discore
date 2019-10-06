#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordEmbedImage
    {
        /// <summary>
        /// Gets the source url of the image.
        /// </summary>
        public string? Url { get; }

        /// <summary>
        /// Gets a proxied url of the image.
        /// </summary>
        public string? ProxyUrl { get; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public int? Width { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public int? Height { get; }

        private DiscordEmbedImage(string? url, string? proxyUrl, int? width, int? height)
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

        internal static DiscordEmbedImage FromJson(JsonElement json)
        {
            return new DiscordEmbedImage(
                url: json.GetPropertyOrNull("url")?.GetString(),
                proxyUrl: json.GetPropertyOrNull("proxy_url")?.GetString(),
                width: json.GetPropertyOrNull("width")?.GetInt32(),
                height: json.GetPropertyOrNull("height")?.GetInt32());
        }
    }
}

#nullable restore
