using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using PhoenixBot.User_Accounts;
using PhoenixBot.Guild_Accounts;

namespace PhoenixBot.Economy
{
    public class PointShop : ModuleBase<SocketCommandContext>
    {
        [Command("BuyHeathincrease")]
        [Cooldown(500)]
        public async Task BuyHealthIncrease(uint power)
        {
            var user = Context.User as SocketGuildUser;
            var userAccount = UserAccounts.GetAccount(user);
            var guildAccount = GuildAccounts.GetAccount(Context.Guild);
            if (userAccount.Points >= (guildAccount.PointsForHealthIncrease * power))
            {
                var PointsUsed = (power * guildAccount.PointsForHealthIncrease);
                var IncreaseAmount = (power * guildAccount.HealthIncrease);
                userAccount.Points -= PointsUsed;
                UserAccounts.SaveAccounts();
                userAccount.MaxHp += IncreaseAmount;
                UserAccounts.SaveAccounts();
                await ReplyAsync($"{user.Mention} now has the Max Health of {userAccount.MaxHp}!");
                return;
            }
            else
            {
                await ReplyAsync("Sorry you do not have enough Feathers for this purchace!");
            }
        }
        [Command("BuyHealth")]
        [Cooldown(500)]
        public async Task BuyHealth(uint power)
        {
            var user = Context.User as SocketGuildUser;
            var userAccount = UserAccounts.GetAccount(user);
            var guildAccount = GuildAccounts.GetAccount(Context.Guild);
            if (userAccount.Points >= (guildAccount.PointsForHealth * power))
            {
                var UPower = power;
                var PointsUsed = (power * guildAccount.PointsForHealth);
                var IncreaseAmount = (power * guildAccount.Health);
                userAccount.Points -= PointsUsed;
                UserAccounts.SaveAccounts();
                userAccount.HP += IncreaseAmount;
                UserAccounts.SaveAccounts();
                if (userAccount.HP > userAccount.MaxHp)
                {
                    userAccount.HP = userAccount.MaxHp;
                }
                await ReplyAsync($"{user.Mention} has been healed. Their health is now {userAccount.HP}!");
                return;
            }
            else
            {
                await ReplyAsync("Sorry you do not have enough Feathers for this purchace!");
            }
        }
        [Command("BuyRevive")]
        [Cooldown(500)]
        public async Task BuyRevive()
        {
            var user = Context.User as SocketGuildUser;
            var userAccount = UserAccounts.GetAccount(user);
            var guildAccount = GuildAccounts.GetAccount(Context.Guild);
            if (userAccount.Points >= (guildAccount.PointsForRevive))
            {
                var PointsUsed = guildAccount.PointsForRevive;
                userAccount.Points -= PointsUsed;
                UserAccounts.SaveAccounts();
                userAccount.IsAlive = true;
                UserAccounts.SaveAccounts();
                userAccount.HP = userAccount.MaxHp;
                UserAccounts.SaveAccounts();
                await ReplyAsync($"{user.Mention} has been revive!");
                return;
            }
            else
            {
                await ReplyAsync("Sorry you do not have enough Feathers for this purchace!");
            }
        }
    }
}
