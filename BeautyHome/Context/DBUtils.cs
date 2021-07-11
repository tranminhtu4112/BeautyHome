using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BeautyHome.Context
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = @"data source=DESKTOP-M8ACK99\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;MultipleActiveResultSets=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}