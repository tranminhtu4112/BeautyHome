using BeautyHome.Context;
using BeautyHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BeautyHome.Controllers
{
    public class LoginController : Controller
    {
        public BeautyHomeEntities db = new BeautyHomeEntities();
        // GET: Login
        public ActionResult Index()
        {
            var listtype = db.type_product.ToList();
            var listfur = db.furnitures.ToList();
            TypeProductView objtypeProductView = new TypeProductView();
            objtypeProductView.listtype = listtype;
            objtypeProductView.listfur = listfur;
            return View(objtypeProductView);
        }
        //POST: Register
        [HttpPost]

        public ActionResult Register(String username, string password, string fullname, string email, String address, string phone)
        {
            if (ModelState.IsValid)
            {
                user _user = new user();
                var check = db.users.FirstOrDefault(s => s.user_name == username);
                if (check == null)
                {
                    password = GetMD5(password);
                    db.Configuration.ValidateOnSaveEnabled = false;
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
                else
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại !";
                    return RedirectToAction("Index", "Login");
                }
            }
            return RedirectToAction("Index", "Login");
        }
        // dang nhap
        [HttpPost]
        public ActionResult ReLogin(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.users.Where(s => s.user_name.Equals(username) && s.password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["userid"] = data.FirstOrDefault().user_id;
                    Session["username"] = data.FirstOrDefault().user_name;
                    Session["password"] = password;
                    Session["fullname"] = data.FirstOrDefault().full_name;
                    Session["email"] = data.FirstOrDefault().email;
                    Session["phone"] = data.FirstOrDefault().phone;
                    Session["address"] = data.FirstOrDefault().address;
                    Session["role"] = data.FirstOrDefault().role;
                    if(Convert.ToInt32(data.FirstOrDefault().role.ToString()) == 0)
                    {
                        return RedirectToAction("Index", "Admin_Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}