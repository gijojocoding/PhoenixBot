using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using PhoenixBot.Features.Games.Hunt;
using System.Threading.Tasks;

namespace PhoenixBot.Modules.Game.AdminCommands
{
    [Group("HuntList")]
    [RequireOwner]
    public class HuntCommands : ModuleBase<SocketCommandContext>
    {
         [Command("TypeList")]
         async Task HuntTypeList()
        {
            string list = "None, Demon, Light, Angel, Undead, Resurected.";
            await ReplyAsync(list);
        }
        [Command("AddNoob")]
        async Task AddNoobMonster(string name, string mtype, int hp, byte attack )
        {
            try
            {
                NoobMonsterList.CreateMonster(name);
                var monster = NoobMonsterList.GetMonster(name);
                monster.MonsterHP = hp;
                monster.MonsterAttack = attack;
                var type = mtype.ToLower();
                if (type == "none") monster.MonsterType = MonsterType.None;
                if (type == "demon") monster.MonsterType = MonsterType.Demon;
                if (type == "angel") monster.MonsterType = MonsterType.Angel;
                if (type == "undead") monster.MonsterType = MonsterType.Undead;
                if (type == "resurected") monster.MonsterType = MonsterType.Resurrecter;
                if (type == "all") monster.MonsterType = MonsterType.All;
                NoobMonsterList.SaveList();
                await ReplyAsync("Monster Added.");
            }
            catch
            {
                await ReplyAsync("Failed to add the Monster.");
            }
        }
        [Command("AddNovice")]
        async Task AddNovieMonster(string name, string mtype, int hp, byte attack)
        {
            try
            {
                NoviceMonsterList.CreateMonster(name);
                var monster = NoviceMonsterList.GetMonster(name);
                monster.MonsterHP = hp;
                monster.MonsterAttack = attack;
                var type = mtype.ToLower();
                if (type == "none") monster.MonsterType = MonsterType.None;
                if (type == "demon") monster.MonsterType = MonsterType.Demon;
                if (type == "angel") monster.MonsterType = MonsterType.Angel;
                if (type == "undead") monster.MonsterType = MonsterType.Undead;
                if (type == "resurected") monster.MonsterType = MonsterType.Resurrecter;
                if (type == "all") monster.MonsterType = MonsterType.All;
                NoviceMonsterList.SaveList();
                await ReplyAsync("Monster Added.");
            }
            catch
            {
                await ReplyAsync("Failed to add the Monster.");
            }
        }
        [Command("AddIntermediate"), Alias("AddIntern")]
        async Task AddIntermediateMonster(string name, string mtype, int hp, byte attack)
        {
            try
            {
                _IntermediateMonsterList.CreateMonster(name);
                var monster = _IntermediateMonsterList.GetMonster(name);
                monster.MonsterHP = hp;
                monster.MonsterAttack = attack;
                var type = mtype.ToLower();
                if (type == "none") monster.MonsterType = MonsterType.None;
                if (type == "demon") monster.MonsterType = MonsterType.Demon;
                if (type == "angel") monster.MonsterType = MonsterType.Angel;
                if (type == "undead") monster.MonsterType = MonsterType.Undead;
                if (type == "resurected") monster.MonsterType = MonsterType.Resurrecter;
                if (type == "all") monster.MonsterType = MonsterType.All;
                _IntermediateMonsterList.SaveList();
                await ReplyAsync("Monster Added.");
            }
            catch
            {
                await ReplyAsync("Failed to add the Monster.");
            }
        }
        [Command("AddMaster")]
        async Task AddMasterMonster(string name, string mtype, int hp, byte attack)
        {
            try
            {
                MasterMonsterList.CreateMonster(name);
                var monster = MasterMonsterList.GetMonster(name);
                monster.MonsterHP = hp;
                monster.MonsterAttack = attack;
                var type = mtype.ToLower();
                if (type == "none") monster.MonsterType = MonsterType.None;
                if (type == "demon") monster.MonsterType = MonsterType.Demon;
                if (type == "angel") monster.MonsterType = MonsterType.Angel;
                if (type == "undead") monster.MonsterType = MonsterType.Undead;
                if (type == "resurected") monster.MonsterType = MonsterType.Resurrecter;
                if (type == "all") monster.MonsterType = MonsterType.All;
                MasterMonsterList.SaveList();
                await ReplyAsync("Monster Added.");
            }
            catch
            {
                await ReplyAsync("Failed to add the Monster.");
            }
        }
        [Command("AddGrandMaster")]
        async Task AddGrandMasterMonster(string name, string mtype, int hp, byte attack)
        {
            try
            {
                GMMonsterList.CreateMonster(name);
                var monster = GMMonsterList.GetMonster(name);
                monster.MonsterHP = hp;
                monster.MonsterAttack = attack;
                var type = mtype.ToLower();
                if (type == "none") monster.MonsterType = MonsterType.None;
                if (type == "demon") monster.MonsterType = MonsterType.Demon;
                if (type == "angel") monster.MonsterType = MonsterType.Angel;
                if (type == "undead") monster.MonsterType = MonsterType.Undead;
                if (type == "resurected") monster.MonsterType = MonsterType.Resurrecter;
                if (type == "all") monster.MonsterType = MonsterType.All;
                GMMonsterList.SaveList();
                await ReplyAsync("Monster Added.");
            }
            catch
            {
                await ReplyAsync("Failed to add the Monster.");
            }
        }
        [Command("AddPhoenix")]
        async Task AddPhoenixMonster(string name, string mtype, int hp, byte attack)
        {
            try
            {
                PhoenixMonsterList.CreateMonster(name);
                var monster = PhoenixMonsterList.GetMonster(name);
                monster.MonsterHP = hp;
                monster.MonsterAttack = attack;
                var type = mtype.ToLower();
                if (type == "none") monster.MonsterType = MonsterType.None;
                if (type == "demon") monster.MonsterType = MonsterType.Demon;
                if (type == "angel") monster.MonsterType = MonsterType.Angel;
                if (type == "undead") monster.MonsterType = MonsterType.Undead;
                if (type == "resurected") monster.MonsterType = MonsterType.Resurrecter;
                if (type == "all") monster.MonsterType = MonsterType.All;
                PhoenixMonsterList.SaveList();
                await ReplyAsync("Monster Added.");
            }
            catch
            {
                await ReplyAsync("Failed to add the Monster.");
            }
        }
    }
}
