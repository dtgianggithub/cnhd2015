﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Areas.Admin.Controllers
{
    public class CategoryManageController : Controller
    {
        // GET: Admin/CategoryManage
        public ActionResult Index()
        {
            if (Session["login"] == null)
                return RedirectToAction("Index", "AccountAdmin");
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
            return RedirectToAction("Index", "CategoryManage");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            ViewBag.Info = "Thêm mới thành công";
            return RedirectToAction("Index", "CategoryManage");
        }
    }
}