using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using ESClient.Models.Code;

namespace ESClient.Areas.Admin.Code
{
    public class LoginCode
    {
        public bool Login(string username, string password)
        {
            HttpClient c = new HttpClient();

            c.BaseAddress = new Uri(API.GetUrlAPI());
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/admin/account/" + username + "/" +password).Result;

            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}