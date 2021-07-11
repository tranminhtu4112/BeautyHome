using BeautyHome.Context;
<<<<<<< HEAD
using BeautyHome.Models;
=======
>>>>>>> fe27e5e7b15610a2ba346cc517de9ef8ee44bff8
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
<<<<<<< HEAD
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
/*            var listpr = db.products.ToList();*/
                 
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select * " +
                "from product, furniture, type_product " +
                "where type_product.type_product_id = product.type_product_id " +
                "and furniture.furniture_id = type_product.furniture_id and furniture.furniture_id = " + furId;
            List<ProductView> listpr = new List<ProductView>();
=======
            string sql = "";
            
>>>>>>> fe27e5e7b15610a2ba346cc517de9ef8ee44bff8
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
<<<<<<< HEAD
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
=======
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
>>>>>>> fe27e5e7b15610a2ba346cc517de9ef8ee44bff8
        }

    }
}