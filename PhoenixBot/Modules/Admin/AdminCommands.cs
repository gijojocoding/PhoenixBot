using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Guild_Accounts;
//using PhoenixBot.Features.Event;

namespace PhoenixBot.Modules.Admin
{
    [RequireOwner]
    [Group("admin")]
    public class AdminCommands : ModuleBase<SocketCommandContext>
    {
<<<<<<< HEAD
        private DiscordSocketClient _client {get; set;}
        const string KingdomString = "kingdom";
        const string DuchyString = "duchy";
        const string CountyString = "county";
        const string LocalString = "local";

        [Command("Startingup", RunMode = RunMode.Async)]
        async Task StartingScript()
        {
            var guild = Context.Guild;
            ulong channelid = GetId.GetChannelID(guild, "general");
            if (channelid == 0) return;
            var channel = guild.GetTextChannel(channelid);
            await channel.SendMessageAsync("Loading.");
            await channel.SendMessageAsync("Loading Bubble Memory.");
            await Task.Delay(2000);
            await channel.SendMessageAsync("Bubble Memory Loaded.");
            await Task.Delay(500);
            await channel.SendMessageAsync("Initializing Bubble Memory.");
            await Task.Delay(300);
            await channel.SendMessageAsync("Bubble Memory Initialized.");
            await Task.Delay(200);
            await channel.SendMessageAsync("Bot is Loaded and Ready.");
        }
        [Command("blockchannel")]
        async Task BlockChannelCmd(params SocketGuildUser[] users)
        {
            ulong channelid = 451890812175777802;
            var channel = Global.Client.GetGuild(Context.Guild.Id).GetTextChannel(channelid);
            foreach (var user in users)
            {
                var deny = new OverwritePermissions(viewChannel: PermValue.Deny);
                await channel.AddPermissionOverwriteAsync(user, deny);
            }
        }
=======
>>>>>>> parent of 0f2d20e... Working on SQL storage
        [Command("ListEvents", RunMode = RunMode.Async)]
        public async Task ListEventsAdmin()
        {
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var guild1 = new EmbedBuilder();
            var guild2 = new EmbedBuilder();
            var town1 = new EmbedBuilder();
            var town2 = new EmbedBuilder();
            var group = new EmbedBuilder();
            if (guild.TownEvent1Running == true)
            {
                town1.WithTitle($"**Town Event: {guild.TownEvent1Name}**")
                .WithDescription($"Date: {guild.TownEvent1Time.Date} at {guild.TownEvent1Time.TimeOfDay}");
                await eventChannel.SendMessageAsync("", false, town1.Build());
            }
            if (guild.TownEvent2Running == true)
            {
                town2.WithTitle($"**Town Event: {guild.TownEvent2Name}**")
                    .WithDescription($"Date: {guild.TownEvent2Time.Date} at {guild.TownEvent2Time.TimeOfDay}");
                await eventChannel.SendMessageAsync("", false, town2.Build());
            }
            await Task.Delay(1);
            if (guild.GuildEvent1Running == true)
            {
                guild1.WithTitle($"**Town Event: {guild.TownEvent1Name}**")
                    .WithDescription($"Date: {guild.TownEvent1Time.Date} at {guild.TownEvent1Time.TimeOfDay}");
                await eventChannel.SendMessageAsync("", false, guild1.Build());
            }
            if (guild.GuildEvent2Running == true)
            {
                guild2.WithTitle($"**Town Event: {guild.TownEvent2Name}**")
                    .WithDescription($"Date: {guild.TownEvent2Time.Date} at {guild.TownEvent2Time.TimeOfDay}");
                await eventChannel.SendMessageAsync("", false, guild2.Build());
            }
            await Task.Delay(1);
            if (guild.GroupEventRunning == true)
            {
                group.WithTitle($"**Town Event: {guild.TownEvent2Name}**")
                    .WithDescription($"Date: {guild.TownEvent2Time.Date} at {guild.TownEvent2Time.TimeOfDay}");
                await eventChannel.SendMessageAsync("", false, group.Build());
            }
        }
        [Command("ListEvent", RunMode = RunMode.Async)]
        public async Task ListEventsAdmin(string type)
        {
            var eventChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.eventID);
            var guild = GuildAccounts.GetAccount(Global.Client.GetGuild(Config.bot.guildID));
            var guild1 = new EmbedBuilder();
            var guild2 = new EmbedBuilder();
            var town1 = new EmbedBuilder();
            var town2 = new EmbedBuilder();
            var group = new EmbedBuilder();
            if (type == "town")
            {
                if (guild.TownEvent1Running == true)
                {
                    town1.WithTitle($"**Town Event: {guild.TownEvent1Name}**")
                    .WithDescription($"Date: {guild.TownEvent1Time.Date} at {guild.TownEvent1Time.TimeOfDay}");
                    await eventChannel.SendMessageAsync("", false, town1.Build());
                }
                if (guild.TownEvent2Running == true)
                {
                    town2.WithTitle($"**Town Event: {guild.TownEvent2Name}**")
                        .WithDescription($"Date: {guild.TownEvent2Time.Date} at {guild.TownEvent2Time.TimeOfDay}");
                    await eventChannel.SendMessageAsync("", false, town2.Build());
                }
                return;
            }
            else if (type == "guild")
            {
                if (guild.GuildEvent1Running == true)
                {
                    guild1.WithTitle($"**Town Event: {guild.TownEvent1Name}**")
                        .WithDescription($"Date: {guild.TownEvent1Time.Date} at {guild.TownEvent1Time.TimeOfDay}");
                    await eventChannel.SendMessageAsync("", false, guild1.Build());
                }
                if (guild.GuildEvent2Running == true)
                {
                    guild2.WithTitle($"**Town Event: {guild.TownEvent2Name}**")
                        .WithDescription($"Date: {guild.TownEvent2Time.Date} at {guild.TownEvent2Time.TimeOfDay}");
                    await eventChannel.SendMessageAsync("", false, guild2.Build());
                }
            }
            else if (type == "group")
            {
                if (guild.GroupEventRunning == true)
                {
                    group.WithTitle($"**Town Event: {guild.TownEvent2Name}**")
                        .WithDescription($"Date: {guild.TownEvent2Time.Date} at {guild.TownEvent2Time.TimeOfDay}");
                    await eventChannel.SendMessageAsync("", false, group.Build());
                }
            }
        }
        [Command("user")]
        [RequireOwner]
        public async Task UsernameCmd()
        {
            await ReplyAsync($"Context user: {Context.User} Context user ToString: {Context.User.ToString()} Context user username: {Context.User.Username}");
        }

        [Command("ChangeLog")]
        [Summary("Posts the Change Log of the most recent changes.")]
        [RequireOwner]
        public async Task ChangeLogCMD()
        {
            await Context.Channel.DeleteMessageAsync(1);
            var postingChannel = Global.Client.GetGuild(Context.Guild.Id).GetTextChannel(ChannelIds.channels.changeLogID);
            var embed = new EmbedBuilder();
            var planned = new EmbedBuilder();
            embed.WithTitle($"The change log for version {ChangeLog.log.Version}")
                .AddField("Change one:", ChangeLog.log.Change1)
                .AddField("Change two:", ChangeLog.log.Change2)
                .AddField("Change three:", ChangeLog.log.Change3)
                .AddField("Change four:", ChangeLog.log.Change4)
                .AddField("Change five:", ChangeLog.log.Change5);
            planned.WithTitle("Some of the things planned for upcoming versions!")
                .AddField("Planned:", ChangeLog.log.Planned1)
                .AddField("Planned:", ChangeLog.log.Planned2)
                .AddField("Planned:", ChangeLog.log.Planned3);
            await postingChannel.SendMessageAsync("", false, embed.Build());
            await postingChannel.SendMessageAsync("", false, planned.Build());
        }
        [Command("SetRandomFactTime")]
        [Summary("Admin command, sets guild's current time.")]
        public async Task SetInfo()
        {
            var guild = Guild_Accounts.GuildAccounts.GetAccount(Context.Guild);
            guild.DayChecked = DateTime.Now;
            GuildAccounts.SaveAccounts();
            await ReplyAsync($"Guild's random post time is now set. Time: {GuildAccounts.GetAccount(Context.Guild).DayChecked}");
        }
        [Command("RandomFact", RunMode = RunMode.Async)]
        [Summary("Posts a random fact.")]
        [RequireOwner]
        public async Task RandomFactPost()
        {
            string Post = Features.RandomFact.CallRandomFact();
            var embed = new EmbedBuilder();
            embed.WithTitle("Random Fact for the day")
                .WithDescription(Post);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("Ping")]
        [Summary("Returns a pong.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Pong()
        {
            await ReplyAsync($"Pong {Context.User}!");
        }
<<<<<<< HEAD

        [Command("AddRule")]
        async Task AddRuleCmd(byte number, [Remainder] string Rule)
        {
            var ruleArray = Rules.Rules.rules;
            if (ruleArray.Count == number)
            {
                await ReplyAsync("Error: That Rule Number is taken.");
                return;
            }
            if (ruleArray.Count == number - 1)
            {
                string ruleString = Rule;
                var rule = Rules.Rules.CreateRule(number, ruleString);
                Rules.Rules.SaveRules();
                await Context.User.SendMessageAsync($"Rule {rule.RuleNumber}: {rule.RuleString}");
                return;
            }
        }
        [Command("UpdateRule")]
        async Task UpdateRuleCmd(byte number, [Remainder] string newInfo)
        {
            var rule = Rules.Rules.GetRule(number);
            rule.RuleString = newInfo;
            Rules.Rules.SaveRules();
            await Context.Channel.SendMessageAsync($"Rule Number {rule.RuleNumber} has been updated to {rule.RuleString}");
        }
        [Command("ListRules")]
        async Task ListRulesCmd()
        {
            if(Rules.Rules.rules.Count == 0)
            {
                await Context.Channel.SendMessageAsync("Error: The Rule List is empty.");
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Rules:");
            foreach(var rule in Rules.Rules.rules)
            {
                embed.AddField(rule.RuleNumber.ToString(), rule.RuleString);
            }
                await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("PurgeRules")]
        async Task PurgeRulesCmd()
        {
            var rules = Rules.Rules.rules;
            rules.Clear();
            Rules.Rules.SaveRules();
            await ReplyAsync("Purge Complete.");
        }
        [Command("TestConnection")]
        [Alias("TestCnn")]
        async Task TestConnection()
        {
            DataAccess Db = new DataAccess();
            Db.test();
        }
    }
=======
        [Command("SyncAccounts", RunMode = RunMode.Async)]
        async Task ConvertHuntCmd()
        {
            foreach(var account in User_Accounts.UserAccounts.accounts)
            {
                Features.Games.UserAccounts.GameUserAccounts.GetAccount(account.ID);
                Features.Games.UserAccounts.GameUserAccounts.SaveAccounts();
            }
            await ReplyAsync("Accounts have been Synced!");
        }
    } 
>>>>>>> parent of 0f2d20e... Working on SQL storage
}
