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
    public class AdminMemberAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpPost]
        [Route("api/admin/member/add")]
        public IHttpActionResult Add([FromBody]THANHVIENModel tv)
        {
            Mapper.CreateMap<THANHVIENModel, THANHVIEN>();
            THANHVIEN _tv = Mapper.Map<THANHVIENModel, THANHVIEN>(tv);
            db.THANHVIENs.Add(_tv);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/member/update")]
        public IHttpActionResult Update([FromBody]THANHVIENModel news_tv)
        {
            Mapper.CreateMap<THANHVIENModel, THANHVIEN>();
            THANHVIEN _news_tv = Mapper.Map<THANHVIENModel, THANHVIEN>(news_tv);
            THANHVIEN tv = (from s in db.THANHVIENs where s.MA == _news_tv.MA select s).First();

            tv.MA = _news_tv.MA;
            tv.TEN = _news_tv.TEN;
            tv.TENDANGNHAP = _news_tv.TENDANGNHAP;
            tv.MATKHAU = _news_tv.MATKHAU;
            tv.DIACHI = _news_tv.DIACHI;
            tv.DIENTHOAI = _news_tv.DIENTHOAI;
            tv.EMAIL = _news_tv.EMAIL;
            tv.THONGTINMOTA = _news_tv.THONGTINMOTA;
            tv.LOAITHANHVIEN = _news_tv.LOAITHANHVIEN;
            tv.DAXOA = _news_tv.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/member/delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            THANHVIEN tv = (from s in db.THANHVIENs where s.MA == id select s).First();
            tv.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
