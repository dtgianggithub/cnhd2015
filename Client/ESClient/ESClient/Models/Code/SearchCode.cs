using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using ESApi.Models.ModelEntity;
using ESClient.Models.DataModel;
using ESClient.Models.EntityModel;

namespace ESClient.Models.Code
{
    public class SearchCode
    {
        public HttpClient c = new HttpClient();
        private string url = API.GetUrlAPI();
        public double TinhGiaToiThieu(string tien)
        {
            double Giatioithieu = 0;
            switch (tien)
            {
                case "2":
                    Giatioithieu = 2000000;
                    break;
                case "3":
                    Giatioithieu = 5000000;
                    break;
                case "4":
                    Giatioithieu = 10000000;
                    break;
                default:
                    break;
            }

            return Giatioithieu;
        }

        public double TinhGiaToiDa(string tien)
        {
            double Giatoida = 0;

            switch (tien)
            {
                case "0":
                    Giatoida = 100000000;
                    break;
                case "1":
                    Giatoida = 2000000;
                    break;
                case "2":
                    Giatoida = 5000000;
                    break;
                case "3":
                    Giatoida = 10000000;
                    break;
                default:
                    Giatoida = 100000000;
                    break;
            }

            return Giatoida;
        }

        public ProductList SearchAdvance(SearchModel search)
        {
            ProductList list = new ProductList();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.PostAsJsonAsync( "api/product/SearchAdvance",search).Result;
            list = response.Content.ReadAsAsync<ProductList>().Result;
            return list;
        }
    }
}