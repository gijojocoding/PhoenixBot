﻿using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using PhoenixBot.Features;
using Victoria;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD

using System.Data;
using MySql.Data;
=======
using System.Reflection;
using Discord.Commands;
using Victoria.Entities;
>>>>>>> parent of 0f2d20e... Working on SQL storage

using System.Reflection;
using Discord.Commands;


namespace PhoenixBot.Modules.Music
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        IServiceProvider _serviceProvider;
        AudioService _audioService;
        LavaSocketClient _lavaSocketClient;
        LavaRestClient _lavaRestClient;

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _handler = new CommandHandler();
            _lavaSocketClient = new LavaSocketClient();
            _lavaRestClient = new LavaRestClient();
            _audioService = new AudioService(_lavaSocketClient, _lavaRestClient);
            _serviceProvider = new ServiceCollection()
                               .AddSingleton(_client)
                               .AddSingleton(_handler)
                               .AddSingleton(_audioService)
                               .AddSingleton(_lavaSocketClient)
                               .AddSingleton(_lavaRestClient)
                               .BuildServiceProvider();
            _client.Ready += OnReady;
            _lavaSocketClient.OnTrackFinished += OnTrackFinishedAsync;
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            await _client.SetGameAsync(Config.bot.cmdPrefix + "help");
            Global.Client = _client;

            await _handler.InitializeAsynce(_client, _serviceProvider);
            await Task.Delay(-1);
        }
<<<<<<< HEAD
        private async Task ConnectedLog()
        {
            Console.WriteLine($"Connected at: {DateTime.Now}");
=======

        private async Task OnTrackFinishedAsync(LavaPlayer player, LavaTrack track, TrackEndReason reason)
        {
            if (!reason.ShouldPlayNext()) return;
            if (!player.Queue.TryDequeue(out var item) || !(item is LavaTrack nextTrack))
                return;
            await player.PlayAsync(nextTrack);
            

>>>>>>> parent of 0f2d20e... Working on SQL storage
        }
        private async Task ClientReady()
        {
            await EventReminder.EventTimeCheck();
        }

        public async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);

        }
        private async Task OnReady()
        {
            await EventReminder.EventTimeCheck();
<<<<<<< HEAD

        }
        public void CloseBot()
        {
            var code = Environment.ExitCode;
            Environment.Exit(code);
            try
            {
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
=======
            await _lavaSocketClient.StartAsync(_client, new Configuration {
                LogSeverity = LogSeverity.Verbose,
                ReconnectAttempts = 3,
                ReconnectInterval = TimeSpan.FromSeconds(5)
            });
>>>>>>> parent of 0f2d20e... Working on SQL storage
        }
    }
}