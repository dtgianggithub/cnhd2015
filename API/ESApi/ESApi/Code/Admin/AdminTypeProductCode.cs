using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models;

namespace ESApi.Models.Code.Admin
{
    public class AdminTypeProductCode
    {
        ESDBEntities db = new ESDBEntities();

        public List<LOAISANPHAMModel> getall()
        {
            List<LOAISANPHAM> listsx = db.LOAISANPHAMs.ToList();

            Mapper.CreateMap<LOAISANPHAM, LOAISANPHAMModel>();
            List<LOAISANPHAMModel> _listsx = Mapper.Map<List<LOAISANPHAM>, List<LOAISANPHAMModel>>(listsx);
            return _listsx;
        }

        public LOAISANPHAMModel getId(int id)
        {
            LOAISANPHAMModel sx = new LOAISANPHAMModel();

            var _sx = (from s in db.LOAISANPHAMs where s.MA == id select s).First();

            Mapper.CreateMap<LOAISANPHAM, LOAISANPHAMModel>();
            sx = Mapper.Map<LOAISANPHAM, LOAISANPHAMModel>(_sx);
            return sx;
        }

        public string GetNameType(int MA)
        {
            var manu = db.LOAISANPHAMs.Where(sp => sp.MA == MA).SingleOrDefault();
            return manu.TEN;
        }

        public int GetIDType(string name)
        {
            var manu = db.LOAISANPHAMs.Where(sp => sp.TEN == name).SingleOrDefault();
            return manu.MA;
        }

        public void add(LOAISANPHAMModel sx)
        {
            Mapper.CreateMap<LOAISANPHAMModel, LOAISANPHAM>();
            LOAISANPHAM _sx = Mapper.Map<LOAISANPHAMModel, LOAISANPHAM>(sx);
            db.LOAISANPHAMs.Add(_sx);
            db.SaveChanges();
        }

        public void update(LOAISANPHAMModel news_sx)
        {
            Mapper.CreateMap<LOAISANPHAMModel, LOAISANPHAM>();
            LOAISANPHAM _news_sx = Mapper.Map<LOAISANPHAMModel, LOAISANPHAM>(news_sx);

            LOAISANPHAM sx = (from s in db.LOAISANPHAMs where s.MA == _news_sx.MA select s).First();

            sx.TEN = news_sx.TEN;
            sx.DAXOA = news_sx.DAXOA;

            db.SaveChanges();
        }
    }
}