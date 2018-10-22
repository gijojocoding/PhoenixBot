using System;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace PhoenixBot
{
    public class Cooldown : PreconditionAttribute
    {
        TimeSpan CooldownLenght { get; set; }
        bool AdminsAreLimited { get; set; }
        readonly ConcurrentDictionary<CooldownInfo, DateTime> _cooldowns = new ConcurrentDictionary<CooldownInfo, DateTime>();

        public Cooldown(int seconds, bool adminsAreLimited = false)
        {
            CooldownLenght = TimeSpan.FromSeconds(seconds);
            AdminsAreLimited = adminsAreLimited;
        }
        public struct CooldownInfo
        {
            public ulong UserID { get; }
            public int CommandHashCode { get; }

            public CooldownInfo(ulong userId, int commandHashCode)
            {
                UserID = userId;
                CommandHashCode = commandHashCode;
            }
        }

        public override Task<PreconditionResult> CheckPermissions(ICommandContext context, CommandInfo command, IServiceProvider _provider)
        {
            if (!AdminsAreLimited && context.User is IGuildUser user && user.GuildPermissions.Administrator)
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            }
            var key = new CooldownInfo(context.User.Id, command.GetHashCode());
            if (_cooldowns.TryGetValue(key, out DateTime endsAt))
            {
                var difference = endsAt.Subtract(DateTime.UtcNow);
                if (difference.Ticks > 0)
                {
                    return Task.FromResult(PreconditionResult.FromError($"You can use this command in {difference.ToString(@"ss")} secounds."));
                }
                var time = DateTime.UtcNow.Add(CooldownLenght);
                _cooldowns.TryUpdate(key, time, endsAt);
            }
            else
            {
                _cooldowns.TryAdd(key, DateTime.UtcNow.Add(CooldownLenght));
            }
            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
