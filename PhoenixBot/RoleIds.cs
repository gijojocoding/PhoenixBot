using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Discord.WebSocket;

namespace PhoenixBot
{
    class RoleIds
    {
        private const string configFolder = "Resources";
        private const string configFile = "RoleIds.json";

        public static RoleConfig roles;


        static RoleIds()
        {
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);
            if (!File.Exists(configFolder + "/" + configFile))
            {
                roles = new RoleConfig();
                string json = JsonConvert.SerializeObject(roles, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                string json = File.ReadAllText(configFolder + "/" + configFile);
                roles = JsonConvert.DeserializeObject<RoleConfig>(json);
            }
        }
    }
    public struct RoleConfig
    {
        //Guild Role ID(s)
        public ulong generalID;
        public ulong applicantID;
        public ulong diplomatID;
        public ulong guildMemberID;
        public ulong townMemberID;

    }
}
