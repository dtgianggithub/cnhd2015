using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
{
    public class SearchModel
    {
        public string Ten { get; set; }
        public string NhaSanSuat { get; set; }
        public string LoaiSanPham { get; set; }
        public double GiaToiThieu { get; set; }
        public double GiaToiDa { get; set; }
        public bool KhuyenMai { get; set; }
        public bool SPBanChay { get; set; }
    }
}