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
    public class ContactController : Controller
    {
        SqlConnection connection = DBUtils.GetDBConnection();
        public BeautyHomeEntities db = new BeautyHomeEntities();
        // GET: Contact
        public ActionResult Index()
        {
            TypeProductView objtypeProductView = new TypeProductView();
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            var listpr = db.products.ToList();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;
            objtypeProductView.listProduct = listpr;
            return View(objtypeProductView);
        }
    }
}