using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using ESApi.Models.Code.Admin;
using ESApi.Models.ModelEntity;
using ESApi.Models;
using System.Web.Http.Cors;
using System.Web;
using AutoMapper;

namespace ESApi.Models.Code.Admin
{
    public class AdminPromotionCode
    {
        ESDBEntities db = new ESDBEntities();

        public List<KHUYENMAIModel> getall()
        {
            List<KHUYENMAI> listkm = db.KHUYENMAIs.ToList();

            Mapper.CreateMap<KHUYENMAI, KHUYENMAIModel>();
            List<KHUYENMAIModel> _listkm = Mapper.Map<List<KHUYENMAI>, List<KHUYENMAIModel>>(listkm);
            return _listkm;
        }

        public KHUYENMAIModel getId(int id)
        {
            KHUYENMAIModel km = new KHUYENMAIModel();

            var _km = (from s in db.KHUYENMAIs where s.MA == id select s).First();

            Mapper.CreateMap<KHUYENMAI, KHUYENMAIModel>();
            km = Mapper.Map<KHUYENMAI, KHUYENMAIModel>(_km);
            return km;
        }

        public void add(KHUYENMAIModel km)
        {
            Mapper.CreateMap<KHUYENMAIModel, KHUYENMAI>();
            KHUYENMAI _km = Mapper.Map<KHUYENMAIModel, KHUYENMAI>(km);
            db.KHUYENMAIs.Add(_km);
            db.SaveChanges();
        }

        public void update(KHUYENMAIModel news_km)
        {
            Mapper.CreateMap<KHUYENMAIModel, KHUYENMAI>();
            KHUYENMAI _news_km = Mapper.Map<KHUYENMAIModel, KHUYENMAI>(news_km);

            KHUYENMAI km = (from s in db.KHUYENMAIs where s.MA == _news_km.MA select s).First();

            km.NGAYBATDAU = news_km.NGAYBATDAU;
            km.NGAYKETTHUC = news_km.NGAYKETTHUC;
            km.NOIDUNG = news_km.NOIDUNG;
            km.DAXOA = news_km.DAXOA;

            db.SaveChanges();
        }
    }
}