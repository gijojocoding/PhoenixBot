using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Modules;

namespace PhoenixBot.Economy
{
    public class EconomyAdmin : ModuleBase<SocketCommandContext>
    {
        [Command("giveFeathers")]
        [Remarks("Gives the target the amount of Feathers.")]
        [RequireOwner]
        public async Task GivePoints(SocketGuildUser user, uint amount)
        {
            if (!RoleCheck.HasChiefRole((SocketGuildUser)user))
            {
                await ReplyAsync("404 You do not have permission to use this command.");
                return;
            }
            var target = user.Mention;
            var targetAccount = User_Accounts.UserAccounts.GetAccount(user);
            targetAccount.Points += amount;
            User_Accounts.UserAccounts.SaveAccounts();
            await ReplyAsync($"{target} now has {targetAccount.Points} ");
        }
        [Command("setFeathers")]
        [Remarks("Sets the target's Feather amount.")]
        [RequireOwner]
        public async Task setPoints(SocketGuildUser user, uint amount)
        {
            if (!RoleCheck.HasChiefRole((SocketGuildUser)user))
            {
                await ReplyAsync("404 You do not have permission to use this command.");
                return;
            }
            var target = user.Mention;
            var targetAccount = User_Accounts.UserAccounts.GetAccount(user);
            targetAccount.Points = amount;
            User_Accounts.UserAccounts.SaveAccounts();
            await ReplyAsync($"{target} now has {targetAccount.Points} ");
        }
        [Command("SetfeathersForHealthIncrease")]
        [Remarks("Sets the amount of Feathers to increase their Max Health.")]
        [RequireOwner]
        public async Task SetPointsForHealthIncrease(uint points)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.PointsForHealthIncrease = points;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Set to: {guild.PointsForHealthIncrease}");
        }
        [Command("SetFeathersForHealth")]
        [Remarks("Sets the amount of Feathers for a small heal.")]
        [RequireOwner]
        public async Task SetPointsForHealth(uint points)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.PointsForHealth = points;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Set to: {guild.PointsForHealth}");
        }
        [Command("SetFeathersForRevive")]
        [Remarks("Sets the amount of Feathers to revive the player.")]
        [RequireOwner]
        public async Task SetPointsForRevive(uint points)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.PointsForRevive = points;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Set to: {guild.PointsForRevive}");
        }
        [Command("SetMaxHealthIncreaseAmount")]
        [RequireOwner]
        public async Task SetMaxHealthIncreaseAmount(uint amount)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.HealthIncrease = amount;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Set to: {guild.HealthIncrease}");
        }
        [Command("SetHealthAmount")]
        [RequireOwner]
        public async Task SetHealthAmount(uint amount)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.Health = amount;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Set to: {guild.Health}");
        }
    }
}
