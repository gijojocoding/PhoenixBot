using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules
{
    public class RoleCommands : ModuleBase<SocketCommandContext>
    {
        DiscordSocketClient _client { get; set; }
        [Command("RecruiterCommands")]
        [RequireUserPermission(GuildPermission.SendTTSMessages)]
        async Task ListRoleCommands()
        {
            var list = new EmbedBuilder();
            list.WithTitle("**Recruiter Command**")
                .WithDescription("Diplomat (tag person) (Who they are with): This lets us know they are a diplomat and who they are with. " +
                "\n GuildMember (tag person): This gives them the Clerk role for the guild. " +
                "\n TownMember (tag person): This gives them the town member role.");
            await Context.User.SendMessageAsync("", false, list.Build());
        }
        [Command("I agree to the rules")]
        [Summary("New people use this to agree to the server's Rules.")]
        public async Task AgreeToTheRules()
        {
            var channelid = GetId.GetChannelID(Context.Guild, "joining");
            if (channelid == 0) return;
            var channel = Context.Guild.GetTextChannel(channelid);
            if (Context.IsPrivate == true) return;
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User) && !RoleCheck.HasDiplomatRole((SocketGuildUser)Context.User))
            {
                var guildUser = Context.User as IGuildUser;
                var memberRole = Context.Guild.GetRole(RoleIds.roles.applicantID);
                await guildUser.AddRoleAsync(memberRole);
                var user = Context.User;
                await Context.Channel.SendMessageAsync($"{user.Mention} has agreed to the rules and is a {memberRole}");
                var deny = new OverwritePermissions(viewChannel: PermValue.Deny);
                await channel.AddPermissionOverwriteAsync(Context.User, deny);
            }
        }
        [Command("Diplomat")]
        [RequireUserPermission(GuildPermission.SendTTSMessages)]
        [Summary("Staff/Recruiter command, used to give a person the Diplomat role and log who they are with.")]
        public async Task Diplomat(SocketGuildUser user, [Remainder] string guild)
        {
            if (Context.IsPrivate == true) return;
            var diplomatLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.diplomatLogID);
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasRecruiterRole((SocketGuildUser)Context.User))
            {
                if (RoleCheck.HasClerkRole((SocketGuildUser)user))
                {
                    await ReplyAsync("Role conflict. Unable to assign the Diplomat role.");
                    return;
                }
                var target = user as IGuildUser;
                var memberRole = Context.Guild.GetRole(RoleIds.roles.diplomatID);
                await target.AddRoleAsync(memberRole);
                await diplomatLog.SendMessageAsync($"{target.Mention} is from the {guild}");
            }
        }
        [Command("GuildMember")]
        [RequireUserPermission(GuildPermission.SendTTSMessages)]
        [Summary("Staff/Recruiter command, used to grant access to guild channels.")]
        public async Task GrantGuildMembership(IGuildUser applicant)
        {
            if (Context.IsPrivate == true) return;
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasRecruiterRole((SocketGuildUser)Context.User))
            {
                if (RoleCheck.HasApplicantRole((SocketGuildUser)applicant) && !RoleCheck.HasDiplomatRole((SocketGuildUser)applicant))
                {
                    var guildUser = applicant;
                    var role = Context.Guild.GetRole(RoleIds.roles.guildMemberID);
                    var oldRole = Context.Guild.GetRole(RoleIds.roles.applicantID);
                    await guildUser.AddRoleAsync(role);
                    await guildUser.RemoveRoleAsync(oldRole);
                    await Context.Channel.SendMessageAsync($"{guildUser.Mention} now has the `{role}` role! This was granted by {Context.User}");
                }
            }
        }
        [Command("TownMember")]
        [RequireUserPermission(GuildPermission.SendTTSMessages)]
        [Summary("Staff/Recruiter command, used to grand access to town channels.")]
        public async Task GrantTownMembership(IGuildUser applicant)
        {
            if (Context.IsPrivate == true) return;
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasRecruiterRole((SocketGuildUser)Context.User))
            {
                if (RoleCheck.HasApplicantRole((SocketGuildUser)applicant) && !RoleCheck.HasDiplomatRole((SocketGuildUser)applicant))
                {
                    var guildUser = applicant;
                    var role = Context.Guild.GetRole(RoleIds.roles.townMemberID);
                    var oldRole = Context.Guild.GetRole(RoleIds.roles.applicantID);
                    await guildUser.AddRoleAsync(role);
                    await guildUser.RemoveRoleAsync(oldRole);
                    await Context.Channel.SendMessageAsync($"{guildUser.Mention} now has the `{role}` role! This was granted by {Context.User}");
                }
            }
        }
        [Command("Trader")]
        [RequireUserPermission(GuildPermission.SendTTSMessages)]
        async Task GrantTraderRole(params IGuildUser[] target)
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasRecruiterRole((SocketGuildUser)Context.User)) return;
            string role = "Trader";
            var roleID = GetId.GetRoleID(Context.Guild, role);
            if (roleID == 0)
            {
                await Context.Channel.SendMessageAsync($"The role {role} could not be found.");
                return;
            }
            foreach (var user in target)
            {
                var guildRole = Context.Guild.GetRole(roleID);
                await user.AddRoleAsync(guildRole);
                await Context.Channel.SendMessageAsync($"{user.Username} now has the {role}");
            }
        }
        [Command("TravelingTrader")]
        [RequireUserPermission(GuildPermission.SendTTSMessages)]
        async Task GrantTravelingTraderRole(params IGuildUser[] target)
        {
            if (!RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || !RoleCheck.HasRecruiterRole((SocketGuildUser)Context.User)) return;
            string role = "Traveling Trader";
            var roleID = GetId.GetRoleID(Context.Guild, role);
            if (roleID == 0)
            {
                await Context.Channel.SendMessageAsync($"The role {role} could not be found.");
                return;
            }
            foreach (var user in target)
            {
                var guildRole = Context.Guild.GetRole(roleID);
                await user.AddRoleAsync(guildRole);
                await Context.Channel.SendMessageAsync($"{user.Username} now has the {role}");
            }
        }
        [Command("GiveRole")]
        [RequireUserPermission(GuildPermission.Administrator)]
        async Task GiveRoleCmd(SocketGuildUser target, [Remainder] string role)
        {
            var roleID = GetId.GetRoleID(Context.Guild, role);
            if(roleID == 0)
            {
                await Context.Channel.SendMessageAsync($"The role {role} could not be found.");
                return;
            }
            var guildRole = Context.Guild.GetRole(roleID);
            await target.AddRoleAsync(guildRole);
            await Context.Channel.SendMessageAsync($"{target.Username} now has the {role}");
        }
    }
}
