using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Victoria;

namespace PhoenixBot
{
    public sealed class AudioProvisoAttribute : PreconditionAttribute
    {
        public bool PlayerCheck { get; }

        public AudioProvisoAttribute(bool playerCheck = true)
        {
            PlayerCheck = playerCheck;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var lavaSocket = services.GetRequiredService<LavaSocketClient>();
            var player = lavaSocket.GetPlayer(context.Guild.Id);
            var user = context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
                return Task.FromResult(PreconditionResult.FromError("You're not connected to a voice channel."));


            if (PlayerCheck && player is null)
                return Task.FromResult(PreconditionResult.FromError("There is no player created for this guild."));

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
