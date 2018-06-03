using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace OnLineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {

            string path = @"D:\Data\ProductCategory\XMLFile1.xml";
            XmlDocument doc=new XmlDocument();
            doc.Load(path);
            XmlNodeList elNodeList = doc.GetElementsByTagName("Category");
            List<string> listData=new List<string>();
            foreach (XmlNode node in elNodeList)
            {
                string name = node.ChildNodes[0].InnerText;
                int order=Convert.ToInt32(node.ChildNodes[1].InnerText);

                //listData.Add();
            }
            return View();
        }
    }
}