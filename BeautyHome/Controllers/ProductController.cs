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
        public ActionResult Index(long furId)
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
                 
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select * " +
                "from product, furniture, type_product " +
                "where type_product.type_product_id = product.type_product_id " +
                "and furniture.furniture_id = type_product.furniture_id and furniture.furniture_id = " + furId;


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
                        listpr.Add(productView);
                    }
                }
            }
            objtypeProductView.listProductViews = listpr;
            return View(objtypeProductView);
        }
        public ActionResult Type(long typeprId)
        {
            TypeProductView objtypeProductView = new TypeProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;
            string sql = "select * " +
                "from product, furniture, type_product " +
                "where type_product.type_product_id = product.type_product_id " +
                "and furniture.furniture_id = type_product.furniture_id and type_product.type_product_id = " + typeprId;

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
                        listpr.Add(productView);
                    }
                }
            }
            objtypeProductView.listProductViews = listpr;
            return View(objtypeProductView);
        }


    }
}