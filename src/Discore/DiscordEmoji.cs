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

        private DiscordEmoji(
            Snowflake id,
            string name, 
            IReadOnlyList<Snowflake> roleIds, 
            DiscordUser user, 
            bool requireColons, 
            bool isManaged, 
            bool isAnimated)
            : base(id)
        {
            Name = name;
            RoleIds = roleIds;
            User = user;
            RequireColons = requireColons;
            IsManaged = isManaged;
            IsAnimated = isAnimated;
        }

        public override string ToString()
        {
            return Name;
        }

        internal static DiscordEmoji FromJson(JsonElement json)
        {
            JsonElement rolesData = json.GetProperty("roles");
            var roleIds = new Snowflake[rolesData.GetArrayLength()];

            for (int i = 0; i < roleIds.Length; i++)
                roleIds[i] = rolesData[i].GetSnowflake();

            return new DiscordEmoji(
                id: json.GetProperty("id").GetSnowflake(),
                name: json.GetProperty("name").GetString(),
                roleIds: roleIds,
                user: DiscordUser.FromJson(json.GetProperty("user")),
                requireColons: json.GetProperty("require_colons").GetBoolean(),
                isManaged: json.GetProperty("managed").GetBoolean(),
                isAnimated: json.GetProperty("animated").GetBoolean());
        }
    }
}

#nullable restore
