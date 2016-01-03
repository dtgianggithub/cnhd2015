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
    public class AdminPromotionAPIController : ApiController
    {
        AdminPromotionCode ad = new AdminPromotionCode();

        [HttpGet]
        [Route("api/admin/promotion/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(ad.getall());
        }

        [HttpGet]
        [Route("api/admin/promotion/byID/{ID}")]
        public IHttpActionResult GetId([FromUri]int id)
        {
            return Ok(ad.getId(id));
        }

        [HttpPost]
        [Route("api/admin/promotion/add")]
        public IHttpActionResult Add([FromBody]KHUYENMAIModel km)
        {
            ad.add(km);

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/promotion/update")]
        public IHttpActionResult Update([FromBody]KHUYENMAIModel news_km)
        {
            ad.update(news_km);

            return Ok();
        }
    }
}
