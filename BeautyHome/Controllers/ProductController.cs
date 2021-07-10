using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class ProductController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Product
        public ActionResult Index()
        {
            string sql = "";
            
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
                        Session["role"] = Convert.ToInt32(reader.GetValue(7));
                    }
                }
            }
            return View();
        }
    }
}