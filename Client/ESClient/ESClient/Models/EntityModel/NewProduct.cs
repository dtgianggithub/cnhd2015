using ESClient.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESClient.Models.EntityModel
{
    public class NewProduct
    {
        public SANPHAMModel newproduct { get; set; }
        public double promotion { get; set; }

        public NewProduct()
        {
            newproduct = new SANPHAMModel();
            promotion = 0;
        }
    }
}