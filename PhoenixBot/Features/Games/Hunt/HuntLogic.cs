using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot.Features.Games.Hunt
{
    public class HuntLogic
    {
        private static float AttributeCheck(string UserAttr, string MonsterAttr)
        {
            float bonus = 1;
            if (UserAttr == "None" && MonsterAttr == "None")
            {
                return bonus;
            }
            else if ((UserAttr == "Demon" || UserAttr == "Angel") && (MonsterAttr == "Angel" || MonsterAttr == "Demon"))
            {
                if (UserAttr == "Demon" && MonsterAttr == "Angel")
                {
                    bonus = 0.5f;
                    return bonus;
                }
                if (UserAttr == "Angel" && MonsterAttr == "Demon")
                {
                    bonus = 1.3f;
                    return bonus;
                }
                else
                {
                    return bonus;
                }
            }
            else if ((UserAttr == "Life" || UserAttr == "Nullifer") && (MonsterAttr == "Undead" || MonsterAttr == "Resurrecter"))
            {
                if (UserAttr == "Life" && MonsterAttr == "Undead")
                {
                    bonus = 1.2f;
                    return bonus;
                }
                if (UserAttr == "Nullifer" && MonsterAttr == "Resurrector")
                {
                    bonus = 0.8f;
                    return bonus;
                }
                else
                {
                    return bonus;
                }

            }
            else
            {
                return bonus;
            }
        }
        internal static MonsterInfo HuntingLogic(UserAccounts.GameUserAccount gameUser, byte level)
        {
            Console.WriteLine("In Noob Hunting.");

            var userHp = (float)gameUser.PlayerMaxHp;
            int count;
            Random list = new Random();
            int pick;
            MonsterInfo monsterPicked;
            if (level == 0)
            {
                Console.WriteLine("In noob pick.");
                count = NoobMonsterList.monsterInfo.Count;
                pick = list.Next(0, count);
                monsterPicked = (MonsterInfo)NoobMonsterList.monsterInfo.ToArray().GetValue(pick);
                //monsterPicked = NoobMonsterList.GetMonster(monster);
                Console.WriteLine($"{monsterPicked.MonsterName}");
            }
            else if (level == 1)
            {
                count = NoviceMonsterList.monsterInfo.Count;
                pick = list.Next(0, count);
                string monster = NoviceMonsterList.monsterInfo.ToArray().GetValue(pick).ToString();
                monsterPicked = NoviceMonsterList.GetMonster(monster);
            }
            else if (level == 2)
            {
                count = _IntermediateMonsterList.monsterInfo.Count;
                pick = list.Next(0, count);
                string monster = _IntermediateMonsterList.monsterInfo.ToArray().GetValue(pick).ToString();
                monsterPicked = _IntermediateMonsterList.GetMonster(monster);
            }
            else if (level == 3)
            {
                count = MasterMonsterList.monsterInfo.Count;
                pick = list.Next(0, count);
                string monster = MasterMonsterList.monsterInfo.ToArray().GetValue(pick).ToString();
                monsterPicked = MasterMonsterList.GetMonster(monster);
            }
            else if (level == 4)
            {
                count = GMMonsterList.monsterInfo.Count;
                pick = list.Next(0, count);
                string monster = GMMonsterList.monsterInfo.ToArray().GetValue(pick).ToString();
                monsterPicked = GMMonsterList.GetMonster(monster);
            }
            else if (level == 5)
            {
                count = PhoenixMonsterList.monsterInfo.Count;
                pick = list.Next(0, count);
                string monster = PhoenixMonsterList.monsterInfo.ToArray().GetValue(pick).ToString();
                monsterPicked = PhoenixMonsterList.GetMonster(monster);
            }
            else 
            {
                return null;
            }
            var monsterHP = monsterPicked.MonsterHP;
            var monsterAtt = monsterPicked.MonsterAttack;
            var monsterAttr = monsterPicked.MonsterType;
            if (monsterAttr.ToString() == "All")
            {
                Random attr = new Random();
                var attrType = attr.Next(0, 4);
                if (attrType == 0)
                {
                    monsterAttr = MonsterType.None;
                } else if (attrType == 1)
                {
                    monsterAttr = MonsterType.Demon;
                }
                else if (attrType == 2)
                {
                    monsterAttr = MonsterType.Angel;
                }
                else if (attrType == 3)
                {
                    monsterAttr = MonsterType.Undead;
                }
                else if (attrType == 4)
                {
                    monsterAttr = MonsterType.Resurrecter;
                }
            }
            var UserAttr = gameUser.Attribute.ToString();
            float strongAgainst = AttributeCheck(UserAttr, monsterAttr.ToString());
            //while (userHp != 0 || monsterHP != 0)
            do
            {
                Random playerAttack = new Random();
                var pAttack = playerAttack.Next(2, (int)gameUser.PlayerAttack);
                Random MonsterAttackRol = new Random();
                var mAttack = MonsterAttackRol.Next(1, monsterAtt);
                Random mDeffenceRol = new Random();
                var mDef = mDeffenceRol.Next(0, 10);
                Random pDeffenceRol = new Random();
                var pDef = mDeffenceRol.Next(0, 10);
                var playerDamage = ((pAttack * strongAgainst) - mDef);
                if (playerDamage < 0)
                {
                    playerDamage = 0;
                }
                if (playerDamage >= monsterHP)
                {
                    playerDamage = monsterHP;
                }
                monsterHP = monsterHP - playerDamage;
                if (monsterHP == 0)
                {
                    return monsterPicked;
                }
                var monsterDamage = (mAttack - (strongAgainst * pDef));
                if(monsterDamage < 0)
                {
                    monsterDamage = 0;
                }
                if (monsterDamage >= userHp)
                {
                    monsterDamage = userHp;
                }
                userHp = userHp - monsterDamage;
                if (userHp == 0)
                {
                    gameUser.PlayerHp = 0;
                    UserAccounts.GameUserAccounts.SaveAccounts();
                    return monsterPicked;
                }
            }
            while (userHp != 0 || monsterHP != 0);
            return monsterPicked;
        }
    

    }
}