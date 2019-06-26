using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using PhoenixBot.Guild_Accounts;

namespace PhoenixBot.Modules.Owner
{
    public class ServerInfo : ModuleBase<SocketCommandContext>
    {
        [Command("roleinfo")]
        [RequireOwner]
        public async Task RoleInfo()
        {
            string info = "These are the Roles for Digital Phoenix Investments. More maybe added for groups within the server! **ALL CHIEF AND STAFF ROLES ARE MERIT ONLY!**";
            string owner = "Leader of the guild, with the final say to resolve issues.";
            string botrole = "The role for the bot";
            string Chief = "The `Chief` roles are for the higher ranked members on the guild. They have their respected area they help manage.";
            string Staff = "The staff that help sort out issues and welcome new members.";
            string trader = "Traders that mainly stick to the town.";
            string travelingTrader = "Traders traders that travel to different towns or outposts.";
            string clerk = "Basic role given to guild members. The bread and butter of the guild. Without our Clerks, we have no guild.";
            string diplomat = "Members that represent other guilds for various reasons. **PEOPLE CAUGHT FROM OTHER GUILDS WITHOUT THIS ROLE ARE BANNED.**";
            string applicant = "New members who have agreed to the rules before being sorted into the Diplomat or Clerk roles. **This role is removed after being sorted.**";
            string townMember = "These are members of the town.";
            var embed = new EmbedBuilder();
            embed.WithTitle("Role Info")
                .WithDescription(info)
                .AddField("Chief Executive Officer:", owner)
                .AddField("Bots:", botrole)
                .AddField("Chief roles following the bot role:", Chief)
                .AddField("Investment Staff:", Staff)
                .AddField("Trader:", trader)
                .AddField("Traveling Trader:", travelingTrader)
                .AddField("Clerk:", clerk)
                .AddField("Town Member:", townMember)
                .AddField("Diplomat:", diplomat)
                .AddField("Applicant:", applicant);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("IDCheck", RunMode = RunMode.Async)]
        [RequireOwner]
        public async Task GetAllIDs()
        {
            var Guild = Global.Client.GetGuild(Context.Guild.Id);
            var user = await Context.User.GetOrCreateDMChannelAsync();
            await user.SendMessageAsync($"Context Guild ID: {Context.Guild.Id}");
            await user.SendMessageAsync($"Config Guild ID: {Config.bot.guildID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.announcementID).SendMessageAsync($"Channel ID: {ChannelIds.channels.announcementID}");
            await Guild.GetTextChannel(ChannelIds.channels.miniGameID).SendMessageAsync($"Channel ID: {ChannelIds.channels.miniGameID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.tradeRequestID).SendMessageAsync($"Channel ID: {ChannelIds.channels.tradeRequestID}");
            await Guild.GetTextChannel(ChannelIds.channels.buyingTradeID).SendMessageAsync($"Channel ID: {ChannelIds.channels.buyingTradeID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.sellingTradeID).SendMessageAsync($"Channel ID: {ChannelIds.channels.sellingTradeID}");
            await Guild.GetTextChannel(ChannelIds.channels.eventID).SendMessageAsync($"Channel ID: {ChannelIds.channels.eventID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.staffCommandID).SendMessageAsync($"Channel ID: {ChannelIds.channels.staffCommandID}");
            await Guild.GetTextChannel(ChannelIds.channels.adminLogID).SendMessageAsync($"Channel ID: {ChannelIds.channels.adminLogID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.diplomatLogID).SendMessageAsync($"Channel ID: {ChannelIds.channels.diplomatLogID}");
            await Guild.GetTextChannel(ChannelIds.channels.warningLogID).SendMessageAsync($"Channel ID: {ChannelIds.channels.warningLogID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.messageLogID).SendMessageAsync($"Channel ID: {ChannelIds.channels.messageLogID}");
            await Guild.GetTextChannel(ChannelIds.channels.muteLogID).SendMessageAsync($"Channel ID: {ChannelIds.channels.muteLogID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(ChannelIds.channels.banKickLogID).SendMessageAsync($"Channel ID: {ChannelIds.channels.banKickLogID}");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync("Command has finished.");
        }
        [Command("GuildDetails")]
        [Summary("Gives server details about the server")]
        [RequireOwner]
        public async Task GetGuildDetails([Remainder] string info = "")
        {
            var currentGuild = Context.Guild;
            var memberCount = currentGuild.MemberCount; //
            var afkChannel = currentGuild.AFKChannel;
            var afkTimer = currentGuild.AFKTimeout;
            var ownerUsername = currentGuild.Owner.Username; //
            var verificationLevel = currentGuild.VerificationLevel; //
            var regionId = currentGuild.VoiceRegionId; //
            var embed = new EmbedBuilder();
            if (info == "General")
            {
                embed.WithTitle($"Info for: {currentGuild.Name}")
                .AddField("Guild Owner:", ownerUsername)
                .AddField("Level of Verification:", verificationLevel)
                .AddField("Member Count:", memberCount)
                .AddField("Region of the server:", regionId)
                .AddField("AFK Timer:", afkTimer)
                .AddField("AFK Channel:", afkChannel)
                .WithColor(30, 60, 120);
                var ChannelToPostTo = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.generalID);
                await ChannelToPostTo.SendMessageAsync("", false, embed.Build());
                return;
            }
            else if (info == "" || info == " ")
            {
                embed.WithTitle($"Guild Info for: {currentGuild.Name}")
                .AddField("Guild Owner:", ownerUsername)
                .AddField("Level of Verification:", verificationLevel)
                .AddField("Member Count:", memberCount)
                .AddField("Region of the server:", regionId)
                .AddField("AFK Timer:", afkTimer)
                .AddField("AFK Channel:", afkChannel)
                .WithColor(30, 60, 120);
                await Context.Channel.SendMessageAsync("", false, embed.Build());
                return;
            }
            else if (info == "Membercount")
            {
                embed.WithTitle($"Guild Info for: {currentGuild.Name}")
                    .AddField("Member Count:", memberCount)
                    .WithColor(30, 60, 120);
            }
            else
            {
                var msg = Context.Message;
                await msg.DeleteAsync();
                return;
            }
        }
        //Used to recreate the Guild Account info when needed.
        [Command("Guild")]
        public async Task Guild()
        {
            GuildAccounts.GetAccount(Context.Guild);
            GuildAccounts.SaveAccounts();
        }
    }
}
