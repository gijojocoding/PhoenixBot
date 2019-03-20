using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PhoenixBot.Features.Games.Hunt
{
    public class PhoenixMonsterList
    {
        internal static List<MonsterInfo> monsterInfo;
        private static string monsterFile = "Resources/PhoenixMonsters.json";


        static PhoenixMonsterList()
        {
            if (ListStorage.SaveFileExists(monsterFile))
            {
                monsterInfo = ListStorage.LoadMonsterList(monsterFile);
            }
            else
            {
                monsterInfo = new List<MonsterInfo>();
                SaveList();
            }
        }
        public static void SaveList()
        {
            ListStorage.SaveMonsterList(monsterInfo, monsterFile);
        }

        internal static MonsterInfo GetMonster(string monster)
        {
            var result = from a in monsterInfo
                         where a.MonsterName == monster
                         select a;
            var account = result.FirstOrDefault();
            if (account == null)
            {
                return null;

            }
            return account;

        }

        internal static MonsterInfo CreateMonster(string monster)
        {
            var newMonster = new MonsterInfo()
            {
                MonsterName = monster,
                MonsterType = MonsterType.None,
                MonsterAttack = 1,
                MonsterHP = 20
            };
            monsterInfo.Add(newMonster);
            SaveList();
            return newMonster;
        }
    }
}
