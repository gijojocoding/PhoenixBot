using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Admin
{
    [RequireUserPermission(GuildPermission.Administrator)]
    public class VoiceControl : ModuleBase<SocketCommandContext>
    {
        [Command("VJoin", RunMode = RunMode.Async)]
        async Task AdminJoinVoice()
        {
            var user = Context.User as IGuildUser;
            var voiceChannel = user.VoiceChannel;
            if (voiceChannel == null) return;
            await voiceChannel.ConnectAsync();
            Console.WriteLine($"The bot has Join {voiceChannel.Name}");
        }
        [Command("VLeave")]
        async Task AdminLeaveVoice()
        {
            var user = Context.Guild.GetUser(Config.bot.botID);
            var voiceChannel = user.VoiceChannel;
            if (voiceChannel == null) return;
            await voiceChannel.DisconnectAsync();
            Console.WriteLine($"The bot has Left {voiceChannel.Name}");
        }
    }
}
