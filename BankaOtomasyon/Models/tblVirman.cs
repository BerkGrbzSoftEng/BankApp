//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankaOtomasyon.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblVirman
    {
        public int virmanId { get; set; }
        public Nullable<int> hesapNumarasi { get; set; }
        public Nullable<int> aliciEkNumarasi { get; set; }
        public Nullable<int> gonderilenbakiye { get; set; }
        public Nullable<System.DateTime> gonderilenTarih { get; set; }
        public Nullable<int> gonderenEkNumarasi { get; set; }
    }
}