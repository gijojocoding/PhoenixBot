﻿using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Staff
{
    public class StaffCommands : ModuleBase<SocketCommandContext>
    {
        [Command("DMMessage")]
        [Summary("Sends a DM to the person tagged. **Must have the role of Chief or Investment Staff.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task AdminDmMessage(SocketGuildUser user, [Remainder] string message)
        {
            if (!RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || !RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User))
            {
                await Context.Message.DeleteAsync();
                var sender = await Context.User.GetOrCreateDMChannelAsync();
                await sender.SendMessageAsync("You do not have the permissions to use this command!");
                return;
            }
            var target = user;
            var messageLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.messageLogID);
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync($"This is a message from the discord server saying: {message}");
            await messageLog.SendMessageAsync($"{Context.User.Mention} sent a message to {target.Mention}. Message was: {message}");
        }

        [Command("warn")]
        [Summary("Staff command, used to send a warning to a person. (tag person) (Rule number broken) \n \"Example: @Gijojo 4\" to tell them they broke rule 4")]
        public async Task Warn(IGuildUser user, byte rule, [Remainder] string reason)
        {
            var warnLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.warningLogID);
            var dmChannel = await user.GetOrCreateDMChannelAsync();
            var ruleText = Rules.Rules.GetRule(rule);
            var embed = new EmbedBuilder();
            embed.WithTitle("**Staff Warn**")
                .WithDescription($"Staff was forced to use the Staff warn command. Please use the `!Appeal Warn` command so the warn can be looked into. If possible please take screenshots if this was abuse of power. Leave rude comments and remarks out of the text/voice channels.")
                .AddField("Rule number broken:", rule)
                .AddField("Rule is:", ruleText)
                .AddField("Reason:", reason);
            await dmChannel.SendMessageAsync("", false, embed.Build());
            await warnLog.SendMessageAsync($"**Staff WARN** {Context.User.Mention} warned {user.Mention} for breaking rule: {rule}. Reason: {reason}");
        }
        [RequireOwner]
        [Command("testingget")]
        async Task getuser(IGuildUser target)
        {
            DataAccess Db = new DataAccess();
            var UserModel = Db.GetUser(target.Id);
            Console.WriteLine("Got User");
                var user = Context.Guild.GetUser(UserModel.Id);
                var e = new EmbedBuilder();
                e.AddField("User: ", user.Username)
                    .AddField("Warning Count: ", UserModel.NumberOfWarnings)
                    .AddField("Mute Status: ", Converter.ConvertToBool(UserModel.IsMuted));
                await ReplyAsync("", false, e.Build());
            
        }
        [RequireOwner]
        [Command("testingadd")]
        async Task adduser(SocketGuildUser target)
        {
            DataAccess Db = new DataAccess();
            Db.AddUser(target.Id);
        }
    }
}
