using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordAttachment : DiscordIdEntity
    {
        /// <summary>
        /// Gets the file name of the attachment.
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Gets the byte file-size of the attachment.
        /// </summary>
        public int Size { get; }
        /// <summary>
        /// Gets the url of this attachment.
        /// </summary>
        public string Url { get; }
        /// <summary>
        /// Gets the proxy url of this attachment.
        /// </summary>
        public string ProxyUrl { get; }
        /// <summary>
        /// Gets the pixel-width of this attachment if the attachment is an image.
        /// </summary>
        public int? Width { get; }
        /// <summary>
        /// Gets the pixel-height of this attachment if the attachment is an image.
        /// </summary>
        public int? Height { get; }

        private DiscordAttachment(
            Snowflake id,
            string fileName,
            int size,
            string url,
            string proxyUrl,
            int? width,
            int? height)
            : base(id)
        {
            FileName = fileName;
            Size = size;
            Url = url;
            ProxyUrl = proxyUrl;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return FileName;
        }

        internal static DiscordAttachment FromJson(JsonElement json)
        {
            return new DiscordAttachment(
                id: json.GetProperty("id").GetUInt64(),
                fileName: json.GetProperty("filename").GetString(),
                size: json.GetProperty("size").GetInt32(),
                url: json.GetProperty("url").GetString(),
                proxyUrl: json.GetProperty("proxy_url").GetString(),
                width: json.GetProperty("width").GetInt32OrNull(),
                height: json.GetProperty("height").GetInt32OrNull());
        }
    }
}

#nullable restore
