using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Linq;

namespace PhoenixBot
{
    public class DataAccess
    {
        internal string nameS = "lite";
        public UserAccountModel GetUser(ulong userId)
        {
            string idString = userId.ToString();
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                var t = Global.Client.GetGuild(Config.bot.guildID).GetUser(userId);
                UserAccountModel model = new UserAccountModel();
                //select * from UserAcc where @IdString
                var users = connection.Query($"SELECT * from UserAcc where Id = " + t.Id + ";");
                foreach (var user in users)
                {

                    model.Id = (ulong)user.Id;
                    model.NumberOfWarnings = user.NumberOfWarnings;
                    model.IsMuted = user.IsMuted;
                    return model;

                }
                return model;
            }
        }
        public void AddUser(ulong UserId)
        {
            string idString = UserId.ToString();
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                connection.Open();
                Console.WriteLine(connection.State);
                var user = connection.Execute($"insert into UserAcc Values({ UserId }, 0, 0)");
                connection.Close();
                Console.WriteLine(connection.State);
            }
        }
        public void UpdateUserMute(ulong userId, bool isMusted)
        {
            var idString = Converter.ConvertToString(userId);
            int Mute = Converter.ConvertFromBool(isMusted);
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DataBaseHandler.CnnVal("nameS")))
            {
                connection.Execute("dbo.spUpdateMute @id @mute", new {id = idString, mute = Mute });
            }
        }
        public void UpdateUserWarning(ulong userId, int warningAdd = 1)
        {
            var idString = Converter.ConvertToString(userId);
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DataBaseHandler.CnnVal("Default")))
            {
                connection.Execute("dbo.spUpdateWarning @id @warning", new { id = idString, warning = warningAdd});
            }
        }
    }
}


