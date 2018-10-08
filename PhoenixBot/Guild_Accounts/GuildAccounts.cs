﻿using System;
using System.Collections.Generic;
using Discord.WebSocket;
using System.Linq;

namespace PhoenixBot.Guild_Accounts
{
    public static class GuildAccounts
    {
        private static List<GuildAccount> accounts;
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
                PointsForHealthIncrease = 500,
                PointsForHealth = 50,
                PointsForRevive = 20,
                HourWarning = false,
                TenMinuteWarning = false,
                EventRunning = false,
                DayChecked = DateTime.Now,
                DebateRunning = false,
                StickHolder = 0
            };
            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}