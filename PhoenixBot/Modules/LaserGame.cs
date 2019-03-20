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
        [Command("Laser", RunMode = RunMode.Async)]
        [Summary("Shoots lasers into space.")]
        [Cooldown(10)]
        public async Task Laserz()
        {
            if (Context.Channel.Id != ChannelIds.channels.miniGameID) return;
            Random hitChance = new Random();
            var hit = hitChance.Next(1, 3);
            if (hit == 1)
            {
                //Missed shots
                await Context.Channel.SendMessageAsync("PEW PEW");
                await Context.Channel.SendMessageAsync("PEW PEW PEW PEW PEW");
                await Context.Channel.SendMessageAsync($"{Context.User} shot their lasers hitting nothing! They need to work on their aim!");
            }
            else if (hit == 2)
            {
                //Shots hit bot doing nothing.
                await Context.Channel.SendMessageAsync("PEW PEW PEW");
                await Context.Channel.SendMessageAsync("PEW PEW PEW PEW PEW");
                await Task.Delay(50);
                await Context.Channel.SendMessageAsync("DING");
                await Context.Channel.SendMessageAsync($"{Context.User} shot their lasers hitting the ancient bot! But they did no damage because of their poor aim!");
            }
            else if (hit == 3)
            {
                //Shots hit bot angering the bot.
                await Context.Channel.SendMessageAsync("PEW PEW");
                await Context.Channel.SendMessageAsync("DING DING");
                await Task.Delay(50);
                await Context.Channel.SendMessageAsync("PEW PEW PEW PEW PEW DING");
                await Context.Channel.SendMessageAsync("DING DING DING");
                await Context.Channel.SendMessageAsync($"{Context.User} shot their lasers hitting the ancient bot, angering the bot! ");
                await Task.Delay(50);
                await Context.Channel.SendMessageAsync($"The ancient bot charged at {Context.User} but lost its balance.");
            }
        }
    }
}
