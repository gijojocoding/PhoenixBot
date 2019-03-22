using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using PhoenixBot.Features;
using Victoria;
using Microsoft.Extensions.DependencyInjection;
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
            _client.Ready += OnReady;
            _serviceProvider = new ServiceCollection()
                                .AddSingleton(_client)
                                .AddSingleton(_handler)
                                .AddSingleton(_audioService)
                                .AddSingleton(_lavaSocketClient)
                                .AddSingleton(_lavaRestClient)
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

            try
            {

                //var node = await _lavalink.AddNodeAsync(_client).ConfigureAwait(false);
                //                node.TrackFinished +=  _serviceProvider.GetService<AudioService>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}