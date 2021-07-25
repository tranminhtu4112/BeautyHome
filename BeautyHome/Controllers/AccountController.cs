using BeautyHome.Context;
using BeautyHome.Models;
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
        public ActionResult Index(String alert)
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

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
                        Session["role"] = Convert.ToInt32(reader.GetValue(7));
                    }
                }
            }

            String sqlOrder = " select [order].order_id, FORMAT ([order].date_order, 'dd/MM/yyyy') as [dateOrder] , " +
                "[order].status, SUM([order].price)  from[order] where[order].user_id = " + Session["userid"].ToString() +
                " group by[order].order_id, " +
                "FORMAT([order].date_order, 'dd/MM/yyyy'), [order].status";

            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sqlOrder;
            List<OrderView> listOrderView = new List<OrderView>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderView orderView = new OrderView();

                        orderView.orderId = Convert.ToInt64(reader.GetValue(0));
                        orderView.dateOrder = Convert.ToString(reader.GetValue(1));
                        orderView.status = Convert.ToInt32(reader.GetValue(2));
                        orderView.totalPrice = Convert.ToDouble(reader.GetValue(3));

                        listOrderView.Add(orderView);
                    }
                }
            }
            objtypeProductView.listOrderViews = listOrderView;
            ViewBag.alert = alert;
            return View(objtypeProductView);
        }
        [HttpPost]
        public ActionResult UpdateAccount(String userid, String username, string password, string fullname, string email, String address, string phone)
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
            catch (Exception ex)
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
        public ActionResult Details(String orderId)
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select product.product_id, image_product.url_Image1, product.name, [order].amount, product.price " +
                         "from[order], product, image_product " +
                         "where[order].order_id =  " + orderId + 
                         " and[order].user_id =  " + Session["userid"].ToString() + 
                         " and[order].product_id = product.product_id and image_product.product_id = product.product_id";

            List<DetailsOrder> listDetailsOrder = new List<DetailsOrder>();
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
                        DetailsOrder detailsOrder = new DetailsOrder();

                        detailsOrder.productId = Convert.ToInt64(reader.GetValue(0));
                        detailsOrder.url_image1 = Convert.ToString(reader.GetValue(1));
                        detailsOrder.name = Convert.ToString(reader.GetValue(2));
                        detailsOrder.amount = Convert.ToInt32(reader.GetValue(3));
                        detailsOrder.price = Convert.ToDouble(reader.GetValue(4));

                        listDetailsOrder.Add(detailsOrder);
                    }
                }
            }
            objtypeProductView.listDetailsOrder = listDetailsOrder;
            return View(objtypeProductView);
        }
    }
}