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

namespace ESApi.Controllers.Admin
{
    public class AdminOrderFormDetailAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpPut]
        [Route("api/admin/orderformdetail/update")]
        public IHttpActionResult Update([FromBody]CHITIETDONHANGModel news_dh)
        {
            Mapper.CreateMap<CHITIETDONHANGModel, CHITIETDONHANG>();
            CHITIETDONHANG _news_dh = Mapper.Map<CHITIETDONHANGModel, CHITIETDONHANG>(news_dh);
            CHITIETDONHANG dh = (from s in db.CHITIETDONHANGs where (s.DONHANG == _news_dh.DONHANG && s.SANPHAM == _news_dh.SANPHAM) select s).First();

            dh.DONHANG = _news_dh.DONHANG;
            dh.SANPHAM = _news_dh.SANPHAM;
            dh.SOLUONG = _news_dh.SOLUONG;
            dh.THANHTIEN = _news_dh.THANHTIEN;
            dh.DAXOA = _news_dh.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/orderformdetail/delete")]
        public IHttpActionResult Delete([FromUri]string iddh, [FromUri]string idsp )
        {
            CHITIETDONHANG dh = (from s in db.CHITIETDONHANGs where (s.DONHANG == iddh && s.SANPHAM == idsp) select s).First();
            dh.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
