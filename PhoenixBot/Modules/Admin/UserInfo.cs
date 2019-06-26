using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace PhoenixBot.Modules.Admin
{
    [RequireOwner]
    public class UserInfo : ModuleBase<SocketCommandContext>
    {
        [Command("usersinfo")]
        async Task UsersInfo(SocketGuildUser user)
        {
                DataAccess Db = new DataAccess();
                UserAccountModel account = new UserAccountModel();
                account = Db.GetUser(user.Id);
                var e = new EmbedBuilder();
                e.WithTitle("User Info")
                    .AddField("Warnings: ", account.NumberOfWarnings)
                    .AddField("Mute State: ", Converter.ConvertToBool(account.IsMuted));
                await ReplyAsync("", false, e.Build());
        }

        [Command("dbsync", RunMode = RunMode.Async)]
        async Task SyncDB()
        {
            DataAccess Db = new DataAccess();
            foreach(var user in Context.Guild.Users)
            {

                DataAccess Db = new DataAccess();
                UserAccountModel account = new UserAccountModel();
                var user = user_ as SocketGuildUser;
                account = Db.GetUser(user.Id);
                await Context.Channel.SendMessageAsync($"{user.Username} has: \n{account.NumberOfWarnings} warnings. \n{account.IsMuted} is not Muted.");
                Task.Delay(100);

                Db.AddUser(user.Id);
                Task.Delay(5000);

            }
            await ReplyAsync("Synced");
        }
        [Command("dbsync", RunMode = RunMode.Async)]
        async Task SyncDB()
        {
            DataAccess Db = new DataAccess();
            foreach(var user in Context.Guild.Users)
            {
                Db.AddUser(user.Id);
                Task.Delay(5000);
            }
            await ReplyAsync("Synced");
        }
    }
}
