using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models;


namespace ESApi.Models.Code.Admin
{
    public class AdminManufactoryCode
    {
        ESDBEntities db = new ESDBEntities();

        public List<NHASANXUATModel> getall()
        {
            List<NHASANXUAT> listsx = db.NHASANXUATs.ToList();

            Mapper.CreateMap<NHASANXUAT, NHASANXUATModel>();
            List<NHASANXUATModel> _listsx = Mapper.Map<List<NHASANXUAT>, List<NHASANXUATModel>>(listsx);
            return _listsx;
        }

        public NHASANXUATModel getId(int id)
        {
            NHASANXUATModel sx = new NHASANXUATModel();

            var _sx = (from s in db.NHASANXUATs where s.MA == id select s).First();

            Mapper.CreateMap<NHASANXUAT, NHASANXUATModel>();
            sx = Mapper.Map<NHASANXUAT, NHASANXUATModel>(_sx);
            return sx;
        }

        public string GetNameManufactory(int MA)
        {
            var manu = db.NHASANXUATs.Where(sp => sp.MA == MA).SingleOrDefault();
            return manu.TEN;
        }

        public int GetIDManufactory(string name)
        {
            var manu = db.NHASANXUATs.Where(sp => sp.TEN == name).SingleOrDefault();
            return manu.MA;
        }

        public void add(NHASANXUATModel sx)
        {
            Mapper.CreateMap<NHASANXUATModel, NHASANXUAT>();
            NHASANXUAT _sx = Mapper.Map<NHASANXUATModel, NHASANXUAT>(sx);
            db.NHASANXUATs.Add(_sx);
            db.SaveChanges();
        }

        public void update(NHASANXUATModel news_sx)
        {
            Mapper.CreateMap<NHASANXUATModel, NHASANXUAT>();
            NHASANXUAT _news_sx = Mapper.Map<NHASANXUATModel, NHASANXUAT>(news_sx);

            NHASANXUAT sx = (from s in db.NHASANXUATs where s.MA == _news_sx.MA select s).First();

            sx.TEN = news_sx.TEN;
            sx.DAXOA = news_sx.DAXOA;

            db.SaveChanges();
        }
    }
}