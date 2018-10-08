using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace PhoenixBot.Modules.General
{
    public class TempChannelCommands : ModuleBase<SocketCommandContext>
    {
        private const int maxTime = 60000;
        [Cooldown(60, adminsAreLimited: true)]
        [Command("CreateVoice", RunMode = RunMode.Async)]
        [Summary("Creates a Voice Channel for a set time.")]
        public async Task CreateVoiceChannel(int time)
        {
            var timeWanted = time * 1000;
            if (timeWanted > maxTime)
            {
                await Context.Channel.SendMessageAsync("The time you requested is more then the time allowed. Your time has been adjusted to the max time.");
                timeWanted = maxTime;
            }
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow);
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny);
            var voiceChannel = await Context.Guild.CreateVoiceChannelAsync($"{Context.User.Username}");
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(Config.bot.memberID), allow);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, deny);
            await Context.Channel.SendMessageAsync($"{Context.User.Username} now has a voice channel for {timeWanted}");
            await Task.Delay(maxTime);
            await voiceChannel.DeleteAsync();
            await Context.Channel.SendMessageAsync($"Voice Channel {Context.User.Username} has been deleted.");
        }
    }
}
