using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;
using System.Linq;
using Discord.WebSocket;
using PhoenixBot.Modules.Music;
using System;

namespace PhoenixBot
{
    public class AudioService : ModuleBase<SocketCommandContext>
    {
        private LavaPlayer _player;
        private LavaSocketClient _lavaSocketClient;
        private LavaRestClient _lavaRestClient;
        //DiscordSocketClient _client { get; set; }
        public AudioService(LavaSocketClient lavaSocketClient, LavaRestClient lavaRestClient)
        {
            _lavaSocketClient = lavaSocketClient;
            _lavaRestClient = lavaRestClient;
        }
        #region Music Region
        [Command("Join", RunMode = RunMode.Async)]
        public async Task Join()
        {

                var user = Context.User as SocketGuildUser;
                var userVoiceChannel = user.VoiceChannel;
                await _lavaSocketClient.ConnectAsync(userVoiceChannel);
                await ReplyAsync("Connected!");

        }


        [Command("Play", RunMode = RunMode.Async)]
        public async Task PlayAsync([Remainder] string query)
        {
            Console.WriteLine("In play command.");
            try
            {
                var search = await _lavaRestClient.SearchYouTubeAsync(query);
                if (search.LoadType == LoadType.NoMatches ||
                    search.LoadType == LoadType.LoadFailed)
                {
                    await ReplyAsync("Nothing found");
                    return;
                }

                var track = search.Tracks.FirstOrDefault();
                Console.WriteLine(_player.VoiceChannel + "" + _player.CurrentTrack);
                Console.WriteLine(track);
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
            }
            catch (Exception ex)
            {
                await ReplyAsync("Error: " + ex);
            }
        }

        [Command("Disconnect")]
        public async Task StopAsync()
        {
            await _lavaSocketClient.DisconnectAsync(_player.VoiceChannel);
            await ReplyAsync("Disconnected!");
        }

        [Command("Skip")]
        public async Task SkipAsync()
        {
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
            var tracks = _player.Queue.Items.Cast<LavaTrack>().Select(x => x.Title);
            return ReplyAsync(tracks.Count() is 0 ?
                "No tracks in queue." : string.Join("\n", tracks));
        }
        #endregion
    }
}
