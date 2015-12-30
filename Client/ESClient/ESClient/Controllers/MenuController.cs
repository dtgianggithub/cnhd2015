using ESClient.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class MenuController : Controller
    {
        [ChildActionOnly]
        // GET: Menu
        public ActionResult Index()
        {
            MenuCode code = new MenuCode();
            return PartialView(code.GetMenuList());
        }
    }
}