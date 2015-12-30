using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
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