using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Areas.Admin.Controllers
{
    public class ManufactoryManageController : Controller
    {
        // GET: Admin/ManufactoryManage
        public ActionResult Index()
        {
            ViewBag.Info = "";
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Ma = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            ViewBag.Info = "Cập nhật thành công";
            return RedirectToAction("Index","ManufactoryManage");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ViewBag.Info = "Thêm mới thành công";
            return RedirectToAction("Index", "ManufactoryManage");
        }
    }
}