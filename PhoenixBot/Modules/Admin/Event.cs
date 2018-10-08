using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Guild_Accounts;

namespace PhoenixBot.Modules.Admin
{
    [Group("Event")]
    public class Event : ModuleBase<SocketCommandContext>
    {
        [Command("Set")]
        [Summary("Admin command, sets event info. Only one event can be set at a time.")]
        [RequireOwner]
        public async Task SetEvent(int sMonth, int sDay, int sYear, int sHour, int sMinute, int sSecond, [Remainder] string name)
        {
            if (Guild_Accounts.GuildAccounts.GetAccount(Context.Guild).EventRunning == true) return;
            var seteventday = new DateTime(sYear, sMonth, sDay, sHour, sMinute, sSecond);
            var cmdEventName = name;
            GuildAccounts.GetAccount(Context.Guild).CurrentEvent = seteventday;
            GuildAccounts.SaveAccounts();
            GuildAccounts.GetAccount(Context.Guild).EventName = cmdEventName;
            GuildAccounts.SaveAccounts();
            GuildAccounts.GetAccount(Context.Guild).EventRunning = true;
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"Event {GuildAccounts.GetAccount(Context.Guild).EventName} is set for {GuildAccounts.GetAccount(Context.Guild).CurrentEvent}");
        }
        [Command("Remove")]
        [Summary("Admin command, removes the current running event.")]
        [RequireOwner]
        public async Task RemoveEvent()
        {
            SocketGuild guild = Global.Client.GetGuild(Config.bot.guildID);
            var guildInfo = Guild_Accounts.GuildAccounts.GetAccount(guild);
            guildInfo.EventRunning = false;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            guildInfo.HourWarning = false;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            guildInfo.TenMinuteWarning = false;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            guildInfo.EventName = null;
            Guild_Accounts.GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync($"Event has been removed!");
            Console.WriteLine($"Event info! Event Is running {GuildAccounts.GetAccount(Context.Guild).EventRunning}, and Event name is {GuildAccounts.GetAccount(Context.Guild).EventName}");
        }
    }
}
