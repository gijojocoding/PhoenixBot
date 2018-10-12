using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace PhoenixBot
{
    class Config
    {
        private const string configFolder = "Resources";
        private const string configFile = "config.json";

        public static BotConfig bot;


        static Config()
        {
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
            if (!File.Exists(configFolder + "/" + configFile))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }
    public struct BotConfig
    {
        public string token;
        public string cmdPrefix;
        //Guild ID
        public ulong guildID;
        //Guild Text Channel ID(s)
        public ulong announcementID;
        public ulong miniGameID;
        public ulong tradeRequestID;
        public ulong buyingTradeID;
        public ulong sellingTradeID;
        public ulong eventID;
        public ulong staffCommandID;
        public ulong adminLogID;
        public ulong diplomatLogID;
        public ulong warningLogID;
        public ulong messageLogID;
        public ulong muteLogID;
        public ulong banKickLogID;
        public ulong changeLogID;
        //Guild Role ID(s)
        public ulong generalID;
        public ulong applicantID;
        public ulong diplomatID;
        public ulong memberID;
        //Guild Debate Channel ID(s)
        public ulong debateVCID;
        public ulong debateTCID;

    }
}
