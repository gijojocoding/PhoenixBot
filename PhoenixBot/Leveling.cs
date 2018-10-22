using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using PhoenixBot.User_Accounts;
using Discord;

namespace PhoenixBot.LevelingSystem
{
    internal static class Leveling
    {
        internal static void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {
            var userAccount = UserAccounts.GetAccount(user);
            var currentTime = DateTime.Now;
            var timeOut = userAccount.LastMessage - currentTime;
            if (timeOut.Minutes <= 5) return;
            uint oldLevel = userAccount.LevelNumber;
            userAccount.XP += 10;
            UserAccounts.SaveAccounts();
            if (oldLevel != userAccount.LevelNumber)
            {
                var pointsAdded = (userAccount.LevelNumber * 20);
                userAccount.Points += pointsAdded;
                UserAccounts.SaveAccounts();
                //User Leveled Up
                var embed = new EmbedBuilder();
                embed.WithTitle("Level Up")
                    .WithDescription($"{user.Mention} **JUST LEVELED UP! THEY ARE NOW {userAccount.LevelNumber}!** They got {pointsAdded} points!");
                channel.SendMessageAsync("", false, embed.Build());
            }
        }
    }
}
