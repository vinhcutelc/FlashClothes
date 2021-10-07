using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Detail(int id)
        {
            {
                var DbProduct = new ProductDao().ViewDetail(id);
                var DbCategoryProduct = new ProductCategoryDao().listCategoryByID((int)DbProduct.CategoryID);
                ViewBag.CategoryProduct = DbCategoryProduct;
                if (DbProduct == null)
                {
                    return HttpNotFound();
                }
                return View(DbProduct);
            }
        }
    }
}