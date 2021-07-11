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
<<<<<<< HEAD
            string connString = @"data source=DESKTOP-M8ACK99\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;multipleactiveresultsets=True";
=======
            string connString = @"data source=DESKTOP-M8ACK99\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;MultipleActiveResultSets=True";
>>>>>>> fe27e5e7b15610a2ba346cc517de9ef8ee44bff8
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}