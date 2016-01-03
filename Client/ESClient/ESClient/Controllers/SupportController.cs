using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ESClient.Models.Code;
using System.Net.Http;
using System.Net;
using HtmlAgilityPack;

using ESClient.Models.EntityModel;

namespace ESClient.Controllers
{
    public class SupportController : Controller
    {
        public SupportCode code = new SupportCode();
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SupportOnline()
        {
            var listsupport = code.GetList();
            return PartialView(listsupport);
        }


        public ActionResult Crawler()
        {
            List<ProductCraw> list = new List<ProductCraw>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.thegioididong.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                string apiurl = "laptop?trang=1";
                HttpResponseMessage respone = client.GetAsync(apiurl).Result;

                if (respone.StatusCode == HttpStatusCode.OK)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    ViewBag.info = data;
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(data);
                    HtmlNode liNode = document.DocumentNode.SelectSingleNode("//ul[@id='lstprods']");
                    if (liNode != null)
                    {
                        // lấy danh sách những thẻ li
                        HtmlNodeCollection liCollection = document.DocumentNode.SelectNodes("//ul[@id='lstprods']/li");
                        foreach (var item in liCollection)
                        {
                            ProductCraw pro = new ProductCraw();
                            //từ nút li hiện tài làm sao lấy được thông tin bên trong
                            //lấy ra thẻ src hình ảnh
                            HtmlNode img = item.SelectSingleNode(".//img");
                            string imageText = img.Attributes["src"].Value;
                            string altText = img.Attributes["alt"].Value;
                            pro.srcimg = imageText;
                            pro.altimg = altText;
                            //lấy tên
                            HtmlNode name = item.SelectSingleNode(".//h3");
                            pro.name = name.InnerText;
                            //lấy thẻ figure
                            HtmlNode figure = item.SelectSingleNode(".//figure");
                            pro.detail = figure.InnerText;
                            
                            //lấy tất cả thẻ a
                            HtmlNodeCollection aCollection = item.SelectNodes(".//a");
                            foreach (var itema in aCollection)
                            {
                                //lấy thẻ đầu tiên
                                pro.url= itema.Attributes["href"].Value;
                                break;
                            }

                            pro.type = "laptop";


                            list.Add(pro);
                        }

                    }
                }
            }
            return View(list);
        }

        public ActionResult CralerNavigation(string type)
        {

            List<ProductCraw> list = new List<ProductCraw>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.thegioididong.com/");
                client.DefaultRequestHeaders.Accept.Clear();

                string apiurl = type + "?trang=1";
                HttpResponseMessage respone = client.GetAsync(apiurl).Result;

                if (respone.StatusCode == HttpStatusCode.OK)
                {
                    string data = respone.Content.ReadAsStringAsync().Result;
                    ViewBag.info = data;
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(data);
                    HtmlNode liNode = document.DocumentNode.SelectSingleNode("//ul[@id='lstprods']");
                    if (liNode != null)
                    {
                        // lấy danh sách những thẻ li
                        HtmlNodeCollection liCollection = document.DocumentNode.SelectNodes("//ul[@id='lstprods']/li");
                        foreach (var item in liCollection)
                        {
                            ProductCraw pro = new ProductCraw();
                            //từ nút li hiện tài làm sao lấy được thông tin bên trong
                            //lấy ra thẻ src hình ảnh
                            HtmlNode img = item.SelectSingleNode(".//img");
                            string imageText = img.Attributes["src"].Value;
                            string altText = img.Attributes["alt"].Value;
                            pro.srcimg = imageText;
                            pro.altimg = altText;
                            //lấy tên
                            HtmlNode name = item.SelectSingleNode(".//h3");
                            pro.name = name.InnerText;
                            //lấy thẻ figure
                            HtmlNode figure = item.SelectSingleNode(".//figure");
                            pro.detail = figure.InnerText;

                            //lấy tất cả thẻ a
                            HtmlNodeCollection aCollection = item.SelectNodes(".//a");
                            foreach (var itema in aCollection)
                            {
                                //lấy thẻ đầu tiên
                                pro.url = itema.Attributes["href"].Value;
                                break;
                            }

                            pro.type = type;

                            list.Add(pro);
                        }

                    }
                }
            }
            return View(list);
        }
    }
}