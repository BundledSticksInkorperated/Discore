#nullable enable

using System.Text.Json;

namespace Discore
{
    public sealed class DiscordMessageActivity
    {
        /// <summary>
        /// Gets the type of activity.
        /// </summary>
        public DiscordMessageActivityType Type { get; }

        /// <summary>
        /// Gets the party ID from a Rich Presence event.
        /// </summary>
        public string? PartyId { get; }

        internal DiscordMessageActivity(JsonElement json)
        {
            Type = (DiscordMessageActivityType)json.GetProperty("type").GetInt32();
            PartyId = json.GetPropertyOrNull("party_id")?.GetString();
        }
    }
}

#nullable restore
