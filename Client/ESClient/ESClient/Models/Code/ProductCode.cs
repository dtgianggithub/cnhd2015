using ESClient.Models.DataModel;
using ESClient.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ESClient.Models.Code
{
    public class ProductCode
    {
        HttpClient c = new HttpClient();
        string url = API.GetUrlAPI();

        public ProductList GetHomeList()
        {
            ProductList productList = new ProductList();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/product/all").Result;
            productList = response.Content.ReadAsAsync<ProductList>().Result;
            return productList;
        }

        public SpecialProduct GetSpecialProduct()
        {
            SpecialProduct special = new SpecialProduct();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/product/specialproduct").Result;
            special = response.Content.ReadAsAsync<SpecialProduct>().Result;
            return special;
        }

        public NewProduct GetNewProduct()
        {
            NewProduct newproduct = new NewProduct();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/product/newproduct").Result;
            newproduct = response.Content.ReadAsAsync<NewProduct>().Result;
            return newproduct;
        }

        public DetailProduct GetDetailProduct(string id)
        {
            DetailProduct detail = new DetailProduct();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/product/ByID/" + id).Result;
            detail = response.Content.ReadAsAsync<DetailProduct>().Result;
            return detail;
        }

        public SANPHAMModel GetProduct(string id)
        {
            SANPHAMModel detail = new SANPHAMModel();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/product/GetByID/" + id).Result;
            detail = response.Content.ReadAsAsync<SANPHAMModel>().Result;
            return detail;
        }

        public ProductList GetProductList(string name, int id)
        {
            ProductList productList = new ProductList();

            c.BaseAddress = new Uri(url);
            c.DefaultRequestHeaders.Accept.Clear();
            c.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            HttpResponseMessage response = c.GetAsync("api/product/ByNameID/" + name + "/" + id).Result;
            productList = response.Content.ReadAsAsync<ProductList>().Result;
            return productList;
        }

        public double Summoney(List<CartSession> listcartsession)
        {
            double sum = 0;
            foreach (var item in listcartsession)
            {
                sum += (double)(item.sp.DONGIABAN) * (item.soluong);
            }

            return sum;
        }


        public int Checkexistproduct(CartSession cartsession, List<CartSession> listCartSession)
        {
            for (int i = 0; i < listCartSession.Count; i++)
            {
                if (cartsession.sp.MA == listCartSession[i].sp.MA)
                {
                    return i;
                }
            }

            return -1;
        }


        public bool CheckOverflowCount(string ma, int sl)
        {
            var sp = GetProduct(ma);
            if (sl > sp.SOLUONG)
                return false;
            return true;
        }

    }
}