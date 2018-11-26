using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Timers;
using PhoenixBot.Guild_Accounts;

namespace PhoenixBot.Features
{
    internal static class EventReminder
    {


        private static Timer eventTimer;

        internal static Task EventTimeCheck()
        {
            eventTimer = new Timer()
            {
                Interval = 5000,
                AutoReset = true,
                Enabled = true
            };
            eventTimer.Elapsed += CheckEvent;
            return Task.CompletedTask;
        }

        private static async void CheckEvent(object sender, ElapsedEventArgs e)
        {
            await CheckEventTime();
        }


        private static async Task CheckEventTime()
        {
        var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
        var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            if (guild.GuildEvent1Running == true)
            {
                await GuildEventOne();
            }
            if (guild.GuildEvent2Running == true)
            {
                await GuildEventTwo();
            }
            if (guild.TownEvent1Running == true)
            {
                await TownEventOne();
            }
            if (guild.TownEvent2Running == true)
            {
                await TownEventTwo();
            }
            if(guild.GroupEventRunning == true)
            {
                await GroupEvent();
            }
        }
        private static async Task GuildEventOne()
        {
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var difference = guild.GuildEvent1Time - DateTime.Now;
            if (guild.GuildEvent1HourWarning == false && difference.TotalHours <= 1)
            {
                guild.GuildEvent1HourWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Guild Event {guild.GuildEvent1Name} is starting in 1 hour!");
            }
            if (guild.GuildEvent1TenMinuteWarning == false && difference.TotalMinutes <= 10)
            {
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Guild Event {guild.GuildEvent1Name} is starting in 10 minutes!");
            }
            if (difference.TotalSeconds <= 2)
            {
                guild.GroupEventRunning = false;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Guild Event {guild.GuildEvent1Name} is now starting!");
            }

        }
        private static async Task GuildEventTwo()
        {
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var difference = guild.GuildEvent2Time - DateTime.Now;
            if (guild.GuildEvent2HourWarning == false && difference.TotalHours <= 1)
            {
                guild.GuildEvent2HourWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Guild Event {guild.GuildEvent2Name} is starting in 1 hour!");
            }
            if (guild.GuildEvent2TenMinuteWarning == false && difference.TotalMinutes <= 10)
            {
                guild.GuildEvent2HourWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Guild Event {guild.GuildEvent2Name} is starting in 10 minutes!");
            }
            if (difference.TotalSeconds <= 2)
            {
                guild.GuildEvent2Running = false;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Guild Event {guild.GuildEvent2Name} is now starting!");
            }

        }
        private static async Task TownEventOne()
        {
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var difference = guild.TownEvent1Time - DateTime.Now;
            if (guild.TownEvent1HourWarning == false && difference.TotalHours <= 1)
            {
                guild.TownEvent1HourWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Town Event {guild.TownEvent2Name} is starting in 1 hour!");
            }
            if (guild.TownEvent1TenMinuteWarning == false && difference.TotalMinutes <= 10)
            {
                guild.TownEvent1TenMinuteWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Town Event {guild.TownEvent2Name} is starting in 10 minutes!");
            }
            if (difference.TotalSeconds <= 2)
            {
                guild.TownEvent1Running = false;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Town Event {guild.TownEvent1Name} is now starting!");
            }

        }
        private static async Task TownEventTwo()
        {
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var difference = guild.TownEvent2Time - DateTime.Now;
            if (guild.TownEvent2HourWarning == false && difference.TotalHours <= 1)
            {
                guild.TownEvent2HourWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Town Event {guild.TownEvent2Name} is starting in 1 hour!");
            }
            if (guild.TownEvent2TenMinuteWarning == false && difference.TotalMinutes <= 10)
            {
                guild.TownEvent2TenMinuteWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Town Event {guild.TownEvent2Name} is starting in 10 minutes!");
            }
            if (difference.TotalSeconds <= 2)
            {
                guild.TownEvent2Running = false;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Town Event {guild.TownEvent2Name} is now starting!");
            }

        }
        private static async Task GroupEvent()
        {
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var difference = guild.GroupEventTime - DateTime.Now;
            if (guild.GroupEventHourWarning == false && difference.TotalHours <= 1)
            {
                guild.GroupEventHourWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Group Event {guild.GroupEventName} is starting in 1 hour!");
            }
            if (guild.GroupEventTenMinuteWarning == false && difference.TotalMinutes <= 10)
            {
                guild.GroupEventTenMinuteWarning = true;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Group Event {guild.GroupEventName} is starting in 10 minutes!");
            }
            if (difference.TotalSeconds <= 2)
            {
                guild.GroupEventRunning = false;
                GuildAccounts.SaveAccounts();
                await eventChannel.SendMessageAsync($"Group Event {guild.GroupEventName} is now starting!");
            }
        }
    }
}
