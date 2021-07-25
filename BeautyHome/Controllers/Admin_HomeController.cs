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
        public ActionResult Index()
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
            string sql = "select [order].order_id, [user].full_name , FORMAT ([order].date_order, 'dd/MM/yyyy') as [dateOrder] ,[user].address, [user].phone, " +
                         "SUM([order].price) , [order].status " +
                         "from[order], [user] " +
                         "where[order].user_id = [user].user_id " + 
                         "group by[order].order_id, FORMAT([order].date_order, 'dd/MM/yyyy'), [order].status,[user].full_name,[user].phone,[user].address";

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
                        order.fullName = Convert.ToString(reader.GetValue(1));
                        order.dateOrder = Convert.ToString(reader.GetValue(2));
                        order.address = Convert.ToString(reader.GetValue(3));
                        order.phone = Convert.ToString(reader.GetValue(4));
                        order.totalPrice = Convert.ToDouble(reader.GetValue(5));
                        order.status = Convert.ToInt32(reader.GetValue(6));

                        listOrder.Add(order);
                    }
                }
            }
            objadminProductView.listAdminOrder = listOrder;
            return View(objadminProductView);
        }
    }
}