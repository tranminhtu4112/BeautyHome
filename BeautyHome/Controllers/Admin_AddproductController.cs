using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_AddproductController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        // GET: Admin_Addproduct
        public ActionResult Index(String productId, String fullname, String count, String address, String phone, String email)
        {
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
            return View();
        }
    }
}