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
                    ViewBag.summoney = code.Summoney(cartsessionlist);
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
                    ViewBag.summoney = code.Summoney(cartsessionlist);
                    return PartialView(cartsessionlist);

                }
            }
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
                    int i = code.Checkexistproduct(cartsession, cartsessionlist);
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
                    int i = code.Checkexistproduct(cartsession, cartsessionlist);
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

        public ActionResult CartContent()
        {
            string username = SessionHelper.GetUserSession();
            List<CartSession> cartsessionlist = null;

            if (username != null)
            {
                if (SessionHelper.GetCartSession(username) == null)
                {
                    ViewBag.summoney = 0;
                    return View(cartsessionlist);
                }
                else
                {

                    cartsessionlist = SessionHelper.GetCartSession(username);
                    ViewBag.summoney = code.Summoney(cartsessionlist);
                    return View(cartsessionlist);
                }
            }
            else
            {
                if (SessionHelper.GetCartSession("cart") == null)
                {
                    ViewBag.summoney = 0;
                    return View(cartsessionlist);
                }
                else
                {

                    cartsessionlist = SessionHelper.GetCartSession("cart");
                    ViewBag.summoney = code.Summoney(cartsessionlist);
                    return View(cartsessionlist);

                }
            }

            //return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CartContent(List<CartSession> listcart)
        {
            string user_name = SessionHelper.GetUserSession();
            bool isError = false;

            if (user_name != null)
            {
                List<CartSession> listcartsession = SessionHelper.GetCartSession(user_name);
                //xoa
                int i = 0;
                while (i < listcartsession.Count)
                {
                    if (listcart[i].daxoa)
                    {
                        listcartsession.RemoveAt(i);
                        listcart.RemoveAt(i);

                    }
                    else
                    {
                        if (listcart[i].soluong > 0 && code.CheckOverflowCount(listcartsession[i].sp.MA, listcart[i].soluong))
                        {
                            listcartsession[i].soluong = listcart[i].soluong;
                        }
                        else
                        {
                            isError = true;
                        }
                        i++;
                    }
                }

                //cap nhat sesssion
                /*
                for (int j = 0; j < listcartsession.Count; j++)
                {
                    listcartsession[j].daxoa = false;
                }

                SessionHelper.SetCartSession(user_name, listcartsession);
                 
                 */
                if (isError)
                {
                    ModelState.AddModelError("", "Số lượng mua không cho phép");
                }
                else
                {
                    ModelState.AddModelError("", "Đã cập nhật giỏ hàng");
                }
                ViewBag.summoney = code.Summoney(listcartsession);

                return View(listcartsession);

            }
            else
            {
                List<CartSession> listcartsession = SessionHelper.GetCartSession("cart");
                //xoa
                int i = 0;
                while (i < listcartsession.Count)
                {
                    if (listcart[i].daxoa)
                    {
                        listcartsession.RemoveAt(i);
                        listcart.RemoveAt(i);

                    }
                    else
                    {
                        if (listcart[i].soluong > 0 && code.CheckOverflowCount(listcartsession[i].sp.MA, listcart[i].soluong))
                        {
                            listcartsession[i].soluong = listcart[i].soluong;
                        }
                        else //<0
                        {
                            isError = true;
                        }
                        i++;
                    }
                }

                if (isError)
                {
                    ModelState.AddModelError("", "Số lượng mua không cho phép");
                }
                else
                {
                    ModelState.AddModelError("", "Đã cập nhật giỏ hàng");
                }
                ViewBag.summoney = code.Summoney(listcartsession);

                return View(listcartsession);
            }

        }


    }
}