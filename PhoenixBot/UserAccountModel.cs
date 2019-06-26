using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot
{
    public class UserAccountModel
    {
        public ulong Id { get; set; }

        public byte NumberOfWarnings { get; set; } = 0;

        public byte IsMuted { get; set; } = 0;



    }

}
