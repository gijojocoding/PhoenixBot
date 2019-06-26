using System;
using System.Collections.Generic;
using Discord.WebSocket;
using System.Linq;

namespace PhoenixBot.Guild_Accounts
{
    public static class GuildAccounts
    {
        private static List<GuildAccount> accounts;

        private const string configFolder = "Resources";
        private static string accountsFile =  configFolder + "/GuildAccounts.json";

        private const string configFolder = "Resources";
        private static string accountsFile =  configFolder + "/GuildAccounts.json";

        private static string accountsFile = "Resources/GuildAccounts.json";



        static GuildAccounts()
        {
            if (GuildDataStorage.SaveFileExists(accountsFile))
            {
                accounts = GuildDataStorage.LoadGuildAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<GuildAccount>();
                SaveAccounts();
            }
        }
        //LoadGuildAccounts

        public static void SaveAccounts()
        {
            GuildDataStorage.SaveGuildAccounts(accounts, accountsFile);
        }

        public static GuildAccount GetAccount(SocketGuild guild)
        {
            return GetOrCreateAccount(guild.Id);
        }
        private static GuildAccount GetOrCreateAccount(ulong id)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;
            var account = result.FirstOrDefault();
            if (account == null)
            {
                //create an account
                account = CreateGuildAccount(id);

            }
            return account;

        }

        private static GuildAccount CreateGuildAccount(ulong id)
        {
            var newAccount = new GuildAccount()
            {
                ID = id,
                NumberOfTempChannels = 0,
                DayChecked = DateTime.Now,
                DebateRunning = false,
                StickHolderId = 0,
                AllowSummoning = false,
                GuildEvent1Running = false,
                GuildEvent2Running = false,
                TownEvent1Running = false,
                TownEvent2Running = false,
                GroupEventRunning = false,
                TownRaffleRunning = false,
                TownRaffleInfo = null,
                GuildRaffleRunning = false,
                GuildRaffleInfo = null
            };
            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}