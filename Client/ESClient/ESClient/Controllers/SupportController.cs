using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESClient.Models.Code;

namespace ESClient.Controllers
{
    public class SupportController : Controller
    {
        public SupportCode code = new SupportCode();
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SupportOnline()
        {
            var listsupport = code.GetList();
            return PartialView(listsupport);
        }
    }
}