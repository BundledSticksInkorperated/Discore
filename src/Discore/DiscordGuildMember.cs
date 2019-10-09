using System;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordGuildMember : DiscordIdEntity
    {
        /// <summary>
        /// Gets the ID of the guild this member is in.
        /// </summary>
        public Snowflake GuildId { get; }

        /// <summary>
        /// Gets the user data for this member.
        /// </summary>
        public DiscordUser User { get; }

        /// <summary>
        /// Gets the guild-wide nickname of the user.
        /// </summary>
        public string? Nickname { get; }

        /// <summary>
        /// Gets the IDs of all of the roles this member has.
        /// </summary>
        public IReadOnlyList<Snowflake> RoleIds { get; }

        /// <summary>
        /// Gets when this member joined the guild.
        /// </summary>
        public DateTime JoinedAt { get; }

        /// <summary>
        /// Gets when this member used their Nitro boost on the guild.
        /// </summary>
        public DateTime? PremiumSince { get; }

        /// <summary>
        /// Gets whether this member is deafened.
        /// </summary>
        public bool IsDeaf { get; }

        /// <summary>
        /// Gets whether this member is muted.
        /// </summary>
        public bool IsMute { get; }

        private DiscordGuildMember(
            Snowflake guildId,
            DiscordUser user,
            string? nickname,
            IReadOnlyList<Snowflake> roleIds,
            DateTime joinedAt,
            DateTime? premiumSince,
            bool isDeaf,
            bool isMute)
            : base(user.Id)
        {
            GuildId = guildId;
            User = user;
            Nickname = nickname;
            RoleIds = roleIds;
            JoinedAt = joinedAt;
            PremiumSince = premiumSince;
            IsDeaf = isDeaf;
            IsMute = isMute;
        }

        public override string ToString()
        {
            return Nickname != null ? $"{User.Username} aka. {Nickname}" : User.Username;
        }

        internal static DiscordGuildMember FromJson(Snowflake guildId, JsonElement json)
        {
            JsonElement rolesData = json.GetProperty("roles");
            var roleIds = new Snowflake[rolesData.GetArrayLength()];

            for (int i = 0; i < roleIds.Length; i++)
                roleIds[i] = rolesData[i].GetSnowflake();

            return new DiscordGuildMember(
                guildId: guildId,
                user: DiscordUser.FromJson(json.GetProperty("user")),
                nickname: json.GetPropertyOrNull("nick")?.GetString(),
                roleIds: roleIds,
                joinedAt: json.GetProperty("joined_at").GetDateTime(),
                premiumSince: json.GetPropertyOrNull("premium_since")?.GetDateTime(),
                isDeaf: json.GetProperty("deaf").GetBoolean(),
                isMute: json.GetProperty("mute").GetBoolean());
        }
    }
}

#nullable restore
