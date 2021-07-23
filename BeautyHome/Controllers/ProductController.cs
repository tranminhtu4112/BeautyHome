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
    public class ProductController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Product

        public ActionResult Index(String CountCart)
        {
            TypeProductView objtypeProductView = new TypeProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select * " +
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
            ViewBag.CountCart = CountCart;

            return View(objtypeProductView);
        }
            public ActionResult Furniture(long furId)
        {

            TypeProductView objtypeProductView = new TypeProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList(); 
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select * " +
                        "from product, furniture, type_product, image_product " +
                        "where type_product.type_product_id = product.type_product_id " +
                        "and furniture.furniture_id = type_product.furniture_id and product.product_id = image_product.product_id " + 
                        "and furniture.furniture_id = " + furId;


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
                        productView.url_image1 = Convert.ToString(reader.GetValue(15));
                        productView.url_image2 = Convert.ToString(reader.GetValue(16));
                        productView.url_image3 = Convert.ToString(reader.GetValue(17));
                        listpr.Add(productView);
                    }
                }
            }
            objtypeProductView.listProductViews = listpr;
            return View(objtypeProductView);
        }
        public ActionResult Type(long typeId)
        {
            TypeProductView objtypeProductView = new TypeProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;
            string sql = "select * " +
                "from product, furniture, type_product, image_product " +
                "where type_product.type_product_id = product.type_product_id " +
                "and furniture.furniture_id = type_product.furniture_id and product.product_id = image_product.product_id and type_product.type_product_id = " + typeId;

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
                        productView.url_image1 = Convert.ToString(reader.GetValue(15));
                        productView.url_image2 = Convert.ToString(reader.GetValue(16));
                        productView.url_image3 = Convert.ToString(reader.GetValue(17));
                        listpr.Add(productView);
                    }
                }
            }
            objtypeProductView.listProductViews = listpr;
            return View(objtypeProductView);
        }
    }
}