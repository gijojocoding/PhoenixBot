using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;

namespace PhoenixBot.Modules.General
{

    public class MusicCmd : ModuleBase<SocketCommandContext>
    {
        AudioService audioService;

        [Command("Play", RunMode = RunMode.Async)]
        public async Task PlayMusic([Remainder] string query)
        {
            var bot = Context.Guild.GetUser(Config.bot.botID);
            var voiceChannel = bot.VoiceChannel;
            await audioService.ConnectAndPlayAsync(voiceChannel, query);
        }
    }
}
