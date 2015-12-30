using ESClient.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESClient.Models.EntityModel
{
    public class CartSession
    {
        public SANPHAMModel sp { get; set; }
        public int soluong { get; set; }
        public bool daxoa { get; set; }
    }
}