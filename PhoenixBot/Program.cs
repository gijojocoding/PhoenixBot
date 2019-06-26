﻿using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Discord;
using PhoenixBot.Features;
using Microsoft.Extensions.DependencyInjection;

using System.Data;
using MySql.Data;

using System.Reflection;
using Discord.Commands;


namespace PhoenixBot.Modules.Music
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        IServiceProvider _serviceProvider;
        //IDbConnection _dbConnection;

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
            //_dbConnection = DataBaseHandler.GetConnection();
            _serviceProvider = new ServiceCollection()
                               .AddSingleton(_client)
                               //.AddSingleton(_dbConnection)
                               .AddSingleton(_handler)
                               .BuildServiceProvider();
            _client.Ready += OnReady;
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();
            await _client.SetGameAsync(Config.bot.cmdPrefix + "help");
            Global.Client = _client;
            _client.Connected += ConnectedLog;

            await _handler.InitializeAsynce(_client, _serviceProvider);
            await Task.Delay(-1);
        }
        private async Task ConnectedLog()
        {
            Console.WriteLine($"Connected at: {DateTime.Now}");
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
        }
    }
}