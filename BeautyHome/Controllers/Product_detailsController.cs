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
    public class Product_detailsController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Product
        public ActionResult Index(long prId)
        {

            TypeProductView objtypeProductView = new TypeProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;

            string sql = "select * " +
                "from product, furniture, type_product, image_product " +
                "where type_product.type_product_id = product.type_product_id " +
                "and furniture.furniture_id = type_product.furniture_id and product.product_id = image_product.product_id and product.product_id = " + prId;

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

            string sqlcomment = "select [comment_product].* ,  [user].full_name from [comment_product], [user] " +
                                "where[user].user_id = [comment_product].user_id and[comment_product].product_id = " + prId;

            List<UserComment> listUserComment = new List<UserComment>();
            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sqlcomment;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserComment cmt = new UserComment();

                        cmt.comment_puduct_id = Convert.ToInt64(reader.GetValue(0));
                        cmt.product_id = Convert.ToInt64(reader.GetValue(1));
                       
                        cmt.txt_comment = Convert.ToString(reader.GetValue(3));
                        try
                        {
                            if (Session["userid"].ToString().Equals(Convert.ToString(reader.GetValue(2))))
                            {
                                cmt.fullname = "Bạn";
                            }
                            else
                            {
                                cmt.fullname = Convert.ToString(reader.GetValue(4));
                            }
                        }
                        catch
                        {
                            cmt.fullname = Convert.ToString(reader.GetValue(4));
                        }
                        
                        listUserComment.Add(cmt);
                    }
                }
            }
            objtypeProductView.listUserComment = listUserComment;

            return View(objtypeProductView);
        }
        [HttpPost]
        public ActionResult addComment(long productId, String commentContent)
        {
            long userId = 0;
            try
            {
                userId = Convert.ToInt64(@Session["userid"].ToString());
            }
            catch
            {
                return RedirectToAction("Index", "Login");
            }
                comment_product comment = new comment_product();
                comment.user_id = userId;
                comment.product_id = productId;
                comment.txt_comment = commentContent;
                db.comment_product.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index", "Product_details", new { prId = productId });
        }
    }
}