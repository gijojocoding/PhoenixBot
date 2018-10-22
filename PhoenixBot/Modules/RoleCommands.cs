using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules
{
    public class RoleCommands : ModuleBase<SocketCommandContext>
    {
        [Command("I agree to the rules")]
        [Summary("New people use this to agree to the server's Rules.")]
        public async Task AgreeToTheRules()
        {
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User) && !RoleCheck.HasDiplomatRole((SocketGuildUser)Context.User))
            {
                var guildUser = Context.User as IGuildUser;
                var memberRole = Context.Guild.GetRole(RoleIds.roles.applicantID);
                await guildUser.AddRoleAsync(memberRole);
                var user = Context.User;
                await Context.Channel.SendMessageAsync($"{user.Mention} has agreed to the rules and is a {memberRole}");

            }
        }
        [Command("Diplomat")]
        [Summary("Staff/Recruiter command, used to give a person the Diplomat role and log who they are with.")]
        public async Task Diplomat(SocketGuildUser user, [Remainder] string guild)
        {
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
        [Command("FullMember")]
        [Summary("Staff/Recruiter command, used grant access to member channels.")]
        public async Task GrantFullMemberShip(IGuildUser applicant)
        {
            if (RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User) || RoleCheck.HasRecruiterRole((SocketGuildUser)Context.User))
            {
                if (RoleCheck.HasApplicantRole((SocketGuildUser)applicant))
                {
                    var guildUser = applicant;
                    var role = Context.Guild.GetRole(RoleIds.roles.memberID);
                    var oldRole = Context.Guild.GetRole(RoleIds.roles.memberID);
                    await guildUser.AddRoleAsync(role);
                    await guildUser.RemoveRoleAsync(oldRole);
                    await Context.Channel.SendMessageAsync($"{guildUser.Mention} now is a {role}! This was granted by {Context.User}");
                }
            }
        }
    }
}
