using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace PhoenixBot.Features.Trade
{
    public static class TradeDataStorage
    {
        //WIP
        public static void SaveTradeList(List<TradeTransaction[]> tradeInfo, string filePath)
        {
            // Save data
            string json = JsonConvert.SerializeObject(tradeInfo);
            File.WriteAllText(filePath, json);
        }

        public static List<TradeTransaction[]> LoadTradeList(string filePath)
        {
            // Load data
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<TradeTransaction[]>>(json);
        }
        public static bool SaveFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

    }
}


