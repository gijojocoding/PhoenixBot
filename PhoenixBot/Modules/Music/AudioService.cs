using Discord;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Victoria;
using Victoria.Entities;
using Victoria.Entities.Enums;
using PhoenixBot.Handlers;
using System.Linq;
using Discord.WebSocket;

namespace PhoenixBot.Modules.Music
{
    public class AudioService
    {
        private Lavalink _lavalink;
        private LavaNode _node;
        public AudioService(Lavalink lavalink, LavaNode node = null)
        {
            _lavalink = lavalink;
            _node = node;
        }
        DiscordSocketClient _client { get; set; }
        private readonly Lazy<ConcurrentDictionary<ulong, AudioOptions>> _lazyOptions
    = new Lazy<ConcurrentDictionary<ulong, AudioOptions>>();

        private ConcurrentDictionary<ulong, AudioOptions> Options
            => _lazyOptions.Value;

        public async Task JoinOrPlayAsync(SocketGuildUser user, IMessageChannel textChannel, ulong guildId, string query = null)
        {
            var tChannel = Global.Client.GetGuild(guildId).GetTextChannel(textChannel.Id);
            //Check If User Is Connected To Voice Cahnnel.
            if (user.VoiceChannel == null)
            {
                await tChannel.SendMessageAsync("Please join a voice channel that I have access to.");
            }
            LavaTrack track;
            LavaPlayer player = _lavalink.DefaultNode.GetPlayer(guildId);
            var node = await _lavalink.AddNodeAsync(_client);
            await node.ConnectAsync(user.VoiceChannel);
            var search = await _lavalink.DefaultNode.SearchYouTubeAsync(query);
            if(player.IsPlaying == true)
            if(search.LoadResultType == LoadResultType.NoMatches)
            {
                await tChannel.SendMessageAsync("I failed to find anything. Please try again or blame my Dev.");
            }

            track = search.Tracks.FirstOrDefault();
            await player.PlayAsync(track);

        }


        /*This is ran when a user uses the command Leave.
            Task Returns an Embed which is used in the command call. */
        public async Task<Embed> LeaveAsync(ulong guildId)
        {
            try
            {
                //Get The Player Via GuildID.
                var player = _lavalink.DefaultNode.GetPlayer(guildId);

                //if The Player is playing, Stop it.
                if (player.IsPlaying)
                    await player.StopAsync();

                //Leave the voice channel.
                var channelName = player.VoiceChannel.Name;
                await _lavalink.DefaultNode.DisconnectAsync(guildId);
                return await EmbedHandler.CreateBasicEmbed("Music", $"I've left {channelName}. Thank you for playing moosik.", Color.Blue);
            }
            //Tell the user about the error so they can report it back to us.
            catch (InvalidOperationException ex)
            {
                return await EmbedHandler.CreateErrorEmbed("Music, Leave", ex.ToString());
            }
        }

        public async Task<Embed> ListAsync(ulong guildId)
        {
            try
            {
                /* Create a string builder we can use to format how we want our list to be displayed. */
                var descriptionBuilder = new StringBuilder();

                /* Get The Player and make sure it isn't null. */
                var player = _lavalink.DefaultNode.GetPlayer(guildId);
                if (player == null)
                    return await EmbedHandler.CreateErrorEmbed("Music, List", $"Could not aquire player.\nAre you using the bot right now? check {Config.bot.cmdPrefix}Help for info on how to use the bot.");

                if (player.IsPlaying)
                {
                    /*If the queue count is less than 1 and the current track IS NOT null then we wont have a list to reply with.
                        In this situation we simply return an embed that displays the current track instead. */
                    if (player.Queue.Count < 1 && player.CurrentTrack != null)
                    {
                        return await EmbedHandler.CreateBasicEmbed($"Now Playing: {player.CurrentTrack.Title}", "Nothing Else Is Queued.", Color.Blue);
                    }
                    else
                    {
                        /* Now we know if we have something in the queue worth replying with, so we itterate through all the Tracks in the queue.
                         *  Next Add the Track title and the url however make use of Discords Markdown feature to display everything neatly.
                            This trackNum variable is used to display the number in which the song is in place. (Start at 2 because we're including the current song.*/
                        var trackNum = 2;
                        foreach (var track in player.Queue.Items)
                        {
                            descriptionBuilder.Append($"{trackNum}: [{track.Title}]({track.Uri}) - {track.Id}\n");
                            trackNum++;
                        }
                        return await EmbedHandler.CreateBasicEmbed("Music Playlist", $"Now Playing: [{player.CurrentTrack.Title}]({player.CurrentTrack.Uri})\n{descriptionBuilder.ToString()}", Color.Blue);
                    }
                }
                else
                {
                    return await EmbedHandler.CreateErrorEmbed("Music, List", "Player doesn't seem to be playing anything right now. If this is an error, Please Contact Draxis.");
                }
            }
            catch (Exception ex)
            {
                return await EmbedHandler.CreateErrorEmbed("Music, List", ex.Message);
            }

        }

        /*This is ran when a user uses the command Skip 
            Task Returns an Embed which is used in the command call. */
        public async Task<Embed> SkipTrackAsync(ulong guildId)
        {
            try
            {
                var player = _lavalink.DefaultNode.GetPlayer(guildId);
                /* Check if the player exists */
                if (player == null)
                    return await EmbedHandler.CreateErrorEmbed("Music, List", $"Could not aquire player.\nAre you using the bot right now? check {Config.bot.cmdPrefix}Help for info on how to use the bot.");
                /* Check The queue, if it is less than one (meaning we only have the current song available to skip) it wont allow the user to skip.
                     User is expected to use the Stop command if they're only wanting to skip the current song. */
                if (player.Queue.Count < 1)
                {
                    return await EmbedHandler.CreateErrorEmbed("Music, SkipTrack", $"Unable To skip a track as there is only One or No songs currently playing." +
                        $"\n\nDid you mean {Config.bot.cmdPrefix}Stop?");
                }
                else
                {
                    try
                    {
                        /* Save the current song for use after we skip it. */
                        var currentTrack = player.CurrentTrack;
                        /* Skip the current song. */
                        await player.SkipAsync();
                        return await EmbedHandler.CreateBasicEmbed("Music Skip", $"I have successfully skiped {currentTrack.Title}", Color.Blue);
                    }
                    catch (Exception ex)
                    {
                        return await EmbedHandler.CreateErrorEmbed("Music, Skip", ex.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
                return await EmbedHandler.CreateErrorEmbed("Music, Skip", ex.ToString());
            }
        }

        /*This is ran when a user uses the command Stop 
            Task Returns an Embed which is used in the command call. */
        public async Task<Embed> StopAsync(ulong guildId)
        {
            try
            {
                var player = _lavalink.DefaultNode.GetPlayer(guildId);
                if (player == null)
                    return await EmbedHandler.CreateErrorEmbed("Music, List", $"Could not aquire player.\nAre you using the bot right now? check {Config.bot.cmdPrefix}Help for info on how to use the bot.");
                /* Check if the player exists, if it does, check if it is playing.
                     If it is playing, we can stop.*/
                if (player.IsPlaying)
                    await player.StopAsync();
                /* Not sure if this is required as I think player.StopAsync(); clears the queue anyway. */
                foreach (var track in player.Queue.Items)
                    player.Queue.Dequeue();
                return await EmbedHandler.CreateBasicEmbed("Music Stop", "I Have stopped playback & the playlist has been cleared.", Color.Blue);
            }
            catch (Exception ex)
            {
                return await EmbedHandler.CreateErrorEmbed("Music, Stop", ex.ToString());
            }
        }

        /*This is ran when a user uses the command Volume 
            Task Returns a String which is used in the command call. */
        public async Task<string> VolumeAsync(ulong guildId, int volume)
        {
            if (volume >= 150 || volume <= 0)
            {
                return $"Volume must be between 0 and 150.";
            }
            try
            {
                var player = _lavalink.DefaultNode.GetPlayer(guildId);
                await player.SetVolumeAsync(volume);
                return $"Volume has been set to {volume}.";
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Pause(ulong guildId)
        {
            try
            {
                var player = _lavalink.DefaultNode.GetPlayer(guildId);
                if (player.IsPlaying)
                {
                    await player.PauseAsync();
                    return $"**Resumed:** Now Playing {player.CurrentTrack.Title}";
                }

                await player.PauseAsync();
                return $"**Paused:** {player.CurrentTrack.Title}, what a bamboozle.";
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> Resume(ulong guildId)
        {
            try
            {
                var player = _lavalink.DefaultNode.GetPlayer(guildId);
                if (!player.IsPlaying)
                    await player.PauseAsync();
                return $"**Resumed:** {player.CurrentTrack.Title}";
            }
            catch (InvalidOperationException ex)
            {
                return ex.Message;
            }
        }

        public async Task OnFinshed(LavaPlayer player, LavaTrack track, TrackReason reason)
        {
            if (reason is TrackReason.LoadFailed || reason is TrackReason.Cleanup)
                return;
            player.Queue.TryDequeue(out LavaTrack nextTrack);

            if (nextTrack is null)
            {
                await player.StopAsync();
            }
            else
            {
                await player.PlayAsync(nextTrack);
                await player.TextChannel.SendMessageAsync("", false, await EmbedHandler.CreateBasicEmbed("Now Playing", $"[{nextTrack.Title}]({nextTrack.Uri})", Color.Blue));
            }
        }
    }
}
