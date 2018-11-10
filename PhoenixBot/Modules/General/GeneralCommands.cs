﻿using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Guild_Accounts;

namespace PhoenixBot.Modules.General
{
    public class GeneralCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Data")]
        [Summary("Sends a DM to the the person tagged, or if none one is tagged to the user who posted the command. Contains info as to data used by the bot.")]
        public async Task DataToS(IGuildUser target = null)
        {
            var dataEmbed = new EmbedBuilder();
            dataEmbed.WithTitle("Data Collected and USED")
                .WithDescription("**Data is only collected from Discord!** This means all data used or collected comes from Discord or what is created by the bot itself! **This means health for the minigame, points, warnings and such!** The only thing I collect is the id created for the servers, **THIS IS NOT YOUR LOGIN ID! I REPEAT THIS INFO HAS NOTHING TO DO WITH YOU LOGGING IN TO DISCORD!** This is an ID that lets servers know who you are generated by Discord. **By using my server you are agreeing to let me use the ID, issued by Discord, to help manage the server and having mini-games you can play!**");
            if (target == null)
            {
                var user = Context.User as SocketGuildUser;
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("", false, dataEmbed.Build());
                return;
            }
            else
            {
                var user = target as SocketGuildUser;
                var dmChannel = await user.GetOrCreateDMChannelAsync();
                await dmChannel.SendMessageAsync("", false, dataEmbed.Build());
            }
        }
        [Command("Stats")]
        [Summary("Gets the stats about the user, or the person tagged. ")]
        public async Task Stats(SocketGuildUser user = null)
        {
            if (Context.IsPrivate == true) return;
            if (user != null)
            {
                var target = User_Accounts.UserAccounts.GetAccount(user);
                var embed = new EmbedBuilder();
                embed.WithTitle("Stats")
                    .AddField("Level:", target.LevelNumber)
                    .AddField("Points:", target.Points)
                    .AddField("HP Info:", $"{target.MaxHp} / {target.HP}");

                await Context.Channel.SendMessageAsync($"{user.Mention}", false, embed.Build());
                return;
            }
            else
            {
                var targetUser = Context.User;
                var target = User_Accounts.UserAccounts.GetAccount(targetUser);
                var embed = new EmbedBuilder();
                embed.WithTitle("Stats")
                    .AddField("Level:", target.LevelNumber)
                    .AddField("Points:", target.Points)
                    .AddField("HP Info:", $"Max HP: {target.MaxHp}! Current HP:{target.HP}");
                await Context.Channel.SendMessageAsync($"{targetUser.Mention}", false, embed.Build());
                return;
            }
        }
        [Command("Bot")]
        [Summary("Posts info about the bot. Does **NOT** include the change log.")]
        public async Task BotInfo()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle($"Digital Phoenix Investment Bot: Master Phoenix")
                .WithDescription("This bot is made to mainly help with crowd control. It features a basic way to post trades in an easy to follow formate so people can find trades with ease, and a basic laser game to help pass the time.")
                .AddField("This bot is made by:", "Jojo")
                .AddField("The current version:", $"{ChangeLog.log.Version}")
                .AddField("Command Prefix:", $"{Config.bot.cmdPrefix}");
            await ReplyAsync("", false, embed.Build());
        }
        [Command("event")]
        [Summary("Posts info about the current running event.")]
        public async Task EventStartsIn()
        {
            if (Context.IsPrivate == true) return;
            var guild = Context.Guild;
            var currentGuild = GuildAccounts.GetAccount(guild);
            if (currentGuild.EventRunning == false)
            {
                await Context.Channel.SendMessageAsync("No event is currently running!");
                return;
            }
            var difference = GuildAccounts.GetAccount(Context.Guild).CurrentEvent - DateTime.Now;
            var EventDays = difference.Days;
            var EventHours = difference.Hours;
            var EventMinutes = difference.Minutes;
            var EventSeconds = difference.Seconds;
            if (difference.TotalSeconds <= 0)
            {
                await Context.Channel.SendMessageAsync("Event has Passed");
                SocketGuild guildID = Global.Client.GetGuild(Config.bot.guildID);
                var guildInfo = Guild_Accounts.GuildAccounts.GetAccount(guildID);
                guildInfo.EventRunning = false;
                Guild_Accounts.GuildAccounts.SaveAccounts();
                guildInfo.HourWarning = false;
                Guild_Accounts.GuildAccounts.SaveAccounts();
                guildInfo.TenMinuteWarning = false;
                Guild_Accounts.GuildAccounts.SaveAccounts();
                guildInfo.EventName = null;
                Guild_Accounts.GuildAccounts.SaveAccounts();
                return;
            }
            await Context.Channel.SendMessageAsync($"Event {GuildAccounts.GetAccount(Context.Guild).EventName} starts in {EventDays} day(s),{EventHours} hour(s), {EventMinutes} minute(s) and {EventSeconds} seconds(s)! ");
        }
        [Command("Tokens")]
        [Summary("Gives info about the tokens in the game.")]
        public async Task Tokens()
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("**TOKENS**")
                .WithDescription("At the time of writing this version of the code, we do not know all of how the tokens can be used.")
                .AddField("Village Tokens:", "When given to the leader of the town, can be used to help the town.")
                .AddField("Guild Tokens:", "When given to the Guild Master, can be used to help the guild.");
            await ReplyAsync("", false, embed.Build());
        }
    }
}
