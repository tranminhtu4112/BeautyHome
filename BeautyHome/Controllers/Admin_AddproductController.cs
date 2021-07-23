using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_AddproductController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Admin_Addproduct
        [HttpGet]
        public ActionResult Index()
        {
            AdminProductView objadminProductView = new AdminProductView();
            var listtype = db.type_product.ToList();
            objadminProductView.listtype = listtype;
            try
            {
                if (Convert.ToInt32(Session["role"].ToString()) == 1)
                    return RedirectToAction("Index", "Login");
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
            order _order = new order();
            return View(objadminProductView);
        }
        [HttpPost]
        public ActionResult Add(product product,image_product image_Product, HttpPostedFileBase url_Image1, HttpPostedFileBase url_Image2, HttpPostedFileBase url_Image3)
        {
            string SqlAdd = " INSERT into product(type_product_id, name, descriptionDetails, description, evaluate, amount, price)" +
                         " VALUES(@type_product_id,@name,@descriptionDetails,@description,@evaluate,@amount,@price)";

            SqlCommand cmd = new SqlCommand();
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = SqlAdd;
            SqlCommand sqlCommand = new SqlCommand(SqlAdd, connection);
            sqlCommand.Parameters.AddWithValue("type_product_id", product.type_product_id);
            sqlCommand.Parameters.AddWithValue("name", product.name);
            sqlCommand.Parameters.AddWithValue("descriptionDetails", product.descriptionDetails);
            sqlCommand.Parameters.AddWithValue("description", product.description);
            sqlCommand.Parameters.AddWithValue("evaluate", product.evaluate);
            sqlCommand.Parameters.AddWithValue("amount", product.amount);
            sqlCommand.Parameters.AddWithValue("price", product.price);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            string Sql = "SELECT MAX(product_id) FROM product";
            long IdMax = 0;
             connection.Open();
            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = Sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IdMax = Convert.ToInt64(reader.GetValue(0));

                    }
                }
            }
            connection.Close();
            string SqlImg = "insert into [image_product](product_id, url_Image1, url_Image2, url_Image3) VALUES(@product_id, @url_Image1, @url_Image2, @url_Image3)";
            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = SqlImg;
            sqlCommand = new SqlCommand(SqlImg, connection);
            sqlCommand.Parameters.AddWithValue("product_id", IdMax);
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
            url_Image3.SaveAs(path3);

            return RedirectToAction("Index", "Admin_Product");
        }

    }
}