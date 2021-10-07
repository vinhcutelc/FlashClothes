using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        FlashClothesContext db = null;
        public UserDao()
        {
            db = new FlashClothesContext();
        }
        public User getUser(string UserName)
        {
            return db.Users.SingleOrDefault(u=>u.UserName==UserName);
        }
        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public IEnumerable<User> listManager(string searchString,int page, int pageSize)
        {
            IOrderedQueryable<User> model = db.Users.Where(u=>u.Role==2).OrderBy(p => p.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = (IOrderedQueryable<User>)model.Where(u => u.UserName.Contains(searchString) || u.Name.Contains(searchString));
            }
            return model.ToPagedList(page, pageSize);
        }
        public IEnumerable<User> listEmployee(int page, int pageSize)
        {
            return db.Users.Where(u => u.Role == 1).OrderBy(p => p.CreatedDate).ToPagedList(page, pageSize);
        }
        public User GetCustomerByUserName(string userName)
        {
            return db.Users.Where(u=>u.UserName==userName).FirstOrDefault();
        }
        public IEnumerable<User> listCustomer(int page, int pageSize)
        {
            return db.Users.Where(u => u.Role == 0).OrderBy(p => p.CreatedDate).ToPagedList(page, pageSize);
        }
        public int Login(string UserName,string Password)
        {
            var user = db.Users.SingleOrDefault(u => u.UserName == UserName);
            if (user==null)
            {
                return 0;
            }
            else
            {
                if (user.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (user.Password == Password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
        }
        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }
        public bool Update(User user)
        {
            try
            {
                var DBUser = db.Users.Find(user.ID);
                DBUser.Password = user.Password;
                DBUser.Name = user.Name;
                DBUser.Phone = user.Phone;
                DBUser.Email = user.Email;
                DBUser.Address = user.Address;
                DBUser.Status = user.Status;
                DBUser.ModifiedDate = DateTime.Now;
                DBUser.ModifiedBy = user.ModifiedBy;
                DBUser.Role = user.Role;
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
                var user = db.Users.Find(id);
                db.Users.Remove(user);
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
