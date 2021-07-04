using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class AccountController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Account
        public ActionResult Index()
        {
            string sql = "select * from [user] where user_id = " + Session["userid"].ToString();
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Session["userid"] = Convert.ToInt64(reader.GetValue(0));
                        Session["username"] = Convert.ToString(reader.GetValue(1));
                        Session["password"] = Session["password"];
                        Session["fullname"] = Convert.ToString(reader.GetValue(3));
                        Session["address"] = Convert.ToString(reader.GetValue(4));
                        Session["email"] = Convert.ToString(reader.GetValue(5));
                        Session["phone"] = Convert.ToString(reader.GetValue(6));

                    }
                }
            }
                        return View();
        }
        [HttpPost]
        public ActionResult UpdateAccount(String userid ,String username, string password, string fullname, string email, String address, string phone)
        {
            connection.Open();
            try
            {
                string sql = "UPDATE [user] " +
                    "SET password = @password, full_name = @full_name, address = @address, " +
                    "email = @email, phone = @phone " +
                    "WHERE user_id = @user_id ";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = LoginController.GetMD5(password);
                cmd.Parameters.Add("@full_name", SqlDbType.NVarChar).Value = fullname;
                cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
                cmd.Parameters.Add("@user_id", SqlDbType.BigInt).Value = userid;

                int rowCount = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
            return RedirectToAction("Index", "Account");
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
    }
}