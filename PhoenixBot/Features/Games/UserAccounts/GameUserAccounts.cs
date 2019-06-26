using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord.WebSocket;

namespace PhoenixBot.Features.Games.UserAccounts
{
    public class GameUserAccounts
    {
        internal static List<GameUserAccount> accounts;
<<<<<<< HEAD
        private static string accountsFile = "HuntAccounts.json";
        private const string configFolder = "Resources";
=======
        private static string accountsFile = "Resources/HuntAccounts.json";

>>>>>>> parent of 0f2d20e... Working on SQL storage

        static GameUserAccounts()
        {
            if (GameUserStorage.SaveFileExists(configFolder + "/" +accountsFile))
            {
                accounts = GameUserStorage.LoadUserAccounts(accountsFile).ToList();
            }
            else
            {
                accounts = new List<GameUserAccount>();
                SaveAccounts();
            }
        }
        public static void SaveAccounts()
        {
            GameUserStorage.SaveUserAccounts(accounts, accountsFile);
        }

        public static GameUserAccount GetAccount(ulong user)
        {
            return GetOrCreateAccount(user);
        }
        private static GameUserAccount GetOrCreateAccount(ulong id)
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

        private static GameUserAccount CreateUserAccount(ulong id)
        {
            var newAccount = new GameUserAccount()
            {
                ID = id,
                PlayerMaxHp = 20,
                PlayerHp = 20,
                PlayerAttack = 4,
                Attribute = Attribute.None,
                HuntingLevel = HuntingLevel.Noob,
                HuntingXP = 0,
                AttributeXP = 0,
                InHunt = false,
                Monster = null,
                MonsterHP = 0,
                MonsterAttack = 0
            };
            accounts.Add(newAccount);
            SaveAccounts();
            return newAccount;
        }
    }
}
