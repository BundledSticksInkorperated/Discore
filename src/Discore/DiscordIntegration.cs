using System;
using System.Text.Json;

#nullable enable

namespace Discore
{
    /// <summary>
    /// A guild integration.
    /// </summary>
    public sealed class DiscordIntegration : DiscordIdEntity
    {
        /// <summary>
        /// Gets the name of this integration.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the type of this integration.
        /// </summary>
        public string Type { get; }
        /// <summary>
        /// Gets whether or not this integration is enabled.
        /// </summary>
        public bool IsEnabled { get; }
        /// <summary>
        /// Gets whether or not this integration is syncing.
        /// </summary>
        public bool IsSyncing { get; }
        /// <summary>
        /// Gets the ID of the associated role with this integration.
        /// </summary>
        public Snowflake RoleId { get; }
        /// <summary>
        /// Gets the expire behavior of this integration.
        /// </summary>
        public int ExpireBehavior { get; }
        /// <summary>
        /// Gets the expire grace period of this integration.
        /// </summary>
        public int ExpireGracePeriod { get; }
        /// <summary>
        /// Gets the associated <see cref="DiscordUser"/> with this integration.
        /// </summary>
        public DiscordUser User { get; }
        /// <summary>
        /// Gets the account of this integration.
        /// </summary>
        public DiscordIntegrationAccount Account { get; }
        /// <summary>
        /// Gets the last time this integration was synced.
        /// </summary>
        public DateTime SyncedAt { get; }
        /// <summary>
        /// Gets the ID of the associated guild with this integration.
        /// </summary>
        public Snowflake GuildId { get; }

        internal DiscordIntegration(Snowflake guildId, JsonElement json)
            : base(json)
        {
            GuildId = guildId;

            Name = json.GetProperty("name").GetString();
            Type = json.GetProperty("type").GetString();
            IsEnabled = json.GetProperty("enabled").GetBoolean();
            IsSyncing = json.GetProperty("syncing").GetBoolean();
            RoleId = json.GetProperty("role_id").GetSnowflake();
            ExpireBehavior = json.GetProperty("expire_behavior").GetInt32();
            ExpireGracePeriod = json.GetProperty("expire_grace_period").GetInt32();
            User = new DiscordUser(json.GetProperty("user"));
            Account = new DiscordIntegrationAccount(json.GetProperty("account"));
            SyncedAt = json.GetProperty("synced_at").GetDateTime();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

#nullable restore
