using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ModelEntity
{
    public class KHUYENMAIModel
    {
        public int MA { get; set; }
        public DateTime NGAYBATDAU { get; set; }
        public DateTime NGAYKETTHUC { get; set; }
        public int NOIDUNG { get; set; }
        public bool DAXOA { get; set; }
    }
}