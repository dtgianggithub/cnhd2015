using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserAPI.Models;
using UserAPI.Models.ViewModels;

namespace UserAPI.Controllers
{
    public class ResourceServerController : ApiController
    {
          //ở đây sẽ viết hàm trả về accesstoken
        [HttpGet]
        [Route("api/{ACCESSTOKEN}/GetAll")]
        public IHttpActionResult RequestAccesstoken(string ACCESSTOKEN )
        {
            using (GHDBEntities db = new GHDBEntities())
            {
                
                NGUOIDUNG nguoidung = db.NGUOIDUNGs.Where(nd => nd.ACCESSTOKEN.Equals(ACCESSTOKEN) && nd.DAXOA == false).SingleOrDefault();
                
                Mapper.CreateMap<NGUOIDUNG,UserViewModel>();
                UserViewModel user = Mapper.Map<NGUOIDUNG,UserViewModel>(nguoidung);
                
                //trả accesstoken về
                return Ok(user);
            }
        }
    }
}
