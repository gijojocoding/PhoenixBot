using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;
using Victoria.Entities.Enums;
namespace PhoenixBot
{
    public sealed class AudioService
    {
        private LavaNode _node;
        private Lavalink _lavalink;

        public AudioService(Lavalink lavalink)
        {
            _lavalink = lavalink;
        }

        public void Initialize(LavaNode node = null)
        {
            _node = node ?? _lavalink.DefaultNode;
        }
        public async Task ConnectAndPlayAsync(IVoiceChannel voiceChannel, string query)
        {
            Console.WriteLine($"Voice Channel name: {voiceChannel.Name}. Looking for {query}");
            var player = _node.GetPlayer(Config.bot.guildID);
            await _node.ConnectAsync(voiceChannel);
            var search = await _node.SearchYouTubeAsync(query);
            var track = search.Tracks.FirstOrDefault();
            Console.WriteLine("Track should be playing.");
            await player.PlayAsync(track);
            _node.TrackFinished = OnFinished;
        }
        private Task OnFinished(LavaPlayer player, LavaTrack track, TrackReason reason) => Task.CompletedTask;
    }
}
