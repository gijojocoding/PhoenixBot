using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using PhoenixBot.Features.Games.UserAccounts;

namespace PhoenixBot.Features.Games.GameLevel
{
    [Group("hunt")]
    public class HuntLeveling : ModuleBase<SocketCommandContext>
    {
        private Timer HuntTrainingTimerOne, HuntTrainingTimerTwo;
        private Timer AttribteTrainingTimerOne, AttribteTrainingTimerTwo;
        private const int NoviceTime = 5, IntermediateTime = 7, MasterTime = 11, GrandMasterTime = 15, PhoenixTime = 19;
        private const float NoviceFloat = 200f, IntermediateFloat = 325f, MasterFloat = 575f, GrandMasterFloat = 950f, PhoenixFloat = 1500f;
        private const int DemonTime = 5, AngelTime = 5, Life = 13, NulliferTime = 19;
        private const float DemonMult = 500, AngelMult = 500, LifeMult = 800, NulliferMult = 1200;
  
        [Command("Training", RunMode = RunMode.Async)]
        [Cooldown(30, adminsAreLimited: true)]
        async Task HuntTraining()
        {
            var user = GameUserAccounts.GetAccount(Context.User.Id);
            var huntLevel = user.HuntingLevel;
            switch (user.HuntingLevel) {
                case HuntingLevel.Noob:
                    if(user.HuntingXP < 0)
                    {
                        await ReplyAsync("Hunt training started.");
                        await Task.Delay(50000);
                        Console.WriteLine("Delay 1 is done.");
                        await Task.Delay(50000);
                        user.HuntingXP -= NoviceFloat;
                        user.HuntingLevel = HuntingLevel.Novice;
                        GameUserAccounts.SaveAccounts();
                        await Context.Channel.SendMessageAsync($"{Context.User} has advanced to the next Hunting Level: {user.HuntingLevel.ToString()}");
                    }

                break;
            }


        }
    }
}
