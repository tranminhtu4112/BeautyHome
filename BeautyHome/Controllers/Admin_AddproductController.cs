using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_AddproductController : Controller
    {
        // GET: Admin_Addproduct
        public ActionResult Index()
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
            return View();
        }
    }
}