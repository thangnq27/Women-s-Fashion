using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAL;
using Model.EF;
using OnLineShop.Common;
using PagedList;

namespace OnLineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(string search, int pageNumber = 1, int pageSize = 1)
        {
            var dal = new UserDAL();
            var model = dal.GetAllUser(search,pageNumber, pageSize);
            ViewBag.search = search;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                string passwordMD5 = Encryption.MD5Hash(user.Password);
                user.Password = passwordMD5;
                long id = dal.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("exception", "Có lỗi xảy ra");
                }
            }

            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dal = new UserDAL();
            var user = dal.GetUserByID(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dal = new UserDAL();
                if (user.Password != "")
                {
                    string passwordMD5 = Encryption.MD5Hash(user.Password);
                    user.Password = passwordMD5;
                }

                bool checkUpdate = dal.Update(user);
                if (checkUpdate)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("error", "Có lỗi xảy ra!");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int ID)
        {
            var dal=new UserDAL();
            bool result = dal.Delete(ID);
            if (result)
                return RedirectToAction("Index");
            else
            {
                ModelState.AddModelError("error","Có lỗi xảy ra!!!");
                return View("Index");

            }
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var dal=new UserDAL();
            var result = dal.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}