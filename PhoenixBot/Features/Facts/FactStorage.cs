using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace PhoenixBot.Features.Facts
{
    class FactStorage
    {
        private const string Folder = "Resources";
        private const string FileName = "Random.json";
        private const string FilePath = Folder + "/" + FileName;

        private static Dictionary<string, string> facts;

        static FactStorage()
        {
            string json = File.ReadAllText(FilePath);
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            facts = data.ToObject<Dictionary<string, string>>();
        }
        public static string GetFacts(string key)
        {
            if (facts.ContainsKey(key)) return facts[key];
            return null;
        }
        public static int AmountOfFacts()
        {
            var numberOfFacts = facts.Count;
            return numberOfFacts;
        }

    }
}
