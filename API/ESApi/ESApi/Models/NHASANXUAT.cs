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
    
    public partial class NHASANXUAT
    {
        public NHASANXUAT()
        {
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public int MA { get; set; }
        public string TEN { get; set; }
        public Nullable<bool> DAXOA { get; set; }
    
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
