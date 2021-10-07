using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            var orderCount = new DetailOrderDao().CountOrder();
            var orderDetail = new DetailOrderDao().ListDetailOrderAll();
            decimal? totalProfit = 0;
            int? totalQuantity = 0;
            foreach (var item in orderDetail)
            {
                totalProfit += item.Price;
                totalQuantity += item.Quantity;
            }
            TempData["totalProfit"] = totalProfit;
            TempData["totalQuantity"] = totalQuantity;
            TempData["count"] = orderCount;
            TempData["averagePrice"] = 0;
            //totalProfit / orderCount;
            return View(orderDetail);
        }
    }
}