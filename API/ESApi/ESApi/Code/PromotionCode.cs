using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ESApi.Models;
using ESApi.Models.ModelEntity;
using AutoMapper;

namespace ESApi.Models.Code
{
    public class PromotionCode
    {
        public ESDBEntities db = new ESDBEntities();

        public KHUYENMAIModel GetPromotion(int id)
        {
            var promotion = db.KHUYENMAIs.Where(km => km.MA == id && km.DAXOA == false && DateTime.Compare(DateTime.Now, km.NGAYBATDAU.Value) >= 0 && DateTime.Compare(DateTime.Now, km.NGAYKETTHUC.Value) <= 0).SingleOrDefault();
            Mapper.CreateMap<KHUYENMAI, KHUYENMAIModel>();
            KHUYENMAIModel ret = Mapper.Map<KHUYENMAI, KHUYENMAIModel>(promotion);
            return ret;
        }
    }
}