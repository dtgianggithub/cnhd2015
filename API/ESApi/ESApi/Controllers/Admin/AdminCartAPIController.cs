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
    public class AdminCartAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpPut]
        [Route("api/admin/cart/update")]
        public IHttpActionResult Update([FromBody]THANHTOANONLINEModel news_tt)
        {
            Mapper.CreateMap<THANHTOANONLINEModel, THANHTOANONLINE>();
            THANHTOANONLINE _news_tt = Mapper.Map<THANHTOANONLINEModel, THANHTOANONLINE>(news_tt);
            THANHTOANONLINE tt = (from s in db.THANHTOANONLINEs where s.MA == _news_tt.MA select s).First();

            tt.MA = _news_tt.MA;
            tt.EMAIL = _news_tt.EMAIL;
            tt.MATHANHVIEN = tt.MATHANHVIEN;
            tt.DAXOA = _news_tt.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/cart/delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            THANHTOANONLINE tt = (from s in db.THANHTOANONLINEs where s.MA == id select s).First();
            tt.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
