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

namespace ESApi.Controllers.Admin
{
    public class AdminProductAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpPost]
        [Route("api/admin/product/add")]
        public IHttpActionResult Add ([FromBody]SANPHAMModel sp)
        {
            Mapper.CreateMap<SANPHAMModel,SANPHAM>();
            SANPHAM _sp = Mapper.Map<SANPHAMModel,SANPHAM>(sp);
            db.SANPHAMs.Add(_sp);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/product/update")]
        public IHttpActionResult Update ([FromBody]SANPHAMModel news_sp)
        {
            Mapper.CreateMap<SANPHAMModel, SANPHAM>();
            SANPHAM _news_sp = Mapper.Map<SANPHAMModel, SANPHAM>(news_sp);

            SANPHAM sp = (from s in db.SANPHAMs where s.MA == _news_sp.MA select s).First();
            sp.MA = _news_sp.MA;
            sp.TEN = _news_sp.TEN;
            sp.MOTA = _news_sp.MOTA;
            sp.DONGIABAN = _news_sp.DONGIABAN;
            sp.HINHANH = _news_sp.HINHANH;
            sp.SOLUONG = _news_sp.SOLUONG;
            sp.LOAISANPHAM = _news_sp.LOAISANPHAM;
            sp.SANPHAMMOI = _news_sp.SANPHAMMOI;
            sp.NHASANXUAT = _news_sp.NHASANXUAT;
            sp.SANPHAMBANCHAY = _news_sp.SANPHAMBANCHAY;
            sp.KHUYENMAI = _news_sp.KHUYENMAI;
            sp.DAXOA = _news_sp.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/product/delete")]
        public IHttpActionResult Delete([FromUri]string id)
        {
            SANPHAM sp = (from s in db.SANPHAMs where s.MA == id select s).First();
            sp.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }


}
