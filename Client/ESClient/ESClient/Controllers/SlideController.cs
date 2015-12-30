using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class SlideController : Controller
    {
        // GET: Slide
        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}