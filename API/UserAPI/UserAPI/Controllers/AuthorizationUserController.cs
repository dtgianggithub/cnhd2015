using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UserAPI.Models;
using UserAPI.Models.ViewModels;

namespace UserAPI.Controllers
{
    public class AuthorizationUserController : Controller
    {

        public GHDBEntities db = new GHDBEntities();


        // GET: AuthorizationView
        public ActionResult UserLogin(int clientid,string callbacklink)
        {
            //check kiểm tra client id có tồn tại trong database hay không ??

            if (db.UNGDUNGs.Where(ud => ud.DAXOA.Value.Equals(false) && ud.MA == clientid).SingleOrDefault() == null)
            {
                //redirect tới trang thông báo lỗi
                return RedirectToAction("ErrorPermission", "AuthorizationUser", new { clientid = clientid });
            }

            //lưu link callback và clientid xuống session
            Session["callback"] = callbacklink;
            Session["clientid"] = clientid;


            string username = Session["username"] as string;
            if (username != null)
            {
                return RedirectToAction("ReturnNow");
            }

            ViewBag.Message = "";
            return View();

        }

        public ActionResult ReturnNow(){
            string callbacklink = Session["callback"] as string;
                //khoi tao bien authorizatoin code bằng 
                string authorizationcode = createAuthorizationCode(10);

                //lưu xuống database
                string username = Session["username"] as string;
                //string password = Session["password"] as string;
                NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username  && nd.DAXOA.Value.Equals(false)).SingleOrDefault();
                if (nguoidung != null) // đúng username và mật khẩu
                {
                    nguoidung.AUTHORIZATIONCODE = authorizationcode;
                    db.SaveChanges();
                }


              //trả về authorization code đúng
                return Redirect(callbacklink + "?AUTHORIZATION_CODE="+ authorizationcode);
        }

        public ActionResult ErrorPermission(int clientid)
        {
            ViewBag.clientId = clientid;
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            //kiểm tra thông tin đăng nhập tại đây
            if (username != "" && password != "")
            {
                //kiểm tra với database
                NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username && nd.DAXOA.Value.Equals(false)).SingleOrDefault();
                if (nguoidung != null) // đúng username và mật khẩu
                {
                    password = encryptSHA(password);
                    if (password == nguoidung.MATKHAU)
                    {
                        //lưu session
                        Session["username"] = username;
                        Session["password"] = password;
                        return RedirectToAction("UserAcceptance");
                    }
                   
                }
                
            }
            ViewBag.Message = "Wrong username or password";
            return View();
        }

        public string encryptSHA(string data)   // Encode password
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = sha.ComputeHash(encoder.GetBytes(data));
            return System.Text.Encoding.UTF8.GetString(hashedBytes);
        }
        public ActionResult UserAcceptance()
        {
            //lấy thông tin của trang web tại đây
            int clientid = (int)Session["clientid"];
            UNGDUNG ud = db.UNGDUNGs.Where(d => d.MA == clientid && d.DAXOA.Value.Equals(false)).SingleOrDefault();
            return View(ud);
        }

        [HttpPost]

        public ActionResult UserAcceptance(string Authorize)
        {
            if (Authorize == "Accept")
            {
             string callbacklink = Session["callback"] as string;
                //khoi tao bien authorizatoin code bằng 
                string authorizationcode = createAuthorizationCode(10);

                //lưu xuống database
                string username = Session["username"] as string;
                //string password = Session["password"] as string;
                NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.TENDANGNHAP == username  && nd.DAXOA.Value.Equals(false)).SingleOrDefault();
                if (nguoidung != null) // đúng username và mật khẩu
                {
                    nguoidung.AUTHORIZATIONCODE = authorizationcode;
                    db.SaveChanges();
                }


              //trả về authorization code đúng
                return Redirect(callbacklink + "?AUTHORIZATION_CODE="+ authorizationcode);
            }

            if (Authorize == "Deny")
            {
                //trả về autorization code là rỗng
                string callbacklink = Session["callback"] as string;
                return Redirect(callbacklink + "?AUTHORIZATION_CODE=");
            }
            //nếu đã accept thì trả về authorization code cho ứng dụng tại đây

            return View();
            
        }

        public string createAuthorizationCode(int maxSize)
        {

            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
       
    }
}