using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Guild_Accounts;

namespace PhoenixBot.Modules.General
{
    [Group("Debate")]
    public class DebateCommands : ModuleBase<SocketCommandContext>
    {
        [Command("GiveTo")]
        [Summary("Used to give the Speaking Stick in the debate voice channel.")]
        public async Task GiveStickTo(SocketGuildUser user)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.DebateRunning == false) return;
            var self = Context.User;
            if (guild.StickHolder != Context.User.Id) return;
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
            await voiceChannel.AddPermissionOverwriteAsync(user, allow);
            await voiceChannel.RemovePermissionOverwriteAsync(self);
            guild.StickHolder = user.Id;
            GuildAccounts.SaveAccounts();
        }
        [Command("GiveUp")]
        [Summary("Used to give the stick up when in the Debate voice channel.")]
        public async Task GiveStickUp()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.DebateRunning == false) return;
            var self = Context.User;
            if (guild.StickHolder != Context.User.Id) return;
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
            await voiceChannel.RemovePermissionOverwriteAsync(self);
            guild.StickHolder = 0;
            GuildAccounts.SaveAccounts();
        }
        [Command("Take")]
        [Summary("Used to take the speaking stick for the Debate voice channel.")]
        public async Task TakeStick()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.DebateRunning == true && guild.StickHolder == 0)
            {
                var self = Context.User;
                var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
                var voiceChannel = Context.Guild.GetVoiceChannel(Config.bot.DebateID);
                await voiceChannel.AddPermissionOverwriteAsync(self, allow);
                guild.StickHolder = self.Id;
                GuildAccounts.SaveAccounts();
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User}: Either the debate period is over or the stick is in someone elses hands.");
            }
        }
    }
}
