using BeautyHome.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class AccountController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateAccount(String username, string password, string fullname, string email, String address, string phone)
        {
            user _user = new user();
            _user.user_name = username;
            _user.password = password;
            _user.full_name = fullname;
            _user.email = email;
            _user.phone = phone;
            _user.address = address;
            _user.role = 1; // set role la khach hang = 1
            db.users.Add(_user);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
    }
}