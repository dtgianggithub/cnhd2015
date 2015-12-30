using ESClient.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class ManufactoryController : Controller
    {
        public ManufactoryCode code = new ManufactoryCode();
        // GET: Manufactory
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView(code.GetManufactoryList());
        }
    }
}