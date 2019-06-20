using System;
using System.Collections.Generic;
using System.Text;
using PhoenixBot.User_Accounts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace PhoenixBot.Modules.Admin
{
    [RequireOwner]
    public class UserInfo : ModuleBase<SocketCommandContext>
    {
        [Command("usersinfo")]
        async Task UsersInfo(params IGuildUser[] users)
        {
            if (users == null)
            {
                return;
            }

            foreach (var user_ in users)
            {
                var user = user_ as SocketGuildUser;
                var account = UserAccounts.GetAccount(user);
                await Context.Channel.SendMessageAsync($"{user.Username} has: \n{account.NumberOfWarnings} warnings. \n{account.IsMuted} is not Muted.");
                Task.Delay(100);
            }
        }
    }
}
