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
        [Command("summon", RunMode = RunMode.Async)]
        async Task AdminJoinVoice()
        {
            var selfBot = Global.Client.CurrentUser as SocketSelfUser;
            Console.WriteLine(selfBot.Mention.ToString());
            var user = Context.User as IGuildUser;
            var voiceChannel = user.VoiceChannel;
            if (voiceChannel == null) return;
            await voiceChannel.ConnectAsync();
            Console.WriteLine($"The bot has Join {voiceChannel.Name}");
        }
        [Command("dispel")]
        async Task AdminLeaveVoice()
        {
            var user = Context.Guild.GetUser(Config.bot.botID);
            var voiceChannel = user.VoiceChannel;
            if (voiceChannel == null) return;
            await voiceChannel.DisconnectAsync();
            await ReplyAsync($" n ");
            Console.WriteLine($"The bot has Left {voiceChannel.Name}");
        }

    }
}
