using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Discord.WebSocket;

namespace PhoenixBot.Modules
{
    public static class GetId
    {
        public static ulong GetRoleID(SocketGuild guild, string roleName)
        {
            string targetRole = roleName;
            var result = from r in guild.Roles
                         where r.Name == targetRole
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {roleName} ID.");
                return 0;
            }
            return roleID;

        }
        public static ulong GetChannelID(SocketGuild guild, string channelName)
        {
            string targetChannel = channelName;
            var result = from r in guild.Channels
                         where r.Name == channelName
                         select r.Id;
            ulong ChannelID = result.FirstOrDefault();
            if (ChannelID == 0)
            {
                Console.WriteLine($"Error in finding the: {channelName} ID.");
                return 0;
            }
            return ChannelID;

        }
    }
}
