using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESApi.Models.Code;
using ESApi.Models.ViewModel;

namespace ESApi.Controllers
{
    public class OrderAPIController : ApiController
    {
        public OrderCode code = new OrderCode();

        [HttpPost]
        [Route("api/order/add")]
        public IHttpActionResult AddProduct([FromBody] OrderDetail order)
        {
            code.AddOrder(order);
            return Ok();
        }
    }
}
