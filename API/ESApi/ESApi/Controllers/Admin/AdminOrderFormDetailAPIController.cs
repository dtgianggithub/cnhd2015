using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESApi.Models.Code.Admin;
using ESApi.Models.ModelEntity;
using ESApi.Models;
using AutoMapper;
using System.Web.Http.Cors;

namespace ESApi.Controllers.Admin
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminOrderFormDetailAPIController : ApiController
    {
        AdminOrderFormDetailCode ad = new AdminOrderFormDetailCode();

        [HttpGet]
        [Route("api/admin/orderformdetail/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(ad.getall());
        }

        [HttpGet]
        [Route("api/admin/orderformdetail/byID/{ID}")]
        public IHttpActionResult GetId([FromUri]string id)
        {
            return Ok(ad.getId(id));
        }

        [HttpPut]
        [Route("api/admin/orderformdetail/update")]
        public IHttpActionResult Update([FromBody]CHITIETDONHANGModel news_dh)
        {
            ad.update(news_dh);
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/orderformdetail/add")]
        public IHttpActionResult Add([FromBody]CHITIETDONHANGModel news_dh)
        {
            ad.add(news_dh);
            return Ok();
        }
    }
}
