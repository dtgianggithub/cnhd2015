using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.Code
{
    public class MenuCode
    {
        public ESDBEntities db = new ESDBEntities();
        public MenuList GetMenuList()
        {
            MenuList menulist = new MenuList();
            var listDanhMuc = db.LOAISANPHAMs.Where(dm => dm.DAXOA.Value.Equals(false) ).ToList();
            Mapper.CreateMap<LOAISANPHAM, LOAISANPHAMModel>();
            menulist.listDanhMuc = Mapper.Map<List<LOAISANPHAM>, List<LOAISANPHAMModel>>(listDanhMuc);
            return menulist;
        }
    }
}