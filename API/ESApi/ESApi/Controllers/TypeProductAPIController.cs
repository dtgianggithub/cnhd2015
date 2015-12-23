using AutoMapper;
using ESApi.Models;
using ESApi.Models.ModelEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ESApi.Controllers
{
    public class TypeProductAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();

        [HttpGet]
        [Route("api/typeproduct/all")]
        public IHttpActionResult GetAllTypeProducts()
        {
            List<LOAISANPHAM> list = db.LOAISANPHAMs.Where(sp => sp.DAXOA == false).ToList();
            Mapper.CreateMap<LOAISANPHAM, LOAISANPHAMModel>();
            List<LOAISANPHAMModel> ret =
                Mapper.Map<List<LOAISANPHAM>, List<LOAISANPHAMModel>>(list);
            return Ok(ret);
        }

        [HttpGet]
        [Route("api/typeproduct/byID")]
        public IHttpActionResult GetTypeProduct(int ID)
        {
            LOAISANPHAM loaisp = db.LOAISANPHAMs.Where(sp => sp.DAXOA == false && sp.MA == ID).SingleOrDefault();
            Mapper.CreateMap<LOAISANPHAM, LOAISANPHAMModel>();
            LOAISANPHAMModel loaisp_model =
                Mapper.Map<LOAISANPHAM, LOAISANPHAMModel>(loaisp);
            return Ok(loaisp_model);
        }

        [HttpPost]
        [Route("api/typeproduct/add")]
        public IHttpActionResult AddTypeProduct([FromBody] LOAISANPHAM sp)
        {
            db.LOAISANPHAMs.Add(sp);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/typeproduct/update")]
        public IHttpActionResult UpdateProduct([FromUri] int Ma, [FromBody] LOAISANPHAM s)
        {
            LOAISANPHAM loaisanpham = db.LOAISANPHAMs.Where(sp => sp.DAXOA == false && sp.MA == Ma).SingleOrDefault();
            loaisanpham.MA = s.MA;
            loaisanpham.TEN = s.TEN;
            loaisanpham.DAXOA = s.DAXOA;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("api/typeproduct/delete")]
        public IHttpActionResult DeleteTypeProduct([FromUri] int Ma)
        {
            LOAISANPHAM loaisanpham = db.LOAISANPHAMs.Where(sp => sp.DAXOA == false && sp.MA == Ma).SingleOrDefault();
            db.LOAISANPHAMs.Remove(loaisanpham);
            db.SaveChanges();
            return Ok();
        }
    }
}
