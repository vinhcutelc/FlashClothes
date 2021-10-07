using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var order = new OrderDao().ListOrder(page,pageSize);
            return View(order);
        }
        public ActionResult Delete(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Status(long id)
        {
            new OrderDao().UpdateStatus(id);
            return RedirectToAction("Index");
        }
        public ActionResult DetailOrder(long id)
        {
            TempData["IDDetailOrder"] = id;
            decimal? total = 0;
            var order = new DetailOrderDao().ListDetailOrderByID(id);
            foreach(var item in order)
            {
                var product = new ProductDao().ViewDetail((int)item.ProductID);
                ViewBag.ProductName = product.Name;
                total += item.Price;
            }
            TempData["TotalDetail"] = total;
            return View(order);
        }
    }
}