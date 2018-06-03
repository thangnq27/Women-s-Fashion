using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnLineShop.Areas.Admin.Models;
using Model.DAL;
using OnLineShop.Common;

namespace OnLineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                var result = dal.Login(model.UserName,Encryption.MD5Hash(model.Password));
                if (result==1)
                {
                    var user = dal.GetUserByUserName(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstaint.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("login","Account does not exits");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("login", "Account has been locked");
                }
                else
                {
                    ModelState.AddModelError("login", "Login fail");
                }
            }
            return View("Index");
        }
    }
}