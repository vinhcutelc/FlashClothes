using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class OrderDao
    {
        FlashClothesContext db = null;
        public OrderDao()
        {
            db = new FlashClothesContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public bool UpdateStatus(long id)
        {
            try
            {
                var order = db.Orders.Where(o => o.ID == id).FirstOrDefault();
                order.Status = order.Status+1;
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public IEnumerable<Order> ListOrderAll()
        {
            return db.Orders.OrderBy(p => p.CreatedDate);
        }
        public List<Order> ListOrderByUsername(string username)
        {
            return db.Orders.Where(o => o.Customer == username).ToList();
        }
        public IEnumerable<Order> ListOrder(int page, int pageSize)
        {
            return db.Orders.OrderBy(p => p.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var order = db.Orders.Find(id);
                db.Orders.Remove(order);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
