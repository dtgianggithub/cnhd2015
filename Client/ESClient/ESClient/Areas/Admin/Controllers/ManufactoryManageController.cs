﻿using System;
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
            return View();
        }

        public ActionResult Edit(string MA)
        {
            return View();
        }
    }
}