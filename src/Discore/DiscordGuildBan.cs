#nullable enable

using System.Text.Json;

namespace Discore
{
    // TODO: Consider renaming to "DiscordBan"

    public sealed class DiscordGuildBan
    {
        /// <summary>
        /// Gets the reason for the ban or null if there was no reason.
        /// </summary>
        public string? Reason { get; }

        /// <summary>
        /// Gets the user that was banned.
        /// </summary>
        public DiscordUser User { get; }

        internal DiscordGuildBan(JsonElement json)
        {
            Reason = json.GetProperty("reason").GetString();
            User = new DiscordUser(json.GetProperty("user"));
        }
    }
}

#nullable restore
