using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.EF;

namespace OnlineShopAPI.Controllers
{
    public class ProductCategoryController : ApiController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        
        public string GetAllCategories()
        {
            //return db.ProductCategories.AsEnumerable();
            
            string result = "[";
            foreach (ProductCategory p in db.ProductCategories)
            {
                if(p.ParentID==null)
                    result += "{\"id\":\"" + p.ID + "\",\"parentname\":\"" + p.Name + "\",\"childname\":\"" + string.Empty + "\"},";
                else
                {
                    result += "{\"id\":\"" + p.ID + "\",\"parentname\":\"" + string.Empty + "\",\"childname\":\"" + p.Name + "\"},";
                }
            }

            
            result = result.TrimEnd(',');
            result = result + "]";
            return result;
        }
    }
}
