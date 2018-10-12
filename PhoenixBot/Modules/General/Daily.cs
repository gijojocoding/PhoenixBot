using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.User_Accounts;

namespace PhoenixBot.Modules.General
{
    
    [Group("Daily")]
    [Cooldown(15)]
    public class Daily : ModuleBase<SocketCommandContext>
    {
        private const int feathers = 200;
        [Command("Claim")]
        [Summary("Used to collect your daily points.")]
        public async Task DailyClaim()
        {
            var user = Context.User;
            var account = UserAccounts.GetAccount(user);
            var currentTime = DateTime.Now;
            var difference = currentTime - account.dailyClaim;
            if(difference.Days >= 1)
            {
                account.Points += feathers;
                UserAccounts.SaveAccounts();
                await ReplyAsync($"You have claimed your 200 daily feathers! You now have {account.Points}!");
            }
            else
            {
                await ReplyAsync($"You have {difference.Hours} hours and {difference.Minutes} minutes!");
            }
        }
    }
}
