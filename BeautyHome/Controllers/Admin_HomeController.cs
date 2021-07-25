using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_HomeController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Admin_Home
        public ActionResult Index(String date)
        {
            try
            {
                if (Int32.Parse(Session["role"].ToString()) == 1)
                    return RedirectToAction("Index", "Login");
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
            AdminProductView objadminProductView = new AdminProductView();
            string sql = "select [order].order_id, [user].user_id , [user].full_name , FORMAT ([order].date_order, 'dd/MM/yyyy') as [dateOrder] ,[user].address, [user].phone, " +
                         "SUM([order].price) , [order].status " +
                         "from[order], [user] " +
                         "where[order].user_id = [user].user_id " +
                         "group by[order].order_id,[user].user_id, FORMAT([order].date_order, 'dd/MM/yyyy'), [order].status,[user].full_name,[user].phone,[user].address";

            List<AdminOrder> listOrder = new List<AdminOrder>();
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
                        AdminOrder order = new AdminOrder();

                        order.orderId = Convert.ToInt64(reader.GetValue(0));
                        order.userId = Convert.ToInt64(reader.GetValue(1));
                        order.fullName = Convert.ToString(reader.GetValue(2));
                        order.dateOrder = Convert.ToString(reader.GetValue(3));
                        order.address = Convert.ToString(reader.GetValue(4));
                        order.phone = Convert.ToString(reader.GetValue(5));
                        order.totalPrice = Convert.ToDouble(reader.GetValue(6));
                        order.status = Convert.ToInt32(reader.GetValue(7));

                        listOrder.Add(order);
                    }
                }
            }
            connection.Close();
            
            if(date == null)
            {
                DateTime dateNow = DateTime.Now;
                if (dateNow.Month.ToString().Count() == 1)
                {
                    date = dateNow.Year + "/0" + dateNow.Month;
                }
                else
                {
                    date = dateNow.Year + "/" + dateNow.Month;
                }
            }
            else
            {
                date = date.Replace("-", "/");
            }

            int totalOrder = getTotalOrder(date);
            int totalUser = getTotalUser(date);
            double totalPrice = getTotalPrice(date);
            int totalAmount = getTotalAmount(date);

            Statistical statistical = new Statistical();
            statistical.totalOrder = totalOrder;
            statistical.totalUser = totalUser;
            statistical.totalPrice = totalPrice;
            statistical.totalAmount = totalAmount;
            objadminProductView.statistical = statistical;

            objadminProductView.listAdminOrder = listOrder;
            return View(objadminProductView);
        }
        public int getTotalOrder(String date)
        {
            string sql = "select count(order_id) as 'totalOrder' " + 
                         "from[order]" +
                         "where FORMAT([order].date_order, 'yyyy/MM') like  '" + date +"'" +  
                         " group by order_id";
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            int i = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i++;
                    }
                }
            }
            connection.Close();
            return i;
        }
        public int getTotalUser(String date)
        {
            string sql = "select count(user_id) as 'totalUser' " +
                         "from[order]" +
                         "where FORMAT([order].date_order, 'yyyy/MM') like  '" + date + "'" +
                         " group by user_id";
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            int i = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i++;
                    }
                }
            }
            connection.Close();
            return i;
        }
        public double getTotalPrice(String date)
        {
            string sql = "select SUM(price) as 'totalPrice' " +
                         "from[order]" +
                         "where FORMAT([order].date_order, 'yyyy/MM') like  '" + date + "'";
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                double s = Convert.ToDouble(reader.GetValue(0));
                                return s;
                            }
                            catch
                            {
                                return 0;
                            }
                        }
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
        public int getTotalAmount(String date)
        {
            string sql = "select SUM(amount) as 'totalAmount' " +
                         "from[order]" +
                         "where FORMAT([order].date_order, 'yyyy/MM') like  '" + date + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                int s = Convert.ToInt32(reader.GetValue(0));
                                return s;
                            }
                            catch
                            {
                                return 0;
                            }
                        }
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }
        public ActionResult Details(long orderId, long userId)
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select product.product_id, image_product.url_Image1, product.name, [order].amount, product.price " +
                         "from[order], product, image_product " +
                         "where[order].order_id =  " + orderId +
                         " and[order].user_id =  " + userId +
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