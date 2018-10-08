using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot
{
    internal class Global
    {
        internal static DiscordSocketClient Client { get; set; }
        internal static ulong MessageIdToTrack { get; set; }
        public static SocketGuildUser PlayerOneId { get; set; }
        internal static int PlayerOneRoll { get; set; }
        internal static SocketGuildUser PlayerTwoId { get; set; }
        internal static int PlayerTwoRoll { get; set; }
    }
}
