using System.Text.Json;

#nullable enable

namespace Discore
{
    public class DiscordInvite
    {
        /// <summary>
        /// Gets the unique invite code ID.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the guild this invite is for.
        /// </summary>
        public DiscordInviteGuild? Guild { get; }

        /// <summary>
        /// Gets the channel this invite is for.
        /// </summary>
        public DiscordInviteChannel Channel { get; }

        /// <summary>
        /// Gets the target user of this invite or null if no specific target exists.
        /// </summary>
        public DiscordUser? TargetUser { get; }

        /// <summary>
        /// Gets the type of target user or null if no specific target user exists.
        /// </summary>
        /// <seealso cref="TargetUser"/>
        public DiscordInviteTargetUserType? TargetUserType { get; }

        /// <summary>
        /// Gets the approximate number of online members in the guild which this invite is for.
        /// Will be null if not available.
        /// </summary>
        public int? ApproximatePresenceCount { get; }

        /// <summary>
        /// Gets the approximate number of total members in the guild which this invite is for.
        /// Will be null if not available.
        /// </summary>
        public int? ApproximateMemberCount { get; }

        internal DiscordInvite(JsonElement json)
        {
            Code = json.GetProperty("code").GetString();
            Channel = new DiscordInviteChannel(json.GetProperty("channel"));
            
            JsonElement? guildData = json.GetPropertyOrNull("guild");
            Guild = guildData != null ? new DiscordInviteGuild(guildData.Value) : null;

            JsonElement? targetUserData = json.GetPropertyOrNull("target_user");
            TargetUser = targetUserData != null ? new DiscordUser(targetUserData.Value) : null;

            TargetUserType = (DiscordInviteTargetUserType?)json.GetPropertyOrNull("target_user_type")?.GetInt32();

            ApproximatePresenceCount = json.GetPropertyOrNull("approximate_presence_count")?.GetInt32();
            ApproximateMemberCount = json.GetPropertyOrNull("approximate_member_count")?.GetInt32();
        }

        public override string ToString()
        {
            return Code;
        }
    }
}

#nullable restore
