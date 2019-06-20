using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using PhoenixBot.Features.Games.UserAccounts;
using PhoenixBot.Features.Games.Hunt;

namespace PhoenixBot.Modules.Game.Commands
{
    [Group("hunt")]
    public class Hunting : ModuleBase<SocketCommandContext>
    {
        private const string RoleWarning = "ERROR 404: You do not have the role to play this game.";
        Random interactionChance = new Random();
        [Command("join")]
        public async Task JoinHunt()
        {
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User))
            {
                await ReplyAsync(RoleWarning);
                return;
            }
            Console.WriteLine("Join command recived " + Context.User.Username + " has used the Hunt Join command.");
            GameUserAccounts.GetAccount(Context.User.Id);
            GameUserAccounts.SaveAccounts();
            await ReplyAsync($"{Context.User.Username} has been added to the hunt! Enjoy the hunt!");
        }
        [Command("quit")]
        public async Task LeaveHunt()
        {
            var user = Context.User as SocketGuildUser;
            var userAccount = GameUserAccounts.GetAccount(user.Id);
            GameUserAccounts.accounts.Remove(userAccount);
            GameUserAccounts.SaveAccounts();
            await ReplyAsync("You have left the hunt game.");
        }
        [Command("search", RunMode = RunMode.Async)]
        [Cooldown(20, adminsAreLimited: true)]
        [Summary("Member channel command. Used to see what you can find in a hunt.")]
        public async Task HuntCommand()
        {
            Console.WriteLine("Searching for prey." + Context.User.Username + " The Hunt Search command was used.");
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User))
            {
                await ReplyAsync(RoleWarning);
                return;
            }
            var user = GameUserAccounts.GetAccount(Context.User.Id);
            var interaction = interactionChance.Next(1, 3);
            if (interaction == 3)
            {
                await Context.Channel.SendMessageAsync($"{Context.User} failed to find anything.");
                return;
            }
            MonsterInfo huntingResult;
            Random xpGained = new Random();
            float xp = (float)xpGained.Next(1, 20) * 0.9251f;


            var embed = new EmbedBuilder();


            if (interaction == 1 || interaction == 2)
            {
                string monster = "null", mHP = "null", pHP = "null";
                if (user.HuntingLevel.ToString() == "Noob")
                {
                    Console.WriteLine("In Noob if statment");
                    huntingResult = HuntLogic.HuntingLogic(user, 0);
                    monster = huntingResult.MonsterName;
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    embed.WithTitle("Hunting Results")
                        .AddField("Monster:", monster)
                        .AddField("Monster HP:", mHP)
                        .AddField("Player HP:", pHP)
                        .AddField("XP gained:", xp);
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    await Context.Channel.SendMessageAsync(Context.User.ToString(), false, embed.Build());
                    user.PlayerHp = user.PlayerMaxHp;
                    user.HuntingXP += xp;
                    user.AttributeXP += xp;
                    GameUserAccounts.SaveAccounts();
                }
                else if (user.HuntingLevel.ToString() == "Novice")
                {
                    huntingResult = HuntLogic.HuntingLogic(user, 1);
                    monster = huntingResult.MonsterName;
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    embed.WithTitle("Hunting Results")
                        .AddField("Monster:", monster)
                        .AddField("Monster HP:", mHP)
                        .AddField("Player HP:", pHP)
                        .AddField("XP gained:", xp);
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    await Context.Channel.SendMessageAsync(Context.User.ToString(), false, embed.Build());
                    user.PlayerHp = user.PlayerMaxHp;
                    user.HuntingXP += xp;
                    user.AttributeXP += xp;
                    GameUserAccounts.SaveAccounts();
                }
                else if (user.HuntingLevel.ToString() == "Intermediate")
                {
                    huntingResult = HuntLogic.HuntingLogic(user, 2);
                    monster = huntingResult.MonsterName;
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    embed.WithTitle("Hunting Results")
                        .AddField("Monster:", monster)
                        .AddField("Monster HP:", mHP)
                        .AddField("Player HP:", pHP)
                        .AddField("XP gained:", xp);
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    await Context.Channel.SendMessageAsync(Context.User.ToString(), false, embed.Build());
                    user.PlayerHp = user.PlayerMaxHp;
                    user.HuntingXP += xp;
                    user.AttributeXP += xp;
                    GameUserAccounts.SaveAccounts();
                }
                else if (user.HuntingLevel.ToString() == "Master")
                {
                    huntingResult = HuntLogic.HuntingLogic(user, 3);
                    monster = huntingResult.MonsterName;
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    embed.WithTitle("Hunting Results")
                        .AddField("Monster:", monster)
                        .AddField("Monster HP:", mHP)
                        .AddField("Player HP:", pHP)
                        .AddField("XP gained:", xp);
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    await Context.Channel.SendMessageAsync(Context.User.ToString(), false, embed.Build());
                    user.PlayerHp = user.PlayerMaxHp;
                    user.HuntingXP += xp;
                    user.AttributeXP += xp;
                    GameUserAccounts.SaveAccounts();
                }
                else if (user.HuntingLevel.ToString() == "GrandMaster")
                {
                    huntingResult = HuntLogic.HuntingLogic(user, 4);
                    monster = huntingResult.MonsterName;
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    embed.WithTitle("Hunting Results")
                        .AddField("Monster:", monster)
                        .AddField("Monster HP:", mHP)
                        .AddField("Player HP:", pHP)
                        .AddField("XP gained:", xp);
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    await Context.Channel.SendMessageAsync(Context.User.ToString(), false, embed.Build());
                    user.PlayerHp = user.PlayerMaxHp;
                    user.HuntingXP += xp;
                    user.AttributeXP += xp;
                    GameUserAccounts.SaveAccounts();
                }
                else if (user.HuntingLevel.ToString() == "Phoenix")
                {
                    huntingResult = HuntLogic.HuntingLogic(user, 5);
                    monster = huntingResult.MonsterName;
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    embed.WithTitle("Hunting Results")
                        .AddField("Monster:", monster)
                        .AddField("Monster HP:", mHP)
                        .AddField("Player HP:", pHP)
                        .AddField("XP gained:", xp);
                    mHP = huntingResult.MonsterHP.ToString();
                    pHP = user.PlayerHp.ToString();
                    await Context.Channel.SendMessageAsync(Context.User.ToString(), false, embed.Build());
                    user.PlayerHp = user.PlayerMaxHp;
                    user.HuntingXP += xp;
                    user.AttributeXP += xp;
                    GameUserAccounts.SaveAccounts();
                }
                else
                {
                    // Failed hunt (precaution for future updates)
                    await Context.Channel.SendMessageAsync("You found nothing");
                }

            }
        }
    }
}
    /*  
        Noob = 0
        Novice = 1
        Intermediate = 2
        Master = 3
        GrandMaster = 4
        Phoenix = 5
    */
