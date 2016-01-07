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
    public class AdminOrderFormAPIController : ApiController
    {
        AdminOrderFormCode ad = new AdminOrderFormCode();

        [HttpGet]
        [Route("api/admin/orderform/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(ad.getall());
        }

        [HttpGet]
        [Route("api/admin/orderform/byID/{ID}")]
        public IHttpActionResult GetId([FromUri]string id)
        {
            return Ok(ad.getId(id));
        }

        [HttpPut]
        [Route("api/admin/orderform/update")]
        public IHttpActionResult Update([FromBody]DONHANGModel news_dh)
        {
            ad.update(news_dh);
            return Ok();
        }

        [HttpPost]
        [Route("api/admin/orderform/add")]
        public IHttpActionResult Add([FromBody]DONHANGModel news_dh)
        {
            ad.add(news_dh);
            return Ok();
        }
    }
}
