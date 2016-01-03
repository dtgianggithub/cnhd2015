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


namespace ESApi.Models.Code.Admin
{
    [EnableCors(origins: "*",headers:"*",methods: "*")]
    public class AdminManufactoryController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpGet]
        [Route("api/admin/manufactory/all")]
        public IHttpActionResult GetAll()
        {
            List<NHASANXUAT> listNhaSanXuat = db.NHASANXUATs.ToList();

            Mapper.CreateMap<NHASANXUAT, NHASANXUATModel>();
            List<NHASANXUATModel> listNSX = Mapper.Map<List<NHASANXUAT>, List<NHASANXUATModel>>(listNhaSanXuat);

            return Ok(listNSX);
        }

        [HttpPost]
        [Route("api/admin/manufactory/add")]
        public IHttpActionResult Add([FromBody]NHASANXUATModel sx)
        {
            Mapper.CreateMap<NHASANXUATModel, NHASANXUAT>();
            NHASANXUAT _sx = Mapper.Map<NHASANXUATModel, NHASANXUAT>(sx);
            db.NHASANXUATs.Add(_sx);
            db.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/admin/manufactory/update")]
        public IHttpActionResult Update([FromBody]NHASANXUATModel news_sx)
        {
            Mapper.CreateMap<NHASANXUATModel, NHASANXUAT>();
            NHASANXUAT _news_sx = Mapper.Map<NHASANXUATModel, NHASANXUAT>(news_sx);
            NHASANXUAT sx = (from s in db.NHASANXUATs where s.MA == _news_sx.MA select s).First();

            sx.MA = _news_sx.MA;
            sx.TEN = _news_sx.TEN;
            sx.DAXOA = _news_sx.DAXOA;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/admin/manufactory/delete")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            NHASANXUAT sx = (from s in db.NHASANXUATs where s.MA == id select s).First();
            sx.DAXOA = true;
            db.SaveChanges();

            return Ok();
        }
    }
}
