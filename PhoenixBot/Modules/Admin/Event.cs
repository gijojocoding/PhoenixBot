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
    [RequireUserPermission(GuildPermission.Administrator)]
    public class Event : ModuleBase<SocketCommandContext>
    {
        [Command("AddTownEvent")]
        [Summary("Admin command, Adds a Town Event.")]
        public async Task SetTownEvent(DateTime date, int hour, int minute, [Remainder] string name)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.TownEvent1Running == false)
            {
                guild.TownEvent1Name = name;
                guild.TownEvent1Running = true;
                guild.TownEvent1HourWarning = false;
                guild.TownEvent1TenMinuteWarning = false;
                guild.TownEvent1Time = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else if (guild.TownEvent2Running == false)
            {
                guild.TownEvent2Name = name;
                guild.TownEvent2Running = true;
                guild.TownEvent2HourWarning = false;
                guild.TownEvent2TenMinuteWarning = false;
                guild.TownEvent2Time= new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                await ReplyAsync("Error 404: Two Town Events are already set.");
            }
        }
        [Command("AddGuildEvent")]
        [Summary("Admin command, Adds a Guild Event.")]
        public async Task SetGuildEvent(DateTime date, int hour, int minute, [Remainder] string name)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.GuildEvent1Running == false)
            {
                guild.GuildEvent1Name = name;
                guild.GuildEvent1Running = true;
                guild.GuildEvent1HourWarning = false;
                guild.GuildEvent1TenMinuteWarning = false;
                guild.GuildEvent1Time= new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else if (guild.GuildEvent2Running == false)
            {
                guild.GuildEvent2Name = name;
                guild.GuildEvent2Running = true;
                guild.GuildEvent2HourWarning = false;
                guild.GuildEvent2TenMinuteWarning = false;
                guild.GuildEvent2Time = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                await ReplyAsync("Error 404: Two Guild Events are already set.");
            }
        }
        [Command("SetGroupEvent")]
        [Summary("Admin Command, Adds a Group Event.")]
        public async Task AddGroupEvent(DateTime date, int hour, int minute, [Remainder] string name)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.GroupEventRunning == false)
            {
                guild.GuildEvent2Name = name;
                guild.GuildEvent2Running = true;
                guild.GuildEvent2HourWarning = false;
                guild.GuildEvent2TenMinuteWarning = false;
                guild.GuildEvent2Time = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                await ReplyAsync("Error 404: A Group Event is already set.");
            }
        }
    }
}
