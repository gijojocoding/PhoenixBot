using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace PhoenixBot.Modules.General
{
    public class TempChannelCommands : ModuleBase<SocketCommandContext>
    {
        private const int maxTime = 3600000;
        private const int MaxNumberOfChannels = 5;
        [Cooldown(60, adminsAreLimited: true)]
        [Command("CreateVoice", RunMode = RunMode.Async)]
        [Summary("Creates a Voice Channel for a set time.")]
        public async Task CreateVoiceChannel(int time)
        {
            var guild = Context.Guild;
            var guildAccount = Guild_Accounts.GuildAccounts.GetAccount(guild);
            if (guildAccount.NumberOfTempChannels >= MaxNumberOfChannels)
            {
                await ReplyAsync("We currently have too many temp channels. Please wait for some to expire or ask to have a channel created.");
            }
            var timeWanted = time * 1000;
            if (timeWanted > maxTime)
            {
                await Context.Channel.SendMessageAsync("The time you requested is more then the time allowed. Your time has been adjusted to the max time of 1 hour.");
                timeWanted = maxTime;
            }
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny);
            var voiceChannel = await Context.Guild.CreateVoiceChannelAsync($"{Context.User.Username}");
            guildAccount.NumberOfTempChannels += 1;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), allow);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, deny);
            await Context.Channel.SendMessageAsync($"{Context.User.Username} now has a voice channel for {timeWanted}");
            await Task.Delay(maxTime);
            await voiceChannel.DeleteAsync();
            await Context.Channel.SendMessageAsync($"Voice Channel {Context.User.Username} has been deleted.");
            guildAccount.NumberOfTempChannels -= 1;
            Guild_Accounts.GuildAccounts.SaveAccounts();

        }
    }
}
