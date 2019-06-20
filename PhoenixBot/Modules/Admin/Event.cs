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
                guild.TownEvent1TenMinuteWarning = false;
                guild.TownEvent1Time = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                await ReplyAsync("Error 404: A Town Event is already set.");
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
                guild.GuildEvent1TenMinuteWarning = false;
                guild.GuildEvent1Time= new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                await ReplyAsync("Error 404: A Guild Event is already set.");
            }
        }
        [Command("SetGroupEvent")]
        [Summary("Admin Command, Adds a Group Event.")]
        public async Task AddGroupEvent(DateTime date, int hour, int minute, [Remainder] string name)
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.GroupEventRunning == false)
            {
                guild.GroupEventName = name;
                guild.GroupEventRunning = true;
                guild.GroupEventTenMinuteWarning = false;
                guild.GroupEventTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                await ReplyAsync("Error 404: A Group Event is already set.");
            }
        }
        [Command("RemoveTownEvent")]
        async Task RemoveTownEvent()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if(guild.GroupEventRunning == true)
            {
                guild.TownEvent1Name = null;
                guild.TownEvent1Running = false;
                guild.TownEvent1TenMinuteWarning = false;
            }
            else
            {
                await ReplyAsync("Error 404: There was no Town Event set.");
            }
        }
        [Command("RemoveGuildEvent")]
        async Task RemoveGuildEvent()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.GuildEvent1Running == true)
            {
                guild.GuildEvent1Name = null;
                guild.GuildEvent1Running = false;
                guild.GuildEvent1TenMinuteWarning = false;
            }
            else
            {
                await ReplyAsync("Error 404: There was no Guild Event set.");
            }
        }
        [Command("RemoveGrouEvent")]
        async Task RemoveGroupEvent()
        {
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.GuildEvent1Running == true)
            {
                guild.GroupEventName = null;
                guild.GuildEvent1Running = false;
                guild.GuildEvent1TenMinuteWarning = false;
            }
            else
            {
                await ReplyAsync("Error 404: There was no Group Event set.");
            }
        }
    }
}
