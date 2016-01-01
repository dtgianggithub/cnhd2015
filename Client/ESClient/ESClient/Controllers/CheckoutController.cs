using ESClient.Models.Code;
using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class CheckoutController : Controller
    {
        public CheckoutCode code = new CheckoutCode();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ReceiverViewModel receiver)
        {
            if (ModelState.IsValid)
            {
                //Luu session cho receiver
                SessionHelper.SetReceiverSession(receiver);
                return RedirectToAction("CheckoutByPayPal", "Checkout");
            }
            else
            {
                return View();
            }
        }


        public ActionResult CheckoutByPayPal()
        {
            //kiem tra coi da dang nhap hay chua de hien thi View tuong ung
            string username = SessionHelper.GetUserSession();
            CheckoutModel checkout = code.GetCheckout();
           
            ViewBag.summoney = code.Summoney(checkout.listCartSession);
            return View(checkout);
        }




        public ActionResult CheckoutSuccess()
        {
            //Luu thong tin don hang tai day dua vao thong tin session
            //kiem tra coi da dang nhap hay chua de hien thi View tuong ung

            string username = SessionHelper.GetUserSession();
            ReceiverViewModel receiver = SessionHelper.GetReceiverSession();
            OrderDetail order = new OrderDetail();

            if (username != null)//da dang nhap //thong tin nguoi mua
            {
                order.listCartSession = SessionHelper.GetCartSession(username); //san pham
                if (receiver != null) //thong tin nguoi nhan
                {
                     order.TotalMoney = code.Summoney(order.listCartSession);
                     order.receive = receiver;
                     
                     //xóa session giỏ hàng và người nhận đi.
                     HttpContext.Session["receiver"] = null;
                     HttpContext.Session[username] = null;
                }
            }
            else
            {
                order.listCartSession = SessionHelper.GetCartSession("cart"); //san pham
                if (receiver != null) //thong tin nguoi nhan
                {
                    order.TotalMoney = code.Summoney(order.listCartSession);
                    order.receive = receiver;

                    //xóa session giỏ hàng và người nhận đi.
                    HttpContext.Session["receiver"] = null;
                    HttpContext.Session[username] = null;
                    }
                }

            return View();
        }


       
    }
}