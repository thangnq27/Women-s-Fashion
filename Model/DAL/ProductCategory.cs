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
            var cat = new Model.EF.ProductCategory();
            try
            {
                //var jObject = JObject.Parse(data);
                List<Model.EF.ProductCategory> jObject = JsonConvert.DeserializeObject<List<Model.EF.ProductCategory>>(data);


                foreach (EF.ProductCategory category in jObject)
                {
                    cat.Name = category.Name;
                    cat.DisplayOrder = category.DisplayOrder;
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
    }
}
