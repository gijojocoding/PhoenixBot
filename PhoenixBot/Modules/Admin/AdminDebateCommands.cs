using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using PhoenixBot.Guild_Accounts;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Admin
{
    [Group("adminDebate")]
    public class AdminDebateCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Open")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task OpenDebate()
        {
            var allow = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow, readMessages: PermValue.Allow);
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny, readMessages: PermValue.Deny);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), allow);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, deny);
            var guild = GuildAccounts.GetAccount(Context.Guild);
            Console.WriteLine(guild);
            guild.StickHolder = null;
            guild.DebateRunning = true;
            await Context.Channel.SendMessageAsync("Guild stick holder id: " + guild.StickHolder);
            await Context.Channel.SendMessageAsync("Debate running: " + guild.DebateRunning);
            GuildAccounts.SaveAccounts();
        }
        [Command("Close")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CloseDebate()
        {
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var currentHolderID =  guild.StickHolder;
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny, readMessages: PermValue.Deny);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), deny);
            await voiceChannel.RemovePermissionOverwriteAsync(currentHolderID);
            guild.StickHolder = null;
            guild.DebateRunning = false;
            GuildAccounts.SaveAccounts();
        }
        [Command("GiveStick")]
        [Alias("GS")]
        public async Task GiveStick(SocketGuildUser user)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow, readMessages: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
            await voiceChannel.AddPermissionOverwriteAsync(user, allow);
            guild.StickHolder = user;
            GuildAccounts.SaveAccounts();
        }
        [Command("RemoveStick")]
        [Alias("RS")]
        public async Task RemoveStick(SocketGuildUser user)
        {
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow, readMessages: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
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
