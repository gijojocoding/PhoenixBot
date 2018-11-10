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
        private static string tradeListFile = "Resources/ListOfTrades.json";



        static TradeLists()
        {
            if (TradeDataStorage.SaveFileExists(tradeListFile))
            {
                tradeInfo = TradeDataStorage.LoadTradeList(tradeListFile).ToList();
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

        public static TradeTransaction GetTrade(string user)
        {
            return GetorCreateTrade(user);
        }
        private static TradeTransaction GetorCreateTrade(string user)
        {
            var result = from a in tradeInfo
                         where a.trader == user
                         select a;
            var trade = result.FirstOrDefault();
            if (trade == null)
            {
                //create a trade
                trade = CreateTrade(user);

            }
            return trade;

        }

        private static TradeTransaction CreateTrade(string user)
        {
            var newTrade = new TradeTransaction
            {
                
                trader = user,
                transactionType = TransactionType.none,
                item = "Null",
                amount = "Null",
                price = "Null"

            };
            //tradeInfo.Add(newTrade);
            //SaveTradeList();
            return newTrade;

        }
    }
}
