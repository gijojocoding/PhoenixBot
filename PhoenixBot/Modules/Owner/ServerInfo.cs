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
                .AddField("Diplomat:", diplomat)
                .AddField("Applicant:", applicant);
            await Context.Channel.SendMessageAsync("", false, embed);
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
            await Guild.GetTextChannel(Config.bot.announcementID).SendMessageAsync($"Channel ID: {Config.bot.announcementID}");
            await Guild.GetTextChannel(Config.bot.miniGameID).SendMessageAsync($"Channel ID: {Config.bot.miniGameID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(Config.bot.tradeRequestID).SendMessageAsync($"Channel ID: {Config.bot.tradeRequestID}");
            await Guild.GetTextChannel(Config.bot.buyingTradeID).SendMessageAsync($"Channel ID: {Config.bot.buyingTradeID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(Config.bot.sellingTradeID).SendMessageAsync($"Channel ID: {Config.bot.sellingTradeID}");
            await Guild.GetTextChannel(Config.bot.eventID).SendMessageAsync($"Channel ID: {Config.bot.eventID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(Config.bot.staffCommandID).SendMessageAsync($"Channel ID: {Config.bot.staffCommandID}");
            await Guild.GetTextChannel(Config.bot.adminLogID).SendMessageAsync($"Channel ID: {Config.bot.adminLogID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(Config.bot.diplomatLogID).SendMessageAsync($"Channel ID: {Config.bot.diplomatLogID}");
            await Guild.GetTextChannel(Config.bot.warningLogID).SendMessageAsync($"Channel ID: {Config.bot.warningLogID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(Config.bot.messageLogID).SendMessageAsync($"Channel ID: {Config.bot.messageLogID}");
            await Guild.GetTextChannel(Config.bot.muteLogID).SendMessageAsync($"Channel ID: {Config.bot.muteLogID}");
            await Task.Delay(1000);
            await Guild.GetTextChannel(Config.bot.banKickLogID).SendMessageAsync($"Channel ID: {Config.bot.banKickLogID}");
            await Task.Delay(1000);
            await Context.Channel.SendMessageAsync("Command has finished.");
        }
        [Command("GuildDetails")]
        [Summary("")]
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
                var ChannelToPostTo = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.generalID);
                await ChannelToPostTo.SendMessageAsync("", false, embed);
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
                await Context.Channel.SendMessageAsync("", false, embed);
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
