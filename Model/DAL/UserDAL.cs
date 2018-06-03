using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.DAL
{
    
    public class UserDAL
    {
        OnlineShopDbContext db = null;
        public UserDAL()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }
        public User GetUserByUserName(string userName)
        {
            //return db.Users.Find(userID);
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public bool Update(User user)
        {
            try
            {
                var u = db.Users.Find(user.ID);
                u.Name = user.Name;
               
                u.Password =  user.Password;
                u.Address = user.Address;
                u.Email = user.Email;
                u.Phone = user.Phone;
                u.ModifyBy = u.UserName;
                u.ModifyDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //write log
                return false;
            }
        }

        public User GetUserByID(int ID)
        {
            return db.Users.Find(ID);
        }
        public int Login(string userName,string password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else 
            {
                if (result.Status == false)
                    return -1;
                else
                {
                    if (result.Password == password)
                        return 1;
                    else
                    {
                        return -2;
                    }
                }
            }
        }


        public bool Delete(int ID)
        {
            try
            {
                var user = db.Users.Find(ID);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //write log
                return false;
            }

        }

        public bool ChangeStatus(long ID)
        {
            var user = db.Users.Find(ID);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }

        public IPagedList<User> GetAllUser(string search, int pageNumber, int pageSize)
        {
            IOrderedQueryable<User> model = db.Users.OrderByDescending(x => x.CreateDate);
            if (!string.IsNullOrEmpty(search))
                model = db.Users.Where(x => x.UserName.Contains(search) || x.Name.Contains(search)).OrderByDescending(x=>x.CreateDate);

            return model.ToPagedList(pageNumber,pageSize);
        }
    }
}
