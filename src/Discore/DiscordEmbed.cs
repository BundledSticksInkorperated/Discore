using System;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace Discore
{
    /// <summary>
    /// Embedded content in a message.
    /// </summary>
    public sealed class DiscordEmbed
    {
        /// <summary>
        /// Gets the title of this embed.
        /// </summary>
        public string? Title { get; }
        /// <summary>
        /// Gets the type of this embed.
        /// </summary>
        public string? Type { get; }
        /// <summary>
        /// Gets the description of this embed.
        /// </summary>
        public string? Description { get; }
        /// <summary>
        /// Gets the url of this embed.
        /// </summary>
        public string? Url { get; }
        /// <summary>
        /// Gets the timestamp of this embed.
        /// </summary>
        public DateTime? Timestamp { get; }
        /// <summary>
        /// Gets the color code of this embed.
        /// </summary>
        public int? Color { get; }
        /// <summary>
        /// Gets the footer information.
        /// </summary>
        public DiscordEmbedFooter? Footer { get; }
        /// <summary>
        /// Gets the image information.
        /// </summary>
        public DiscordEmbedImage? Image { get; }
        /// <summary>
        /// Gets the thumbnail of this embed.
        /// </summary>
        public DiscordEmbedThumbnail? Thumbnail { get; }
        /// <summary>
        /// Gets the video information.
        /// </summary>
        public DiscordEmbedVideo? Video { get; }
        /// <summary>
        /// Gets the provider of this embed.
        /// </summary>
        public DiscordEmbedProvider? Provider { get; }
        /// <summary>
        /// Gets the author information.
        /// </summary>
        public DiscordEmbedAuthor? Author { get; }
        /// <summary>
        /// Gets a list of all fields in this embed.
        /// </summary>
        public IReadOnlyList<DiscordEmbedField>? Fields { get; }

        internal DiscordEmbed(JsonElement json)
        {
            Title = json.GetPropertyOrNull("title")?.GetString();
            Type = json.GetPropertyOrNull("type")?.GetString();
            Description = json.GetPropertyOrNull("description")?.GetString();
            Url = json.GetPropertyOrNull("url")?.GetString();
            Timestamp = json.GetPropertyOrNull("timestamp")?.GetDateTime();
            Color = json.GetPropertyOrNull("color")?.GetInt32();

            JsonElement? footerData = json.GetPropertyOrNull("footer");
            Footer = footerData != null ? new DiscordEmbedFooter(footerData.Value) : null;

            JsonElement? imageData = json.GetPropertyOrNull("image");
            Image = imageData != null ? new DiscordEmbedImage(imageData.Value) : null;

            JsonElement? thumbnailData = json.GetPropertyOrNull("thumbnail");
            Thumbnail = thumbnailData != null ? new DiscordEmbedThumbnail(thumbnailData.Value) : null;

            JsonElement? videoData = json.GetPropertyOrNull("video");
            Video = videoData != null ? new DiscordEmbedVideo(videoData.Value) : null;

            JsonElement? providerData = json.GetPropertyOrNull("provider");
            Provider = providerData != null ? new DiscordEmbedProvider(providerData.Value) : null;

            JsonElement? authorData = json.GetPropertyOrNull("author");
            Author = authorData != null ? new DiscordEmbedAuthor(authorData.Value) : null;

            if (json.TryGetProperty("fields", out JsonElement fieldsData))
            {
                var fields = new DiscordEmbedField[fieldsData.GetArrayLength()];

                for (int i = 0; i < fields.Length; i++)
                    fields[i] = new DiscordEmbedField(fieldsData[i]);

                Fields = fields;
            }
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }
    }
}

#nullable restore
