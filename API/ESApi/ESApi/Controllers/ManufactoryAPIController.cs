using ESApi.Models.Code;
using ESApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ESApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ManufactoryAPIController : ApiController
    {
        public ManufactoryCode Manu = new ManufactoryCode();

        [HttpGet]
        [Route("api/Manufactory/all")]
        public IHttpActionResult GetAllProducts()
        {
            return Ok(Manu.GetListNSX());
        }

        [HttpGet]
        [Route("api/Manufactory/AllManu")]
        public IHttpActionResult GetAllManu()
        {
            return Ok(Manu.GetAllNSX());
        }

        [HttpGet]
        [Route("api/Manufactory/ByID/{ID}")]
        public IHttpActionResult GetManu(string ID)
        {
            return Ok(Manu.GetNSX(int.Parse(ID)));
        }
    }
}
