using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAL;
using OnLineShop.Areas.Admin.Controllers;
using ProductCategory = Model.EF.ProductCategory;

namespace OnLineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Model.DAL.Product dal = new Model.DAL.Product();
        List<Model.EF.Product> listProduct = new List<Model.EF.Product>();
        public ActionResult Index(string id)
        {
            listProduct = dal.GetProductByCategorId(id);
            return PartialView("_listProduct", listProduct);
            //return View();
        }

        public ActionResult ModalProduct(string id)
        {
            Product p = new Product();
            Model.EF.Product product = p.GetProductById(id);
            return PartialView("_ModalProduct", product);
        }
        public ActionResult GetProductById(string id)
        {
            Product product=new Product();
            Model.EF.Product p = product.GetProductById(id);
            return View("GetProductById",p);
        }

        
    }
}