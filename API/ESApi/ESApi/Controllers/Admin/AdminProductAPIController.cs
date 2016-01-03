using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ESApi.Models.Code.Admin;
using ESApi.Models.ModelEntity;
using ESApi.Models;
using System.Web.Http.Cors;

namespace ESApi.Controllers.Admin
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminProductAPIController : ApiController
    {
        AdminProductCode ad = new AdminProductCode();

        [HttpGet]
        [Route("api/admin/product/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(ad.getall());
        }

        [HttpGet]
        [Route("api/admin/product/byID/{ID}")]
        public IHttpActionResult GetId([FromUri]string id)
        {
            return Ok(ad.getId(id));
        }

        [HttpPost]
        [Route("api/admin/product/add")]
        public IHttpActionResult Add([FromBody]SANPHAMModel sp)
        {
            ad.add(sp);
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/product/update")]
        public IHttpActionResult Update([FromBody]SANPHAMModel news_sp)
        {
            ad.update(news_sp);
            return Ok();
        }
    }
}
