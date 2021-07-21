

using System.Data.SqlClient;

namespace BeautyHome.Context
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = @"data source=LAPTOP-85HDK6US\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;multipleactiveresultsets=True";
       
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}