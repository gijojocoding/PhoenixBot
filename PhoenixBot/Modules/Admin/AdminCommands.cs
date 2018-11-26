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
    public class AdminCommands : ModuleBase<SocketCommandContext>
    {
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
        [Command("AddXp")]
        [Summary("Admin command, adds xp to a user's account.")]
        [RequireOwner]
        public async Task AddXp(SocketGuildUser user, uint xp)
        {
            if (!RoleCheck.HasChiefRole((SocketGuildUser)Context.User)) return;
            var userAccount = User_Accounts.UserAccounts.GetAccount(user);
            uint oldLevel = userAccount.LevelNumber;
            userAccount.XP += xp;
            User_Accounts.UserAccounts.SaveAccounts();
            if (oldLevel != userAccount.LevelNumber)
            {
                var pointsAdded = (userAccount.LevelNumber * 20);
                userAccount.Points += pointsAdded;
                User_Accounts.UserAccounts.SaveAccounts();
                //User Leveled Up
                var embed = new EmbedBuilder();
                embed.WithTitle("Level Up")
                    .WithDescription($"{user.Mention} **JUST LEVELED UP! THEY ARE NOW {userAccount.LevelNumber}!** They got {pointsAdded} feathers!");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
        [Command("RemoveXp")]
        [Summary("Admin command, removes xp from a user's account.")]
        [RequireOwner]
        public async Task RemoveXp(SocketGuildUser user, uint xp)
        {
            if (!RoleCheck.HasChiefRole((SocketGuildUser)Context.User)) return;
            var userAccount = User_Accounts.UserAccounts.GetAccount(user);
            if (xp > userAccount.XP)
            {
                userAccount.XP = 0;
                User_Accounts.UserAccounts.SaveAccounts();
                await ReplyAsync($"{user.Mention} now has {userAccount.XP}");
                return;
            }
            userAccount.XP -= xp;
            User_Accounts.UserAccounts.SaveAccounts();
            await ReplyAsync($"{user.Mention} now has {userAccount.XP}");
        }
        [Command("adminpurge")]
        [Summary("Admin command, deletes a set of messages. no log created.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task AdminPurgeChat(int delnum)
        {
            //var messages = await Context.Channel.GetMessagesAsync(delnum + 1).Flatten();
            var Msgs = await Context.Channel.GetMessagesAsync(delnum + 1).FlattenAsync();

            foreach (IUserMessage Msg in Msgs)
            {
                await Msg.DeleteAsync();
                await Task.Delay(1000);
            }
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
    } 
}
