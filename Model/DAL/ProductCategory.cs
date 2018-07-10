using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Model.EF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Model.DAL
{
    public class ProductCategory
    {
        OnlineShopDbContext db = null;

        public ProductCategory()
        {
            db = new OnlineShopDbContext();
        }

        public bool SynChronizedData(string data)
        {
            var cat = new EF.ProductCategory();
            try
            {
                //var jObject = JObject.Parse(data);
                List<Model.EF.ProductCategory> jObject = JsonConvert.DeserializeObject<List<Model.EF.ProductCategory>>(data);


                foreach (EF.ProductCategory category in jObject)
                {
                    cat.Name = category.Name;
                    cat.ParentID = category.ParentID;
                    cat.DisplayOrder = category.DisplayOrder;
                    cat.CreateDate = DateTime.Now;
                    cat.CreateBy = category.CreateBy;
                    cat.ShowOnHome = true;
                    cat.Status = true;
                    db.ProductCategories.Add(cat);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                //write log
                return false;
            }


        }

        public EF.ProductCategory GetProductCategoryById(string id)
        {
            EF.ProductCategory pc = new EF.ProductCategory();
            pc = db.ProductCategories.Find(Int64.Parse(id));
            return pc;
        }

        public IQueryable<IGrouping<bool, EF.ProductCategory>> GetAllCategories()
        {
            //return db.ProductCategories.ToList();
            var categories = db.ProductCategories;
            //var results = categories.GroupBy(
            //    p => p.ParentID,
            //    p => p.Name,
            //    (key, g) => new { PersonId = key, Cars = g.ToList() });
            //return results.AsQueryable();

            var x = categories.GroupBy(p => p.ParentID != null);
            //foreach (IGrouping<bool, EF.ProductCategory> item in x)
            //{

            //}

            var result = x.ToList().AsQueryable();
            return result;
        }

        public EF.ProductCategory Update(EF.ProductCategory pc)
        {
            //try
            //{
                Int64 id = pc.ID;
                EF.ProductCategory productCategory = db.ProductCategories.Find(id);
                productCategory.Name = pc.Name;
                productCategory.DisplayOrder = pc.DisplayOrder;
            productCategory.MetaTitle = pc.MetaTitle;
            db.SaveChanges();
                return productCategory;
            //}
            //catch (Exception e)
            //{
            //    return pc;
            //}
        }

    }
}
