using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using Discord.WebSocket;

namespace PhoenixBot.Modules
{
    public static class RoleCheck
    {
        public static bool HasChiefRole(SocketGuildUser user)
        {
            string targetRoleName = "Chief";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {targetRoleName} role.");
                return false;
            }
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        public static bool HasInvestmentStaffRole(SocketGuildUser user)
        {
            string targetRoleName = "Investment Staff";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {targetRoleName} role.");
                return false;
            }
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        public static bool HasRecruiterRole(SocketGuildUser user)
        {
            string targetRoleName = "Recruiter";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {targetRoleName} role.");
                return false;
            }
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        public static bool HasClerkRole(SocketGuildUser user)
        {
            string targetRoleName = "Clerk";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {targetRoleName} role.");
                return false;
            }
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        public static bool HasApplicantRole(SocketGuildUser user)
        {
            string targetRoleName = "Applicant";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {targetRoleName} role.");
                return false;
            }
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
        public static bool HasDiplomatRole(SocketGuildUser user)
        {
            string targetRoleName = "Diplomat";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0)
            {
                Console.WriteLine($"Error in finding the: {targetRoleName} role.");
                return false;
            }
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}
