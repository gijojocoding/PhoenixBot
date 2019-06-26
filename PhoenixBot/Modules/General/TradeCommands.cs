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
        [Command("null")]
        [Summary("Posts an item your looking to buy in the `Buying` Channel.")]
        public async Task TradeRequest(string itemName, string howMany, [Remainder] string price)
        {
            if (Context.Message.Channel.Id == ChannelIds.channels.tradeRequestID)
            {
                var buyEmbed = new EmbedBuilder();
                buyEmbed.WithTitle("Buying Trade Request Info")
                    .AddField("Buyer:", $"{Context.User.Mention}")
                    .AddField("Item Looking for:", $"{itemName}")
                    .AddField("Qty:", $"{howMany}")
                    .AddField("Price:", $"{price}");

                await Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.buyingTradeID).SendMessageAsync("", false, buyEmbed.Build());
            }
            else
            {
                //var messages = await Context.Channel.GetMessagesAsync(1).Flatten();
                await Context.Channel.DeleteMessageAsync(1);
                await Context.Channel.SendMessageAsync("Error 404: Wrong channel. Please post in the Trade Request Cahnnel.");
            }
        }
        [Command("null")]
        [Summary("Posts an item your looking to sell in the `Selling` Channel.")]
        public async Task SellRequest(string itemName, string howMany, [Remainder] string price)
        {
            if (Context.Message.Channel.Id == ChannelIds.channels.tradeRequestID)
            {
                var sellEmbed = new EmbedBuilder();
                sellEmbed.WithTitle("Selling Trade Request Info")
                    .AddField("Buyer:", $"{Context.User.Mention}")
                    .AddField("Item being sold:", $"{itemName}")
                    .AddField("Qty:", $"{howMany}")
                    .AddField("Price:", $"{price}");

                await Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(ChannelIds.channels.sellingTradeID).SendMessageAsync("", false, sellEmbed.Build());
            }
            else
            {
                //var messages = await Context.Channel.GetMessagesAsync(1).Flatten();
                await Context.Channel.DeleteMessageAsync(1);
                await Context.Channel.SendMessageAsync("Error 404: Wrong channel. Please post in the Trade Request Cahnnel.");

            }
        }
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
            //TradeLists.tradeInfo.Add(trade);
            TradeLists.SaveTradeList();
            await Context.Channel.SendMessageAsync($"Your trade has been added.");
        }
        [Command("List")]
        async Task ListTrades(string type)
        {
            ulong id = Context.Channel.Id;
            await TradeListReply.TradeListType(Context.User, type, id);
        }
        [Command("sellTradeRemove"), Alias("STR")]
        async Task RemoveSellingTrade(string item)
        {
            foreach(var trade in TradeLists.tradeInfo)
            {
                if(trade.trader == Context.User.ToString() && trade.transactionType == TransactionType.Sell && trade.item == item)
                {
                    TradeLists.tradeInfo.Remove(trade);
                    TradeLists.SaveTradeList();
                }
            }
            await Context.Channel.SendMessageAsync("Your trade has been removed.");
        }
        
    }
}
