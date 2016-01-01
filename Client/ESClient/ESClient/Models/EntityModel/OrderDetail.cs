using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESClient.Models.EntityModel
{
    public class OrderDetail
    {
        public List<CartSession> listCartSession { get; set; }
        public double TotalMoney { get; set; }
        public ReceiverViewModel receive { get; set; }

        public OrderDetail()
        {
            listCartSession = new List<CartSession>();
            TotalMoney = 0;
        }
    }
}