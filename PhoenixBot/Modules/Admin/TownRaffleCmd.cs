using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using PhoenixBot.Features.Raffles.Town;


namespace PhoenixBot.Modules.Admin
{
    [Group("TownRaffle")]
    public class TownRaffleCmd : ModuleBase<SocketCommandContext>
    {
        [Command("SetInfo")]
        public async Task SetGuildRaffleInfo(string info)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.TownRaffleInfo = info;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild Raffle Info: {guild.TownRaffleInfo}");
        }
        [Command("Open")]
        public async Task OpenGuildRaffle()
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.TownRaffleRunning = true;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild Raffle is open? {guild.TownRaffleRunning}");
        }
        [Command("Close")]
        public async Task CloseGuildRaffle()
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.GuildRaffleRunning = false;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild Raffle is open? {guild.TownRaffleRunning}");
        }
        [Command("GetInfo")]
        public async Task GuildRaffleInfo()
        {
            var list = TownRaffles.townRaffle;
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            var count = list.Count;
            var embed = new EmbedBuilder();
            embed.WithTitle("Guild Raffle Info")
                .AddField("Number of tickets Sold:", count)
                .AddField("Info:", guild.TownRaffleInfo);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("Add")]
        [Summary("Admin command to add a raffle ")]
        public async Task AddTicket(SocketGuildUser holder, int ticketNumber)
        {
            var NumberHeld = 0;
            if (NumberHeld == 0)
            {
                foreach (var Ticket in TownRaffles.townRaffle)
                {
                    if (Ticket.TicketNumber == ticketNumber)
                    {
                        NumberHeld += 1;
                    }
                }
            }
            if (NumberHeld <= 1)
            {
                await ReplyAsync($"The ticket number {ticketNumber} is already taken.");
                return;
            }
            var Holder = holder.Id;
            var ticket = new TownRaffle();
            ticket.HolderID = Holder;
            ticket.TicketNumber = ticketNumber;
            TownRaffles.townRaffle.Add(ticket);
            TownRaffles.SaveTownRaffle();
            await ReplyAsync($"{holder} now has the ticket number: {ticketNumber}");
        }
        [Command("Run")]
        public async Task RunGuildRaffle()
        {
            var max = TownRaffles.townRaffle.Count;
            Random Raffle = new Random();
            var WinningNumber = Raffle.Next(1, max);
            var WinningTicket = TownRaffles.GetTownRaffle(WinningNumber);
            var embed = new EmbedBuilder();
            SocketGuildUser TicketHolder = Global.Client.GetGuild(Config.bot.guildID).GetUser(WinningTicket.HolderID);
            embed.WithTitle("**Winning Guild Raffle Ticket!**")
                .WithDescription($"**{TicketHolder.Mention} HAS WON THE GUILD RAFFLE WITH THE TICKET NUMBER {WinningTicket.TicketNumber}!**");
            await ReplyAsync("", false, embed.Build());
        }
        [Command("purge"), Alias("empty")]
        public async Task TownRaffleEmpty()
        {
            var list = TownRaffles.townRaffle;
            list.Clear();
            TownRaffles.SaveTownRaffle();
            await ReplyAsync($"Raffle list has been purged. List count: {list.Count}");
        }
    }
}
