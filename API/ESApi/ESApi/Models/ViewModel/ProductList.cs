using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
{
    public class ProductList
    {
        public List<SANPHAMModel> newList { get; set; }
        public List<double> newListPromotion { get; set; }
        public List<SANPHAMModel> specialList { get; set; }
        public List<double> specialListPromotion { get; set; }
        public string path { get; set; }

        public ProductList()
        {
            newList = new List<SANPHAMModel>();
            newListPromotion = new List<double>();
            specialList = new List<SANPHAMModel>();
            specialListPromotion = new List<double>();

        }
    }
}