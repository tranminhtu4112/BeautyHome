using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class ProductController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        SqlConnection connection = DBUtils.GetDBConnection();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}