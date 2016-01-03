using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESClient.Models.EntityModel
{
    public class ProductCraw
    {

        public string srcimg { get; set; }
        public string altimg { get; set; }
        public string name { get; set; }
        public string detail { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }
}