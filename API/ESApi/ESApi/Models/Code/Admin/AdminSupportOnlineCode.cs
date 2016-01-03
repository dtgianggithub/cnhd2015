using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.Code.Admin
{
    public class AdminSupportOnlineCode
    {
        ESDBEntities db = new ESDBEntities();

        public List<HOTROONLINE> getall()
        {
            List<HOTROONLINE> list = db.HOTROONLINEs.ToList();
            return list;
        }

        public HOTROONLINE getId(int id)
        {
            HOTROONLINE sx = new HOTROONLINE();
            var _sx = (from s in db.HOTROONLINEs where s.MA == id select s).First();

            return sx;
        }

        public void add(HOTROONLINE ht)
        {
            db.HOTROONLINEs.Add(ht);
            db.SaveChanges();
        }

        public void update(HOTROONLINE news_ht)
        {
            HOTROONLINE ht = (from s in db.HOTROONLINEs where s.MA == news_ht.MA select s).First();

            ht.TEN = news_ht.TEN;
            ht.DAXOA = news_ht.DAXOA;
            ht.SKYPE = news_ht.SKYPE;

            db.SaveChanges();
        }
    }
}