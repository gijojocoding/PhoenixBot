
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using PhoenixBot.Features;
using Victoria;
using Microsoft.Extensions.DependencyInjection;
using PhoenixBot.Modules.Music;

namespace PhoenixBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        IServiceProvider _serviceProvider;

        AudioService _audioService;
        Lavalink _lavalink;
        LavaNode node;

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
           _lavalink = new Lavalink();
            _audioService = new AudioService(_lavalink);
            _client.Ready += OnReady;
            _serviceProvider = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_handler)
                .AddSingleton(_audioService)
                .AddSingleton(_lavalink)
                .AddSingleton(node)
                .BuildServiceProvider();
            


            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            await _client.SetGameAsync(Config.bot.cmdPrefix + "help");
            Global.Client = _client;
            //_client.Ready += EventReminder.EventTimeCheck;

            await _handler.InitializeAsynce(_client, _serviceProvider);
            await Task.Delay(-1);

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

            _audioService.Initialize(node);
            try
            {

                var node = await _lavalink.AddNodeAsync(_client).ConfigureAwait(false);
//                node.TrackFinished +=  _serviceProvider.GetService<AudioService>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}