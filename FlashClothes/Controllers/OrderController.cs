using FlashClothes.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlashClothes.Controllers
{
    public class OrderController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListCartSession()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddOrder(string name,string phone,string address,string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipName = name;
            order.ShipPhone = phone;
            order.ShipAddress = address;
            order.ShipEmail = email;
            order.Status = 0;
            order.Customer = (string)Session["UserName"];
            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new DetailOrderDao();
                foreach (var item in cart)
                {
                    var detailOrder = new DetailOrder();
                    detailOrder.ProductID = item.Product.ID;
                    detailOrder.OrderID = id;
                    detailOrder.Price = item.Product.Price;
                    detailOrder.Quantity = item.Quantity;

                    detailDao.Insert(detailOrder);
                }
                Session[CartSession] = null;
            }
            catch(Exception)
            {

            }
            return RedirectToAction("Success");
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult DetailOrder()
        {
            var orderDb = new OrderDao().ListOrderByUsername((string)Session["UserName"]);
            decimal? total = 0;
            foreach (var order in orderDb)
            {
                var detailOrder = new DetailOrderDao().ListDetailOrderByID(order.ID);
                foreach (var item in detailOrder)
                {
                    var product = new ProductDao().listProductByID((int)item.ProductID);
                    ViewBag.Product = product;
                    total += item.Price;
                }
            }
            TempData["TotalDetail"] = total;

            return View(orderDb);
        }
        public ActionResult Delete(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveByCustomer(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index","Home");
        }
    }
}