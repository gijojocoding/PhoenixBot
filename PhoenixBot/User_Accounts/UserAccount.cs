using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot.User_Accounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }

        public uint XP { get; set; }
        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(XP / 75);
            }
        }

        public uint HP { get; set; }
        public uint MaxHp { get; set; }

        public uint Points { get; set; }

        public uint NumberOfWarnings { get; set; }


        public bool IsAlive { get; set; }

        public bool IsMuted { get; set; }

        public DateTime dailyClaim { get; set; }

        public DateTime MuteTime { get; set; }

        public DateTime LastMessage { get; set; }

    }
}
