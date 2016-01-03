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
    public class AdminTypetypeproductAPIController : ApiController
    {
        AdminTypeProductCode ad = new AdminTypeProductCode();

        [HttpGet]
        [Route("api/admin/typeproduct/all")]
        public IHttpActionResult GetAll()
        {
            return Ok(ad.getall());
        }

        [HttpGet]
        [Route("api/admin/typeproduct/byID/{ID}")]
        public IHttpActionResult GetId([FromUri]int id)
        {
            return Ok(ad.getId(id));
        }

        [HttpPost]
        [Route("api/admin/typeproduct/add")]
        public IHttpActionResult Add([FromBody]LOAISANPHAMModel lsp)
        {
            ad.add(lsp);

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/typeproduct/update")]
        public IHttpActionResult Update([FromBody]LOAISANPHAMModel news_lsp)
        {
            ad.update(news_lsp);
            return Ok();
        }
    }
}
