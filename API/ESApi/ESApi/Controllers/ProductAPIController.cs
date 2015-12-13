using ESApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESApi.Models.ModelEntity;
using AutoMapper;

namespace ESApi.Controllers
{
    public class ProductAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();
        [HttpGet]
        [Route("api/product/all")]
        public IHttpActionResult GetAllProducts()
        {
                List<SANPHAM> list = db.SANPHAMs.ToList();
                Mapper.CreateMap<SANPHAM, SANPHAMModel>();
                List<SANPHAMModel> ret =
                    Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(list);
                return Ok(ret);
        }
    }
}
