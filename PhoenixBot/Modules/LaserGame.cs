using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules
{
    public class LaserGame : ModuleBase<SocketCommandContext>
    {
        [Command("Laserz")]
        [Summary("Shoots lasers into space.")]
        [Cooldown(10)]
        public async Task Laserz()
        {
            if (Context.Channel.Id != ChannelIds.channels.miniGameID) return;
            await Context.Channel.SendMessageAsync($"PEW PEW");
            await Context.Channel.SendMessageAsync($"PEW PEW PEW PEW PEW");
            await Context.Channel.SendMessageAsync($"{Context.User} shot the laserz hitting nothing! They need to work on their aim!");
        }
        [Command("Laser", RunMode = RunMode.Async)]
        [Summary("Shoots lasers at a tagged person.")]
        [Cooldown(15)]
        public async Task LaserTarget(IGuildUser user)
        {
            if (Context.Channel.Id != ChannelIds.channels.miniGameID) return;
            var account = User_Accounts.UserAccounts.GetAccount(Context.User);
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser;
            var targetAccount = User_Accounts.UserAccounts.GetAccount(target);
            if (account.IsAlive == false)
            {
                await Context.Channel.SendMessageAsync("You are dead and can not attack!");
                return;
            }
            if (target.IsBot == true && account.IsAlive == true)
            {

                account.HP = 0;
                User_Accounts.UserAccounts.SaveAccounts();
                account.IsAlive = false;
                User_Accounts.UserAccounts.SaveAccounts();
                await Context.Channel.SendFileAsync($"{Context.User} attacked the bot and lost!");
                return;
            }
            if (account.IsAlive == true)
            {

                Random attackRandom = new Random();
                int AttackAmount = attackRandom.Next(0, 50);
                uint damageAmount = (uint)AttackAmount;
                if (targetAccount.IsAlive == true)
                {
                    await Context.Channel.SendMessageAsync($"PEW PEW");
                    await Context.Channel.SendMessageAsync($"PEW PEW PEW PEW PEW");
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} shot at {target.Mention} hitting them for {AttackAmount}!");
                    targetAccount.HP -= damageAmount;
                    User_Accounts.UserAccounts.SaveAccounts();
                    if (targetAccount.HP <= 0)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User} killed {target.Mention}!");
                        targetAccount.IsAlive = false;
                        User_Accounts.UserAccounts.SaveAccounts();
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{target.Mention} is already dead!");
                }
            }
        }
        [Command("Revive")]
        [Summary("Revives the tagged person. **Can not be used on yourself!**")]
        [Cooldown(10)]
        public async Task RevivePlayer(IGuildUser user)
        {
            if (Context.Channel.Id != ChannelIds.channels.miniGameID) return;
            var account = User_Accounts.UserAccounts.GetAccount(Context.User);
            SocketUser target = null;
            var mentionedUser = Context.Message.MentionedUsers.FirstOrDefault();
            target = mentionedUser;
            var targetAccount = User_Accounts.UserAccounts.GetAccount(target);
            if (account.IsAlive == false)
            {
                await Context.Channel.SendMessageAsync("You are dead and can't revive anyone one!");
                return;
            }
            if (account.IsAlive == true)
            {
                if (targetAccount.IsAlive == false)
                {
                    targetAccount.IsAlive = true;
                    targetAccount.HP = 100;
                    User_Accounts.UserAccounts.SaveAccounts();
                    await Context.Channel.SendMessageAsync($"{Context.User} revived {target.Mention}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("You couldn't revive them as they are still alive!");
                }
            }


        }
        [Command("adminrevive")]
        [Summary("Used to revive the tagged person.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AdminRevive(IGuildUser user)
        {
            var target = Context.Message.MentionedUsers.FirstOrDefault();
            if (target == null) return;
            var targetAccount = User_Accounts.UserAccounts.GetAccount(target);
            targetAccount.IsAlive = true;
            User_Accounts.UserAccounts.SaveAccounts();
            targetAccount.HP = 100;
            User_Accounts.UserAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"{target} is now alive with full hp!");
        }
        [Command("heal")]
        [Summary("Used to heal a tagged person.")]
        [Cooldown(10)]
        public async Task SelfHeal(SocketGuildUser user)
        {
            if (Context.Channel.Id != ChannelIds.channels.miniGameID) return;
            var cmdUser = Context.User as SocketGuildUser;
            SocketGuildUser target = user;
            if (cmdUser == target)
            {
                await Context.Channel.SendMessageAsync("You can't heal yourself!");
                return;
            }
            var targetAccount = User_Accounts.UserAccounts.GetAccount(user);
            if (targetAccount.HP == 100 || targetAccount.IsAlive == false)
            {
                await Context.Channel.SendMessageAsync($"Unable to heal {target}");
                return;
            }
            targetAccount.HP += 10;
            User_Accounts.UserAccounts.SaveAccounts();
            if (targetAccount.HP >= 101)
            {
                targetAccount.HP = 100;
                User_Accounts.UserAccounts.SaveAccounts();
            }
            await Context.Channel.SendMessageAsync($"{target} now has {targetAccount.HP} HP");
        }
    }
}
