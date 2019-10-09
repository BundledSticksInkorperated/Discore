using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordMessageApplication : DiscordIdEntity
    {
        /// <summary>
        /// Gets the ID of the embed's image asset.
        /// </summary>
        public string? CoverImage { get; }
        /// <summary>
        /// Gets the description of the application.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Gets the ID of the application's icon.
        /// </summary>
        public string? Icon { get; }
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        public string Name { get; }

        internal DiscordMessageApplication(JsonElement json)
            : base(json)
        {
            CoverImage = json.GetPropertyOrNull("cover_image")?.GetString();
            Description = json.GetProperty("description").GetString();
            Icon = json.GetProperty("icon").GetString();
            Name = json.GetProperty("name").GetString();
        }
    }
}

#nullable restore
