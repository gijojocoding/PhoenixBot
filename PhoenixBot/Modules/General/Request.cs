using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules.General
{
    [Group("Request")]
    [Summary("Used to send a request.")]
    public class Request : ModuleBase<SocketCommandContext>
    {
        [Command("VoiceChannel")]
        [Summary("Used to send a request for a channel to be made, until next time. for all `date` inputs please use the MM/DD formate.")]
        public async Task RequestChannel(string channelName, string startDate, string endDate)
        {
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync("**ERROR 404** You lack the role to use this command.");
                return;
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Voice Channel Request:")
                .AddField("Who:", Context.User.Mention)
                .AddField("Channel name:", channelName)
                .AddField("Start date:", startDate)
                .AddField("Expected end dated:", endDate);
            var requestChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.requestID);
            await requestChannel.SendMessageAsync("", false, embed.Build());
        }
        [Command("Debate")]
        [Summary("Used to request a time to run a debate.")]
        public async Task RequestDebate(string topic, string when, string length = "Unknown")
        {
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync("**ERROR 404** You lack the role to use this command.");
                return;
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Voice Channel Request:")
                .AddField("Who:", Context.User.Mention)
                .AddField("Topic:", topic)
                .AddField("When:", when)
                .AddField("Length:", length);
            var requestChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.requestID);
            await requestChannel.SendMessageAsync("", false, embed.Build());
        }
        [Command("Meeting")]
        [Summary("Used to request a time to run a meeting.")]
        public async Task RequestMeeting(string topic, string when, string length)
        {
            if (!RoleCheck.HasClerkRole((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync("**ERROR 404** You lack the role to use this command.");
                return;
            }
            var embed = new EmbedBuilder();
            embed.WithTitle("Voice Channel Request:")
                .AddField("Who:", Context.User.Mention)
                .AddField("Topic:", topic)
                .AddField("When:", when)
                .AddField("Length:", length);
            var requestChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.requestID);
            await requestChannel.SendMessageAsync("", false, embed.Build());
        }
        [Command("info")]
        [Summary("Sends a DM with info.")]
        public async Task Info()
        {
            var dm = await Context.User.GetOrCreateDMChannelAsync();
            var embed = new EmbedBuilder();
            embed.WithTitle("Voice Channel Request:")
                .WithDescription("All requests should be done more then 4 days ahead of the start time! This gives us time to get ready or if there is a issue to allow corrections/cahnges. Debates/Meetings need a staff member to be part of the event should things get out of hand.");
            await dm.SendMessageAsync("", false, embed.Build());

        }
    }
}
