using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot.Features.Games.UserAccounts
{
    public class GameUserAccount
    {
        // Player ID
        public ulong ID { get; set; }
        // Player Info
        public int PlayerMaxHp { get; set; }
        public float PlayerHp { get; set; }
        public float PlayerAttack { get; set; }
        public HuntingLevel HuntingLevel { get; set; }
        public Attribute Attribute { get; set; }
        public float HuntingXP { get; set; }
        public float AttributeXP { get; set; }
        // Current Hunt Info
        public bool InHunt { get; set; }
        public string Monster { get; set; }
        public MonsterType monsterType { get; set; }
        public float MonsterHP { get; set; }
        public float MonsterAttack { get; set; }
    }
    public enum HuntingLevel : byte
    {
        Noob = 0,
        Novice = 1,
        Intermediate = 2,
        Master = 3,
        GrandMaster = 4,
        Phoenix = 5
    }
    public enum Attribute : byte
    {
        None = 0,
        Demon = 1,
        Angel = 2,
        Life = 3,
        Nullifer = 4
    }
    public enum MonsterType : byte
    {
        None = 0,
        Demon = 1,
        Angel = 2,
        Undead = 3,
        Resurrecter = 4,
        All = 5
    }
}
