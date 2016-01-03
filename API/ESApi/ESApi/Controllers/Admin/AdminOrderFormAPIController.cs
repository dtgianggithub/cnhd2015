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
        ESDBEntities db = new ESDBEntities();

        [HttpPut]
        [Route("api/admin/orderform/update")]
        public IHttpActionResult Update([FromBody]DONHANGModel news_dh)
        {
            Mapper.CreateMap<DONHANGModel, DONHANG>();
            DONHANG _news_dh = Mapper.Map<DONHANGModel, DONHANG>(news_dh);
            DONHANG dh = (from s in db.DONHANGs where s.MA == _news_dh.MA select s).First();

            dh.MA = _news_dh.MA;
            dh.TONGTIEN = _news_dh.TONGTIEN;
            dh.NGAYDATHANG = _news_dh.NGAYDATHANG;
            dh.NGAYNHANHANG = _news_dh.NGAYNHANHANG;
            dh.TENNGUOINHAN = _news_dh.TENNGUOINHAN;
            dh.DIACHINHAN = _news_dh.DIACHINHAN;
            dh.DIENTHOAINGUOINHAN = _news_dh.DIENTHOAINGUOINHAN;
            dh.TRANGTHAI = _news_dh.TRANGTHAI;
            dh.DAXOA = _news_dh.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/orderform/delete")]
        public IHttpActionResult Delete([FromUri]string id)
        {
            DONHANG dh = (from s in db.DONHANGs where s.MA == id select s).First();
            dh.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
