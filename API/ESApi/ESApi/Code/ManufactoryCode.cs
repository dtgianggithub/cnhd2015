using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.Code
{
    public class ManufactoryCode
    {
        ESDBEntities db = new ESDBEntities();
        public ManufactoryList GetListNSX()
        {
            ManufactoryList list = new ManufactoryList();

            var listNhaSanXuat = db.NHASANXUATs.Where(nsx => nsx.DAXOA.Value.Equals(false)).ToList();

            Mapper.CreateMap<NHASANXUAT, NHASANXUATModel>();
            list.listNhaSanXuat = Mapper.Map<List<NHASANXUAT>, List<NHASANXUATModel>>(listNhaSanXuat);
            return list;
        }

        public List<NHASANXUATModel> GetAllNSX()
        {
            List<NHASANXUATModel> list = new List<NHASANXUATModel>();

            var listNhaSanXuat = db.NHASANXUATs.Where(nsx => nsx.DAXOA.Value.Equals(false)).ToList();

            Mapper.CreateMap<NHASANXUAT, NHASANXUATModel>();
            list = Mapper.Map<List<NHASANXUAT>, List<NHASANXUATModel>>(listNhaSanXuat);
            return list;
        }

        public NHASANXUATModel GetNSX(int ID)
        {
            NHASANXUATModel list = new NHASANXUATModel();

            var NhaSanXuat = db.NHASANXUATs.Where(nsx => nsx.DAXOA.Value.Equals(false) && nsx.MA == ID ).SingleOrDefault();

            Mapper.CreateMap<NHASANXUAT, NHASANXUATModel>();
            list = Mapper.Map<NHASANXUAT, NHASANXUATModel>(NhaSanXuat);
            return list;
        }
    }
}