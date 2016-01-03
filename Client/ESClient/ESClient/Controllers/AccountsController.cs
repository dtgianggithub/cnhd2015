using ESClient.Models.Code;
using ESClient.Models.EntityModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ESClient.Controllers
{
    public class AccountsController : Controller
    {
        public ActionResult UpdateAccount()
        {
            return View();
        }

        // GET: Accounts
        [ChildActionOnly]
        public ActionResult Index()
        {

            if (Request.IsAuthenticated)
            {
                //đã đăng nhập bằng google
                //lưu session tại đây
                SessionHelper.SetUserSession(User.Identity.GetUserName());
                SessionHelper.SetTypeLoginSession("GG");
                return PartialView("LoginSucess");
            }

            //nếu không có thể là đã login vào UserAPI hoặc không
            string type = SessionHelper.GetTypeLoginSession();
            if(type != null && type == "GH")
            {
                return PartialView("LoginSucess");
            }

            //nếu đều chưa login vô bất cứ cái nào
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult LoginSucess()
        {
            return PartialView();
        }


        public ActionResult Login()
        {
            ViewBag.returnURrl = "returnUrl";
            return View();
        }


        public ActionResult Logout()
        {
            string type = SessionHelper.GetTypeLoginSession();
            if (type == "GH")
            {
                //do something
                Session["username"] = null;
                Session["type"] = null;
                Session["access"] = null;
            }
            if (type == "GG")
            {
                //do somthing
               // Session["username"] = null;
                //Session["type"] = null;

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }


            return RedirectToAction("Index", "Home");
        }



        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        [HttpPost]
        public ActionResult Login(string logintype)
        {
            if (logintype == "Login with GH")
            {

                //client id
                //int clientid = 1;
                //string callbacklink = "http://localhost:13172/Account/GetAuthorizationUser";
                //return Redirect("http://ghapi.somee.com/AuthorizationUser/UserLogin" + "?clientid=" + clientid + "&&callbacklink=" + callbacklink);
                return Redirect(createLinkGetAuthorizationUser());
            }

            if (logintype == "Login with GG")
            {

            }
            return View();
        }


        public string createLinkGetAuthorizationUser()
        {
            //client id
            int clientid = int.Parse(WebConfigurationManager.AppSettings["CLIENT_ID"].ToString());
            string callbacklink = WebConfigurationManager.AppSettings["CALLBACKLINK"].ToString();
            return WebConfigurationManager.AppSettings["URLAUTHORIZATIONUSER"].ToString() + "?clientid=" + clientid + "&&callbacklink=" + callbacklink;
        }

        public string createLinkGetAccessToken(string AUTHORIZATION_CODE)
        {
            int clientid = int.Parse(WebConfigurationManager.AppSettings["CLIENT_ID"].ToString());
            return "api/oauth/" + clientid + "/" + AUTHORIZATION_CODE + "/accesstoken";
        }


        public ActionResult Error()
        {
            return View();
        }

        public ActionResult GetAuthorizationUser(string AUTHORIZATION_CODE)
        {
            //kiểm tra xem authorization code lấy về có được k ?
            if (AUTHORIZATION_CODE != "")
            {
                //tiến hành get Accesstoken tại đây
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["BASEURLAPI"].ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    int clientid = int.Parse(WebConfigurationManager.AppSettings["CLIENT_ID"].ToString());
                    HttpResponseMessage response = client.GetAsync(createLinkGetAccessToken(AUTHORIZATION_CODE)).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var data = response.Content.ReadAsAsync<AccessTokenModel>().Result;
                        //lưu access token vào Session
                        SessionHelper.SetAccessTokenSession(data.ACCESSTOKEN);

                        //lưu dữ liệu get về vào session
                        UserModel user = GetInfoUser();
                        if (user != null)
                        {
                            SessionHelper.SetUserSession(user.TENDANGNHAP);
                            SessionHelper.SetTypeLoginSession("GH");
                        }


                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Error", "Account");
            }
            return View();
        }

        //get data user
        public UserModel GetInfoUser()
        {

            //get thong tin User từ server
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["BASEURLAPI"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );

                string ACCESSTOKEN = SessionHelper.GetAccessTokenSession();
                HttpResponseMessage response = client.GetAsync("api/" + ACCESSTOKEN + "/GetAll").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var data = response.Content.ReadAsAsync<UserModel>().Result;
                    return data;
                }
            }

            return null;

        }

    }
}