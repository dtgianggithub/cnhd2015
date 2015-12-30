using ESClient.Models.Code;
using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class CartController : Controller
    {
        public ProductCode code = new ProductCode();
        public PromotionCode pro = new PromotionCode();
        // GET: Cart
        [ChildActionOnly]
        public ActionResult Index()
        {
            string username = SessionHelper.GetUserSession();
            List<CartSession> cartsessionlist = null;

            if (username != null)
            {
                if (SessionHelper.GetCartSession(username) == null)
                {

                    return PartialView(cartsessionlist);
                }
                else
                {

                    cartsessionlist = SessionHelper.GetCartSession(username);
                    ViewBag.summoney = Summoney(cartsessionlist);
                    return PartialView(cartsessionlist);
                }
            }
            else
            {
                if (SessionHelper.GetCartSession("cart") == null)
                {
                    ViewBag.summoney = 0;
                    return PartialView(cartsessionlist);
                }
                else
                {

                    cartsessionlist = SessionHelper.GetCartSession("cart");
                    ViewBag.summoney = Summoney(cartsessionlist);
                    return PartialView(cartsessionlist);

                }
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

        public ActionResult AddCart(string id)
        {
            string username = SessionHelper.GetUserSession();
            if (username != null)
            {
                if (SessionHelper.GetCartSession(username) == null)
                {
                    CartSession cartsession = new CartSession();
                    cartsession.sp = code.GetProduct(id);

                    //kiem tra coi co khuyen mai hay k
                    if (cartsession.sp.MAKHUYENMAI != 0)
                    {
                        var km = pro.GetPromotion(cartsession.sp.MAKHUYENMAI.ToString());
                        double gia = (double)(cartsession.sp.DONGIABAN * (100 - km.NOIDUNG)) / 100;
                        cartsession.sp.DONGIABAN = gia;
                    }
                    cartsession.soluong = 1;
                    cartsession.daxoa = false;
                    List<CartSession> cartsessionlist = new List<CartSession>();
                    cartsessionlist.Add(cartsession);
                    SessionHelper.SetCartSession(username, cartsessionlist);
                }
                else
                {
                    CartSession cartsession = new CartSession();
                    cartsession.sp = code.GetProduct(id);

                    //kiem tra coi co khuyen mai hay k
                    if (cartsession.sp.MAKHUYENMAI != 0)
                    {
                        var km = pro.GetPromotion(id);
                        double gia = (double)(cartsession.sp.DONGIABAN * (100 - km.NOIDUNG)) / 100;
                        cartsession.sp.DONGIABAN = gia;
                    }

                    cartsession.soluong = 1;
                    cartsession.daxoa = false;

                    List<CartSession> cartsessionlist = SessionHelper.GetCartSession(username);
                    int i = Checkexistproduct(cartsession, cartsessionlist);
                    if (i == -1)
                    {
                        cartsessionlist.Add(cartsession);
                    }
                    else
                    {
                        cartsessionlist[i].soluong++;
                    }

                    SessionHelper.SetCartSession(username, cartsessionlist);

                }
            }
            else
            {
                if (SessionHelper.GetCartSession("cart") == null)
                {
                    CartSession cartsession = new CartSession();
                    cartsession.sp = code.GetProduct(id);
                    //kiem tra coi co khuyen mai hay k
                    if (cartsession.sp.MAKHUYENMAI != 0)
                    {
                        var km = pro.GetPromotion(id); 
                        double gia = (double)(cartsession.sp.DONGIABAN * (100 - km.NOIDUNG)) / 100;
                        cartsession.sp.DONGIABAN = gia;

                    }

                    cartsession.soluong = 1;
                    cartsession.daxoa = false;

                    List<CartSession> cartsessionlist = new List<CartSession>();
                    cartsessionlist.Add(cartsession);
                    SessionHelper.SetCartSession("cart", cartsessionlist);
                }
                else
                {
                    CartSession cartsession = new CartSession();
                    cartsession.sp = code.GetProduct(id);
                    //kiem tra coi co khuyen mai hay k
                    if (cartsession.sp.MAKHUYENMAI != 0)
                    {
                        var km = pro.GetPromotion(id); 
                        double gia = (double)(cartsession.sp.DONGIABAN * (100 - km.NOIDUNG)) / 100;
                        cartsession.sp.DONGIABAN = gia;
                    }


                    cartsession.soluong = 1;
                    cartsession.daxoa = false;

                    List<CartSession> cartsessionlist = SessionHelper.GetCartSession("cart");
                    int i = Checkexistproduct(cartsession, cartsessionlist);
                    if (i == -1)
                    {
                        cartsessionlist.Add(cartsession);
                    }
                    else
                    {
                        cartsessionlist[i].soluong++;
                    }

                    SessionHelper.SetCartSession("cart", cartsessionlist);

                }
            }

            //return Content("<script language='javascript' type='text/javascript'>alert('Thêm giỏ hàng thành công !');</script>");
            return RedirectToAction("CartContent", "Cart");
        }


        public int Checkexistproduct(CartSession cartsession, List<CartSession> listCartSession)
        {
            for (int i = 0; i < listCartSession.Count; i++)
            {
                if (cartsession.sp.MA == listCartSession[i].sp.MA)
                {
                    return i;
                }
            }

            return -1;

        }

    }
}