using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Features.Trade;

namespace PhoenixBot.Modules.General
{
    [Group("Trade")]
    [Cooldown(5)]
    public class TradeCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Buy"), Alias("b")]
        public async Task TestBuy(string Item, string Amount, [Remainder] string Price)
        {
            Console.WriteLine("Command recieved.");
            var poster = Context.User.ToString();
            var trade = TradeLists.GetTrade(poster);
            trade.transactionType = TransactionType.Buy;
            trade.item = Item;
            trade.amount = Amount;
            trade.price = Price;
            trade.TradeID = Context.Message.Id.ToString();
            TradeLists.tradeInfo.Add(trade);
            TradeLists.SaveTradeList();
            await Context.Channel.SendMessageAsync($"Your trade has been added.");
        }
        [Command("Sell"), Alias("s")]
        public async Task TestSell(string Item, string Amount, [Remainder] string Price)
        {
            Console.WriteLine("Command recieved.");
            var poster = Context.User.ToString();
            var trade = TradeLists.GetTrade(poster);
            trade.transactionType = TransactionType.Sell;
            trade.item = Item;
            trade.amount = Amount;
            trade.price = Price;
            TradeLists.tradeInfo.Add(trade);
            TradeLists.SaveTradeList();
            await Context.Channel.SendMessageAsync($"Your trade has been added.");
        }
        [Command("List")]
        async Task ListTrades(string type)
        {
            ulong id = Context.Channel.Id;
            await TradeListReply.TradeListType(Context.User, type, id);
        }
        [Command("removeTrade")]
        async Task RemoveTrade(string tradeId)
        {
            foreach(var trade in TradeLists.tradeInfo)
            {
                if ((trade.trader == Context.User.ToString() && trade.TradeID == tradeId) || (Context.User == Context.Guild.Owner || RoleCheck.HasChiefRole((SocketGuildUser)Context.User)))
                {
                    TradeLists.tradeInfo.Remove(trade);
                    TradeLists.SaveTradeList();
                }
            }
            await Context.Channel.SendMessageAsync("The trade has been removed.");
        }
        
    }
}
