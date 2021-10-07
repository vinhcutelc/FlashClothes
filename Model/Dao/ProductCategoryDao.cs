using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        FlashClothesContext db = null;
        public ProductCategoryDao()
        {
            db = new FlashClothesContext();
        }
        public List<ProductCategory> listAll()
        {
            return db.ProductCategories.Where(p => p.Status == true).ToList();
        }
        public IEnumerable<ProductCategory> listCategory(int page, int pageSize)
        {
            return db.ProductCategories.OrderByDescending(p=>p.CreatedDate).ToPagedList(page,pageSize);
        }
        public List<ProductCategory> listCategoryByID(int id)
        {
            return db.ProductCategories.Where(p=>p.ID==id).OrderByDescending(p => p.CreatedDate).ToList();
        }
        public ProductCategory ViewDetail(int id)
        {
            return db.ProductCategories.Find(id);
        }
        public long Insert(ProductCategory productCategory)
        {
            db.ProductCategories.Add(productCategory);
            db.SaveChanges();
            return productCategory.ID;
        }
        public bool Update(ProductCategory productCategory)
        {
            try
            {
                var DbProductCategory = db.ProductCategories.Find(productCategory.ID);
                DbProductCategory.Name = productCategory.Name;
                DbProductCategory.Status = productCategory.Status;
                DbProductCategory.ModifiedDate = DateTime.Now;
                DbProductCategory.ModifiedBy = productCategory.ModifiedBy;
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
                var productCategories = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productCategories);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
