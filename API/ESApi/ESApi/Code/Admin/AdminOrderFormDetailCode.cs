using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Http.Cors;
using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models;

namespace ESApi.Models.Code.Admin
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminOrderFormDetailCode
    {
        ESDBEntities db = new ESDBEntities();

        public List<CHITIETDONHANGModel> getall()
        {
            List<CHITIETDONHANG> listdh = db.CHITIETDONHANGs.ToList();

            Mapper.CreateMap<CHITIETDONHANG, CHITIETDONHANGModel>();
            List<CHITIETDONHANGModel> _listdh = Mapper.Map<List<CHITIETDONHANG>, List<CHITIETDONHANGModel>>(listdh);
            return _listdh;
        }

        public CHITIETDONHANGModel getId(string iddh)
        {
            CHITIETDONHANGModel dh = new CHITIETDONHANGModel();

            var _dh = (from s in db.CHITIETDONHANGs where (s.DONHANG == iddh ) select s).First();

            Mapper.CreateMap<CHITIETDONHANG, CHITIETDONHANGModel>();
            dh = Mapper.Map<CHITIETDONHANG, CHITIETDONHANGModel>(_dh);
            return dh;
        }

        public void update(CHITIETDONHANGModel news_dh)
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
        }

        public void add(CHITIETDONHANGModel news_dh)
        {
            Mapper.CreateMap<CHITIETDONHANGModel, CHITIETDONHANG>();
            CHITIETDONHANG _news_dh = Mapper.Map<CHITIETDONHANGModel, CHITIETDONHANG>(news_dh);
            db.CHITIETDONHANGs.Add(_news_dh);
            db.SaveChanges();
        }
    }
}