using FlashClothes.Common;
using FlashClothes.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FlashClothes.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View("SignUp");
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(loginModel.UserName, EncryptPassword.MD5Hash(loginModel.Password));
                if (result == 1)
                {
                    var user = dao.getUser(loginModel.UserName);
                    var userSession = new CustomerLogin();
                    if (user.Role !=0)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                    }
                    else
                    {
                        userSession.UserID = user.ID;
                        userSession.UserName = user.UserName;
                        Session["UserName"] = user.UserName;
                        Session["Name"] = user.Name;
                        Session["Role"] = user.Role;
                        Session.Add(Const.CUSTOMER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản chưa được kích hoạt");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
            }
            return View("Index","Home");
        }
        public ActionResult CreateAccount(User user)
        {
            if (ModelState.IsValid)
            {
                var encryptPassword = EncryptPassword.MD5Hash(user.Password);
                user.Password = encryptPassword;
                user.CreatedDate = DateTime.Now;
                user.Status = true;
                user.Role = 0;
                var dao = new UserDao();
                long id = dao.Insert(user);
                if (id > 0)
                {
                    RedirectToAction("Index", "Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tạo tài khoản thành công");
            }

            return View("Index");
        }
        public ActionResult LogOut()
        {
            if (Session != null)
            {
                Session.Clear();
                return View("Index");
            }
            else
            {
                return View("Index");
            }
        }
    }
}