using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BankaOtomasyon.Models;

namespace BankaOtomasyon.Controllers
{

    public class UyelikController : Controller
    {
        private BankaDBEntities db=new BankaDBEntities();
        // GET: Uyelik
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Kayit()
        {
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Kayit(KayitModel model,string returnUrl)
        {
          
            string message = "";
            bool Status = false;
            #region //User Register
            if (ModelState.IsValid)
            {

                var isExist = IsKimlikNoExist(model.TCKN);
                string hesapNo = HesapNoÜret();
                if (isExist)
                {
                    ModelState.AddModelError("TCKN", "Bu TCKN ait baska bir müsteri vardir!");
                    message = "Bu TCKN ait baska bir müsteri vardir!";
                    ViewBag.message = message;
                    return View(model);
                }

                var sifre = Crypto.Hash(model.Sifre);
                var user = new tblMusteri();
                var hesap=new tblMusteriHesap();
                user.musteriAd = model.Ad;
                user.musteriSoyAd = model.SoyAd;
                user.musteriMail = model.Email;
                user.musteriSifre = sifre;
                user.musteriTCKN = model.TCKN;
                user.musteriTelefon = model.TelefonNo;
                user.musteriNumarası = Convert.ToInt32(hesapNo);
                
                var result = db.tblMusteri.Add(user);
                if (result != null)
                {
                    db.SaveChanges();
                    Status = true;
                     
                    return RedirectToAction("Giris", "Uyelik");

                }

            }
            else
            {
                message = "Gecersiz İstek";
            }

            #endregion

            ViewBag.Message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Giris(GirisModel model)
        {
            string message = "";
            string errormessage = "";
            var user = db.tblMusteri.Where(i => i.musteriTCKN == model.TCKN).FirstOrDefault();
            if (user != null)
            {
                 
                if (string.Compare(Crypto.Hash(model.Sifre), user.musteriSifre) == 0 )
                { 
                    FormsAuthentication.SetAuthCookie(model.TCKN.ToString(),false);
                    ViewBag.message = message;
                    Session["musteriFullName"] = user.musteriAd+" "+user.musteriSoyAd;
                    Session["musteriHesapNo"] = user.musteriNumarası;
                    Session["musteriId"] = user.musteriId;
                    Session["musteriKimlikNumara"] = user.musteriTCKN;
                    return RedirectToAction("Anasayfa", "Home");

                }
                else
                {
                    errormessage = "Şifre veya Kimlik Numarasi Hatali";
                    ViewBag.errormessage = errormessage;
                }

            }
            else
            {

                errormessage = "Hatali Giriş";
                ViewBag.errormessage = errormessage;
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult Giris()
        {
            return View();
        }

        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Giris");
        }

        [NonAction]
        public bool IsKimlikNoExist(long kimlikNo)
        {
            using (BankaDBEntities db = new BankaDBEntities())
            {
                var v = db.tblMusteri.Where(a => a.musteriTCKN == kimlikNo).FirstOrDefault();
                return v != null;
            }
        }

        public string HesapNoÜret()
        {
            Random rand=new Random();
            int no;
            string hesap = "";
            for (int i = 1; i < 10; i++)
            {
                no= rand.Next(10) + 0;
                hesap += no.ToString();

            }

            return hesap;
        }
    }
}