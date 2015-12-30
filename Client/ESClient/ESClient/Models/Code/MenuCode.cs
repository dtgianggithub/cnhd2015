
using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace ESClient.Models.Code
{
    public class MenuCode
    {
        public HttpClient c = new HttpClient();
        string url = API.GetUrlAPI();
        public MenuList GetMenuList()
        {
            MenuList menulist = new MenuList();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/menu/all").Result;
            menulist = response.Content.ReadAsAsync<MenuList>().Result;
            return menulist;
        }
    }
}