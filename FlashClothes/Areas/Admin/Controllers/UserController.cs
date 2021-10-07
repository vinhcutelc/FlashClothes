using FlashClothes.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult ListManager(string searchString,int page = 1, int pageSize = 10)
        {
            var manager = new UserDao().listManager(searchString,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(manager);
        }
        public ActionResult ListEmployee(int page = 1, int pageSize = 10)
        {
            var employee = new UserDao().listEmployee(page, pageSize);
            return View(employee);
        }
        public ActionResult ListCustomer(int page = 1, int pageSize = 10)
        {
            var customer = new UserDao().listCustomer(page, pageSize);
            return View(customer);
        }
        public ActionResult SignUp()
        {
            return View("CreateAccount");
        }
        public ActionResult CreateAccount(User user)
        {
            if (ModelState.IsValid)
            {
                var encryptPassword = EncryptPassword.MD5Hash(user.Password);
                user.Password = encryptPassword;
                user.CreatedDate = DateTime.Now;
                user.Status = true;
                user.Role = 1;
                var dao = new UserDao();
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("ListManager");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tạo tài khoản thành công");
            }
            return View("ListManager");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var DbUser = new UserDao().ViewDetail((int)user.ID);
                if (DbUser.Password != user.Password)
                {
                    var encryptPassword = EncryptPassword.MD5Hash(user.Password);
                    user.Password = encryptPassword;
                }
                else
                {
                    user.Password = DbUser.Password;
                }
                user.ModifiedBy = (string)Session["fullname"];
                user.ModifiedDate = DateTime.Now;
                var dao = new UserDao();
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("ListManager", "User");
                }
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật tài khoản thất bại");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("ListManager");
        }
    }
}