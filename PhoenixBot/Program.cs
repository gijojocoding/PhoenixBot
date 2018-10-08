using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using PhoenixBot.Features;

namespace PhoenixBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;

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
            await _client.SetGameAsync("!help");
            Global.Client = _client;
            _client.Ready += EventReminder.EventTimeCheck;
            _handler = new CommandHandler();
            await _handler.InitializeAsynce(_client);
            await Task.Delay(-1);

        }
        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);

        }
    }
}
