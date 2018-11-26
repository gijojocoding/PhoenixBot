using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using PhoenixBot.Guild_Accounts;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Admin
{
    [Group("Debate")]
    [Alias("AD")]
    public class AdminDebateCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Open", RunMode = RunMode.Async)]
        [Summary("Admin command to open the debate channels.")]
        public async Task OpenDebate(string target = "guild")
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id != Context.Guild.Owner.Id) return;
            var vAllow = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow, readMessageHistory: PermValue.Allow);
            var vDeny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny, readMessageHistory: PermValue.Deny);
            var tAllow = new OverwritePermissions(connect: PermValue.Allow, readMessageHistory: PermValue.Allow, sendMessages: PermValue.Allow);
            var tDeny = new OverwritePermissions(connect: PermValue.Deny, readMessageHistory: PermValue.Deny, sendMessages: PermValue.Deny);
            var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
            var textChannel = Context.Guild.GetTextChannel(ChannelIds.channels.debateTCID);
            if(target == "guild")
            {
                await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), vAllow);
                await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), tAllow);
            }
            if (target == "town")
            {
                await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.townMemberID), vAllow);
                await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.townMemberID), tAllow);
            }
            if (target == "all")
            {
                await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), vAllow);
                await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), tAllow);
                await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.townMemberID), vAllow);
                await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.townMemberID), tAllow);
            }
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), vAllow);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, vDeny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, tDeny);

            var guild = GuildAccounts.GetAccount(Context.Guild);
            Console.WriteLine(guild);
            guild.StickHolderId = 0;
            guild.DebateRunning = true;
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync("Guild stick holder id: " + guild.StickHolderId);
            await Context.Channel.SendMessageAsync("Debate running: " + guild.DebateRunning);
            GuildAccounts.SaveAccounts();
            await Context.Channel.SendMessageAsync("Both debate channels should be open.");
        }
        [Command("Close")]
        [Summary("Admin command to close the debate channels.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CloseDebate()
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id != Context.Guild.Owner.Id) return;
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
            var textChannel = Context.Guild.GetTextChannel(ChannelIds.channels.debateTCID);
            if(guild.StickHolderId != 0)
            {
                var currentHolderId = guild.StickHolderId;
                SocketGuildUser holder = Global.Client.GetGuild(Config.bot.guildID).GetUser(currentHolderId);
                await voiceChannel.RemovePermissionOverwriteAsync(holder);
                guild.StickHolderId = 0;
                GuildAccounts.SaveAccounts();
            }
            //var currentHolderID =  guild.StickHolderId;
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Deny, readMessageHistory: PermValue.Deny);
            var tDeny = new OverwritePermissions(connect: PermValue.Deny, readMessageHistory: PermValue.Deny, sendMessages: PermValue.Deny);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), deny);
            await voiceChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.townMemberID), deny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, tDeny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.guildMemberID), tDeny);
            await textChannel.AddPermissionOverwriteAsync(Context.Guild.GetRole(RoleIds.roles.townMemberID), tDeny);
            guild.DebateRunning = false;
            GuildAccounts.SaveAccounts();
        }
        [Command("GiveStick")]
        [Alias("GS")]
        [Summary("Admin command to force the speaking stick to someone else")]
        public async Task GiveStick(SocketGuildUser user)
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id != Context.Guild.Owner.Id) return;
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if(guild.DebateRunning == false)
            {
                await ReplyAsync("The debate is not running.");
                return;
            }
            var currentHolderId = guild.StickHolderId;
            SocketGuildUser holder = Global.Client.GetGuild(Config.bot.guildID).GetUser(currentHolderId);
            var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
            await voiceChannel.RemovePermissionOverwriteAsync(holder);
            var allow = new OverwritePermissions(speak: PermValue.Allow, connect: PermValue.Allow, viewChannel: PermValue.Allow);
            await voiceChannel.AddPermissionOverwriteAsync(user, allow);
            guild.StickHolderId = user.Id;
            GuildAccounts.SaveAccounts();
        }
        [Command("RemoveStick")]
        [Alias("RS")]
        [Summary("Admin command to remove the speaking stick from the current holder.")]
        public async Task RemoveStick(SocketGuildUser user)
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id != Context.Guild.Owner.Id) return;
            var guild = GuildAccounts.GetAccount(Context.Guild);
            if (guild.DebateRunning == false)
            {
                await ReplyAsync("The debate is not running.");
                return;
            }
            var deny = new OverwritePermissions(speak: PermValue.Deny, connect: PermValue.Allow);
            var voiceChannel = Context.Guild.GetVoiceChannel(ChannelIds.channels.debateVCID);
            await voiceChannel.RemovePermissionOverwriteAsync(user);
            var debateTC = Global.Client.GetGuild(Context.Guild.Id).GetTextChannel(ChannelIds.channels.debateTCID);
            guild.StickHolderId = 0;
            GuildAccounts.SaveAccounts();
            await debateTC.SendMessageAsync("The speaking stick is open for grabs!");
        }
        [Command("info")]
        [Summary("Admin command to get the current info on the debate.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DebateInfo()
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id != Context.Guild.Owner.Id) return;
            var guild = GuildAccounts.GetAccount(Context.Guild);
            var currentHolderId = guild.StickHolderId;
            SocketGuildUser holder = Global.Client.GetGuild(Config.bot.guildID).GetUser(currentHolderId);
            var embed = new EmbedBuilder();
            embed.WithTitle("Current Debate Info")
                .AddField("Person holding the stick:", holder.Username + "or" + holder.Nickname)
                .AddField("ID set for who is holding the stick:", guild.StickHolderId)
                .AddField("Debate is running:", guild.DebateRunning);
            await ReplyAsync("", false, embed.Build());
        }
        [Command("Rules")]
        [Summary("Used to post the debate rules.")]
        public async Task DebateRules(SocketGuildUser user = null)
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || Context.User.Id != Context.Guild.Owner.Id) return;
            var debateTC = Global.Client.GetGuild(Context.Guild.Id).GetTextChannel(ChannelIds.channels.debateTCID);
            var rules = new EmbedBuilder();
            rules.WithTitle("**Rules for the Debate Channels!**")
                .WithDescription("The rules are for debates/meetings using the Debate Channels. This are enforced with bans from the debate channels to keep them as inviting as possible. If you have been banned please talk to the Owner about being able to rejoin through DMs.")
                .AddField("Rule 1:", "Please be respectful at all times. Debates are to be fun and enjoyable, while meetings are there to get everyone's collective input in a voice chat.")
                .AddField("Rule 2:", "This rule is a reminder, spamming the bot is against the rules.")
                .AddField("Rule 3:", "No complaining that you didn't get the speaking stick. This is distracting for everyone not only the person with the stick.")
                .AddField("Rule 4:", "No begging for the stick. Again, this is distracting for everyone not only the person with the stick.")
                .AddField("Rule 5:", "If your hogging the speaking stick, it can be removed by admin commands.")
                .AddField("Rule 6:", "Weither you have the speaking stick or not, keep both channels on topic.")
                .AddField("Rule 7:", "Do not ask for commands(if your the speaker asking how to give the stick up or to someone is one thing!) there is a `!help debate` command.")
                .AddField("Rule 8:", "**DO NOT** argue with staff members. If it is the case of power abuse please contact the leader.");
            if(user == null)
            {
                await debateTC.SendMessageAsync("", false, rules.Build());
                return;
            }
            else if (user != null)
            {
                var dm = await user.GetOrCreateDMChannelAsync();
                await dm.SendMessageAsync("", false, rules.Build());
                return;
            }

        }
    }
}
