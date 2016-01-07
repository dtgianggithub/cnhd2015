using ESClient.Models.Code;
using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

                     code.AddOrder(order);
                    SendNotificationEmail(SessionHelper.GetUserSession(),order.TotalMoney);
                     //xóa session giỏ hàng và người nhận đi.
                     Session["receiver"] = null;
                     Session[username] = null;
                }
            }
            else
            {
                order.listCartSession = SessionHelper.GetCartSession("cart"); //san pham
                if (receiver != null) //thong tin nguoi nhan
                {
                    order.TotalMoney = code.Summoney(order.listCartSession);
                    order.receive = receiver;
                    code.AddOrder(order);
                    //xóa session giỏ hàng và người nhận đi.
                    HttpContext.Session["receiver"] = null;
                    HttpContext.Session[username] = null;
                    }
                }

            return View();
        }

        public bool SendNotificationEmail(string ToEmail, double total)  // Get password from Email
        {
            try
            {
                // MailMessage class is present is System.Net.Mail namespace
                MailMessage mailMessage = new MailMessage("dtgiang1994@gmail.com", ToEmail);


                // StringBuilder class is present in System.Text namespace
                StringBuilder sbEmailBody = new StringBuilder();
                sbEmailBody.Append("Chào bạn,<br/><br/>");
                sbEmailBody.Append("Bạn vừa mua thành công sản phẩm từ cửa hàng chúng tôi.");
                sbEmailBody.Append("<br/>");
                sbEmailBody.Append("Đơn hàng có giá trị là :");
                sbEmailBody.Append(total.ToString());
                sbEmailBody.Append("<br/><br/>");


                mailMessage.IsBodyHtml = true;

                mailMessage.Body = sbEmailBody.ToString();
                mailMessage.Subject = "Thông báo mua hàng thành công";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "dtgiang1994@gmail.com",
                    Password = "hogojqklbjpupbqm"
                };

                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}