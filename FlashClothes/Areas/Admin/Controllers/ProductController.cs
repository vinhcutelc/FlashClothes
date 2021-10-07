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
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var productDao = new ProductDao();
            var model = productDao.listProduct(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult NewProduct()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult NewProduct(Product product, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string urlImage = Server.MapPath("~/Areas/Admin/Asset/productImage/" + fileName);
                file.SaveAs(urlImage);
                product.Image = "/../Areas/Admin/Asset/productImage/" + fileName;
            }
            if (ModelState.IsValid)
            {
                product.CreatedBy = (string)Session["fullname"];
                product.CreatedDate = DateTime.Now;
                product.Status = true;
                var dao = new ProductDao();
                long id = dao.Insert(product);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            }
            SetViewBag();
            return View();
        }
        public void SetViewBag(long? selectedID=null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.listAll(), "ID", "Name", selectedID);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            SetViewBag();
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product,HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string urlImage = Server.MapPath("~/Areas/Admin/Asset/productImage/" + fileName);
                file.SaveAs(urlImage);
                product.Image = "/../Areas/Admin/Asset/productImage/" + fileName;
            }
            if (ModelState.IsValid)
            {
                product.ModifiedBy = (string)Session["fullname"];
                product.ModifiedDate = DateTime.Now;
                var dao = new ProductDao();
                var result = dao.Update(product);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
            {
                ModelState.AddModelError("", "Cập nhật sản phẩm thất bại");
            }
            SetViewBag();
            return View();
        }
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}