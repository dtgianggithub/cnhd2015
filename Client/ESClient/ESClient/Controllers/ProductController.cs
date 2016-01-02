using ESClient.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class ProductController : Controller
    {
        public ProductCode code = new ProductCode();
        public SearchCode searchcode = new SearchCode();
        // GET: Product
        public ActionResult Index(string name, int id)
        {
            if (name == "AdvanceSearch")
                return View(searchcode.SearchAdvance(TempData["Advance_search"] as SearchModel));
            return View(code.GetProductList(name, id));
        }

        [ChildActionOnly]
        public ActionResult SpecialProduct()
        {
            return PartialView(code.GetSpecialProduct());
        }

        public ActionResult Details(string id) // nhan id de biet dang hien thi detail san pham nao
        {
            return View(code.GetDetailProduct(id));
        }

        [ChildActionOnly]
        public ActionResult NewProduct()
        {
            return PartialView(code.GetNewProduct());
        }


        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            return RedirectToAction("Index", new { name = form["Search_string"], id = -2 });
        }
    }
}