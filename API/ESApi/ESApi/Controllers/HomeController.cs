using ESApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESApi.Controllers
{
    public class HomeController : Controller
    {

        ESDBEntities db = new ESDBEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
