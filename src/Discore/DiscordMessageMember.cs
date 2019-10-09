using System;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordMessageMember
    {
        /// <summary>
        /// Gets the IDs of all of the roles this member has.
        /// </summary>
        public IReadOnlyList<Snowflake> RoleIds { get; }

        /// <summary>
        /// Gets the guild-wide nickname of the user.
        /// </summary>
        public string? Nickname { get; }

        /// <summary>
        /// Gets the time this member joined the guild.
        /// </summary>
        public DateTime JoinedAt { get; }

        /// <summary>
        /// Gets whether this member is deafened.
        /// </summary>
        public bool IsDeaf { get; }

        /// <summary>
        /// Gets whether this member is muted.
        /// </summary>
        public bool IsMute { get; }

        internal DiscordMessageMember(JsonElement json)
        {
            Nickname = json.GetPropertyOrNull("nick")?.GetString();
            JoinedAt = json.GetProperty("joined_at").GetDateTime();
            IsDeaf = json.GetProperty("deaf").GetBoolean();
            IsMute = json.GetProperty("mute").GetBoolean();

            JsonElement rolesData = json.GetProperty("roles");
            var roleIds = new Snowflake[rolesData.GetArrayLength()];

            for (int i = 0; i < roleIds.Length; i++)
                roleIds[i] = rolesData[i].GetSnowflake();

            RoleIds = roleIds;
        }
    }
}

#nullable restore
