using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankaOtomasyon.Models
{
    public class KayitModel
    {
        [Required]
        [Display(Name = "TCKN")]
        public long TCKN { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        [StringLength(50, ErrorMessage = "Şifreniz en az {1} a en fazla {2} uzunlugunda olmalidir!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required]
        [Display(Name = "Sifre Onay")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Sifre", ErrorMessage = "Şifreler Uyumsuz!")]
        public string SifreOnay { get; set; }

        [Required]
        [Display(Name = "İsim")]
        public string Ad { get; set; }

        [Required]
        [Display(Name = "Soyisim")]
        public string SoyAd { get; set; }

        [Required]
        [Display(Name = "Telefon No")]
        public string TelefonNo { get; set; }

    }
}