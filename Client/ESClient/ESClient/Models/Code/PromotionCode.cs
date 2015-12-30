using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ESClient.Models.Code
{
    public class PromotionCode
    {
        HttpClient c = new HttpClient();
        string url = API.GetUrlAPI();
        public KHUYENMAIModel GetPromotion(string id)
        {
            KHUYENMAIModel km = new KHUYENMAIModel();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/promotion/ByID/" + id).Result;
            km = response.Content.ReadAsAsync<KHUYENMAIModel>().Result;
            return km;
        }
    }
}