﻿using System.Collections.Generic;

namespace Discore.Http
{
    /// <summary>
    /// An optional set of parameters to change the attributes of a guild member.
    /// </summary>
    public class ModifyGuildMemberParameters
    {
        /// <summary>
        /// The users nickname for the guild.
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// A list of roles IDs the member is assigned to.
        /// </summary>
        public IEnumerable<Snowflake> RoleIds { get; set; }
        /// <summary>
        /// Whether the user is server muted.
        /// </summary>
        public bool? IsServerMute { get; set; }
        /// <summary>
        /// Whether the user is deafened.
        /// </summary>
        public bool? IsServerDeaf { get; set; }
        /// <summary>
        /// ID of the voice channel to move the user to (if they are currently connected to voice).
        /// </summary>
        public Snowflake? ChannelId { get; set; }

        internal DiscordApiData Build()
        {
            DiscordApiData data = new DiscordApiData(DiscordApiDataType.Container);
            data.Set("nick", Nickname);
            data.Set("mute", IsServerMute);
            data.Set("deaf", IsServerDeaf);
            data.Set("channel_id", ChannelId);

            if (RoleIds != null)
            {
                DiscordApiData rolesArray = new DiscordApiData(DiscordApiDataType.Array);
                foreach (Snowflake roleId in RoleIds)
                    rolesArray.Values.Add(new DiscordApiData(roleId));

                data.Set("roles", rolesArray);
            }

            return data;
        }
    }
}
