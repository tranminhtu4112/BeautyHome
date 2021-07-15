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
    public class Admin_ProductController : Controller
    {
        // GET: Admin_Product
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        public ActionResult Index()
        {
            AdminProductView objadminProductView = new AdminProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            var listpr = db.products.ToList();
            objadminProductView.listtype = listtype;
            objadminProductView.listfur = listfur;
            objadminProductView.listProduct = listpr;
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
        public ActionResult Delete(String productId)
        {
            String SQLDeleteImg = "delete from image_product where product_id =  " + productId;
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(SQLDeleteImg, connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            String SQLDeleteProduct = "delete from product where product_id = " + productId;
            connection.Open();
            sqlCommand = new SqlCommand(SQLDeleteProduct, connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            AdminProductView objadminProductView = new AdminProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            var listpr = db.products.ToList();
            objadminProductView.listtype = listtype;
            objadminProductView.listfur = listfur;
            objadminProductView.listProduct = listpr;

            return RedirectToAction("Index", "Admin_Product");
        }
/*        public ActionResult DeleteType(String typeId)
        {
            String SQLDelete = " delete from type_product where type_product_id = " + typeId;

            connection.Open();
            SqlCommand sqlCommand = new SqlCommand(SQLDelete, connection);
            sqlCommand.ExecuteNonQuery();
            connection.Close();

            AdminProductView objadminProductView = new AdminProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            var listpr = db.products.ToList();
            objadminProductView.listtype = listtype;
            objadminProductView.listfur = listfur;
            objadminProductView.listProduct = listpr;

            return RedirectToAction("Index", "Admin_Product");
        }*/
    }
}