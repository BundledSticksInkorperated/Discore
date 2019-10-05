using Discore.Http.Internal;
using System.Text.Json;
using System.Threading.Tasks;

#nullable enable

namespace Discore.Http
{
    partial class DiscordHttpClient
    {
        /// <summary>
        /// Gets the user object of the current bot.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task<DiscordUser> GetCurrentUser()
        {
            using JsonDocument? data = await rest.Get("users/@me", "users/@me").ConfigureAwait(false);

            return DiscordUser.FromJson(data!.RootElement);
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task<DiscordUser> GetUser(Snowflake id)
        {
            using JsonDocument? data = await rest.Get($"users/{id}", "users/user").ConfigureAwait(false);

            return DiscordUser.FromJson(data!.RootElement);
        }

        /// <summary>
        /// Modifies the current bot's user object.
        /// Parameters left null will leave the properties unchanged.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task<DiscordUser> ModifyCurrentUser(string? username = null, DiscordImageData? avatar = null)
        {
            string requestData = BuildJsonContent(writer =>
            {
                writer.WriteStartObject();

                if (username != null)
                    writer.WriteString("username", username);

                if (avatar != null)
                    writer.WriteString("avatar", avatar.ToDataUriScheme());

                writer.WriteEndObject();
            });

            using JsonDocument? returnData = await rest.Patch("users/@me", requestData, "users/@me").ConfigureAwait(false);

            return DiscordUser.FromJson(returnData!.RootElement);
        }

        /// <summary>
        /// Gets a list of user guilds the current bot is in.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task<DiscordUserGuild[]> GetCurrentUserGuilds(int? limit = null,
            Snowflake? baseGuildId = null, GuildGetStrategy getStrategy = GuildGetStrategy.After)
        {
            // Reqest
            var paramBuilder = new UrlParametersBuilder();

            if (baseGuildId.HasValue)
                paramBuilder.Add(getStrategy.ToString().ToLower(), baseGuildId.ToString());

            if (limit.HasValue)
                paramBuilder.Add("limit", limit.Value.ToString());

            using JsonDocument? data = await rest.Get($"users/@me/guilds{paramBuilder.ToQueryString()}", 
                "users/@me/guilds").ConfigureAwait(false);

            // Response
            JsonElement guildData = data!.RootElement;

            var guilds = new DiscordUserGuild[guildData.GetArrayLength()];

            for (int i = 0; i < guilds.Length; i++)
                guilds[i] = DiscordUserGuild.FromJson(guildData[i]);

            return guilds;
        }

        /// <summary>
        /// Removes the current bot from the specified guild.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task LeaveGuild(Snowflake guildId)
        {
            await rest.Delete($"users/@me/guilds/{guildId}", "users/@me/guilds/guild").ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of currently open DM channels for the current user.
        /// <para/>
        /// Note: For bot users, this will always return an empty array.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task<DiscordDMChannel[]> GetUserDMs()
        {
            using JsonDocument? data = await rest.Get("users/@me/channels", "users/@me/channels").ConfigureAwait(false);

            JsonElement dmData = data!.RootElement;

            var dms = new DiscordDMChannel[dmData.GetArrayLength()];

            for (int i = 0; i < dms.Length; i++)
                dms[i] = DiscordDMChannel.FromJson(dmData[i]);

            return dms;
        }

        /// <summary>
        /// Opens a DM channel with the specified user.
        /// </summary>
        /// <exception cref="DiscordHttpApiException"></exception>
        public async Task<DiscordDMChannel> CreateDM(Snowflake recipientId)
        {
            string requestData = BuildJsonContent(writer =>
            {
                writer.WriteStartObject();
                writer.WriteString("recipient_id", recipientId.ToString());
                writer.WriteEndObject();
            });

            using JsonDocument? returnData = await rest.Post("users/@me/channels", requestData,
                "users/@me/channels").ConfigureAwait(false);

            return DiscordDMChannel.FromJson(returnData!.RootElement);
        }
    }
}

#nullable restore
