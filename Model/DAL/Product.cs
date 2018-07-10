using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAL
{
    public class Product
    {
        OnlineShopDbContext db = null;
        public Product()
        {
            db = new OnlineShopDbContext();
        }

        public List<EF.Product> GetallProducts()
        {
            return db.Products.Take(10).ToList();
        }

        public List<EF.Product> GetProductByCategorId(string id)
        {
            List<EF.Product> listProduct=new List<EF.Product>();
            long cateid = Convert.ToInt64(id);
            listProduct = db.Products.Where(p => p.CategoryID == cateid).ToList();
            return listProduct;
        }

        public EF.Product GetProductById(string id)
        {
            return db.Products.Find(Convert.ToInt64(id));
        }

        public string ImportData(List<EF.Product> products)
        {
            try
            {
                foreach (EF.Product p in products)
                {
                    //EF.Product product=new EF.Product();
                    //product.Name = p.Name;
                    //product.Images = p.Images;
                    //product.Price = p.Price;
                    //product.PromotionPrice = p.PromotionPrice;
                    //product.Status = p.Status;
                    db.Products.Add(p);
                    db.SaveChanges();
                }
                return "success";
            }
            catch (Exception e)
            {
                return "fail" + e.StackTrace;

            }
        }

        /*
         * Add to cart
         */
        //public string Update
       
    }
}
