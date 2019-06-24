using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot
{
    public class UserAccountModel
    {
        public ulong Id { get; set; }

        public int NumberOfWarnings { get; set; } = 0;

        public int IsMuted { get; set; } = 0;



    }

}
