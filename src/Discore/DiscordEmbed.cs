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

        private DiscordEmbed(
            string? title,
            string? type,
            string? description,
            string? url,
            DateTime? timestamp,
            int? color,
            DiscordEmbedFooter? footer,
            DiscordEmbedImage? image,
            DiscordEmbedThumbnail? thumbnail,
            DiscordEmbedVideo? video,
            DiscordEmbedProvider? provider,
            DiscordEmbedAuthor? author,
            IReadOnlyList<DiscordEmbedField>? fields)
        {
            Title = title;
            Type = type;
            Description = description;
            Url = url;
            Timestamp = timestamp;
            Color = color;
            Footer = footer;
            Image = image;
            Thumbnail = thumbnail;
            Video = video;
            Provider = provider;
            Author = author;
            Fields = fields;
        }

        public override string ToString()
        {
            return Title ?? base.ToString();
        }

        internal static DiscordEmbed FromJson(JsonElement json)
        {
            JsonElement? footerData = json.GetPropertyOrNull("footer");
            DiscordEmbedFooter? footer = footerData != null ? DiscordEmbedFooter.FromJson(footerData.Value) : null;

            JsonElement? imageData = json.GetPropertyOrNull("image");
            DiscordEmbedImage? image = imageData != null ? DiscordEmbedImage.FromJson(imageData.Value) : null;

            JsonElement? thumbnailData = json.GetPropertyOrNull("thumbnail");
            DiscordEmbedThumbnail? thumbnail = thumbnailData != null ? DiscordEmbedThumbnail.FromJson(thumbnailData.Value) : null;

            JsonElement? videoData = json.GetPropertyOrNull("video");
            DiscordEmbedVideo? video = videoData != null ? DiscordEmbedVideo.FromJson(videoData.Value) : null;

            JsonElement? providerData = json.GetPropertyOrNull("provider");
            DiscordEmbedProvider? provider = providerData != null ? DiscordEmbedProvider.FromJson(providerData.Value) : null;

            JsonElement? authorData = json.GetPropertyOrNull("author");
            DiscordEmbedAuthor? author = authorData != null ? DiscordEmbedAuthor.FromJson(authorData.Value) : null;

            DiscordEmbedField[]? fields = null;
            if (json.TryGetProperty("fields", out JsonElement fieldsData))
            {
                fields = new DiscordEmbedField[fieldsData.GetArrayLength()];

                for (int i = 0; i < fields.Length; i++)
                    fields[i] = DiscordEmbedField.FromJson(fieldsData[i]);
            }

            return new DiscordEmbed(
                title: json.GetPropertyOrNull("title")?.GetString(),
                type: json.GetPropertyOrNull("type")?.GetString(),
                description: json.GetPropertyOrNull("description")?.GetString(),
                url: json.GetPropertyOrNull("url")?.GetString(),
                timestamp: json.GetPropertyOrNull("timestamp")?.GetDateTime(),
                color: json.GetPropertyOrNull("color")?.GetInt32(),
                footer: footer,
                image: image,
                thumbnail: thumbnail,
                video: video,
                provider: provider,
                author: author,
                fields: fields);
        }
    }
}

#nullable restore
