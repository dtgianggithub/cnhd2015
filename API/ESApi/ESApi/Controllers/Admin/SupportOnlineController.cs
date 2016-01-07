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
    public class SupportOnlineController : AccountAPIController
    {
        AdminSupportOnlineCode ad = new AdminSupportOnlineCode();

        [HttpGet]
        [Route("api/admin/supportonline/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(ad.getall());
        }

        [HttpGet]
        [Route("api/admin/supportonline/byID/{ID}")]
        public IHttpActionResult GetId([FromUri]int id)
        {
            return Ok(ad.getId(id));
        }

        [HttpPost]
        [Route("api/admin/supportonline/add")]
        public IHttpActionResult Add([FromBody]HOTROONLINE ht)
        {
            ad.add(ht);
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/supportonline/update")]
        public IHttpActionResult Update([FromBody]HOTROONLINE news_ht)
        {
            ad.update(news_ht);
            return Ok();
        }
    }
}