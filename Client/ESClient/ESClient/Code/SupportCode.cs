using ESClient.Models.Code.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ESClient.Models.EntityModel;

namespace ESClient.Models.Code
{
    public class SupportCode
    {
        HttpClient c = new HttpClient();
        private string url = API.GetUrlAPI();

        public List<HOTROONLINE> GetList()
        {
            List<HOTROONLINE> supportList = new List<HOTROONLINE>();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/support/All").Result;
            supportList = response.Content.ReadAsAsync<List<HOTROONLINE>>().Result;
            return supportList;
        }
    }
}