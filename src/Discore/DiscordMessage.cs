﻿using Discore.Http.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discore
{
    /// <summary>
    /// Represents a message sent in a channel within Discord.
    /// </summary>
    public sealed class DiscordMessage : DiscordIdObject
    {
        public const int MAX_CHARACTERS = 2000;

        /// <summary>
        /// Gets the id of the channel this message is in.
        /// </summary>
        public Snowflake ChannelId { get; }
        /// <summary>
        /// Gets the author of this message.
        /// </summary>
        public DiscordUser Author
        {
            get
            {
                return cache != null 
                    ? authorId.HasValue ? cache.Users[authorId.Value] : null 
                    : author;
            }
        }
        /// <summary>
        /// Gets the contents of this message.
        /// </summary>
        public string Content { get; }
        /// <summary>
        /// Gets the time this message was first sent.
        /// </summary>
        public DateTime Timestamp { get; }
        /// <summary>
        /// Gets the time of the last edit to this message.
        /// </summary>
        public DateTime? EditedTimestamp { get; }
        /// <summary>
        /// Gets whether or not this message was sent with the /tts command.
        /// </summary>
        public bool TextToSpeech { get; }
        /// <summary>
        /// Gets whether or not this message mentioned everyone via @everyone.
        /// </summary>
        public bool MentionEveryone { get; }
        /// <summary>
        /// Gets a list of all user-specific mentions in this message.
        /// </summary>
        public IReadOnlyList<DiscordUser> Mentions
        {
            get
            {
                return cache != null
                    ? mentionIds != null ? cache.Users[mentionIds] : null
                    : mentions;
            }
        }
        /// <summary>
        /// Gets a list of all the ids of mentioned roles in this message.
        /// </summary>
        public IReadOnlyList<Snowflake> MentionedRoles { get; }
        /// <summary>
        /// Gets a list of all attachments in this message.
        /// </summary>
        public IReadOnlyList<DiscordAttachment> Attachments { get; }
        /// <summary>
        /// Gets a list of all embedded attachments in this message.
        /// </summary>
        public IReadOnlyList<DiscordEmbed> Embeds { get; }
        /// <summary>
        /// Gets a list of all reactions to this message.
        /// </summary>
        public IReadOnlyList<DiscordReaction> Reactions { get; }
        /// <summary>
        /// Used for validating if a message was sent.
        /// </summary>
        public Snowflake? Nonce { get; }
        /// <summary>
        /// Gets whether or not this message is pinned in the containing channel.
        /// </summary>
        public bool IsPinned { get; }
        /// <summary>
        /// If this message was generated by a webhook, gets the id of that webhook.
        /// </summary>
        public Snowflake? WebhookId { get; }

        IDiscordApplication app;
        HttpChannelsEndpoint channelsHttp;

        DiscoreCache cache;
        DiscordUser author;
        Snowflake? authorId;
        IReadOnlyList<DiscordUser> mentions;
        Snowflake[] mentionIds;

        internal DiscordMessage(DiscoreCache cache, IDiscordApplication app, DiscordApiData data)
            : this(app, data, true)
        {
            this.cache = cache;
        }

        internal DiscordMessage(IDiscordApplication app, DiscordApiData data)
            : this(app, data, false)
        { }

        private DiscordMessage(IDiscordApplication app, DiscordApiData data, bool isWebSocket)
            : base(data)
        {
            this.app = app;
            channelsHttp = app.HttpApi.InternalApi.Channels;

            Content         = data.GetString("content");
            Timestamp       = data.GetDateTime("timestamp").GetValueOrDefault();
            EditedTimestamp = data.GetDateTime("edited_timestamp").GetValueOrDefault();
            TextToSpeech    = data.GetBoolean("tts").GetValueOrDefault();
            MentionEveryone = data.GetBoolean("mention_everyone").GetValueOrDefault();
            Nonce           = data.GetSnowflake("nonce");
            IsPinned        = data.GetBoolean("pinned").GetValueOrDefault();
            ChannelId       = data.GetSnowflake("channel_id").GetValueOrDefault();
            WebhookId       = data.GetSnowflake("webhook_id");

            if (!isWebSocket)
            {
                // Get author
                DiscordApiData authorData = data.Get("author");
                if (authorData != null)
                    author = new DiscordUser(authorData, WebhookId.HasValue);

                // Get mentions
                IList<DiscordApiData> mentionsArray = data.GetArray("mentions");
                if (mentionsArray != null)
                {
                    DiscordUser[] mentions = new DiscordUser[mentionsArray.Count];

                    for (int i = 0; i < mentionsArray.Count; i++)
                        mentions[i] = new DiscordUser(mentionsArray[i]);

                    this.mentions = mentions;
                }
            }
            else
            {
                // Get author id
                authorId = data.LocateSnowflake("author.id");

                // Get user mention ids
                IList<DiscordApiData> mentionsArray = data.GetArray("mentions");
                if (mentionsArray != null)
                {
                    mentionIds = new Snowflake[mentionsArray.Count];

                    for (int i = 0; i < mentionIds.Length; i++)
                        mentionIds[i] = mentionsArray[i].GetSnowflake("id").Value;
                }
            }

            // Get mentioned roles
            IList<DiscordApiData> mentionRolesArray = data.GetArray("mention_roles");
            if (mentionRolesArray != null)
            {
                Snowflake[] mentionedRoles = new Snowflake[mentionRolesArray.Count];

                for (int i = 0; i < mentionRolesArray.Count; i++)
                    mentionedRoles[i] = mentionRolesArray[i].ToSnowflake().Value;

                MentionedRoles = mentionedRoles;
            }

            // Get attachments
            IList<DiscordApiData> attachmentsArray = data.GetArray("attachments");
            if (attachmentsArray != null)
            {
                DiscordAttachment[] attachments = new DiscordAttachment[attachmentsArray.Count];

                for (int i = 0; i < attachmentsArray.Count; i++)
                    attachments[i] = new DiscordAttachment(attachmentsArray[i]);

                Attachments = attachments;
            }

            // Get embeds
            IList<DiscordApiData> embedsArray = data.GetArray("embeds");
            if (embedsArray != null)
            {
                DiscordEmbed[] embeds = new DiscordEmbed[embedsArray.Count];

                for (int i = 0; i < embedsArray.Count; i++)
                    embeds[i] = new DiscordEmbed(embedsArray[i]);

                Embeds = embeds;
            }

            // Get reactions
            IList<DiscordApiData> reactionsArray = data.GetArray("reactions");
            if (reactionsArray != null)
            {
                DiscordReaction[] reactions = new DiscordReaction[reactionsArray.Count];

                for (int i = 0; i < reactionsArray.Count; i++)
                    reactions[i] = new DiscordReaction(reactionsArray[i]);

                Reactions = reactions;
            }
        }

        /// <summary>
        /// Adds a reaction to this message.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool AddReaction(DiscordReactionEmoji emoji)
        {
            return AddReaction(emoji.Name, emoji.Id.Value);
        }

        /// <summary>
        /// Adds a reaction to this message.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> AddReactionAsync(DiscordReactionEmoji emoji)
        {
            return await AddReactionAsync(emoji.Name, emoji.Id.Value);
        }

        /// <summary>
        /// Adds a reaction to this message.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool AddReaction(string emojiName, Snowflake? customEmojiId = null)
        {
            try { return AddReactionAsync(emojiName, customEmojiId).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Adds a reaction to this message.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> AddReactionAsync(string emojiName, Snowflake? customEmojiId = null)
        {
            DiscordApiData data = await channelsHttp.CreateReaction(ChannelId, Id, 
                new DiscordReactionEmoji(emojiName, customEmojiId));
            return data.IsNull;
        }

        /// <summary>
        /// Removes a reaction from this message added from the current authenticated user.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool RemoveMyReaction(DiscordReactionEmoji reactionEmoji)
        {
            try { return RemoveMyReactionAsync(reactionEmoji).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Removes a reaction from this message added from the current authenticated user.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> RemoveMyReactionAsync(DiscordReactionEmoji reactionEmoji)
        {
            DiscordReactionEmoji emoji = new DiscordReactionEmoji(reactionEmoji.Name, reactionEmoji.Id);

            DiscordApiData data = await channelsHttp.DeleteOwnReaction(ChannelId, Id, emoji);
            return data.IsNull;
        }

        /// <summary>
        /// Removes a reaction from this message added from the current authenticated user.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool RemoveMyReaction(string emojiName, Snowflake? customEmojiId = null)
        {
            try { return RemoveMyReactionAsync(emojiName, customEmojiId).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Removes a reaction from this message added from the current authenticated user.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> RemoveMyReactionAsync(string emojiName, Snowflake? customEmojiId = null)
        {
            DiscordReactionEmoji emoji = new DiscordReactionEmoji(emojiName, customEmojiId);

            DiscordApiData data = await channelsHttp.DeleteOwnReaction(ChannelId, Id, emoji);
            return data.IsNull;
        }

        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <param name="user">The user who added the reacted.</param>
        /// <param name="reactionEmoji"></param>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool RemoveReaction(DiscordUser user, DiscordReactionEmoji reactionEmoji)
        {
            try { return RemoveReactionAsync(user, reactionEmoji).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <param name="user">The user who added the reacted.</param>
        /// <param name="reactionEmoji"></param>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> RemoveReactionAsync(DiscordUser user, DiscordReactionEmoji reactionEmoji)
        {
            DiscordReactionEmoji emoji = new DiscordReactionEmoji(reactionEmoji.Name, reactionEmoji.Id);

            DiscordApiData data = await channelsHttp.DeleteUserReaction(ChannelId, Id, user.Id, emoji);
            return data.IsNull;
        }

        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <param name="user">The user who added the reacted.</param>
        /// <param name="emojiName"></param>
        /// <param name="customEmojiId"></param>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool RemoveReaction(DiscordUser user, string emojiName, Snowflake? customEmojiId = null)
        {
            try { return RemoveReactionAsync(user, emojiName, customEmojiId).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Removes a reaction from this message.
        /// </summary>
        /// <param name="user">The user who added the reacted.</param>
        /// <param name="emojiName"></param>
        /// <param name="customEmojiId"></param>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> RemoveReactionAsync(DiscordUser user, string emojiName, Snowflake? customEmojiId = null)
        {
            DiscordReactionEmoji emoji = new DiscordReactionEmoji(emojiName, customEmojiId);

            DiscordApiData data = await channelsHttp.DeleteUserReaction(ChannelId, Id, user.Id, emoji);
            return data.IsNull;
        }

        /// <summary>
        /// Gets all users who reacted with the specified emoji to this message.
        /// </summary>
        public IReadOnlyList<DiscordUser> GetReactions(DiscordReactionEmoji reactionEmoji)
        {
            try { return GetReactionsAsync(reactionEmoji).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Gets all users who reacted with the specified emoji to this message.
        /// </summary>
        public async Task<IReadOnlyList<DiscordUser>> GetReactionsAsync(DiscordReactionEmoji reactionEmoji)
        {
            DiscordApiData data = await channelsHttp.GetReactions(ChannelId, Id, reactionEmoji);
            DiscordUser[] reactions = new DiscordUser[data.Values.Count];
            for (int i = 0; i < data.Values.Count; i++)
                reactions[i] = new DiscordUser(data.Values[i]);

            return reactions;
        }

        /// <summary>
        /// Gets all users who reacted with the specified emoji to this message.
        /// </summary>
        public IReadOnlyList<DiscordUser> GetReactions(string emojiName, Snowflake? customEmojiId = null)
        {
            try { return GetReactionsAsync(emojiName, customEmojiId).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Gets all users who reacted with the specified emoji to this message.
        /// </summary>
        public async Task<IReadOnlyList<DiscordUser>> GetReactionsAsync(string emojiName, Snowflake? customEmojiId = null)
        {
            DiscordReactionEmoji emoji = new DiscordReactionEmoji(emojiName, customEmojiId);
            return await GetReactionsAsync(emoji);
        }

        /// <summary>
        /// Deletes all reactions to this message.
        /// </summary>
        public void DeleteAllReactions()
        {
            try { DeleteAllReactionsAsync().Wait(); }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Deletes all reactions to this message.
        /// </summary>
        public async Task DeleteAllReactionsAsync()
        {
            await channelsHttp.DeleteAllReactions(ChannelId, Id);
        }

        /// <summary>
        /// Pins this message to the channel it was sent in.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool Pin()
        {
            try { return PinAsync().Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Pins this message to the channel it was sent in.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> PinAsync()
        {
            DiscordApiData data = await channelsHttp.AddPinnedChannelMessage(ChannelId, Id);
            return data.IsNull;
        }

        /// <summary>
        /// Unpins this message from the channel it was sent in.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public bool Unpin()
        {
            try { return UnpinAsync().Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Unpins this message from the channel it was sent in.
        /// </summary>
        /// <returns>Returns whether the operation was successful.</returns>
        public async Task<bool> UnpinAsync()
        {
            DiscordApiData data = await channelsHttp.DeletePinnedChannelMessage(ChannelId, Id);
            return data.IsNull;
        }

        /// <summary>
        /// Changes the contents of this message.
        /// Note: changes will not be reflected in this message instance.
        /// </summary>
        /// <param name="newContent">The new contents.</param>
        /// <returns>Returns the editted message.</returns>
        public DiscordMessage Edit(string newContent)
        {
            try { return EditAsync(newContent).Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Changes the contents of this message.
        /// Note: changes will not be reflected in this message instance.
        /// </summary>
        /// <param name="newContent">The new contents.</param>
        /// <returns>Returns the editted message.</returns>
        public async Task<DiscordMessage> EditAsync(string newContent)
        {
            DiscordApiData data = await channelsHttp.EditMessage(ChannelId, Id, newContent);

            return new DiscordMessage(app, data);
        }

        /// <summary>
        /// Deletes this message.
        /// </summary>
        public bool Delete()
        {
            try { return DeleteAsync().Result; }
            catch (AggregateException aex) { throw aex.InnerException; }
        }

        /// <summary>
        /// Deletes this message.
        /// </summary>
        public async Task<bool> DeleteAsync()
        {
            DiscordApiData data = await channelsHttp.DeleteMessage(ChannelId, Id);
            return data.IsNull;
        }
    }
}
