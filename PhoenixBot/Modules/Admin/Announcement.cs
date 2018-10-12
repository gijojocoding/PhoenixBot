using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace PhoenixBot.Modules.Admin
{
    [Group("Announcement")]
    public class Announcement : ModuleBase<SocketCommandContext>
    {
        [Command("tageveryone")]
        [Summary("Admin command, posts a message in the Announcement Channel with the `everyone` tag.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task TagEveryOne([Remainder] string message)
        {
            var announcements = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.announcementID);
            var announcementEmbed = new EmbedBuilder();
            announcementEmbed.WithTitle("**ANNOUNCEMENT FOR ALL GUILD MEMBERS**")
            .WithDescription(message);
            await announcements.SendMessageAsync("@everyone", false, announcementEmbed);
        }
        [Command("taghere")]
        [Summary("Admin command, posts a message in the Announcement Channel with the `here` tag.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task TagHere([Remainder] string message)
        {
            var announcements = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.announcementID);
            var announcementEmbed = new EmbedBuilder();
            announcementEmbed.WithTitle("**ANNOUNCEMENT FOR ALL GUILD MEMBERS**")
            .WithDescription(message);
            await announcements.SendMessageAsync("@here", false, announcementEmbed);
        }
        [Command("triva")]
        [Summary("Admin command, picks a random fact then posts it in the general channel.")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task TrivaPost()
        {
            var announcements = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.generalID);
            var embed = new EmbedBuilder();
            var fact = Features.RandomFact.CallRandomFact();
            embed.WithTitle("**RANDOM FACT TIME**")
                .WithDescription(fact);
            await announcements.SendMessageAsync("", false, embed);
        }
        [Command("Poll")]
        [Summary("Owner command, posts a poll for people to vote on.")]
        [RequireOwner]
        public async Task PollCmd(string pollTitle, [Remainder] string msg)
        {
            var channel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.announcementID);
            var embed = new EmbedBuilder();
            embed.WithTitle(pollTitle)
                .WithDescription(msg)
                .WithColor(20, 50, 20);
            await channel.SendMessageAsync("", false, embed);
        }
    }
}
