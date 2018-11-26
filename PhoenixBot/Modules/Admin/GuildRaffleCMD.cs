using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using PhoenixBot.Features.Raffles.Guild;


namespace PhoenixBot.Modules.Admin
{
    [Group("GuildRaffle"), Alias("GR")]
    [Summary("Admin command for Guild Raffles.")]
    public class GuildRaffleCMD : ModuleBase<SocketCommandContext>
    {
        [Command("SetInfo")]
        public async Task SetGuildRaffleInfo(string info)
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.GuildRaffleInfo = info;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild Raffle Info: {guild.GuildRaffleInfo}");
        }
        [Command("Open")]
        public async Task OpenGuildRaffle()
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.GuildRaffleRunning = true;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild Raffle is open? {guild.GuildRaffleRunning}");
        }
        [Command("Close")]
        public async Task CloseGuildRaffle()
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.GuildRaffleRunning = false;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild Raffle is open? {guild.GuildRaffleRunning}");
        }
        [Command("GetInfo")]
        public async Task GuildRaffleInfo()
        {
            var list = GuildRaffles.guildRaffle;
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            var count = list.Count;
            var embed = new EmbedBuilder();
            embed.WithTitle("Guild Raffle Info")
                .AddField("Number of tickets Sold:", count)
                .AddField("Info:", guild.GuildRaffleInfo);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("Add")]
        [Summary("Admin command to add a raffle ")]
        public async Task AddTicket(SocketGuildUser holder, int ticketNumber)
        {
            var NumberHeld = 0;
            if(NumberHeld == 0)
            {
                foreach(var Ticket in GuildRaffles.guildRaffle)
                {
                    if(Ticket.TicketNumber == ticketNumber)
                    {
                        NumberHeld += 1;
                    }
                }
            }
            if(NumberHeld >= 1)
            {
                await ReplyAsync($"The ticket number {ticketNumber} is already taken.");
                return;
            }
            var Holder = holder.Id;
            var ticket = new GuildRaffle();
            ticket.HolderId = Holder;
            ticket.TicketNumber = ticketNumber;
            GuildRaffles.guildRaffle.Add(ticket);
            GuildRaffles.SaveGuildRaffleList();
            await ReplyAsync($"{holder} now has the ticket number: {ticketNumber}");
        }
        [Command("Run")]
        public async Task RunGuildRaffle()
        {
            var max = GuildRaffles.guildRaffle.Count;
            Random Raffle = new Random();
            var WinningNumber = Raffle.Next(1, max);
            var WinningTicket = GuildRaffles.GetGuildRaffleTicket(WinningNumber);
            var embed = new EmbedBuilder();
            SocketGuildUser TicketHolder = Global.Client.GetGuild(Config.bot.guildID).GetUser(WinningTicket.HolderId);
            embed.WithTitle("**Winning Guild Raffle Ticket!**")
                .WithDescription($"**{TicketHolder.Mention} HAS WON THE GUILD RAFFLE WITH THE TICKET NUMBER {WinningTicket.TicketNumber}!**");
            await ReplyAsync("", false, embed.Build());
        }
        [Command("purge"), Alias("empty")]
        public async Task TownRaffleEmpty()
        {
            var list = GuildRaffles.guildRaffle;
            list.Clear();
            GuildRaffles.SaveGuildRaffleList();
            await ReplyAsync($"Raffle list has been purged. List count: {list.Count}");
        }
    }
}
