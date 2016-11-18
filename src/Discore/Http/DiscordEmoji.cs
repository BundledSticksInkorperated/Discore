﻿using System.Collections.Generic;

namespace Discore.Http
{
    public class DiscordEmoji : DiscordIdObject
    {
        /// <summary>
        /// Gets the name of this emoji.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the ids of associated roles with this emoji.
        /// </summary>
        public Snowflake[] RoleIds { get; }
        /// <summary>
        /// Gets whether or not colons are required around the emoji name to use it.
        /// </summary>
        public bool RequireColons { get; }
        /// <summary>
        /// Gets whether or not this emoji is managed.
        /// </summary>
        public bool IsManaged { get; }

        public DiscordEmoji(DiscordApiData data)
            : base(data)
        {
            Name          = data.GetString("name");
            RequireColons = data.GetBoolean("require_colons").Value;
            IsManaged     = data.GetBoolean("managed").Value;

            IList<DiscordApiData> roles = data.GetArray("roles");
            RoleIds = new Snowflake[roles.Count];

            for (int i = 0; i < roles.Count; i++)
                RoleIds[i] = roles[i].ToSnowflake().Value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
