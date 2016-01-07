using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ModelEntity
{
    public class THANHVIENModel
    {
        public int MA { get; set; }
        public string TENDANGNHAP { get; set; }
        public string MATKHAU { get; set; }
        public string TEN { get; set; }
        public string DIACHI { get; set; }
        public string DIENTHOAI { get; set; }
        public string EMAIL { get; set; }
        public string THONGTINMOTA { get; set; }
        public string ACCESSTOKEN { get; set; }
        public int LOAITHANHVIEN { get; set; }
        public bool DAXOA { get; set; }
    }
}