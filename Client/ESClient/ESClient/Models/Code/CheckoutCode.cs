using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using ESClient.Models.EntityModel;

namespace ESClient.Models.Code
{
    public class CheckoutCode
    {
        public CheckoutModel GetCheckout()
        {
            //kiem tra coi da dang nhap hay chua de hien thi View tuong ung
            string username = SessionHelper.GetUserSession();
            CheckoutModel checkout = new CheckoutModel();
            if (username != null)
            {
                checkout.listCartSession = SessionHelper.GetCartSession(username);
                //checkout.user = (from u in db.THANHVIENs where u.TENDANGNHAP == username select u).SingleOrDefault();
                //ViewBag.summoney = Summoney(checkout.listCartSession);
                return checkout;
            }
            else
            {
                checkout.listCartSession = SessionHelper.GetCartSession("cart");
                return checkout;
            }

        }


        public double Summoney(List<CartSession> listcartsession)
        {
            double sum = 0;
            foreach (var item in listcartsession)
            {
                sum += (double)(item.sp.DONGIABAN) * (item.soluong);
            }

            return sum;
        }

        public void AddOrder(OrderDetail order)
        {
            // HTTP POST
            HttpClient c = new HttpClient();

            HttpResponseMessage response = c.PostAsJsonAsync(
                "api/order/add",
                order
            ).Result;
        }
    }
}