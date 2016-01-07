using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using AutoMapper;
using ESApi.Models.ModelEntity;
using ESApi.Models;


namespace ESApi.Models.Code.Admin
{
    public class AdminOrderFormCode
    {

       ESDBEntities db = new ESDBEntities();

       public List<DONHANGModel> getall()
       {
           List<DONHANG> listdh = db.DONHANGs.ToList();

           Mapper.CreateMap<DONHANG, DONHANGModel>();
           List<DONHANGModel> _listdh = Mapper.Map<List<DONHANG>, List<DONHANGModel>>(listdh);
           return _listdh;
       }

       public DONHANGModel getId(string id)
       {
           DONHANGModel dh = new DONHANGModel();

           var _dh = (from s in db.DONHANGs where s.MA == id select s).First();

           Mapper.CreateMap<DONHANG, DONHANGModel>();
           dh = Mapper.Map<DONHANG, DONHANGModel>(_dh);
           return dh;
       }

       public void update(DONHANGModel news_dh)
        {
            Mapper.CreateMap<DONHANGModel, DONHANG>();
            DONHANG _news_dh = Mapper.Map<DONHANGModel, DONHANG>(news_dh);
            DONHANG dh = (from s in db.DONHANGs where s.MA == _news_dh.MA select s).First();

            dh.MA = _news_dh.MA;
            dh.TONGTIEN = _news_dh.TONGTIEN;
            dh.NGAYDATHANG = _news_dh.NGAYDATHANG;
            dh.NGAYNHANHANG = _news_dh.NGAYNHANHANG;
            dh.TENNGUOINHAN = _news_dh.TENNGUOINHAN;
            dh.DIACHINHAN = _news_dh.DIACHINHAN;
            dh.DIENTHOAINGUOINHAN = _news_dh.DIENTHOAINGUOINHAN;
            dh.TRANGTHAI = _news_dh.TRANGTHAI;
            dh.DAXOA = _news_dh.DAXOA;

            db.SaveChanges();
        }

       public void add(DONHANGModel news_dh)
       {
           Mapper.CreateMap<DONHANGModel, DONHANG>();
           DONHANG _news_dh = Mapper.Map<DONHANGModel, DONHANG>(news_dh);
           db.DONHANGs.Add(_news_dh);
           db.SaveChanges();
       }
    }
}