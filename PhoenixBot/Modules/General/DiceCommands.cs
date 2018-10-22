using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Features;

namespace PhoenixBot.Modules.General
{
    public class DiceCommand : ModuleBase<SocketCommandContext>
    {
        private const int GoalNumber = 7;
        [Command("Roll")]
        [Summary("Rolls the dice.")]
        public async Task RollDiceGame()
        {
            int TossOne = DiceGame.Roll();
            int TossTwo = DiceGame.Roll();
            int Total = TossOne + TossTwo;
            var gameChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.miniGameID);
            var embed = new EmbedBuilder();
            if (Global.PlayerOneRoll == 0 || Global.PlayerTwoRoll == 0)
            {
                if (Global.PlayerOneRoll == 0)
                {
                    embed.WithTitle($"Dice have been Rolled!")
                        .AddField("The dice are:", $"Dice one: {TossOne}! Dice two: {TossTwo}!")
                        .AddField($"{Context.User} has rolled:", Total);
                    await gameChannel.SendMessageAsync("", false, embed.Build());
                    Global.PlayerOneId = Context.User as SocketGuildUser;
                    Global.PlayerOneRoll = Total;

                }
                else if (Global.PlayerTwoRoll == 0)
                {
                    embed.WithTitle($"Dice have been Rolled!")
                        .AddField("The dice are:", $"Dice one: {TossOne}! Dice two: {TossTwo}!")
                        .AddField($"{Context.User} has rolled:", Total);
                    await gameChannel.SendMessageAsync("", false, embed.Build());
                    Global.PlayerTwoId = Context.User as SocketGuildUser;
                    Global.PlayerTwoRoll = Total;
                }
            }
            if (Global.PlayerOneRoll != 0 && Global.PlayerTwoRoll != 0)
            {
                await DiceGameFinish();
            }
        }
        private async Task DiceGameFinish()
        {
            var gameChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.miniGameID);
            int PlayerOneComp = Math.Abs(Global.PlayerOneRoll - GoalNumber);
            var PlayerOne = Global.PlayerOneId;
            int PlayerTwoComp = Math.Abs(Global.PlayerTwoRoll - GoalNumber);
            var PlayerTwo = Global.PlayerTwoId;
            var embed = new EmbedBuilder();
            embed.WithTitle("Dice Game has been finished!");
            if (PlayerOneComp > PlayerTwoComp)
            {
                embed.WithDescription($"{PlayerOne.Mention} has WON the game with {PlayerOneComp}! With {PlayerTwo.Mention} getting {PlayerTwoComp}.");
            }
            else if (PlayerOneComp < PlayerTwoComp)
            {
                embed.WithDescription($"{PlayerTwo.Mention} has WON the game with {PlayerTwoComp}! With {PlayerOne.Mention} getting {PlayerOneComp}.");
            }
            else if (PlayerOneComp == PlayerTwoComp)
            {
                embed.WithDescription("It was TIE!")
                    .AddField($"{PlayerOne} had:", PlayerOneComp)
                    .AddField($"{PlayerTwo} had:", PlayerTwoComp);
            }
            await gameChannel.SendMessageAsync("", false, embed.Build());
            return;
        }
    }
}
