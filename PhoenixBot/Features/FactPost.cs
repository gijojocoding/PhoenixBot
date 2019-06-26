using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace PhoenixBot.Features
{
    class FactPost

    {
        private const string configFolder = "Resources";
        private const string configFile = "Random.json";

        public static RPost post;
        static FactPost()
        {
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
            if (!File.Exists(configFolder + "/" + configFile))
            {
                post = new RPost();
                string json = JsonConvert.SerializeObject(post, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                post = JsonConvert.DeserializeObject<RPost>(json);
            }
        }
    }
    public struct RPost
    {
        public string one;
        public string two;
        public string three;
        public string four;
        public string five;
    }
}
