using FlashClothes.Models;
using Model.Dao;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";

        public ActionResult Index(int? category, string currentFilter, string searchString, int? page)
        {
            SetViewBag();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var dbProductCategory = new ProductCategoryDao().listAll();
            ViewBag.Category = dbProductCategory.ToList();
            if (category != null)
            {
                var dbProduct = new ProductDao().listAllProduct();
                var products = dbProduct.Where(a => a.CategoryID == category).ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    products = products.Where(c => c.Name.Contains(searchString)).ToList();
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                ViewBag.Product = products.ToPagedList(pageNumber, pageSize);
                return View(ViewBag.Product);
            }
            else
            {
                var dbProduct = new ProductDao().listAllProduct();
                if (!String.IsNullOrEmpty(searchString))
                {
                    dbProduct = dbProduct.Where(c => c.Name.Contains(searchString)).ToList();
                }
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                ViewBag.Product = dbProduct.ToPagedList(pageNumber, pageSize);
                return View(ViewBag.Product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.listAll(), "ID", "Name", selectedID);
        }
        [ChildActionOnly]
        public PartialViewResult CartItem()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}