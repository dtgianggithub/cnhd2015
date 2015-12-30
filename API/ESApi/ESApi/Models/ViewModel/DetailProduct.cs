using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
{
    public class DetailProduct
    {
        public SANPHAMModel detailProduct { get; set; }
        public double detailProductPromotion { get; set; }
        public string manufactoryName { get; set; }
        public string typeProduct { get; set; }
        public List<SANPHAMModel> relativeList { get; set; }
        public List<double> relativeListPromotion { get; set; }

        public List<string> subjectDescription { get; set; }
        public List<string> contentDescription { get; set; }

        public DetailProduct()
        {
            detailProduct = new SANPHAMModel();
            detailProductPromotion = 0;
            manufactoryName = "";
            typeProduct = "";
            relativeList = new List<SANPHAMModel>();
            relativeListPromotion = new List<double>();
            subjectDescription = new List<string>();
            contentDescription = new List<string>();
        }
    }
}