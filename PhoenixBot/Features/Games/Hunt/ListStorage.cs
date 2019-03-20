using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
namespace PhoenixBot.Features.Games.Hunt
{
    public static class ListStorage
    {
        public static void SaveMonsterList(IEnumerable<MonsterInfo> tradeInfo, string filePath)
        {
            // Save data
            string json = JsonConvert.SerializeObject(tradeInfo);
            File.WriteAllText(filePath, json);
        }

        public static List<MonsterInfo> LoadMonsterList(string filePath)
        {
            // Load data
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<MonsterInfo>>(json);
        }
        public static bool SaveFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
