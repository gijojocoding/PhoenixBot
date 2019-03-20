using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace PhoenixBot.Features.Games.UserAccounts
{
    public class GameUserStorage
    {
        public static void SaveUserAccounts(IEnumerable<GameUserAccount> accounts, string filePath)
        {
            // save data
            string json = JsonConvert.SerializeObject(accounts);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<GameUserAccount> LoadUserAccounts(string filePath)
        {
            // Load data
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GameUserAccount>>(json);
        }

        public static bool SaveFileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
