using System.Collections.Generic;
using System.Text.Json;

#nullable enable

namespace Discore
{
    public sealed class DiscordEmoji : DiscordIdEntity
    {
        /// <summary>
        /// Gets the name of this emoji.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets the IDs of the roles that this emoji is whitelisted to.
        /// </summary>
        public IReadOnlyList<Snowflake> RoleIds { get; }
        /// <summary>
        /// Gets the user that created this emoji.
        /// </summary>
        public DiscordUser User { get; }
        /// <summary>
        /// Gets whether or not colons are required around the emoji name to use it.
        /// </summary>
        public bool RequireColons { get; }
        /// <summary>
        /// Gets whether or not this emoji is managed.
        /// </summary>
        public bool IsManaged { get; }
        /// <summary>
        /// Gets whether or not this emoji is animated.
        /// </summary>
        public bool IsAnimated { get; }

        internal DiscordEmoji(JsonElement json)
            : base(json)
        {
            Name = json.GetProperty("name").GetString();
            User = new DiscordUser(json.GetProperty("user"));
            RequireColons = json.GetProperty("require_colons").GetBoolean();
            IsManaged = json.GetProperty("managed").GetBoolean();
            IsAnimated = json.GetProperty("animated").GetBoolean();

            JsonElement rolesData = json.GetProperty("roles");
            var roleIds = new Snowflake[rolesData.GetArrayLength()];

            for (int i = 0; i < roleIds.Length; i++)
                roleIds[i] = rolesData[i].GetSnowflake();

            RoleIds = roleIds;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

#nullable restore
