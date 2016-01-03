using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAPI.Models.ViewModels
{
    public class UserViewModel
    {
        public int MA { get; set; }
        public string TENDANGNHAP { get; set; }
        public string HOTEN { get; set; }
        public DateTime NGAYSINH { get; set; }
        public string GIOITINH { get; set; }
        public string DIACHI { get; set; }
        public string SDT { get; set; }
       
      
    }
}