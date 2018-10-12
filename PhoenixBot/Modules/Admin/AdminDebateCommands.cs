using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using PhoenixBot.Guild_Accounts;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Admin
{
    [Group("AdminDebate")]
    [Alias("AD")]
    public class AdminDebateCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Open", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task OpenDebate()
        {
            var vAllow = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow, readMessages: PermValue.Allow);
            var vDeny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny, readMessages: PermValue.Deny);
            var tAllow = new OverwritePermissions(connect: PermValue.Allow, readMessageHistory: PermValue.Allow, readMessages: PermValue.Allow, sendMessages: PermValue.Allow);
            var tDeny = new OverwritePermissions(connect: PermValue.Deny, readMessageHistory: PermValue.Deny, readMessages: PermValue.Deny, sendMessages: PermValue.Deny);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.debateVCID);
            var textChannel = Context.Guild.GetTextChannel(Config.bot.debateTCID);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), vAllow);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, vDeny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, tDeny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), tAllow);
            var guild = GuildAccounts.GetAccount(Context.Guild);
            Console.WriteLine(guild);
            guild.StickHolder = null;
            guild.DebateRunning = true;
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync("Guild stick holder id: " + guild.StickHolder);
            await Context.Channel.SendMessageAsync("Debate running: " + guild.DebateRunning);
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync("Both debate channels should be open.");
        }
        [Command("Close")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CloseDebate()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.debateVCID);
            var textChannel = Context.Guild.GetTextChannel(Config.bot.debateTCID);
            if(guild.StickHolder != null)
            {
                var currentHolder = guild.StickHolder;
                await voiceChannel.RemovePermissionOverwriteAsync(currentHolder);
                guild.StickHolder = null;
                GuildAccounts.SaveAccounts();
            }
            var currentHolderID =  guild.StickHolder;
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny, readMessages: PermValue.Deny);
            var tDeny = new OverwritePermissions(connect: PermValue.Deny, readMessageHistory: PermValue.Deny, readMessages: PermValue.Deny, sendMessages: PermValue.Deny);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), deny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, tDeny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), tDeny);
            guild.DebateRunning = false;
            GuildAccounts.SaveAccounts();
        }
        [Command("GiveStick")]
        [Alias("GS")]
        public async Task GiveStick(SocketGuildUser user)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if(guild.DebateRunning != true)
            {
                await ReplyAsync("The debate is not running.");
                return;
            }
            if(guild.StickHolder != Context.User)
            {
                await ReplyAsync($"Currently you don't hold the speaking stick. {guild.StickHolder} has the stick.");
                return;
            }
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow, readMessages: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.debateVCID);
            await voiceChannel.AddPermissionOverwriteAsync(user, allow);
            await voiceChannel.RemovePermissionOverwriteAsync(guild.StickHolder);
            guild.StickHolder = user;
            GuildAccounts.SaveAccounts();
        }
        [Command("RemoveStick")]
        [Alias("RS")]
        public async Task RemoveStick(SocketGuildUser user)
        {
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow, readMessages: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.debateVCID);
            await voiceChannel.AddPermissionOverwriteAsync(user, deny);
            var guild = GuildAccounts.GetAccount(Context.Guild);
            guild.StickHolder = null;
            GuildAccounts.SaveAccounts();
        }
        [Command("info")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DebateInfo()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var embed = new EmbedBuilder();
            embed.WithTitle("Current Debate Info")
                .AddField("ID set for who is holding the stick:", guild.StickHolder)
                .AddField("Debate is running:", guild.DebateRunning);
            await ReplyAsync("", false, embed);
        }
    }
}
