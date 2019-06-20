using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace PhoenixBot
{
    class ChangeLog
    {
        private static string folder = Config.bot.SaveLocation;
        private const string changeLogFile = "ChangeLog.json";

        public static BotChangeLog log;


        static ChangeLog()
        {
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            if (!File.Exists(folder + "/" + changeLogFile))
            {
                log = new BotChangeLog();
                string json = JsonConvert.SerializeObject(log, Formatting.Indented);
                File.WriteAllText(folder + "/" + changeLogFile, json);
            }
            else
            {
                string json = File.ReadAllText(folder + "/" + changeLogFile);
                log = JsonConvert.DeserializeObject<BotChangeLog>(json);
            }
        }
    }
    public struct BotChangeLog
    {
        public string Version;
        public string Change1;
        public string Change2;
        public string Change3;
        public string Change4;
        public string Change5;
        public string Planned1;
        public string Planned2;
        public string Planned3;
    }
}
