using ESApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ESApi.Models.ModelEntity;
using AutoMapper;
using ESApi.Models.Code;
using ESApi.Models.ViewModel;

namespace ESApi.Controllers
{
    public class ProductAPIController : ApiController
    {
        ESDBEntities db = new ESDBEntities();
        ProductCode code = new ProductCode();

        [HttpGet]
        [Route("api/product/all")]
        public IHttpActionResult GetAllProducts()
        {
                return Ok(code.GetProductList());
        }

        [HttpGet]
        [Route("api/product/ByNameID/{Name}/{ID}")]
        public IHttpActionResult GetProductsbyNameID(string Name, string ID)
        {
            return Ok(code.GetProductsByType(Name,int.Parse(ID)));
        }

        [HttpPost]
        [Route("api/product/SearchAdvance")]
        public IHttpActionResult SeaarchAdvance([FromBody] SearchModel search)
        {
            return Ok(code.GetProductsByMultiType(search));
        }

        [HttpGet]
        [Route("api/product/specialproduct")]
        public IHttpActionResult GetSpeacialProducts()
        {
            return Ok(code.GetSpecialProduct());
        }

        [HttpGet]
        [Route("api/product/newproduct")]
        public IHttpActionResult GetNewProducts()
        {
            return Ok(code.GetNewProduct());
        }

        [HttpGet]
        [Route("api/product/ByID/{ID}")]
        public IHttpActionResult GetProductbyID(string ID)
        {
            return Ok(code.GetDetailProduct(ID));
        }

        [HttpGet]
        [Route("api/product/GetByID/{ID}")]
        public IHttpActionResult GetProduct(string ID)
        {
            return Ok(code.GetProduct(ID));
        }

        [HttpGet]
        [Route("api/product/ByLSP/{LOAISANPHAM}")]
        public IHttpActionResult GetProduct(int LOAISANPHAM)
        {
            List<SANPHAM> list = db.SANPHAMs.Where(sp => sp.DAXOA == false && sp.LOAISANPHAM == LOAISANPHAM).ToList();
            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            List<SANPHAMModel> ret =
                Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(list);
            return Ok(ret);
        }

        [HttpGet]
        [Route("api/product/ByNSX/{NHASANXUAT}")]
        public IHttpActionResult GetProductbyNSX(int NHASANXUAT)
        {
            List<SANPHAM> list = db.SANPHAMs.Where(sp => sp.DAXOA == false && sp.NHASANXUAT == NHASANXUAT).ToList();
            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            List<SANPHAMModel> ret =
                Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(list);
            return Ok(ret);
        }

        [HttpPost]
        [Route("api/product/add")]
        public IHttpActionResult AddProduct([FromBody] SANPHAM sp)
        {
            db.SANPHAMs.Add(sp);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("api/product/update")]
        public IHttpActionResult UpdateProduct([FromUri] string Ma, [FromBody] SANPHAM s)
        {
            SANPHAM sanpham = db.SANPHAMs.Where(sp => sp.DAXOA == false && sp.MA == Ma).SingleOrDefault();
            sanpham.MA = s.MA;
            sanpham.TEN = s.TEN;
            sanpham.DONGIABAN = s.DONGIABAN;
            sanpham.MOTA = s.MOTA;
            sanpham.SOLUONG = s.SOLUONG;
            sanpham.LOAISANPHAM = s.LOAISANPHAM;
            sanpham.SANPHAMMOI = s.SANPHAMMOI;
            sanpham.SANPHAMMOI = s.SANPHAMMOI;
            sanpham.SANPHAMBANCHAY = s.SANPHAMBANCHAY;
            sanpham.DAXOA = s.DAXOA;
            sanpham.MAKHUYENMAI = s.MAKHUYENMAI;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("api/product/delete")]
        public IHttpActionResult DeleteProduct([FromUri] string Ma)
        {
            SANPHAM sanpham = db.SANPHAMs.Where(sp => sp.DAXOA == false && sp.MA == Ma).SingleOrDefault();
            db.SANPHAMs.Remove(sanpham);
            db.SaveChanges();
            return Ok();
        }
    }
}
