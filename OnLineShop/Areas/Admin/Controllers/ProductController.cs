using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.Design;
using System.Xml;
using Model.EF;
using Newtonsoft.Json;

namespace OnLineShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var path = @"D:\Data\Product\Products.xml";
            XmlDocument doc=new XmlDocument();
            doc.Load(path);

            List<Product> products=new List<Product>();
            XmlNodeList nodes = doc.GetElementsByTagName("Product");
            foreach (XmlNode node in nodes)
            {
                Product p=new Product();
                p.Name = node["Name"].InnerText;
                p.Images = node["Images"].InnerText;
                p.Price = decimal.Parse(node["Price"].InnerText);
                p.PromotionPrice = decimal.Parse(node["PromotionPrice"].InnerText);
                p.Status = Convert.ToBoolean(node["Status"].InnerText);
                products.Add(p);
            }


            return View(products);
        }

        public JsonResult ImportData(string data)
        {
            Model.DAL.Product p=new Model.DAL.Product();
            List<Model.EF.Product> jObject = JsonConvert.DeserializeObject<List<Model.EF.Product>>(data);

            p.ImportData(jObject);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProduct()
        {
            Model.DAL.Product dal=new Model.DAL.Product();
            List<Model.EF.Product> products = dal.GetallProducts();
            return View("GetAllProduct",products);
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            HttpPostedFileBase file = Request.Files["FileUpload"];
            file.SaveAs(Server.MapPath("~/Areas/Admin/assets/images/products/") + file.FileName);
            return new JsonResult
            {
                Data = "success",
                ContentType = "application/json;charset=utf-8"
            };
        }
    }
}