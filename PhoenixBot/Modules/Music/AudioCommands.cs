using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Victoria;


namespace PhoenixBot.Modules.Music
{
    [RequireOwner]
    public class AudioCommands : ModuleBase<SocketCommandContext>
    {
        public  AudioService AudioService { get; set; }
        [Command("Play", RunMode = RunMode.Async)]
        public async Task Play([Remainder]string search)
        {
            await AudioService.JoinOrPlayAsync((SocketGuildUser)Context.User, Context.Channel, Context.Guild.Id, search);
        }


        [Command("Stop")]
        public async Task Stop()
            => await ReplyAsync("", false, await AudioService.StopAsync(Context.Guild.Id));

        [Command("List")]
        public async Task List()
            => await ReplyAsync("", false, await AudioService.ListAsync(Context.Guild.Id));

        [Command("Skip")]
        public async Task Delist(string id = null)
            => await ReplyAsync("", false, await AudioService.SkipTrackAsync(Context.Guild.Id));

        [Command("Volume")]
        public async Task Volume(int volume)
            => await ReplyAsync(await AudioService.VolumeAsync(Context.Guild.Id, volume));

        [Command("Pause")]
        public async Task Pause()
            => await ReplyAsync(await AudioService.Pause(Context.Guild.Id));

        [Command("Resume")]
        public async Task Resume()
            => await ReplyAsync(await AudioService.Pause(Context.Guild.Id));
    }
}

