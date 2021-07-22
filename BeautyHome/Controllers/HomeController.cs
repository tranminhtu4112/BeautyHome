using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection connection = DBUtils.GetDBConnection();
        public BeautyHomeEntities db = new BeautyHomeEntities();
        public ActionResult Index()
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();

            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select TOP(8) * " +
                        "from product, image_product " +
                        "where product.product_id = image_product.product_id";

            List<ProductView> listpr = new List<ProductView>();
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
                        ProductView productView = new ProductView();

                        productView.productId = Convert.ToInt64(reader.GetValue(0));
                        productView.name = Convert.ToString(reader.GetValue(2));
                        productView.descriptionDetails = Convert.ToString(reader.GetValue(3));
                        productView.description = Convert.ToString(reader.GetValue(4));
                        productView.evaluate = Convert.ToDouble(reader.GetValue(5));
                        productView.amount = Convert.ToDouble(reader.GetValue(6));
                        productView.price = Convert.ToDouble(reader.GetValue(7));
                        productView.color = Convert.ToString(reader.GetValue(8));
                        productView.url_image1 = Convert.ToString(reader.GetValue(10));
                        productView.url_image2 = Convert.ToString(reader.GetValue(11));
                        productView.url_image3 = Convert.ToString(reader.GetValue(12));
                        listpr.Add(productView);
                    }
                }
            }
            objtypeProductView.listProductViews = listpr;

            return View(objtypeProductView);
        }
    }
}