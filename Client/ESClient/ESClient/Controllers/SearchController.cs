using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESApi.Models.ModelEntity;
using ESClient.Models.Code;

namespace ESClient.Controllers
{
    public class SearchController : Controller
    {
        public ManufactoryCode code = new ManufactoryCode();
        public MenuCode menucode = new MenuCode();
        public  SearchCode searchcode = new SearchCode();

        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult AdvanceSearch()
        {
            List<SelectListItem> listitem = new List<SelectListItem>();
            var listNSX = code.GetManufactoryList();

            listitem.Add(new SelectListItem { Text = "---Tất cả---", Value = "0" });
            foreach (NHASANXUATModel n in listNSX.listNhaSanXuat)
            {
                SelectListItem item = new SelectListItem();
                item.Text = n.TEN;
                listitem.Add(item);
            }
            ViewBag.NHASANXUAT = listitem;

            var listLSP = menucode.GetMenuList().listDanhMuc;
            listitem = new List<SelectListItem>();
            listitem.Add(new SelectListItem { Text = "---Tất cả---", Value = "0" });

            foreach (LOAISANPHAMModel n in listLSP)
            {
                SelectListItem item = new SelectListItem();
                item.Text = n.TEN;
                listitem.Add(item);
            }
            ViewBag.LOAISANPHAM = listitem;

            listitem = new List<SelectListItem>();
            listitem.Add(new SelectListItem { Text = "---Tất cả---", Value = "0" });
            listitem.Add(new SelectListItem { Text = "Dưới 2 triệu", Value = "1" });
            listitem.Add(new SelectListItem { Text = "2 triệu - dưới 5 triệu", Value = "2" });
            listitem.Add(new SelectListItem { Text = "5 triệu - dưới 10 triệu", Value = "3" });
            listitem.Add(new SelectListItem { Text = "Từ 10 triệu trở lên", Value = "4" });
            ViewBag.GIABAN = listitem;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvanceSearch(FormCollection form)
        {
            SearchModel search = new SearchModel();
            search.Ten = form["TEN"];

            if (form["NHASANXUAT"].ToString() != "0")
                search.NhaSanSuat = form["NHASANXUAT"];
            else
                search.NhaSanSuat = "";

            if (form["LOAISANPHAM"].ToString() != "0")
                search.LoaiSanPham = form["LOAISANPHAM"];
            else
            {
                search.LoaiSanPham = "";
            }

            search.GiaToiThieu = searchcode.TinhGiaToiThieu(form["GiaBan"]);
            search.GiaToiDa = searchcode.TinhGiaToiDa(form["GiaBan"]);

            if (form["KHUYENMAI"] == "false")
                search.KhuyenMai = false;
            else
                search.KhuyenMai = true;

            if (form["BANCHAY"] == "false")
                search.SPBanChay = false;
            else
                search.SPBanChay = true;

            TempData["Advance_search"] = search;
            return RedirectToAction("Index", "Product", new { name = "AdvanceSearch", id = -1 });
        }

        
    }
}