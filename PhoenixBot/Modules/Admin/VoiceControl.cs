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
            var selfBot = Global.Client.GetUser(Config.bot.botID);
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
            var selfbot = Context.Guild.GetUser(Config.bot.botID) as SocketGuildUser;
            var user = Context.User as SocketGuildUser;
            var voiceChannel = user.VoiceChannel;
            if (voiceChannel == null) return;
            await selfbot.VoiceChannel.DisconnectAsync();
            await ReplyAsync($" n ");
            Console.WriteLine($"The bot has Left {voiceChannel.Name}");
        }

    }
}
