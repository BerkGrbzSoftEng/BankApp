using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace BankaOtomasyon.Models
{
    public class CokluModel
    {
        public IPagedList<tblVirman> TblVirmans { get; set; }
        public IPagedList<tblHavale> TblHavales { get; set; }
    }
}