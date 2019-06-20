using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord.WebSocket;
using PhoenixBot.Features.Games.UserAccounts;
using PhoenixBot.Features.Games.Hunt;
using System.Threading.Tasks;
using Discord;

namespace PhoenixBot.Modules.Game.Commands
{
    [Group("Training")]
    [Cooldown(50, adminsAreLimited: true)]
    public class GameTraining : ModuleBase<SocketCommandContext>
    {
        /*
        Noob = 0,
        Novice = 1,
        Intermediate = 2,
        Master = 3,
        GrandMaster = 4,
        Phoenix = 5
        */
        [Command("List")]
        public async Task AttributeList()
        {
            var AttributeEmbed = new EmbedBuilder();
            AttributeEmbed.WithTitle("List of Attribute:")
                .WithDescription("None \n Demon \n Angel \n Life \n Nullifer");
            await Context.Channel.SendMessageAsync("", false, AttributeEmbed.Build());
        }
        [Command("Hunting")]
        async Task HuntingTrainingCMD()
        {
            var userId = Context.User.Id;
            var gameAccount = GameUserAccounts.GetAccount(userId);
            var huntingLvlByte = Convert.ToByte(gameAccount.HuntingLevel);
            var xpNeeded = huntingLvlByte * 1000;
            if (gameAccount.HuntingXP < xpNeeded)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Username}: You do not have enough Hunting XP. \n You need: {xpNeeded - gameAccount.HuntingXP}");
                return;
            }
            Task.Delay(500);
            if (huntingLvlByte == 0)
            {
                gameAccount.HuntingLevel = HuntingLevel.Novice;
                gameAccount.HuntingXP -= xpNeeded;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{Context.User.Username}, your hunting level is now {gameAccount.HuntingLevel}!");
            }
            else if (huntingLvlByte == 1)
            {
                gameAccount.HuntingLevel = HuntingLevel.Intermediate;
                gameAccount.HuntingXP -= xpNeeded;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{Context.User.Username}, your hunting level is now {gameAccount.HuntingLevel}!");
            }
            else if (huntingLvlByte == 2)
            {
                gameAccount.HuntingLevel = HuntingLevel.Master;
                gameAccount.HuntingXP -= xpNeeded;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{Context.User.Username}, your hunting level is now {gameAccount.HuntingLevel}!");
            }
            else if (huntingLvlByte == 3)
            {
                gameAccount.HuntingLevel = HuntingLevel.GrandMaster;
                gameAccount.HuntingXP -= xpNeeded;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{Context.User.Username}, your hunting level is now {gameAccount.HuntingLevel}!");
            }
            else if (huntingLvlByte == 4)
            {
                gameAccount.HuntingLevel = HuntingLevel.Phoenix;
                gameAccount.HuntingXP -= xpNeeded;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{Context.User.Username}, your hunting level is now {gameAccount.HuntingLevel}!");
            }

        }
        [Command("Attribute", RunMode = RunMode.Async), Alias("Att")]
        public async Task AttributeTrainingCMD(string att)
        {
            var userId = Context.User.Id;
            var gameAccount = GameUserAccounts.GetAccount(userId);
            if (gameAccount.AttributeXP <   1000)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Username}: You do not have enough Attribute XP. \n You need: {1000 - gameAccount.AttributeXP}");
                return;
            }
            att = att.ToLower();
            Task.Delay(5000);
            if (att == "demon")
            {
                gameAccount.Attribute = Features.Games.UserAccounts.Attribute.Demon;
                gameAccount.AttributeXP -= 1000;
                GameUserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"{Context.User}: You have finished the training and now have the Attribute of {gameAccount.AttributeXP.ToString()}");
            }
            else if (att == "angel")
            {
                gameAccount.Attribute = Features.Games.UserAccounts.Attribute.Angel;
                gameAccount.AttributeXP -= 1000;
                GameUserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"{Context.User}: You have finished the training and now have the Attribute of {gameAccount.AttributeXP.ToString()}");
            }
            else if (att == "life")
            {
                gameAccount.Attribute = Features.Games.UserAccounts.Attribute.Life;
                gameAccount.AttributeXP -= 1000;
                GameUserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"{Context.User}: You have finished the training and now have the Attribute of {gameAccount.AttributeXP.ToString()}");
            }
            else if (att == "nullifer")
            {
                gameAccount.Attribute = Features.Games.UserAccounts.Attribute.None;
                gameAccount.AttributeXP -= 1000;
                GameUserAccounts.SaveAccounts();
                await Context.Channel.SendMessageAsync($"{Context.User}: You have finished the training and now have the Attribute of {gameAccount.AttributeXP.ToString()}");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Please make sure you typed in the correct Attribute name.");
                return;
            }
        }
    }
}
