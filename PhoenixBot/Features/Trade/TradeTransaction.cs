using System;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;

namespace PhoenixBot.Features.Trade
{
    //WIP
    public class TradeTransaction
    {
        public string trader { get; set; }
        public TransactionType transactionType { get; set; }
        public string item { get; set; }
        public string amount { get; set; }
        public string price { get; set; }
    }
    public enum TransactionType
    {
        None,
        Sell,
        Buy
    }
}
