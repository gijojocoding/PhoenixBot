using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace PhoenixBot.Modules.Admin
{
    [Group("Announcement")]
    [RequireOwner]
    public class Announcement : ModuleBase<SocketCommandContext>
    {
        [Command("tageveryone")]
        [Summary("Admin command, posts a message in the Announcement Channel with the `everyone` tag.")]
        public async Task TagEveryOne([Remainder] string message)
        {
            var announcements = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.announcementID);
            var announcementEmbed = new EmbedBuilder();
            announcementEmbed.WithTitle("**ANNOUNCEMENT FOR ALL GUILD MEMBERS**")
            .WithDescription(message);
            await announcements.SendMessageAsync("@everyone", false, announcementEmbed.Build());
        }
        [Command("taghere")]
        [Summary("Admin command, posts a message in the Announcement Channel with the `here` tag.")]
        public async Task TagHere([Remainder] string message)
        {
            var announcements = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.announcementID);
            var announcementEmbed = new EmbedBuilder();
            announcementEmbed.WithTitle("**ANNOUNCEMENT FOR ALL GUILD MEMBERS**")
            .WithDescription(message);
            await announcements.SendMessageAsync("@here", false, announcementEmbed.Build());
        }
        [Command("Poll")]
        [Summary("Owner command, posts a poll for people to vote on.")]
        public async Task PollCmd(string pollTitle, [Remainder] string msg)
        {
            var channel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.announcementID);
            var embed = new EmbedBuilder();
            embed.WithTitle(pollTitle)
                .WithDescription(msg)
                .WithColor(20, 50, 20);
            await channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("town")]
        async Task TownPost(string tag, [Remainder] string message)
        {
            var channelName = "town-general-chat";
            var channelID = GetId.GetChannelID(Context.Guild, channelName);
            if (channelID == 0) return;
            var channel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(channelID);
            var embed = new EmbedBuilder();
            embed.WithTitle("Town Notice")
                .WithDescription(message)
                .WithColor(20, 50, 20);
            if (tag == "no")
            {
                await channel.SendMessageAsync("", false, embed.Build());
            }else if(tag == "yes")
            {
                await channel.SendMessageAsync("@here", false, embed.Build());
            }
            else
            {
                return;
            }
        }
        [Command("guild")]
        async Task GuildPost(string tag, [Remainder] string message)
        {
            var channelName = "guild-general-chat";
            var channelID = GetId.GetChannelID(Context.Guild, channelName);
            if (channelID == 0) return;
            var channel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(channelID);
            var embed = new EmbedBuilder();
            embed.WithTitle("Guild Notice")
                .WithDescription(message)
                .WithColor(20, 50, 20);
            if (tag == "no")
            {
                await channel.SendMessageAsync("", false, embed.Build());
            }
            else if (tag == "yes")
            {
                await channel.SendMessageAsync("@here", false, embed.Build());
            }
            else
            {
                return;
            }
        }
    }
}
