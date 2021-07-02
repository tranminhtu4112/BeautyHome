using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class HomeController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}