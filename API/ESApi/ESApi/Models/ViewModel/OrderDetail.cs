using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.ViewModel
{
    public class OrderDetail
    {
        public List<CartSession> listCartSession { get; set; }
        public ReceiveViewModel receive { get; set; }
        public double TotalMoney { get; set; }

        public OrderDetail()
        {
            listCartSession = new List<CartSession>();
            receive =new ReceiveViewModel();
            TotalMoney = 0;
        }
    }
}