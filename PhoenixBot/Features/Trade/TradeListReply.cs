using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using Discord.Webhook;
using Discord.Commands;
using PhoenixBot.Modules.General;
using System.Threading.Tasks;

namespace PhoenixBot.Features.Trade
{
    public class TradeListReply : ModuleBase<SocketCommandContext>
    {
        public static async Task TradeListType(SocketUser user, string type, ulong channelId)
        {
            var channel = Global.Client.GetGuild(Config.bot.guildID).GetTextChannel(channelId);
            if (type == "buy" || type == "buying")
            {
                foreach (var trade in TradeLists.tradeInfo)
                {
                    var embed = new EmbedBuilder();
                    if (trade.transactionType == TransactionType.Buy)
                    {
                        var dmChannel = await user.GetOrCreateDMChannelAsync();
                        embed.WithTitle("Trade:")
                            .AddField("Trader:", trade.trader)
                            .AddField("Buying:", trade.item)
                            .AddField("Amount:", trade.amount)
                            .AddField("Price:", trade.price);
                        await channel.SendMessageAsync("", false, embed.Build());
                        await Task.Delay(500);
                    }
                }
            }
            if (type == "sell" || type == "selling")
            {
                foreach (var trade in TradeLists.tradeInfo)
                {
                    var embed = new EmbedBuilder();
                    if (trade.transactionType == TransactionType.Sell)
                    {
                        var dmChannel = await user.GetOrCreateDMChannelAsync();
                        embed.WithTitle("Trade:")
                            .AddField("Trader:", trade.trader)
                            .AddField("Buying:", trade.item)
                            .AddField("Amount:", trade.amount)
                            .AddField("Price:", trade.price);
                        await channel.SendMessageAsync("", false, embed.Build());
                        await Task.Delay(500);
                    }
                }
            }


        }
    }
}
