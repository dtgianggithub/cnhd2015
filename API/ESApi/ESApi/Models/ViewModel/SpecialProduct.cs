using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
{
    public class SpecialProduct
    {
        public SANPHAMModel specialproduct { get; set; }
        public double promotion { get; set; }

        public SpecialProduct()
        {
            specialproduct = new SANPHAMModel();
            promotion = 0;
        }
    }
}