

using System.Data.SqlClient;

namespace BeautyHome.Context
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string connString = @"data source=DESKTOP-U2LCGG2\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;MultipleActiveResultSets=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}