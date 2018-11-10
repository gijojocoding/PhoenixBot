using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace PhoenixBot.Modules.General
{

    [Group("Appeal")]
    public class AppelCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Warning")]
        [Summary("Puts in an appeal about a warning you got. Please give enough info to argue your case, like when you recieved the warning and the reason you were given. Abuse of this command will result in a mute.")]
        public async Task WarningAppeal([Remainder] string message)
        {
            if (Context.IsPrivate == true) return;
            await Context.Message.DeleteAsync();
            var logChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.requestID);
            var embed = new EmbedBuilder();
            embed.WithTitle("Mute appeal")
                .WithDescription(message)
                .WithColor(0, 150, 0);
            await logChannel.SendMessageAsync("", false, embed.Build());
        }
        [Command("DeniedRequest")]
        [Summary("Puts in an appeal about a denied request. Please explain what you had requested, the start/end times (based upon request), topic (if a meeting/debate request), and the reason given. Abuse of this command will result in a warning and/or mute.")]
        public async Task RequestAppeal([Remainder] string message)
        {
            if (Context.IsPrivate == true) return;
            await Context.Message.DeleteAsync();
            var logChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.requestID);
            var embed = new EmbedBuilder();
            embed.WithTitle("Mute appeal")
                .WithDescription(message)
                .WithColor(150, 0, 0);
            await logChannel.SendMessageAsync("", false, embed.Build());


        }
    }
}
