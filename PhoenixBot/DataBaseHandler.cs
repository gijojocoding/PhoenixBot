using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Configuration;

namespace PhoenixBot
{
    public static class DataBaseHandler
    {
        //Default
        //Dev

        internal static string _cnnstring;

        //static string server = Config.bot.Host
        //    static string database = Config.
        //    static string uid = Config.
        //    static string password = Config.
        //    static string _connstring = ;
        //static DataBaseHandler()
        //{
        //    _connstring = Server = myServerAddress; Database = myDataBase; Trusted_Connection = True;
        //}

        public static string CnnVal(string name = "Dev")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
