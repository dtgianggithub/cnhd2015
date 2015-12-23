using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using ESApi.Models;
using ESApi.Models.ModelEntity;

namespace ESApi.Controllers
{
    public class PromotionAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpGet]
        [Route("api/promotion/all")]
        public IHttpActionResult GetAllPromotion()
        {
            List<KHUYENMAI> list = db.KHUYENMAIs.Where(sp => sp.DAXOA == false).ToList();
            Mapper.CreateMap<KHUYENMAI, KHUYENMAIModel>();
            List<KHUYENMAIModel> ret =
                Mapper.Map<List<KHUYENMAI>, List<KHUYENMAIModel>>(list);
            return Ok(ret);
        }
        [HttpPost]
        [Route("api/promotion/add")]
        public IHttpActionResult AddPromotion([FromBody] KHUYENMAI km)
        {
            db.KHUYENMAIs.Add(km);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/promotion/update")]
        public IHttpActionResult UpdateProduct([FromUri] int Ma, [FromBody] KHUYENMAI s)
        {
            KHUYENMAI khuyenmai = db.KHUYENMAIs.Where(sp => sp.DAXOA == false && sp.MA == Ma).SingleOrDefault();
            khuyenmai.MA = s.MA;
            khuyenmai.NGAYBATDAU = s.NGAYBATDAU;
            khuyenmai.NGAYKETTHUC = s.NGAYKETTHUC;
            khuyenmai.NOIDUNG = s.NOIDUNG;
            khuyenmai.DAXOA = s.DAXOA;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("api/promotion/delete")]
        public IHttpActionResult DeleteTypeProduct([FromUri] int Ma)
        {
            KHUYENMAI khuyenmai = db.KHUYENMAIs.Where(sp => sp.DAXOA == false && sp.MA == Ma).SingleOrDefault();
            db.KHUYENMAIs.Remove(khuyenmai);
            db.SaveChanges();
            return Ok();
        }


    }
}
