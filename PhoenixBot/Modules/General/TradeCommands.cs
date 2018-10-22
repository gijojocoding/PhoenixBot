﻿using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace PhoenixBot.Modules.General
{

    [Group("Trade")]
    [Cooldown(5)]
    public class TradeCommands : ModuleBase<SocketCommandContext>
    {
        [Command("Buy")]
        [Summary("Posts an item your looking to buy in the `Buying` Channel.")]
        public async Task TradeRequest(string type, string itemName, string howMany, [Remainder] string price)
        {
            if (Context.Message.Channel.Id == ChannelIds.channels.buyingTradeID)
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
                await Context.Channel.SendMessageAsync("404 Wrong channel. Please post in the Trade Request Cahnnel.");
            }
        }
        [Command("Sell")]
        [Summary("Posts an item your looking to sell in the `Selling` Channel.")]
        public async Task SellRequest(string type, string itemName, string howMany, [Remainder] string price)
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
                await Context.Channel.SendMessageAsync("404 Wrong channel. Please post in the Trade Request Cahnnel.");

            }
        }
    }
}
