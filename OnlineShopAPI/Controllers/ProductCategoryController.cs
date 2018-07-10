using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Model.EF;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnlineShopAPI.Controllers
{
    [RoutePrefix("api/ProductCategory")]
    public class ProductCategoryController : ApiController
    {
        private OnlineShopDbContext db = new OnlineShopDbContext();

        [Route("GetAllCategories")]
        //[HttpPost]
        public string GetAllCategories()
        {
            //return db.ProductCategories.AsEnumerable();

            //string result = "[";
            
            Hashtable hashCategory = new Hashtable();

            //Dictionary<string,List<ProductCategory>> dicCategory=new Dictionary<string, List<ProductCategory>>();
            Dictionary<string, string> dicCategory = new Dictionary<string, string>();
            string result = "{";
            foreach (ProductCategory p in db.ProductCategories.Where(r=>r.ParentID==null))
            {
                //if (p.ParentID == null)
                //{
                //    //hashCategory.Add(p.ID,p.ID);
                //    result += "{\"id\":\"" + p.ID + "\",\"parentname\":\"" + p.Name + "\",\"childname\":\"" + string.Empty + "\"},";
                //}
                //else
                //{
                //    //hashCategory.Add(p.ParentID,p.ID);
                //    result += "{\"id\":\"" + p.ID + "\",\"parentname\":\"" + string.Empty + "\",\"childname\":\"" + p.Name + "\"},";
                //}

                //foreach (ProductCategory child in db.ProductCategories.Where(r => r.ParentID == p.ID))
                //{
                //    //result += "{\"idParent\":\"" + p.ID + "\",\"parentname\":\"" + p.Name + "\",\"idChild\":\"" + child.ID + "\",\"childname\":\"" + child.Name + "\"},";
                //}
                //string key = p.ID + ";#" + p.Name;
                string key = "\""+p.ID+";#" +p.Name+"\":[";
                string tmp = "";
                IEnumerable<ProductCategory> child = db.ProductCategories.Where(r => r.ParentID == p.ID);
                //dicCategory.Add(key, new List<ProductCategory>(child));
                
                foreach (ProductCategory x in child)
                {
                    tmp += "{\"id\":\"" + x.ID + "\",\"name\":\"" + x.Name + "\"},";
                    //tmp += "{id:" + x.ID + ",name:" + x.Name + "},";
                }
                tmp = tmp.TrimEnd(',')+"],";
                //tmp = tmp + "}";
                dicCategory.Add(key, tmp);
                result += key + tmp;

            }
            result = result.TrimEnd(',')+"}";
            //string result = JsonConvert.SerializeObject(dicCategory, Formatting.Indented);
            //result = result.TrimEnd(',');
            //result = result + "]";

            return result;
        }

        [HttpPost]
        [Route("GetCategoryById")]
        public string GetCategoryById(string id)
        {
            ProductCategory pc = new ProductCategory();
            pc = db.ProductCategories.Find(Int64.Parse(id));
            return JsonConvert.SerializeObject(pc, Formatting.Indented);
        }

        [Route("GetProductByCategoryId")]
        public string GetProductByCategoryId(string id)
        {
            Int64 categoryId = Int64.Parse(id);
            List<Product> products = db.Products.Where(p => p.CategoryID == categoryId).OrderBy(p => p.ID).ToList();
            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }

        [Route("UpdateCategory")]
        [HttpPost]
        public Model.EF.ProductCategoryTEST UpdateCategory(Model.EF.ProductCategoryTEST productCategoryTest)
        {

            return productCategoryTest;
        }
    }
}
