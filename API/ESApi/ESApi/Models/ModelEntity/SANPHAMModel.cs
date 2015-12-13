﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ModelEntity
{
    public class SANPHAMModel
    {
        public string MA { get; set; }
        public string TEN { get; set; }
        public Nullable<double> DONGIABAN { get; set; }
        public string HINHANH { get; set; }
        public string MOTA { get; set; }
        public int  SOLUONG { get; set; }
        public int LOAISANPHAM { get; set; }
        public int SANPHAMMOI { get; set; }
        public int NHASANXUAT { get; set; }
        public int SANPHAMBANCHAY { get; set; }
        public bool DAXOA { get; set; }
        public int MAKHUYENMAI { get; set; }
    }
}