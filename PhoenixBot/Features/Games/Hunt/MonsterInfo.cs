using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot.Features.Games.Hunt
{
    public class MonsterInfo
    {
        public string MonsterName { get; set; }
        public MonsterType MonsterType { get; set; }
        public float MonsterHP { get; set; }
        public byte MonsterAttack { get; set; }
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
