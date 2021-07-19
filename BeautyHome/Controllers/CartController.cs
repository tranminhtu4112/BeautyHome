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
    public class CartController : Controller
    {
        SqlConnection connection = DBUtils.GetDBConnection();
        public BeautyHomeEntities db = new BeautyHomeEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            List<long> listProductId = (List<long>)Session["listProductId"];
            List<ProductView> listpr = new List<ProductView>();
            if (listProductId != null)
            {
                connection.Open();
                for (int i = 0; i < listProductId.Count; i++)
                {
                    string sql = "select * from product where product.product_id = " + listProductId[i];
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductView productView = new ProductView();

                                productView.productId = Convert.ToInt64(reader.GetValue(0));
                                productView.name = Convert.ToString(reader.GetValue(2));
                                productView.descriptionDetails = Convert.ToString(reader.GetValue(3));
                                productView.description = Convert.ToString(reader.GetValue(4));
                                productView.evaluate = Convert.ToDouble(reader.GetValue(5));
                                productView.amount = Convert.ToDouble(reader.GetValue(6));
                                productView.price = Convert.ToDouble(reader.GetValue(7));
                                productView.color = Convert.ToString(reader.GetValue(8));
                                listpr.Add(productView);
                            }
                        }
                    }
                } // end for 
                objtypeProductView.listProductViews = listpr;
                double totalPrice = 0;
                foreach(var item in listpr)
                {
                    totalPrice += item.price;
                }

                ViewBag.totalPrice = totalPrice;
            }
            connection.Close();
            return View(objtypeProductView);
        }
        public ActionResult AddCart(String productId)
        {
            List<long> listProductId;
            if (Session["listProductId"] == null)
            {
                listProductId = new List<long>();
                listProductId.Add(long.Parse(productId));
                Session["listProductId"] = listProductId;
                return RedirectToAction("Index", "Product", new { CountCart = listProductId.Count });
            }
            listProductId = (List<long>)Session["listProductId"];
            for(int i = 0; i < listProductId.Count; i++)
            {
                if(long.Parse(productId) == listProductId[i])
                {
                    return RedirectToAction("Index", "Product", new { CountCart = listProductId.Count });
                }
            }
            listProductId.Add(long.Parse(productId));
            Session["listProductId"] = listProductId;

            return RedirectToAction("Index", "Product", new { CountCart = listProductId.Count });
        }
        [HttpPost]
        public ActionResult PayCart(order order)
        {
            List<long> listProductId = (List<long>)Session["listProductId"];
            if (listProductId.Count > 0)
            {
                var idProduct = listProductId[0];
                var productItem = db.products.Where(n => n.product_id == idProduct).FirstOrDefault();
                order.product_id = listProductId[0];
                order.user_id = Convert.ToInt64(Session["userid"]);
                order.product_name = productItem.name;
                order.amount = 1;
                order.price = productItem.price;
                DateTime dateNow = DateTime.Now;
                order.date_order = dateNow;
                order.datereceived = dateNow;
                order.status = 0;
                db.orders.Add(order);
                db.SaveChanges();
                var maxOrderId = db.orders.Max(x => x.order_id);
                if(listProductId.Count > 1)
                {
                    for(int i = 1; i < listProductId.Count; i++)
                    {
                        idProduct = listProductId[i];
                        productItem = db.products.Where(n => n.product_id == idProduct).FirstOrDefault();
                        order.order_id = maxOrderId;
                        order.product_id = listProductId[i];
                        order.user_id = Convert.ToInt64(Session["userid"]);
                        order.product_name = productItem.name;
                        order.amount = 1;
                        order.price = productItem.price;
                        order.date_order = dateNow;
                        order.datereceived = dateNow;
                        order.status = 0;
                        var orderItem = order;

                        string SqlAdd = "SET IDENTITY_INSERT [order] ON; INSERT into [order](order_id,product_id,user_id,full_name," +
                            "address,phone,mail,product_name,amount,price,date_order,datereceived,status)" +
                         " VALUES(@order_id, @product_id, @user_id, @full_name, @address, @phone, @mail ,@product_name , @amount, @price," +
                         " @date_order, @datereceived, @status); SET IDENTITY_INSERT [order] OFF";
                        SqlCommand cmd = new SqlCommand();
                        connection.Open();
                        cmd.Connection = connection;
                        cmd.CommandText = SqlAdd;
                        SqlCommand sqlCommand = new SqlCommand(SqlAdd, connection);
                        sqlCommand.Parameters.AddWithValue("order_id", orderItem.order_id);
                        sqlCommand.Parameters.AddWithValue("product_id", orderItem.product_id);
                        sqlCommand.Parameters.AddWithValue("user_id", orderItem.user_id);
                        sqlCommand.Parameters.AddWithValue("full_name", orderItem.full_name);
                        sqlCommand.Parameters.AddWithValue("address", orderItem.address);
                        sqlCommand.Parameters.AddWithValue("phone", orderItem.phone);
                        sqlCommand.Parameters.AddWithValue("mail", orderItem.mail);
                        sqlCommand.Parameters.AddWithValue("product_name", orderItem.product_name);
                        sqlCommand.Parameters.AddWithValue("amount", orderItem.amount);
                        sqlCommand.Parameters.AddWithValue("price", orderItem.price);
                        sqlCommand.Parameters.AddWithValue("date_order", orderItem.date_order);
                        sqlCommand.Parameters.AddWithValue("datereceived", orderItem.datereceived);
                        sqlCommand.Parameters.AddWithValue("status", orderItem.status);
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            return RedirectToAction("Index", "Account", new { alert = "Đặt hàng thành công !" });
        }
        public ActionResult DeleteCart(String productId)
        {
            List<long> listProductId = (List<long>)Session["listProductId"];
            for(int i = 0; i < listProductId.Count; i++)
            {
                if (listProductId[i].Equals(long.Parse(productId)))
                    listProductId.RemoveAt(i);
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}