﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class Admin_ProductController : Controller
    {
        // GET: Admin_Product
        public ActionResult Index()
        {
            try
            {
                if (Int32.Parse(Session["role"].ToString()) == 1)
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