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
        ESDBEntities db = new ESDBEntities();

        [HttpPost]
        [Route("api/admin/typeproduct/add")]
        public IHttpActionResult Add([FromBody]LOAISANPHAMModel lsp)
        {
            Mapper.CreateMap<LOAISANPHAMModel, LOAISANPHAM>();
            LOAISANPHAM _lsp = Mapper.Map<LOAISANPHAMModel, LOAISANPHAM>(lsp);
            db.LOAISANPHAMs.Add(_lsp);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/typeproduct/update")]
        public IHttpActionResult Update([FromBody]LOAISANPHAMModel news_lsp)
        {
            Mapper.CreateMap<LOAISANPHAMModel, LOAISANPHAM>();
            LOAISANPHAM _news_lsp = Mapper.Map<LOAISANPHAMModel, LOAISANPHAM>(news_lsp);
            LOAISANPHAM lsp = (from s in db.LOAISANPHAMs where s.MA == _news_lsp.MA select s).First();

            lsp.MA = _news_lsp.MA;
            lsp.TEN = _news_lsp.TEN;
            lsp.DAXOA = _news_lsp.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/typeproduct/delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            LOAISANPHAM lsp = (from s in db.LOAISANPHAMs where s.MA == id select s).First();
            lsp.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
