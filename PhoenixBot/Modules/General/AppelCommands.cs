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
        [Command("Mute")]
        [Summary("Puts in an appeal about a mute you are currently under.")]
        public async Task MuteAppeal([Remainder] string msg)
        {
            DataAccess Db = new DataAccess();
            UserAccountModel user = new UserAccountModel();

            user = Db.GetUser(Context.User.Id);
            var logChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.requestID);
            //var msg = Context.Message.Content.Remove(0, 13);
            if (user.IsMuted == 1)
            {
                var embed = new EmbedBuilder();
                embed.WithTitle($"Mute Appeal by {Context.User.Username}")
                    .WithDescription(msg)
                    .WithColor(0, 0, 150);
                await logChannel.SendMessageAsync("", false, embed.Build());
                return;
            }


        }
        [Command("Warning")]
        [Summary("Puts in an appeal about a warning you got. Please give enough info to argue your case.")]
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
        [Summary("Puts in an appeal about a denied request. Repeated use after being denied a 2nd time will resault in a warning or mute.")]
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
