

using System.Data.SqlClient;

namespace BeautyHome.Context
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
<<<<<<< HEAD
            string connString = @"data source=DESKTOP-M8ACK99\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;multipleactiveresultsets=True";
=======
            string connString = @"data source=DESKTOP-U2LCGG2\SQLEXPRESS;initial catalog=SQL_BeautyHome;integrated security=True;MultipleActiveResultSets=True";
>>>>>>> e256d1c6938dfa3494ce87e2d21a1a06053196f2
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}