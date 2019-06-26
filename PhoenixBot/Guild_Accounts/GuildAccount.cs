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

        public DateTime DayChecked { get; set; }

        public bool DebateRunning { get; set; }
        public ulong StickHolderId { get; set; }

        public bool AllowSummoning { get; set; }
        // Town Raffle
        public bool TownRaffleRunning { get; set; }
        public string TownRaffleInfo { get; set; }
        // Guild Raffle
        public bool GuildRaffleRunning { get; set; }
        public string GuildRaffleInfo { get; set; }
        // Guild Events
        public string GuildEvent1Name { get; set; }
        public bool GuildEvent1Running { get; set; }
        public bool GuildEvent1HourWarning { get; set; }
        public bool GuildEvent1TenMinuteWarning { get; set; }
        public DateTime GuildEvent1Time { get; set; }

        public string GuildEvent2Name { get; set; }
        public bool GuildEvent2Running { get; set; }
        public bool GuildEvent2HourWarning { get; set; }
        public bool GuildEvent2TenMinuteWarning { get; set; }
        public DateTime GuildEvent2Time { get; set; }

        // Town Events
        public string TownEvent1Name { get; set; }
        public bool TownEvent1Running { get; set; }
        public bool TownEvent1HourWarning { get; set; }
        public bool TownEvent1TenMinuteWarning { get; set; }
        public DateTime TownEvent1Time { get; set; }

        public string TownEvent2Name { get; set; }
        public bool TownEvent2Running { get; set; }
        public bool TownEvent2HourWarning { get; set; }
        public bool TownEvent2TenMinuteWarning { get; set; }
        public DateTime TownEvent2Time { get; set; }

        // Group Event
        public string GroupEventName { get; set; }
        public bool GroupEventRunning { get; set; }
        public bool GroupEventHourWarning { get; set; }
        public bool GroupEventTenMinuteWarning { get; set; }
        public DateTime GroupEventTime { get; set; }
    }
}
