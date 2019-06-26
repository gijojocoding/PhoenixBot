using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace PhoenixBot
{
    class ChannelIds
    {
        private const string configFolder = "Resources";
        private const string configFile = "channels.json";

        public static ChannelConfig channels;


        static ChannelIds()
        {
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
            if (!File.Exists(configFolder + "/" + configFile))
            {
                channels = new ChannelConfig();
                string json = JsonConvert.SerializeObject(channels, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                channels = JsonConvert.DeserializeObject<ChannelConfig>(json);
            }
        }
    }
    public struct ChannelConfig
    {
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
        public ulong generalID;
        //Guild Debate Channel ID(s)
        public ulong debateVCID;
        public ulong debateTCID;
        //Request ID(s)
        public ulong requestID;

    }
}
