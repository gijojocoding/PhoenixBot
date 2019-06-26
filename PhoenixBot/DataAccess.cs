using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PhoenixBot
{
    public class DataAccess
    {
        internal string nameS = "lite";
        public void test()
        {
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                connection.Open();
                Console.WriteLine(connection.State);
                connection.Close();
                Console.WriteLine(connection.State);
            }
        }
        public UserAccountModel GetUser(ulong userId)
        {
            string idString = userId.ToString();
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                var t = Global.Client.GetGuild(Config.bot.guildID).GetUser(userId);
                UserAccountModel model = new UserAccountModel();
                //select * from UserAcc where @IdString
                var users = connection.Query($"SELECT * from UserAcc where Id = " + t.Id + ";");
                var UsersL = users.ToList();
                foreach (var p in UsersL)
                {
                    //Console.WriteLine($" Id: {p.Id} \n Warnings: {p.NumberOfWarnings} \n Is Muted: {p.IsMuted}");
                    model.Id = (ulong) p.Id;
                    model.NumberOfWarnings = (byte) p.NumberOfWarnings;
                    model.IsMuted = (byte) p.IsMuted;
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
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                connection.Execute($"update UserAcc set IsMuted = {Converter.ConvertFromBool(isMusted)} WHERE Id = {userId};");
            }
        }
        public void UpdateUserWarning(ulong userId, byte warningAdd)
        {
            var idString = Converter.ConvertToString(userId);
            using (IDbConnection connection = new SQLiteConnection(DataBaseHandler.CnnVal(nameS)))
            {
                UserAccountModel model = new UserAccountModel();
                connection.Open();

                var users = connection.Query($"SELECT * from UserAcc where Id = " + userId + ";");
                var UsersL = users.ToList();
                foreach (var p in UsersL)
                {
                    Console.WriteLine($" Id: {p.Id} \n Warnings: {p.NumberOfWarnings} \n Is Muted: {p.IsMuted}");
                    model.Id = (ulong)p.Id;
                    model.NumberOfWarnings = (byte)p.NumberOfWarnings;
                    model.IsMuted = (byte)p.IsMuted;
                    connection.Execute($"update UserAcc set NumberOfWarnings = {warningAdd + model.NumberOfWarnings} WHERE Id = {userId};");
                }

                connection.Close();

            }
        }
    }
}


