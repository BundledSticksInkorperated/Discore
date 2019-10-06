#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordEmbedVideo
    {
        /// <summary>
        /// Gets the source url of the video.
        /// </summary>
        public string? Url { get; }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        public int? Width { get; }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        public int? Height { get; }

        private DiscordEmbedVideo(string? url, int? width, int? height)
        {
            Url = url;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return Url ?? base.ToString();
        }

        internal static DiscordEmbedVideo FromJson(JsonElement json)
        {
            return new DiscordEmbedVideo(
                url: json.GetPropertyOrNull("url")?.GetString(),
                width: json.GetPropertyOrNull("width")?.GetInt32(),
                height: json.GetPropertyOrNull("height")?.GetInt32());
        }
    }
}

#nullable restore
