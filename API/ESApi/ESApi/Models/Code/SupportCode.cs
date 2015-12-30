using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESApi.Models.Code
{
    public class SupportCode
    {
        public ESDBEntities db = new ESDBEntities();

        public List<HOTROONLINE> GetList()
        {
            var listsupport = db.HOTROONLINEs.Where(s => s.DAXOA == false).ToList();
            return listsupport;
        }
    }
}