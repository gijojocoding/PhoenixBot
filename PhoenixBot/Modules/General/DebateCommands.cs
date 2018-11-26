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
            if (Context.IsPrivate == true) return;
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var currentHolderId = guild.StickHolderId;
            SocketGuildUser holder = Global.Client.GetGuild(Config.bot.guildID).GetUser(currentHolderId);
            if (Context.Channel.Id != ChannelIds.channels.debateTCID)
            {
                await ReplyAsync("Please use the command in the Debate text channel thank you.");
                return;
            }
            if (guild.DebateRunning == false) return;
            var self = Context.User;
            if (holder != Context.User) return;
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
            await voiceChannel.AddPermissionOverwriteAsync(user, allow);
            await voiceChannel.RemovePermissionOverwriteAsync(self);
            guild.StickHolderId = user.Id;
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"{self} passed the Speaking Stick to {user}");
        }
        [Command("GiveUp")]
        [Summary("Used to give the stick up when in the Debate voice channel.")]
        public async Task GiveStickUp()
        {
            if (Context.IsPrivate == true) return;
            if (Context.Channel.Id != ChannelIds.channels.debateTCID)
            {
                await ReplyAsync("Please use the command in the Debate text channel thank you.");
                return;
            }
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.DebateRunning == false) return;
            var self = Context.User;
            var currentHolderId = guild.StickHolderId;
            SocketGuildUser holder = Global.Client.GetGuild(Config.bot.guildID).GetUser(currentHolderId);
            if (holder != Context.User) return;
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
            await voiceChannel.RemovePermissionOverwriteAsync(self);
            guild.StickHolderId = 0;
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"{self} gave up the stick! Quick grab it!");
        }
        [Command("Take")]
        [Summary("Used to take the speaking stick for the Debate voice channel.")]
        public async Task TakeStick()
        {
            if (Context.IsPrivate == true) return;
            if (Context.Channel.Id != ChannelIds.channels.debateTCID)
            {
                await ReplyAsync("Please use the command in the Debate text channel thank you.");
                return;
            }
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var currentHolderId = guild.StickHolderId;
            SocketGuildUser holder = Global.Client.GetGuild(Config.bot.guildID).GetUser(currentHolderId);
            if (guild.DebateRunning == true && guild.StickHolderId == 0)
            {
                var self = Context.User as SocketGuildUser;
                var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
                var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
                await voiceChannel.AddPermissionOverwriteAsync(self, allow);
                guild.StickHolderId = self.Id;
                GuildAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"{self} got the stick!");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User}: Either the debate period is over or the stick is in someone elses hands.");
            }
        }
    }
}
