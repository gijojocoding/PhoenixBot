using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixBot.Features.Raffles.Guild
{
    public static class GuildRaffles
    {
        //WIP
        public static List<GuildRaffle> guildRaffle;
        private static string guildRaffleFile = "Resources/GuildRaffle.json";



        static GuildRaffles()
        {
            if (GuildRaffleDataStorage.SaveFileExists(guildRaffleFile))
            {
                guildRaffle = GuildRaffleDataStorage.LoadGuildRaffleList(guildRaffleFile).ToList();
            }
            else
            {
                guildRaffle = new List<GuildRaffle>();
                SaveGuildRaffleList();
            }

        }

        public static void SaveGuildRaffleList()
        {
            GuildRaffleDataStorage.SaveGuildRaffleList(guildRaffle, guildRaffleFile);
        }

        public static void LoadGuildRaffleList()
        {
            GuildRaffleDataStorage.LoadGuildRaffleList(guildRaffleFile);
        }

        public static GuildRaffle GetGuildRaffleTicket(int number)
        {
            var result = from a in guildRaffle
                         where a.TicketNumber == number
                         select a;
            var ticket = result.FirstOrDefault();
            if (ticket == null)
            {
                return null;

            }
            return ticket;

        }

        public static GuildRaffle CreateGuildRaffleTicket(ulong id)
        {
            var newTicket = new GuildRaffle
            {
                HolderId = id,
                TicketNumber = 0
            };
            //tradeInfo.Add(newTrade);
            //SaveTradeList();
            return newTicket;

        }
    }
}
