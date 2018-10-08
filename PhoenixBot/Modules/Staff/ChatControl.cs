using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Staff
{
    public class ChatControl : ModuleBase<SocketCommandContext>
    {
        [Command("Mute")]
        [Summary("Staff Command, used stop a person from sending messages. **DO NOT ABUSE THE COMMAND!**")]
        public async Task Mute(IGuildUser user, [Remainder]string reason)
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.muteLogID);
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser;
            var account = User_Accounts.UserAccounts.GetAccount(target);

            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id == Context.Guild.Owner.Id)
            {
                if (reason == "" || reason == null || reason == " ")
                {
                    await Context.Channel.SendMessageAsync("Failed to provide reason for the command.");
                }
                account.IsMuted = true;
                User_Accounts.UserAccounts.SaveAccounts();
                await muteLog.SendMessageAsync($"{target.Mention} has been muted by {Context.User.Mention} for {reason}.");
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync($"{target.Mention}, you have been muted for {reason}. Please contact the Guild Leader. If you feel this was abuse of power please contact the Server Own with proof.");
            }
            else
            {
                await Context.Channel.SendMessageAsync("404 Command Permission not found.");
                return;
            }
        }
        [Command("UnMute")]
        [Summary("Staff command, used to allow people to send messages if muted.")]
        public async Task UnMute(IGuildUser user)
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.muteLogID);
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser;
            var account = User_Accounts.UserAccounts.GetAccount(target);
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id == Context.Guild.Owner.Id)
            {
                account.IsMuted = false;
                User_Accounts.UserAccounts.SaveAccounts();
                await muteLog.SendMessageAsync($"{target.Mention} has been unmuted. Their setting is {account.IsMuted}");
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync($"{user.Mention},  you have been unmuted, please follow the rules from now on. If you feel it was from abuse of power please send a message to the Server  owner if you have not already done so.");
            }
            else
            {
                await Context.Channel.SendMessageAsync("404 Command Permission not found.");
                return;
            }
        }
        [Command("warn")]
        [Summary("Staff command, used to send a warning to a person. **DO NOT ABUSE THE COMMAND!**")]
        public async Task warn(IGuildUser user, [Remainder] string reason)
        {
            var warnLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.warningLogID);
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User))
            {
                await dmChannel.SendMessageAsync($"{user.Mention} was warned for {reason}");
                await warnLog.SendMessageAsync($"{Context.User.Mention} warned {user.Mention} for {reason}");
            }
        }
        [Command("Ban")]
        [Summary("Staff command, used only when a member is no longer welcome within the community. **DO NOT ABUSE THE COMMAND!**")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task BanUser(IGuildUser user, [Remainder] string reason)
        {
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) && (reason != null || reason != "" || reason != " "))
            {
                var banKickLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.banKickLogID);
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                var target = user.Mention;
                await dmChannel.SendMessageAsync($"It is with much debate and regret that we must inform you that you are banned from the server. The reason is: {reason}");
                await banKickLog.SendMessageAsync($"{user.Mention} was banned from the server for: {reason}");
                await user.Guild.AddBanAsync(user, 7, reason);
            }
            else
            {
                return;
            }
        }
        [Command("kick")]
        [Summary("Staff command, used to kick members. Used when mutes and warnings do not fix the issue. **DO NOT ABUSE THE COMMAND!**")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, [Remainder] string reason)
        {
            int deleteNumber = 1;
            var banKickLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.banKickLogID);
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User))
            {
                await Context.Message.DeleteAsync();
                await dmChannel.SendMessageAsync($"You have been kicked form the server. The reason is {reason}");
                await banKickLog.SendMessageAsync($"{Context.User.Mention} kicked {user.Mention}. The reason is: {reason}");
                await user.KickAsync(reason);
            }
        }
        [Command("purge")]
        [Summary("Staff command, used to delete the last series of messages.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task PurgeChat(int delnum, [Remainder] string reason)
        {
            var adminLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.adminLogID);
            var messages = await Context.Channel.GetMessagesAsync(delnum + 1).Flatten();
            await Context.Channel.DeleteMessagesAsync(messages);
            await adminLog.SendMessageAsync($"{Context.User.Mention} deleted {delnum} for {reason}");
        }
        [Command("vmute")]
        [Summary("Staff command, used to mute")]
        public async Task VMute(IGuildUser user, [Remainder] string reason)
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.muteLogID);
            var dmchannel = await user.GetOrCreateDMChannelAsync();
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow);
            //var vChannels = Context.Guild.VoiceChannels;
            foreach (var vChannel in Context.Guild.VoiceChannels)
            {
                await vChannel.AddPermissionOverwriteAsync(user, deny);
                await ReplyAsync($"{user.Mention} has been deafen");
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Voice Mute")
                .AddField("Person issuing voice mute:", Context.User.Mention)
                .AddField("Reason for the voice mute:", reason);
            await muteLog.SendMessageAsync("", false, embed);
            await dmchannel.SendMessageAsync($"You have been muted on all voice channels for: {reason}");
        }
        [Command("Vunmute")]
        public async Task VUnmute(IGuildUser user)
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.muteLogID);
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
    }
}
