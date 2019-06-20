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
            var idString = userId.ToString();
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                UserAccountModel model = new UserAccountModel();
                //select * from UserAcc where @IdString
                var users =  connection.Query($"SELECT * from UserAcc where Id = '{idString}';");
                foreach( var user in users)
                {
                    if (userId.ToString() == user.Id)
                    {
                        model.Id = user.Id;
                        model.NumberOfWarnings = user.NumberOfWarnings;
                        model.IsMuted = user.IsMuted;
                        return model;
                    }
                }
                return model;
            }   
        }
        public void AddUser(ulong id)
        {
            string idString = id.ToString();
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                connection.Open();
                Console.WriteLine(connection.State);
                var user = connection.Execute($"INSERT INTO UserAcc Values (Id, NumberOfWarnings, IsMuted);", new { Id = idString, NumberOfWarnings = 0, IsMuted = 0 });
                connection.Close();
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


