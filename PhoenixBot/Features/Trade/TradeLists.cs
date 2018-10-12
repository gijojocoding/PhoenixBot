using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Discord.WebSocket;
using System.Linq;

namespace PhoenixBot.Features.Trade
{
    public static class TradeLists
    {
        //WIP
        public static List<TradeTransaction> tradeInfo;
        private static string tradeListFile = "Resources/tradeInfoList.json";



        static void TradeInfo()
        {
            if (TradeDataStorage.SaveFileExists(tradeListFile))
            {
                tradeInfo = TradeDataStorage.LoadTradeList(tradeListFile);
            }
            else
            {
                tradeInfo = new List<TradeTransaction>();
                SaveTradeList();
            }

        }
        public static void SaveTradeList()
        {
            TradeDataStorage.SaveTradeList(tradeInfo, tradeListFile);
        }
        public static void LoadTradeList()
        {
            TradeDataStorage.LoadTradeList(tradeListFile);
        }
        public static TradeTransaction GetTrade(SocketGuildUser user)
        {
            return CreateTrade(user);
        }
        public static TradeTransaction CreateTrade(SocketGuildUser user)
        {
            var newTrade = new TradeTransaction()
            {
                trader = user,
                transactionType = TransactionType.none,
                item = "null",
                amount = 0,
                price = "null"

            };
            tradeInfo.Add(newTrade);
            SaveTradeList();
            return newTrade;

        }
    }
}
