using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        FlashClothesContext db = null;
        public ProductDao()
        {
            db = new FlashClothesContext();
        }
        public List<Product> listAllProduct()
        {
            return db.Products.OrderBy(p => p.Name).ToList();
        }
        public IEnumerable<Product> listProductByID(long id)
        {
            return db.Products.Where(p=>p.ID==id).ToList();
        }
        public IEnumerable<Product> listProduct(int page, int pageSize)
        {
            return db.Products.OrderBy(p => p.CreatedDate).ToPagedList(page, pageSize);
        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
        public long Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }
        public bool Update(Product product)
        {
            try
            {
                var DbProduct = db.Products.Find(product.ID);
                DbProduct.Image = product.Image;
                DbProduct.Name = product.Name;
                DbProduct.Status = product.Status;
                DbProduct.ModifiedDate = DateTime.Now;
                DbProduct.ModifiedBy = product.ModifiedBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
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
