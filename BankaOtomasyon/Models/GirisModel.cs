using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankaOtomasyon.Models
{
    public class GirisModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "TCKN Zorunlu!")]
        [Display(Name = "TCKN")]
        public long TCKN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sifre Zorunlu!")]
        [Display(Name = "Sifre")]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}