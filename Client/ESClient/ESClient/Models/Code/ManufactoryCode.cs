using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ESApi.Models.ModelEntity;

namespace ESClient.Models.Code
{
    public class ManufactoryCode
    {
        public string url = API.GetUrlAPI();
        HttpClient c = new HttpClient();
        public ManufactoryList GetManufactoryList()
        {
            ManufactoryList manulist = new ManufactoryList();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/Manufactory/all").Result;
            manulist = response.Content.ReadAsAsync<ManufactoryList>().Result;
            return manulist;
        }

    }
}