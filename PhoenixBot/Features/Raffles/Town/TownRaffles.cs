using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoenixBot.Features.Raffles.Town
{
    public static class TownRaffles
    {
        //WIP
        public static List<TownRaffle> townRaffle;
        private static string townRaffleFile = "Resources/TownRaffle.json";



        static TownRaffles()
        {
            if (TownRaffleDataStorage.SaveFileExists(townRaffleFile))
            {
                townRaffle = TownRaffleDataStorage.LoadTownRaffleList(townRaffleFile).ToList();
            }
            else
            {
                townRaffle = new List<TownRaffle>();
                SaveTownRaffle();
            }

        }

        public static void SaveTownRaffle()
        {
            TownRaffleDataStorage.SaveTownRaffleList(townRaffle, townRaffleFile);
        }

        public static void LoadTownRaffle()
        {
            TownRaffleDataStorage.LoadTownRaffleList(townRaffleFile);
        }

        public static TownRaffle GetTownRaffle(int ticketNumber)
        {
            var result = from a in townRaffle
                         where a.TicketNumber == ticketNumber
                         select a;
            var ticket = result.FirstOrDefault();
            if (ticket == null)
            {
                return null;
            }
            return ticket;

        }

        private static TownRaffle CreateTownRaffleTicket(ulong id)
        {
            var newTicket = new TownRaffle
            {
                HolderID = id,
                TicketNumber = 0
            };
            //tradeInfo.Add(newTrade);
            //SaveTradeList();
            return newTicket;

        }
    }
}
