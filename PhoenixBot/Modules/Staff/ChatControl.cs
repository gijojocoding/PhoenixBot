using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Staff
{
    [Group("Staff")]
    public class ChatControl : ModuleBase<SocketCommandContext>
    {

        [Command("Mute")]
        [Summary("Staff Command, used stop a person from sending messages.")]
        public async Task Mute(IGuildUser user, [Remainder]string reason = "")
        {
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.muteLogID);

            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id == Context.Guild.Owner.Id)
            {

                //var target = user as SocketGuildUser;
                if (user == Context.Guild.Owner)
                {
                    await Context.User.SendMessageAsync("You can not mute the owner.");
                    return;
                }

                if (reason == "" || reason == null || reason == " ")
                {
                    await Context.Channel.SendMessageAsync("Failed to provide reason for the command.");
                }
                DataAccess Db = new DataAccess();
                Db.UpdateUserMute(user.Id, true);

                await muteLog.SendMessageAsync($"{user.Mention} has been muted by {Context.User.Mention} for {reason}.");
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                var embed = new EmbedBuilder();
                embed.WithTitle("**Staff Mute**")
                    .WithDescription($"{user.Username}, this an automated message. At the momement you are muted on all channels, please read the following as to why and how to get the mute removed.")
                    .AddField("Reason for the mute:", reason)
                    .AddField("How to Appeal your mute:", "Please use `!Appeal mute (your message)` exectly, if you don't get a dm from the bot then you entered in the `!Appeal mute` wrong. If you feel this was abuse by a staff member please use the Appeal command and then send a DM to the server owner.");
                await dmChannel.SendMessageAsync("", false, embed.Build());
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
            var muteLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.muteLogID);
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id == Context.Guild.Owner.Id)
            {
                DataAccess Db = new DataAccess();
                Db.UpdateUserMute(user.Id, false);
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
        [Summary("Staff command, used to send a warning to a person.")]
        public async Task Warn(SocketGuildUser user, byte rule, [Remainder] string reason)
        {

            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User))
            {

                DataAccess Db = new DataAccess();
                UserAccountModel model = new UserAccountModel();
                var warnLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.warningLogID);
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                var ruleText = Rules.Rules.GetRule(rule).RuleString;
                Db.UpdateUserWarning(user.Id, 1);
                var embed = new EmbedBuilder();
                embed.WithTitle("**Staff Warn**")
                    .WithDescription($"Staff was forced to use the Staff warn command. Please use the `!Appeal Warn` command so the warn can be looked into. If possible please take screenshots if this was abuse of power. Leave rude comments and remarks out of the text/voice channels.")
                    .AddField("Rule number broken:", rule)
                    .AddField("Rule is:", ruleText)
                    .AddField("Reason:", reason);
                await dmChannel.SendMessageAsync("", false, embed.Build());
                await warnLog.SendMessageAsync($"**Staff WARN** {Context.User.Mention} warned {user.Mention} for breaking rule: {rule}. Reason: {reason}");
            }
        }
        [Command("Ban")]
        [Summary("Staff command, used only when a member is no longer welcome within the community.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanUser(IGuildUser user, [Remainder] string reason)
        {
            if (RoleCheck.HasChiefRole((SocketGuildUser)Context.User) && (reason != null || reason != "" || reason != " "))
            {
                var banKickLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.banKickLogID);
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                var target = user.Mention;
                await dmChannel.SendMessageAsync($"It is with much debate and regret that we must inform you that you are banned from the server. The reason is: {reason}");
                await banKickLog.SendMessageAsync($"{user.Mention} was banned from the server for: {reason}");
                await user.Guild.AddBanAsync(user, 7, reason);
            }
            else
            {
                await ReplyAsync("This is for `Chief` level staff use to help prevent abuse.");
            }
        }
        [Command("kick")]
        [Summary("Staff command, used to kick members. Used when mutes and warnings do not fix the issue.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, [Remainder] string reason)
        {
            int deleteNumber = 1;
            var banKickLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.banKickLogID);
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            if (RoleCheck.HasChiefRole((SocketGuildUser)Context.User) && (reason != null || reason != "" || reason != " "))
            {
                await Context.Message.DeleteAsync();
                await dmChannel.SendMessageAsync($"You have been kicked form the server. The reason is {reason}");
                await banKickLog.SendMessageAsync($"{Context.User.Mention} kicked {user.Mention}. The reason is: {reason}");
                await user.KickAsync(reason);
            }
            else
            {
                await ReplyAsync("This is for `Chief` level staff use to help prevent abuse.");
            }

        }
    }
}
