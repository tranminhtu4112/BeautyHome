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
    public class CartController : Controller
    {
        SqlConnection connection = DBUtils.GetDBConnection();
        public BeautyHomeEntities db = new BeautyHomeEntities();
        // GET: Cart
        public ActionResult Index()
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();

            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;
            return View(objtypeProductView);
        }
        public ActionResult AddCart(String productId)
        {
            List<long> listProductId;
            if (Session["listProductId"] == null)
            {
                listProductId = new List<long>();
                listProductId.Add(long.Parse(productId));
                Session["listProductId"] = listProductId;
                return RedirectToAction("Index", "Product", new { CountCart = listProductId.Count });
            }
            listProductId = (List<long>)Session["listProductId"];
            listProductId.Add(long.Parse(productId));
            Session["listProductId"] = listProductId;

            return RedirectToAction("Index", "Product", new { CountCart = listProductId.Count });
        }
    }
}