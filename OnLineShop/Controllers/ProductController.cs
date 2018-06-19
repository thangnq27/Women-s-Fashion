using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAL;
using OnLineShop.Areas.Admin.Controllers;

namespace OnLineShop.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        //Model.DAL.Product dal = new Model.DAL.Product();
        //List<Model.EF.Product> listProduct = new List<Model.EF.Product>();
        public ActionResult Index()
        {
            //listProduct = dal.GetProductByCategorId(id);
            ////return View(listProduct);
            return View();
        }

        //public ActionResult ModalProduct(string id)
        //{
        //    Product p=new Product();
        //    Model.EF.Product product = p.GetProductById(id);
        //    return PartialView("ModalProduct", product);
        //}
    }
}