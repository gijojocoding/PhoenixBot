using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PhoenixBot.Features.Raffles.Town
{
    public class TownRaffleDataStorage
    {
        public static void SaveTownRaffleList(IEnumerable<TownRaffle> townRaffles, string filePath)
        {
            // Save data
            string json = JsonConvert.SerializeObject(townRaffles);
            File.WriteAllText(filePath, json);
        }

        public static List<TownRaffle> LoadTownRaffleList(string filePath)
        {
            // Load data
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<TownRaffle>>(json);
        }
        public static bool SaveFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}
