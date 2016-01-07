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
    public class AdminProductCode
    {
        ESDBEntities db = new ESDBEntities();
        AdminManufactoryCode code = new AdminManufactoryCode();
        private AdminTypeProductCode typecode = new AdminTypeProductCode();

        public List<SANPHAMModel> getall()
        {
            List<SANPHAM> listSP = db.SANPHAMs.ToList();

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            List<SANPHAMModel> _listSP = Mapper.Map<List<SANPHAM>, List<SANPHAMModel>>(listSP);

            for (int i = 0; i < listSP.Count; i++)
            {
                _listSP[i].TENNHASANXUAT = code.GetNameManufactory(listSP[i].NHASANXUAT.Value);
                _listSP[i].TENLOAISANPHAM = typecode.GetNameType(listSP[i].LOAISANPHAM.Value);
            }
            return _listSP;
        }

        public SANPHAMModel getId (string id)
        {
            SANPHAMModel sp = new SANPHAMModel();

            var _sp = (from s in db.SANPHAMs where s.MA == id select s).First();

            Mapper.CreateMap<SANPHAM, SANPHAMModel>();
            sp = Mapper.Map<SANPHAM,SANPHAMModel>(_sp);
            sp.TENNHASANXUAT = code.GetNameManufactory(sp.NHASANXUAT);
            sp.TENLOAISANPHAM = typecode.GetNameType(sp.LOAISANPHAM);
            return sp;
        }

        public void add(SANPHAMModel sx)
        {
            Mapper.CreateMap<SANPHAMModel, SANPHAM>();
            SANPHAM _sx = Mapper.Map<SANPHAMModel, SANPHAM>(sx);
            db.SANPHAMs.Add(_sx);
            db.SaveChanges();
        }

        public void update(SANPHAMModel news_sp)
        {
            Mapper.CreateMap<SANPHAMModel, SANPHAM>();
            SANPHAM _news_sp = Mapper.Map<SANPHAMModel, SANPHAM>(news_sp);

            SANPHAM sp = (from s in db.SANPHAMs where s.MA == _news_sp.MA select s).First();
            sp.MA = _news_sp.MA;
            sp.TEN = _news_sp.TEN;
            sp.MOTA = _news_sp.MOTA;
            sp.DONGIABAN = _news_sp.DONGIABAN;
            sp.HINHANH = _news_sp.HINHANH;
            sp.SOLUONG = _news_sp.SOLUONG;
            sp.LOAISANPHAM = news_sp.LOAISANPHAM; //typecode.GetIDType(news_sp.TENLOAISANPHAM);
            sp.SANPHAMMOI = _news_sp.SANPHAMMOI;
            sp.NHASANXUAT = code.GetIDManufactory(news_sp.TENNHASANXUAT);
            sp.SANPHAMBANCHAY = _news_sp.SANPHAMBANCHAY;
            sp.MAKHUYENMAI = _news_sp.MAKHUYENMAI;
            sp.DAXOA = _news_sp.DAXOA;

            db.SaveChanges();
        }
    }
}