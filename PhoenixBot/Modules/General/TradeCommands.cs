using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;
using PhoenixBot.Features.Trade;

namespace PhoenixBot.Modules.General
{
    [Group("Trade")]
    [Cooldown(10)]
    public class TradeCommands : ModuleBase<SocketCommandContext>
    {
        //private TransactionType selling;
        //private TransactionType buying;

        [Command("buy")]
        public async Task BuyTrade(string itemName, uint howMany, [Remainder] string Price)
        {
            /*SocketGuildUser user = (SocketGuildUser)Context.User;
            var item = itemName;
            var amount = howMany;
            var price = Price; */
            var postChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.buyingTradeID);
            var info = new TradeTransaction
            { //{user, TransactionType.Buy, item, amount, price };
                trader = (SocketGuildUser)Context.User,
            transactionType = TransactionType.Buy,
            item = itemName,
            amount = howMany,
            price = Price };
        TradeLists.tradeInfo.Add(info);
            TradeLists.SaveTradeList();
            /*info[0].trader = (SocketGuildUser)Context.User;
            info[1].transactionType = TransactionType.Buy;
            info[2].item = itemName;
            info[3].amount = howMany;
            info[4].price = Price;
            TradeLists.tradeInfo.Add(info);*/
            TradeLists.SaveTradeList();
            await postChannel.SendMessageAsync("Post created.");
        }
        [Command("sell")]
        public async Task SellTrade(string itemName, uint howMany, [Remainder] string Price)
        {
            var postChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.sellingTradeID);
            var info = new TradeTransaction
            { //{user, TransactionType.Buy, item, amount, price };
                trader = (SocketGuildUser)Context.User,
                transactionType = TransactionType.Buy,
                item = itemName,
                amount = howMany,
                price = Price
            };
            TradeLists.tradeInfo.Add(info);
            TradeLists.SaveTradeList();
            await postChannel.SendMessageAsync("Post created.");
        }
        [Command("list")]
        public async Task ListTrades(TransactionType wanted)
        {
            var buyingChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.buyingTradeID);
            var sellingChannel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.sellingTradeID);
            var embed = new EmbedBuilder();
            embed.WithTitle("Buying Trade:");
            foreach(var trade in TradeLists.tradeInfo)
            {
                if(trade.transactionType == wanted)
                {
                    embed.WithTitle($"{wanted} Trade:")
                        .AddField("Trader:", trade.trader)
                        .AddField("Item:", trade.item)
                        .AddField("Amount:", trade.amount)
                        .AddField("Price:", trade.price);
                    if(wanted == TransactionType.Buy)
                    {
                        await buyingChannel.SendMessageAsync("", false, embed);
                    }
                    if (wanted == TransactionType.Buy)
                    {
                        await sellingChannel.SendMessageAsync("", false, embed);
                    }
                }
            }
        }
    }

    /* Works but errored out for the WIP Features -> Trade Files.
    [Group("Trade")]
    [Cooldown(5)]
    public class TradeCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Buy")]
        [Summary("Posts an item your looking to buy in the `Buying` Channel.")]
        public async Task TradeRequest(string type, string itemName, string howMany, [Remainder] string price)
        {
            if (Context.Message.Channel.Id == Config.bot.buyingTradeID)
            {
                var buyEmbed = new EmbedBuilder();
                buyEmbed.WithTitle("Buying Trade Request Info")
                    .AddField("Buyer:", $"{Context.User.Mention}")
                    .AddField("Item Looking for:", $"{itemName}")
                    .AddField("Qty:", $"{howMany}")
                    .AddField("Price:", $"{price}");

                await Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.buyingTradeID).SendMessageAsync("", false, buyEmbed);
            }
            else
            {
                var messages = await Context.Channel.GetMessagesAsync(1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messages);
                await Context.Channel.SendMessageAsync("404 Wrong channel. Please post in the Trade Request Cahnnel.");
            }
        }
        [Command("Sell")]
        [Summary("Posts an item your looking to sell in the `Selling` Channel.")]
        public async Task SellRequest(string type, string itemName, string howMany, [Remainder] string price)
        {
            if (Context.Message.Channel.Id != Config.bot.tradeRequestID)
            {
                var sellEmbed = new EmbedBuilder();
                sellEmbed.WithTitle("Selling Trade Request Info")
                    .AddField("Buyer:", $"{Context.User.Mention}")
                    .AddField("Item being sold:", $"{itemName}")
                    .AddField("Qty:", $"{howMany}")
                    .AddField("Price:", $"{price}");

                await Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(Config.bot.sellingTradeID).SendMessageAsync("", false, sellEmbed);
            }else
            {
                var messages = await Context.Channel.GetMessagesAsync(1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messages);
                await Context.Channel.SendMessageAsync("404 Wrong channel. Please post in the Trade Request Cahnnel.");

            }
        }

    }*/
}
