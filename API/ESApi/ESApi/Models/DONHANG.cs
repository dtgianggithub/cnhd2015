//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DONHANG
    {
        public DONHANG()
        {
            this.CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
        }
    
        public string MA { get; set; }
        public Nullable<double> TONGTIEN { get; set; }
        public Nullable<System.DateTime> NGAYDATHANG { get; set; }
        public Nullable<System.DateTime> NGAYNHANHANG { get; set; }
        public string TENNGUOINHAN { get; set; }
        public string DIACHINHAN { get; set; }
        public string DIENTHOAINGUOINHAN { get; set; }
        public Nullable<int> TRANGTHAI { get; set; }
        public Nullable<bool> DAXOA { get; set; }
    
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }
        public virtual TRANGTHAIDONHANG TRANGTHAIDONHANG { get; set; }
    }
}