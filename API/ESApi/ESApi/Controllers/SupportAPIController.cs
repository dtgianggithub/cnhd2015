using ESApi.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESApi.Controllers
{
    public class SupportAPIController : ApiController
    {
        public SupportCode code = new SupportCode();

        [HttpGet]
        [Route("api/support/All")]
        public IHttpActionResult GetSupport()
        {
            return Ok(code.GetList());
        }
    }
}
