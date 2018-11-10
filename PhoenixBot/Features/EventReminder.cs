using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using System.Timers;

namespace PhoenixBot.Features
{
    internal static class EventReminder
    {
        private static ulong GuildId_ = Config.bot.guildID;
        private static ulong eventChannelID = ChannelIds.channels.eventID;

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
            SocketGuild guild = Global.Client.GetGuild(GuildId_);
            var guildInfo = Guild_Accounts.GuildAccounts.GetAccount(guild);

            if (guildInfo.EventRunning == true)
            {
                var difference = guildInfo.CurrentEvent - DateTime.Now;
                if (difference.TotalHours <= 1 && guildInfo.HourWarning == false)
                {
                    await Global.Client.GetGuild(GuildId_).GetTextChannel(eventChannelID).SendMessageAsync($"Event {guildInfo.EventName} starts in 1 hour!");
                    Console.WriteLine($"{difference}");
                    guildInfo.HourWarning = true;
                    Guild_Accounts.GuildAccounts.SaveAccounts();
                    return;
                }
                if (difference.TotalMinutes <= 10 && guildInfo.TenMinuteWarning == false)
                {
                    await Global.Client.GetGuild(GuildId_).GetTextChannel(eventChannelID).SendMessageAsync($"Event {guildInfo.EventName} starts in 10 minutes!");
                    Console.WriteLine($"{difference}");
                    guildInfo.TenMinuteWarning = true;
                    Guild_Accounts.GuildAccounts.SaveAccounts();
                    return;
                }
                if (difference.TotalSeconds == 0)
                {
                    await Global.Client.GetGuild(GuildId_).GetTextChannel(eventChannelID).SendMessageAsync($"Event {guildInfo.EventName} has started!");
                    guildInfo.EventRunning = false;
                    Guild_Accounts.GuildAccounts.SaveAccounts();
                    guildInfo.HourWarning = false;
                    Guild_Accounts.GuildAccounts.SaveAccounts();
                    guildInfo.TenMinuteWarning = false;
                    Guild_Accounts.GuildAccounts.SaveAccounts();
                    guildInfo.EventName = null;
                    Guild_Accounts.GuildAccounts.SaveAccounts();
                }

            }
        }
        private static async Task RandomFactPost()
        {
            var guildInfo = Global.Client.GetGuild(Config.bot.guildID);
            var guild = Guild_Accounts.GuildAccounts.GetAccount(guildInfo);
            var LastCheckedDay = guild.DayChecked;
            var CurrentTimeCheck = DateTime.Now;
            var difference = CurrentTimeCheck - LastCheckedDay;
            if (difference.Hours > 24)
            {
                var number = "No post found.";
                Random Post = new Random();
                int postNumber = Post.Next(1, 5);
                if (postNumber == 1)
                {
                    number = Features.FactPost.post.one;
                }
                else if (postNumber == 2)
                {
                    number = Features.FactPost.post.two;
                }
                else if (postNumber == 3)
                {
                    number = Features.FactPost.post.three;
                }
                else if (postNumber == 4)
                {
                    number = Features.FactPost.post.four;
                }
                else if (postNumber == 5)
                {
                    number = Features.FactPost.post.five;
                }
                var embed = new EmbedBuilder();
                embed.WithTitle("Random Post for the day!")
                    .WithDescription(number);
                var channel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.generalID);
                await channel.SendMessageAsync("", false, embed.Build());
                guild.DayChecked = DateTime.Now;
                Guild_Accounts.GuildAccounts.SaveAccounts();
                return;
            }
        }
    }
}
