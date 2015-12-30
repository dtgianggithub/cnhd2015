using ESApi.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESApi.Controllers
{
    public class MenuAPIController : ApiController
    {
        MenuCode code = new MenuCode();

        [HttpGet]
        [Route("api/menu/all")]
        public IHttpActionResult GetMenu()
        {
            return Ok(code.GetMenuList());
        }
    }
}
