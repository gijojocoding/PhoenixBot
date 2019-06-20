using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using PhoenixBot.Features.Games.UserAccounts;

namespace PhoenixBot.Modules.Game.AdminCommands
{
    [Group("HuntControl")]
    public class HuntUserControls : ModuleBase<SocketCommandContext>
    {
        [Command("SetXP")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task setXP(string type, IGuildUser user, float xp)
        {
            var target = GameUserAccounts.GetAccount(user.Id);
            if(type == "att" || type == "Att")
            {
                target.AttributeXP = xp;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{user.Username}: Attribute XP is now {target.AttributeXP}");
            }
            if (type == "Hunt" || type == "hunt")
            {
                target.HuntingXP = xp;
                GameUserAccounts.SaveAccounts();
                await ReplyAsync($"{user.Username}: Attribute XP is now {target.HuntingXP}");
            }
            else
            {
                await ReplyAsync("There was an error please look at the logs.");
                return;
            }

        }
    }
}
