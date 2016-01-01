using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ESClient.Models.Code;
using ESClient.Models.EntityModel;

namespace ESClient.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [ChildActionOnly]
        public ActionResult Index()
        {

            if (SessionHelper.GetUserSession() == null)
                return PartialView("Index");
            return PartialView("LoginSucess");
        }

        [ChildActionOnly]
        public ActionResult LoginSucess()
        {
            return PartialView();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string logintype)
        {
            if (logintype == "Login with GH")
            {

                //client id
                int clientid = 1;
                string callbacklink = "http://localhost:13172/Home/GetAuthorizationUser";
                return Redirect("http://ghapi.somee.com/AuthorizationUser/UserLogin" + "?clientid=" + clientid + "&&callbacklink=" + callbacklink);
            }

            if (logintype == "Login with FB")
            {

            }
            return View();
        }

        public string createLinkGetAuthorizationUser()
        {
            //client id
            int clientid = int.Parse(WebConfigurationManager.AppSettings["CLIENT_ID"].ToString());
            string callbacklink = "http://localhost:13172/Account/GetAuthorizationUser";
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
                        SessionHelper.SetUserSession(data.ACCESSTOKEN);
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
    }
}