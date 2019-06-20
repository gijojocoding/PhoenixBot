 using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Discord.WebSocket;
using System.Linq;

namespace PhoenixBot.User_Accounts
{
    public static class UserAccounts
    {
        internal static List<UserAccount> accounts;
        private static string accountsFile = Config.bot.SaveLocation + "accounts.json";


        static UserAccounts()
        {
            if (DataStorageUser.SaveFileExists(accountsFile))
            {
                accounts = DataStorageUser.LoadUserAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<UserAccount>();
                SaveAccounts();
            }
        }
        public static void SaveAccounts()
        {
            DataStorageUser.SaveUserAccounts(accounts, accountsFile);
        }

        public static UserAccount GetAccount(SocketUser user)
        {
            return GetOrCreateAccount(user.Id);
        }
        private static UserAccount GetOrCreateAccount(ulong id)
        {
            var result = from a in accounts
                         where a.ID == id
                         select a;
            var account = result.FirstOrDefault();
            if (account == null)
            {
                //create an account
                account = CreateUserAccount(id);

            }
            return account;

        }

        private static UserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new UserAccount()
            {
                ID = id,
                NumberOfWarnings = 0,
                IsMuted = false

            };
            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}
