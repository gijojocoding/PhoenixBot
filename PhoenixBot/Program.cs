using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using PhoenixBot.Features;
using Victoria;

namespace PhoenixBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        AudioService _audioService;
        Lavalink _lavalink;
        
        
        

        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _client.Log += Log;

            _handler = new CommandHandler();
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            await _client.SetGameAsync(Config.bot.cmdPrefix + "help");
            Global.Client = _client;
            _client.Ready += EventReminder.EventTimeCheck;
            _handler = new CommandHandler();
            await _handler.InitializeAsynce(_client);
            await Task.Delay(-1);

        }
        public async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);

        }
        private async Task OnReady()
        {
            await EventReminder.EventTimeCheck();
            var node = await _lavalink.AddNodeAsync(_client).ConfigureAwait(false);
            _audioService.Initialize(node);
        }
    }
}
