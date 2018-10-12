using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoenixBot
{
    public class GuildAccount
    {
        public ulong ID { get; set; }
        public int NumberOfTempChannels { get; set; }
        // Points needed to increase these stats
        public uint PointsForHealthIncrease { get; set; }
        public uint PointsForHealth { get; set; }
        public uint PointsForRevive { get; set; }
        // Amount Increased
        public uint HealthIncrease { get; set; }
        public uint Health { get; set; }

        public string EventName { get; set; }

        public bool EventRunning { get; set; }

        public bool HourWarning { get; set; }

        public bool TenMinuteWarning { get; set; }

        public DateTime CurrentEvent { get; set; }

        public DateTime DayChecked { get; set; }

        public bool DebateRunning { get; set; }
        public SocketGuildUser StickHolder { get; set; }
    }
}
