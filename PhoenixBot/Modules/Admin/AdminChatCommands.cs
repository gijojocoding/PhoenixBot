using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Admin
{
    [Group("Admin")]
    [Summary("Admin commands for chat control.")]
    [RequireUserPermission(GuildPermission.Administrator)]
    public class AdminChatCommands : ModuleBase<SocketCommandContext>
    {
        [Command("mute", RunMode = RunMode.Async)]
        async Task AdminMuteCmd(SocketGuildUser target, string reason = "You have actively violated several rules to the point that an Admin Mute has been issued.")
        {
            DataAccess Db = new DataAccess();
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.muteLogID);
            Db.UpdateUserMute(target.Id, true);

            await muteLog.SendMessageAsync($"{target.Mention} has been muted by {Context.User.Mention} for {reason}.");
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            await VMuteSubTask(target);
            var embed = new EmbedBuilder();
            embed.WithTitle("**Admin Mute**")
                .WithDescription($"{target.Username}, this an automated message. At the momement you are muted on all channels, please read the following as to why and how to get the mute removed.")
                .AddField("Reason for the mute:", reason)
                .AddField("How to Appeal your mute:", "Please use `!Appeal mute (your message)` exectly, if you don't get a dm from the bot then you entered in the `!Appeal mute` wrong.");
            await dmChannel.SendMessageAsync("", false, embed.Build());
        }
        [Command("MassMute"), Alias("mmute")]
        async Task AdminMassMuteCmd(params IGuildUser[] users)
        {
            foreach(var user in users)
            {
                DataAccess Db = new DataAccess();
                var target = user as SocketGuildUser;
                if (target == Context.Guild.Owner)
                {
                    await Context.User.SendMessageAsync("You can not mute the owner.");
                }
                else
                {
                    Db.UpdateUserMute(user.Id, true);
                    
                    await target.SendMessageAsync("Mass Chat Mute has been issued due to the number of people breaking rules so that it can be resolved without having to fight for control of the channel(s). Please wait for staff to post in the channel(s).");
                }
            }
            await Context.User.SendMessageAsync("Users have been muted.");
        }
        [Command("MassUnMute"), Alias("munmute")]
        async Task AdminMassUnMuteCmd(params SocketGuildUser[] users)
        {
            DataAccess Db = new DataAccess();
            foreach (var user in users)
            {
                Db.UpdateUserMute(user.Id, false);
                await user.SendMessageAsync("Mass Chat Unmute has been done. ");
            }
            await Context.User.SendMessageAsync("Users have been unmuted.");
        }
        [Command("unmute", RunMode = RunMode.Async)]
        async Task AddminUnmuteCmd(SocketGuildUser target)
        {
            DataAccess Db = new DataAccess();
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.muteLogID);
            Db.UpdateUserMute(target.Id, false);
            await muteLog.SendMessageAsync($"{target.Mention} has been Unmuted by {Context.User}");
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            await VUnmuteSubTask(target);
            var embed = new EmbedBuilder();
            embed.WithTitle("**Admin UnMute**")
                .WithDescription($"{target.Username}, this an automated message. You have been unmuted.");
            await dmChannel.SendMessageAsync("", false, embed.Build());
        }
        [Command("vmute", RunMode = RunMode.Async)]
        [Summary("Admin command, used to mute")]
        public async Task AdminVMute(IGuildUser user, [Remainder] string reason = "You have actively violated several rules to the point that an Admin Voice Mute has been issued.")
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.muteLogID);
            var dmchannel = await user.GetOrCreateDMChannelAsync();
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow);
            foreach (var vChannel in Context.Guild.VoiceChannels)
            {
                await vChannel.AddPermissionOverwriteAsync(user, deny);
                await ReplyAsync($"{user.Mention} has been deafen");
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Voice Mute")
                .AddField("Person issuing voice mute:", Context.User.Mention)
                .AddField("Reason for the voice mute:", reason)
                .AddField("How to Appeal your mute:", "Please use `!Appeal mute (your message)` exectly, if you don't get a dm from the bot then you entered in the `!Appeal mute` wrong.");
            await muteLog.SendMessageAsync("", false, embed.Build());
            await dmchannel.SendMessageAsync($"You have been muted on all voice channels for: {reason}");
        }
        [Command("Vunmute", RunMode = RunMode.Async)]
        public async Task AdminVUnmute(IGuildUser user)
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.muteLogID);
            var dmchannel = await user.GetOrCreateDMChannelAsync();
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
            foreach (var vChannel in Context.Guild.VoiceChannels)
            {
                await vChannel.AddPermissionOverwriteAsync(user, allow);
                await ReplyAsync($"{user.Mention} has been deafen");
            }
            await muteLog.SendMessageAsync($"{user.Mention} has been unmuted on all voice channels.");
            await dmchannel.SendMessageAsync("Default message: You have been unmuted on all voice channels. Please follow the rules from now on.");
        }
        [Command("Kick")]
        async Task AdminKickCmd(SocketGuildUser target, [Remainder] string reason = "Repeated violation of rules after warnings, that Admin was force to kick you from the server.")
        {
            var banKickLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.banKickLogID);
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            var embed = new EmbedBuilder();
            embed.WithTitle("**Admin Kick**")
                .WithDescription("");
            await dmChannel.SendMessageAsync("", false, embed.Build());
            await target.KickAsync(reason);
        }
        [Command("ban")]
        async Task AdminBanCmd(SocketGuildUser target, [Remainder] string reason = "You have acted in such a way that Admin had to use the Admin Ban command. While it is regreted that it had to be done, we promote a safe and friendly server.")
        {
            var banKickLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.banKickLogID);
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            var embed = new EmbedBuilder();
            embed.WithTitle("**Admin Ban**")
                .WithDescription("This is an automated notification message. We regret to inform you that you have been banned from our Discord server, because of the severity (or repeated) issue(s).")
                .AddField("The reason:", reason);
            await dmChannel.SendMessageAsync("", false, embed.Build());
            await banKickLog.SendMessageAsync($"{target.Username} has been banned for: {reason}");
            await Context.Guild.AddBanAsync(target, 13, reason);
        }
        [Command("warn")]
        [Summary("Admin command, used to send a warning to a person. **DO NOT ABUSE THE COMMAND!**")]
        public async Task Warn(IGuildUser user, [Remainder] string reason)
        {
            var warnLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.warningLogID);
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            var embed = new EmbedBuilder();
            embed.WithTitle("**Admin Warn**")
                .WithDescription($"Admin was forced to use the Admin warn command. Please use the `!Appeal Warn` command so the warn can be looked into. If possible please take screenshots if this was abuse of power. Leave rude comments and remarks out of the text/voice channels.")
                .AddField("Reason:", reason);
            await dmChannel.SendMessageAsync("", false, embed.Build());
            await warnLog.SendMessageAsync($"**ADMIN WARN** {Context.User.Mention} warned {user.Mention} for {reason}");
        }
        [Command("purge")]
        [Summary("Admin command, deletes a set of messages. no log created.")]
        public async Task AdminPurgeChat(int delnum)
        {
            //var messages = await Context.Channel.GetMessagesAsync(delnum + 1).Flatten();
            var Msgs = await Context.Channel.GetMessagesAsync(delnum + 1).FlattenAsync();

            foreach (IUserMessage Msg in Msgs)
            {
                await Msg.DeleteAsync();
                await Task.Delay(1000);
            }
        }
        private async Task VMuteSubTask(SocketGuildUser target)
        {
            var deny = new OverwritePermissions(speak: PermValue.Deny);
            foreach (var vChannel in Context.Guild.VoiceChannels)
            {
                await vChannel.AddPermissionOverwriteAsync(target, deny);
            }
        }
        private async Task VUnmuteSubTask(SocketGuildUser target)
        {
            var allow = new OverwritePermissions(speak: PermValue.Allow);
            foreach (var vChannel in Context.Guild.VoiceChannels)
            {
                await vChannel.AddPermissionOverwriteAsync(target, allow);
            }
        }
    }
}
