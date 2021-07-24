using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_EditproductController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Admin_Editproduct
        public ActionResult Index(String CountCart, long prId)
        {
            AdminProductView objadminProductView = new AdminProductView();
            var listtype = db.type_product.ToList();
            objadminProductView.listtype = listtype;
            string sql = "select * " +
            "from product, image_product,type_product " +
            "where product.product_id = image_product.product_id and type_product.type_product_id = product.type_product_id and product.product_id = " + prId;

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
                        productView.type_product_id = Convert.ToInt64(reader.GetValue(1));
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
                        productView.nametype = Convert.ToString(reader.GetValue(14));
                        listpr.Add(productView);
                    }
                }
            }
            objadminProductView.listProductViews = listpr;
            ViewBag.CountCart = CountCart;
            try
            {
                if (Int32.Parse(Session["role"].ToString()) == 1)
                    return RedirectToAction("Index", "Login");
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View(objadminProductView);
        }

        [HttpPost]
        public ActionResult UpdateProduct(product product, image_product image_Product, HttpPostedFileBase url_Image1, HttpPostedFileBase url_Image2, HttpPostedFileBase url_Image3)
        {
          
          string PrSql =  "UPDATE [product] " +
                "SET type_product_id = @type_product_id, name = @name, descriptionDetails = @descriptionDetails, description = @description, " +
                "evaluate = @evaluate, amount = @amount, price = @price  " +
                "WHERE product_id = @product_id";
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = PrSql;
            SqlCommand sqlCommand = new SqlCommand(PrSql, connection);
            sqlCommand.Parameters.AddWithValue("product_id", product.product_id);
            sqlCommand.Parameters.AddWithValue("type_product_id", product.type_product_id);
            sqlCommand.Parameters.AddWithValue("name", product.name);
            sqlCommand.Parameters.AddWithValue("descriptionDetails", product.descriptionDetails);
            sqlCommand.Parameters.AddWithValue("description", product.description);
            sqlCommand.Parameters.AddWithValue("evaluate", product.evaluate);
            sqlCommand.Parameters.AddWithValue("amount", product.amount);
            sqlCommand.Parameters.AddWithValue("price", product.price);
            sqlCommand.ExecuteNonQuery();
            connection.Close();


            /*string SqlImg = "UPDATE[image_product]" +
                            "SET url_Image1 = @url_Image1, url_Image2= @url_Image2, url_Image2 = @url_Image3" +
                            "WHERE product_id = @product_id";
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = SqlImg;
            sqlCommand = new SqlCommand(SqlImg, connection);
            sqlCommand.Parameters.AddWithValue("product_id", product.product_id);
            sqlCommand.Parameters.AddWithValue("url_Image1", url_Image1.FileName);
            sqlCommand.Parameters.AddWithValue("url_Image2", url_Image2.FileName);
            sqlCommand.Parameters.AddWithValue("url_Image3", url_Image3.FileName);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            var fileName1 = Path.GetFileName(url_Image1.FileName);
            var path1 = Path.Combine(Server.MapPath("~/dbimages/uploads"), fileName1);
            url_Image1.SaveAs(path1);

            var fileName2 = Path.GetFileName(url_Image2.FileName);
            var path2 = Path.Combine(Server.MapPath("~/dbimages/uploads"), fileName2);
            url_Image2.SaveAs(path2);

            var fileName3 = Path.GetFileName(url_Image3.FileName);
            var path3 = Path.Combine(Server.MapPath("~/dbimages/uploads"), fileName3);
            url_Image3.SaveAs(path3);*/

            return RedirectToAction("Index", "Admin_Product");
        }
    }
}