using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ModelEntity
{
    public class DONHANGModel
    {
        public string MA { get; set; }
        public double TONGTIEN { get; set; }
        public DateTime NGAYDATHANG { get; set; }
        public DateTime NGAYNHANHANG { get; set; }
        public string TENNGUOINHAN { get; set; }
        public string DIACHINHAN { get; set; }
        public string DIENTHOAINGUOINHAN { get; set; }
        public int TRANGTHAI { get; set; }
        public bool DAXOA { get; set; }
    }
}