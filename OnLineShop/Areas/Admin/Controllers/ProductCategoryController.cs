using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using Model.EF;
using  Model.DAL;
using OnLineShop.Common;

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
            List<Model.EF.ProductCategory> listData = new List<Model.EF.ProductCategory>();
            foreach (XmlNode node in elNodeList)
            {
                string name = node.ChildNodes[0].InnerText;
                int parentID = Convert.ToInt32(node.ChildNodes[1].InnerText);
                int order = Convert.ToInt32(node.ChildNodes[2].InnerText);

                Model.EF.ProductCategory category=new Model.EF.ProductCategory();
                category.Name = name;
                category.ParentID = parentID;
                category.DisplayOrder = order;
                category.CreateBy = CommonConstaint.USER_SESSION;
                listData.Add(category);
            }
            UserLogin userLogin = (UserLogin)Session["USER_SESSION"];
            //Session.Add(CommonConstaint.USER_SESSION, userSession);
            Session.Add(CommonConstaint.USERNAME_SESSION, userLogin.UserName);
            return View(listData);
        }

        public JsonResult Synchronized(string dataJson)
        {
           var dal=new Model.DAL.ProductCategory();
            var result = dal.SynChronizedData(dataJson);
            return Json(new
            {
                status=result
            });
        }
    }
}