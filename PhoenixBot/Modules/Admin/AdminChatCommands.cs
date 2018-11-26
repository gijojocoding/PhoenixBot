using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.User_Accounts;

namespace PhoenixBot.Modules.Admin
{
    [Group("Admin")]
    [Summary("Admin commands for chat control.")]
    [RequireUserPermission(GuildPermission.Administrator)]
    public class AdminChatCommands : ModuleBase<SocketCommandContext>
    {
        [Command("mute")]
        public async Task AdminMuteCmd(SocketGuildUser target, string reason = "You have actively  violated several rules to the point Admin staff ")
        {

        } 
    }

}
