using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCatetogy
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDao();
            var model = dao.listCategory(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult NewProductCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewProductCategory(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.CreatedBy = (string)Session["fullname"];
                productCategory.CreatedDate = DateTime.Now;
                productCategory.Status = true;
                ViewBag.ProductCategory = productCategory;
                var dao = new ProductCategoryDao();
                long id = dao.Insert(productCategory);
                if (id > 0)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thêm danh mục sản phẩm thất bại");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productCategory = new ProductCategoryDao().ViewDetail(id);
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                productCategory.ModifiedBy = (string)Session["fullname"];
                productCategory.ModifiedDate = DateTime.Now;
                var dao = new ProductCategoryDao();
                var result = dao.Update(productCategory);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}