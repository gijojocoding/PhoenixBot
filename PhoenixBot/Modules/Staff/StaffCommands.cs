using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Staff
{
    public class StaffCommands : ModuleBase<SocketCommandContext>
    {
        [Command("DMMessage")]
        [Summary("Sends a DM to the person tagged. **Must have the role of Chief or Investment Staff.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task AdminDmMessage(SocketGuildUser user, [Remainder] string message)
        {
            if (!RoleCheck.HasChiefRole((SocketGuildUser)Context.User) || !RoleCheck.HasInvestmentStaffRole((SocketGuildUser)Context.User))
            {
                await Context.Message.DeleteAsync();
                var sender = await Context.User.GetOrCreateDMChannelAsync();
                await sender.SendMessageAsync("You do not have the permissions to use this command!");
                return;
            }
            var target = user;
            var messageLog = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.messageLogID);
            var dmChannel = await target.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync($"This is a message from the discord server saying: {message}");
            await messageLog.SendMessageAsync($"{Context.User.Mention} sent a message to {target.Mention}. Message was: {message}");
        }
    }
}
