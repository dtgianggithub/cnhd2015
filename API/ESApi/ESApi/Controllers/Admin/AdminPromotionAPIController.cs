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
        ESDBEntities db = new ESDBEntities();

        [HttpPost]
        [Route("api/admin/promotion/add")]
        public IHttpActionResult Add([FromBody]KHUYENMAIModel km)
        {
            Mapper.CreateMap<KHUYENMAIModel, KHUYENMAI>();
            KHUYENMAI _km = Mapper.Map<KHUYENMAIModel, KHUYENMAI>(km);
            db.KHUYENMAIs.Add(_km);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/promotion/update")]
        public IHttpActionResult Update([FromBody]KHUYENMAIModel news_km)
        {
            Mapper.CreateMap<KHUYENMAIModel, KHUYENMAI>();
            KHUYENMAI _news_km = Mapper.Map<KHUYENMAIModel, KHUYENMAI>(news_km);
            KHUYENMAI km = (from s in db.KHUYENMAIs where s.MA == _news_km.MA select s).First();

            km.MA = _news_km.MA;
            km.NGAYBATDAU = _news_km.NGAYKETTHUC;
            km.NGAYKETTHUC = _news_km.NGAYKETTHUC;
            km.NOIDUNG = _news_km.NOIDUNG;
            km.DAXOA = _news_km.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/promotion/delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            KHUYENMAI km = (from s in db.KHUYENMAIs where s.MA == id select s).First();
            km.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
