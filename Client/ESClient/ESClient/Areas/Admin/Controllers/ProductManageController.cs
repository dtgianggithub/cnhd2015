using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Areas.Admin.Controllers
{
    public class ProductManageController : Controller
    {
        // GET: Admin/ProductManage
        public ActionResult Index()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "AccountAdmin");
            return View();
        }

        public ActionResult Edit(String id)
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "AccountAdmin");
            ViewBag.Ma = "'" + id + "'";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            ViewBag.Info = "Cập nhật thành công";
            return RedirectToAction("Index", "ProductManage");
        }

        public ActionResult Create()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "AccountAdmin");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ViewBag.Info = "Thêm mới thành công";
            return RedirectToAction("Index", "ProductManage");
        }
    }
}