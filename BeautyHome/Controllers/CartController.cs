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
                var productItem = db.products.Where(n => n.product_id == listProductId[0]).FirstOrDefault();
                order.product_id = listProductId[0];
                order.user_id = Convert.ToInt64(Session["userid"]);
            }
            return null;
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