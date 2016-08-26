﻿namespace Discore
{
    /// <summary>
    /// Embedded content in a <see cref="DiscordMessage"/>.
    /// </summary>
    public class DiscordEmbed : IDiscordObject
    {
        /// <summary>
        /// Gets the title of this embed.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the type of this embed.
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Gets the description of this embed.
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Gets the url of this embed.
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Gets the thumbnail of this embed.
        /// </summary>
        public DiscordEmbedThumbnail Thumbnail { get; private set; }
        /// <summary>
        /// Gets the provider of this embed.
        /// </summary>
        public DiscordEmbedProvider Provider { get; private set; }

        /// <summary>
        /// Updates this embed with the specified <see cref="DiscordApiData"/>.
        /// </summary>
        /// <param name="data">The data to update this embed with.</param>
        public void Update(DiscordApiData data)
        {
            Title = data.GetString("title") ?? Title;
            Type = data.GetString("type") ?? Type;
            Description = data.GetString("description") ?? Description;
            Url = data.GetString("url") ?? Url;

            DiscordApiData thumbnailData = data.Get("thumbnail");
            if (thumbnailData != null)
            {
                if (Thumbnail == null)
                    Thumbnail = new DiscordEmbedThumbnail();

                Thumbnail.Update(thumbnailData);
            }

            DiscordApiData providerData = data.Get("provider");
            if (providerData != null)
            {
                if (Provider == null)
                    Provider = new DiscordEmbedProvider();

                Provider.Update(providerData);
            }
        }
    }
}
