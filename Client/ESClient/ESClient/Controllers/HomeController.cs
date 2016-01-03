using ESClient.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class HomeController : Controller
    {
        public ProductCode code = new ProductCode();

        public ActionResult Index()
        {
            //kiểm tra coi login chưa ?
            

            return View(code.GetHomeList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}