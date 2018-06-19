using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OnLineShop.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        public ActionResult Index(ProductCategory pc)
        {
            return View(pc);
        }

        public ActionResult GetCategoryById(string id)
        {
            ProductCategory dal=new ProductCategory();
            Model.EF.ProductCategory pc=new Model.EF.ProductCategory();
            pc = dal.GetProductCategoryById(id);
            return View(pc);
        }
    }
}