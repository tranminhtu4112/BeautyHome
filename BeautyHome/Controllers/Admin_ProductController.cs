using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_ProductController : Controller
    {
        // GET: Admin_Product
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        public ActionResult Index(int page = 1, int pagesize = 12)
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

            ViewBag.ListProductadmin = listpr.ToPagedList(page, pagesize);
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
        public ActionResult DeleteType(String typeId)
        {
            string Sql = "select product.product_id " +
            "from type_product, product " +
             "where type_product.type_product_id = product.product_id and product.type_product_id =" + typeId;

            List<long> list = new List<long>();
            long Id = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = Sql;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Id = Convert.ToInt64(reader.GetValue(0));
                        list.Add(Id);

                    }
                }
            }
            SqlCommand sqlCommand;
            for (int i = 0; i < list.Count; i++)
            {
                string sqlimg = " delete from  image_product where product_id = " + list[i];
                sqlCommand = new SqlCommand(sqlimg, connection);
                sqlCommand.ExecuteNonQuery();
            }
            for (int i = 0; i < list.Count; i++)
            {
                string SQLDeleteProduct = "delete from product where product_id = " + list[i];
                sqlCommand = new SqlCommand(SQLDeleteProduct, connection);
                sqlCommand.ExecuteNonQuery();
            }


            string SQLDelete = " delete from type_product where type_product_id = " + typeId;

            sqlCommand = new SqlCommand(SQLDelete, connection);
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

    }
}