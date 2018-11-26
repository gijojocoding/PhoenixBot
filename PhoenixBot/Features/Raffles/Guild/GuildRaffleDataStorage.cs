using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PhoenixBot.Features.Raffles.Guild
{
    public class GuildRaffleDataStorage
    {
        public static void SaveGuildRaffleList(IEnumerable<GuildRaffle> townRaffles, string filePath)
        {
            // Save data
            string json = JsonConvert.SerializeObject(townRaffles);
            File.WriteAllText(filePath, json);
        }

        public static List<GuildRaffle> LoadGuildRaffleList(string filePath)
        {
            // Load data
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GuildRaffle>>(json);
        }
        public static bool SaveFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}
