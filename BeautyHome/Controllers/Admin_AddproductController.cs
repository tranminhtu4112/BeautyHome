using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public ActionResult Add(product product, image_product image_Product)
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

            string SqlImg = "insert into imge_product(product_id, url_Image1, url_Image2, url_Image3) VALUES(@product_id, @url_Image1, @url_Image2, @url_Image3)"

            connection.Open();
            cmd.Connection = connection;
            cmd.CommandText = SqlAdd;
            sqlCommand = new SqlCommand(SqlAdd, connection);
            sqlCommand.Parameters.AddWithValue("product_id", product.product_id);
            sqlCommand.Parameters.AddWithValue("url_Image1", image_Product.url_Image1);
            sqlCommand.Parameters.AddWithValue("url_Image1", image_Product.url_Image2);
            sqlCommand.Parameters.AddWithValue("url_Image1", image_Product.url_Image3);
            sqlCommand.ExecuteNonQuery();
            connection.Close();


            return RedirectToAction("Index", "Admin_Product");
        }

    }
}