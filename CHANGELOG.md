## v5.0.0

### Breaking Changes
- Changed target framework to `netstandard2.1`
- Renamed `DiscordChannel.ChannelType` to `Type`
- Renamed `ITextChannel.ChannelType` to `Type`
- Renamed `DiscordInviteChannel.ChannelId` to `Id`
- Renamed `DiscordInviteGuild.GuildId` to `Id`
- Replaced `string DiscordInviteGuild.SplashHash` with `DiscordCdnUrl? Splash`
- Replaced `Snowflake? DiscordEmoji.UserId` with `DiscordUser User`
- Replaced `ulong DiscordHttpRateLimitException.Reset` with `double? Reset`
- Removed `DiscordInviteMetadata.IsRevoked` (obsolete)
- Removed `DiscordHttpRateLimitException.ResetHighPrecision` (obsolete)

### Additions
- Added `DiscordOverwriteType.Unknown`
- Added `DiscordGuildMember.PremiumSince`
- Added `DiscordMessage.GuildId`
- Added `DiscordGuildVoiceChannel.Nsfw` (see changes)
- Added `DiscordGuildCategoryChannel.ParentId` (see changes)
- Added `DiscordGuildCategoryChannel.Nsfw` (see changes)

### Changes
- Moved `ParentId` and `Nsfw` properties down into `DiscordGuildChannel` (used to be only in text/voice channels, now available for all guild channels)

