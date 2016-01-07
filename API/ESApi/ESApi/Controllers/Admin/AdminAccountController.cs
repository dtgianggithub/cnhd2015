using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESApi.Models.Code.Admin;

namespace ESApi.Controllers.Admin
{
    public class AdminAccountController : ApiController
    {
        AdminAccountCode code = new AdminAccountCode();

        [HttpGet]
        [Route("api/admin/account/{username}/{password}")]
        public IHttpActionResult GetAll(string username, string password)
        {
            string pass = code.encryptSHA("123456");

            if (code.Login(username, pass))
                return Ok();
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/admin/register/{username}/{password}")]
        public IHttpActionResult Register(string username, string password)
        {
            code.register(username,password);
            return Ok();
        }
    }
}
