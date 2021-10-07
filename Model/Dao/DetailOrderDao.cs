using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class DetailOrderDao
    {
        FlashClothesContext db = null;
        public DetailOrderDao()
        {
            db = new FlashClothesContext();
        }
        public bool Insert(DetailOrder detailOrder)
        {
            try
            {
                db.DetailOrders.Add(detailOrder);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public List<DetailOrder> ListDetailOrderByID(long id)
        {
            return db.DetailOrders.Where(d => d.OrderID == id).ToList();
        }
        public long CountOrder()
        {
            return db.DetailOrders.Select( x=>x.OrderID).Distinct().Count();
        }
        public IEnumerable<DetailOrder> ListDetailOrderAll()
        {
            return db.DetailOrders.OrderBy(x => x.OrderID).ToList();
        }
    }
}
