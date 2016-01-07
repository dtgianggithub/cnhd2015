using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESClient.Areas.Admin.Code;

namespace ESClient.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        public LoginCode code = new LoginCode();
        // GET: Admin/Account

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            if (code.Login(form["UserName"], form["PassWord"]))
            {
                Session["login"] = form["UserName"];
                return RedirectToAction("Loginsuccess", "AccountAdmin");
            }

            ViewBag.info = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }

        public ActionResult Loginsuccess()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["login"] = null;
            return RedirectToAction("Index", "AccountAdmin");
        }
    }
}