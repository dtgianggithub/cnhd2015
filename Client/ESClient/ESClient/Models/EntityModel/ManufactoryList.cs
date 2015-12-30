using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESClient.Models.EntityModel
{
    public class ManufactoryList
    {
        public List<NHASANXUATModel> listNhaSanXuat { get; set; }

        public ManufactoryList()
        {
            listNhaSanXuat = new List<NHASANXUATModel>();
        }
    }
}