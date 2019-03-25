using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;
using System.Linq;
using Discord.WebSocket;
using PhoenixBot.Modules.Music;
using System;
using System.Collections.Generic;

namespace PhoenixBot
{
    public class AudioService : ModuleBase<SocketCommandContext>
    {
        private LavaPlayer _player;
        private LavaSocketClient _lavaSocketClient;
        private LavaRestClient _lavaRestClient;
        public AudioService(LavaSocketClient lavaSocketClient, LavaRestClient lavaRestClient)
        {
            _lavaSocketClient = lavaSocketClient;
            _lavaRestClient = lavaRestClient;
           
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            _player = _lavaSocketClient.GetPlayer(Context.Guild.Id);
            base.BeforeExecute(command);
        }
        [Command("Join"), AudioProviso]
        public async Task Join()
        {
            try
            {
                var user = Context.User as SocketGuildUser;
                var userVoiceChannel = user.VoiceChannel;
                //await userVoiceChannel.ConnectAsync();
                await _lavaSocketClient.ConnectAsync(user.VoiceChannel);
                 _lavaSocketClient.GetPlayer(Config.bot.guildID);
                await ReplyAsync("Connected!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }


        [Command("Play")]
        public async Task PlayAsync([Remainder] string query)
        {
            try
            {
                //var user = Context
                _player = _lavaSocketClient.GetPlayer(Context.Guild.Id);
                var search = await _lavaRestClient.SearchYouTubeAsync(query);
                if (search.LoadType == LoadType.NoMatches ||
                    search.LoadType == LoadType.LoadFailed)
                {
                    await ReplyAsync("Nothing found");
                    return;
                }

                var track = search.Tracks.FirstOrDefault();

                if (_player.IsPlaying)
                {
                    _player.Queue.Enqueue(track);
                    await ReplyAsync($"{track.Title} has been queued.");
                }
                else
                {
                    await _player.PlayAsync(track);
                    await ReplyAsync($"Now Playing: {track.Title}");
                }
            } catch (Exception ex)
            {
                await ReplyAsync(ex.ToString());
            }
        }

        [Command("Disconnect")]
        public async Task StopAsync()
        {
            try
            {
                var user = Context.User as SocketGuildUser;
                await user.VoiceChannel.DisconnectAsync();
                await ReplyAsync("Disconnected!");
            }
            catch (Exception ex)
            {
                await ReplyAsync(ex.ToString());
            }
        }

        [Command("Skip")]
        public async Task SkipAsync()
        {
            LavaPlayer _player = _lavaSocketClient.GetPlayer(Config.bot.guildID);
            try
            {
                var skipped = await _player.SkipAsync();
                await ReplyAsync($"Skipped: {skipped.Title}\nNow Playing: {_player.CurrentTrack.Title}");
            }
            catch
            {
                await ReplyAsync("There are no more items left in queue.");
            }
        }

        [Command("NowPlaying")]
        public async Task NowPlaying()
        {
            LavaPlayer _player = _lavaSocketClient.GetPlayer(Config.bot.guildID);

            if (_player.CurrentTrack is null)
            {
                await ReplyAsync("There is no track playing right now.");
            }

            var track = _player.CurrentTrack;
            var thumb = await track.FetchThumbnailAsync();
            var embed = new EmbedBuilder();
                embed.WithAuthor($"Now Playing {_player.CurrentTrack.Title}", thumb, $"{track.Uri}")
                .WithThumbnailUrl(thumb)
                .AddField("Author", track.Author, true)
                .AddField("Length", track.Length, true)
                .AddField("Position", track.Position, true)
                .AddField("Streaming?", track.IsStream, true);

            await ReplyAsync("", false, embed.Build());
        }

        [Command("Lyrics")]
        public async Task LyricsAsync()
        {
            LavaPlayer _player = _lavaSocketClient.GetPlayer(Config.bot.guildID);

            if (_player.CurrentTrack is null)
            {
                await ReplyAsync("There is no track playing right now.");
            }

            var lyrics = await _player.CurrentTrack.FetchLyricsAsync();
            var thumb = await _player.CurrentTrack.FetchThumbnailAsync();

            var embed = new EmbedBuilder();
                embed.WithImageUrl(thumb)
                .WithDescription(lyrics)
                .WithAuthor($"Lyrics For {_player.CurrentTrack.Title}", thumb);

            await ReplyAsync("", false, embed.Build());
        }

        [Command("Queue")]
        public Task Queue()
        {
            LavaPlayer _player = _lavaSocketClient.GetPlayer(Config.bot.guildID);

            var tracks = _player.Queue.Items.Cast<LavaTrack>().Select(x => x.Title);
            return ReplyAsync(tracks.Count() is 0 ?
                "No tracks in queue." : string.Join("\n", tracks));
        }

    }
}
