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
    public class AdminTypeMemberController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpPost]
        [Route("api/admin/typemember/add")]
        public IHttpActionResult Add([FromBody]LOAITHANHVIENModel ltv)
        {
            Mapper.CreateMap<LOAITHANHVIENModel, LOAITHANHVIEN>();
            LOAITHANHVIEN _ltv = Mapper.Map<LOAITHANHVIENModel, LOAITHANHVIEN>(ltv);
            db.LOAITHANHVIENs.Add(_ltv);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/typemember/update")]
        public IHttpActionResult Update([FromBody]LOAITHANHVIENModel news_ltv)
        {
            Mapper.CreateMap<LOAITHANHVIENModel, LOAITHANHVIEN>();
            LOAITHANHVIEN _news_ltv = Mapper.Map<LOAITHANHVIENModel, LOAITHANHVIEN>(news_ltv);
            LOAITHANHVIEN ltv = (from s in db.LOAITHANHVIENs where s.MA == _news_ltv.MA select s).First();

            ltv.MA = _news_ltv.MA;
            ltv.TENLOAI = _news_ltv.TENLOAI;
            ltv.DAXOA = _news_ltv.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/typemember/delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            LOAITHANHVIEN ltv = (from s in db.LOAITHANHVIENs where s.MA == id select s).First();
            ltv.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
