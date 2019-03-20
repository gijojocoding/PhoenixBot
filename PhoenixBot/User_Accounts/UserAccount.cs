using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot.User_Accounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }
        public uint LoyaltyPoints { get; set; }



        public byte NumberOfWarnings { get; set; }

        public bool IsMuted { get; set; }

        public DateTime MuteTime { get; set; }

        public DateTime LastMessage { get; set; }

    }
}
