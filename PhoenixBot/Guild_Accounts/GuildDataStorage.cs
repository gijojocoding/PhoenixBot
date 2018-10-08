using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace PhoenixBot.Guild_Accounts
{
    public static class GuildDataStorage
    {
        public static void SaveGuildAccounts(IEnumerable<GuildAccount> accounts, string filePath)
        {
            // save data
            string json = JsonConvert.SerializeObject(accounts);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<GuildAccount> LoadGuildAccounts(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GuildAccount>>(json);
        }
        public static bool SaveFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}

