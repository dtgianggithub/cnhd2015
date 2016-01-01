using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESApi.Models.ModelEntity;

namespace ESClient.Models.EntityModel
{
    public class CheckoutModel
    {
        public List<CartSession> listCartSession { get; set; }
        public THANHVIENModel user { get; set; }

        public CheckoutModel()
        {
            listCartSession = new List<CartSession>();
            user = new THANHVIENModel();
        }
    }
}