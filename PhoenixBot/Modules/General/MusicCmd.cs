using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using PhoenixBot.Modules.Music;

namespace PhoenixBot.Modules.General
{
    [RequireOwner]
    public class MusicCmd : ModuleBase<SocketCommandContext>
    {
        AudioService AudioService { get; set; }

        [Command("blank", RunMode = RunMode.Async)]
        public async Task PlayMusic([Remainder] string query)
        {
            var bot = Context.Guild.GetUser(Config.bot.botID);
            var voiceChannel = bot.VoiceChannel;
            await AudioService.ConnectAndPlayAsync(voiceChannel, query);
        }
    }
}
