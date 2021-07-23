using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_AccountController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        

        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Admin_Account
        public ActionResult Index()
        {
            var listAccount = db.users.ToList();
            AdminAccount objadminAccount = new AdminAccount();
            objadminAccount.listAccount = listAccount;
            try
            {
                if (Int32.Parse(Session["role"].ToString()) == 1)
                    return RedirectToAction("Index", "Login");
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
            return View(objadminAccount);
        }
        [HttpPost]
        public ActionResult AddAccount(user user)
        {
            string SqlAdd = "INSERT into[user]([user_name],[password],[full_name],[address],[email],[phone],[role]) " +
                            " VALUES(@user_name,@password,@full_name,@address,@email,@phone,@role)";

            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = SqlAdd;
            SqlCommand sqlCommand = new SqlCommand(SqlAdd, connection);
            sqlCommand.Parameters.AddWithValue("user_name", user.user_name);
            sqlCommand.Parameters.AddWithValue("password", user.password);
            sqlCommand.Parameters.AddWithValue("full_name", user.full_name);
            sqlCommand.Parameters.AddWithValue("address", user.address);
            sqlCommand.Parameters.AddWithValue("email", user.email);
            sqlCommand.Parameters.AddWithValue("phone", user.phone);
            sqlCommand.Parameters.AddWithValue("role", 0);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("Index", "Admin_Account");


        }
    }
}