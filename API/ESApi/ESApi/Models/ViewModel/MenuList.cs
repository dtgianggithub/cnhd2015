using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
{
    public class MenuList
    {
        public List<LOAISANPHAMModel> listDanhMuc { get; set; }

        public MenuList()
        {
            listDanhMuc = new List<LOAISANPHAMModel>();
        }
    }
}