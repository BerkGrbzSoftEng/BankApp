using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using BankaOtomasyon.Models;
using PagedList;

namespace BankaOtomasyon.Controllers
{
    [LoginController]
    public class TransferController : Controller
    {
        private BankaDBEntities db=new BankaDBEntities();
        // GET: Transfer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Virman()
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
            
            ViewBag.GonderenHesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            ViewBag.AliciHesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }
        [HttpPost]
        public ActionResult Virman([Bind(Include = "GonderilenBakiye,GonderenHesapEkNo,AliciHesapEkNo")]VirmanModel model)
        {
            int? a = Convert.ToInt32(Session["musteriHesapNo"]);
            int? b = model.GonderenHesapEkNo;
            int? c = model.GonderilenBakiye;
            int? d = model.AliciHesapEkNo;
            DateTime tarih=DateTime.Now;
            var bakiye = db.tblMusteriHesap.Where(i => i.hesapEkNumarasi == b && i.hesapNumarasi == a)
                .Max(i => i.hesapBakiye);
            if (ModelState.IsValid && bakiye>=c)
            {
                db.VirmanKaydet(d, b, c, a, tarih);
                db.Virman(d, b, c, a);
                return RedirectToAction("HesapOlustur", "Hesap");
            }
            else
            {
                int mID = Convert.ToInt32(Session["musteriId"]);
                string message = "";
                message = "Hesabınızda yeterli miktara para yok isleminiz gerçekleştirelemiyor!";
                ViewBag.message = message;
                var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
                ViewBag.AliciHesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
                ViewBag.GonderenHesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            }

            return View();
        }


        public JsonResult aliciDoldur(int GonderenHesapEkNo)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int a = Convert.ToInt32(Session["musteriHesapNo"]);
            
            var model = db.Doldur(GonderenHesapEkNo, a).Where(i=>i.hesapAcikMi==true);
         
            return Json(model.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult bakiyeDoldur(int GonderenHesapEkNo)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int a = Convert.ToInt32(Session["musteriHesapNo"]);

            var model = db.tblMusteriHesap.Where(i => i.hesapNumarasi == a && i.hesapEkNumarasi == GonderenHesapEkNo);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult bakiyeGetir(int EkNo)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int a = Convert.ToInt32(Session["musteriHesapNo"]);

            var model = db.tblMusteriHesap.Where(i => i.hesapNumarasi == a && i.hesapEkNumarasi == EkNo);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Havale()
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);

            ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }
        [HttpPost]
        public ActionResult Havale([Bind(Include = "EkNo,bakiye,AliciHesapNo,AliciHesapEkNo")]HavaleModel model)
        {

            int? a = Convert.ToInt32(Session["musteriHesapNo"]);
            decimal? b = model.bakiye;
            int? c = model.AliciHesapNo;
            int? d = model.AliciHesapEkNo;
            int e = model.EkNo;
            DateTime tarih = DateTime.Now;
            var bakiye = db.tblMusteriHesap.Where(i => i.hesapEkNumarasi == e && i.hesapNumarasi == a)
                .Max(i => i.hesapBakiye);
            var hesap = db.tblMusteriHesap.FirstOrDefault(i => i.hesapNumarasi == a);
            if (ModelState.IsValid && bakiye >= b)
            {
                if (Kontrol(c, d)==true)
                {
                    
                        db.Havale(a, b, c, e, d);
                        db.HavaleKaydet(d, e, b, c, a, tarih);
                        return RedirectToAction("HesapOlustur", "Hesap");
                   
                }
                else
                {
                    string message = "";
                    message = "Kayitli hesap bulunamadi";
                    ViewBag.message = message;
                    int mID = Convert.ToInt32(Session["musteriId"]);
                    var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);


                    ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
                }
                
            }
            else
            {
                
                string message = "";
                message = "Hesabınızda yeterli miktara para yok isleminiz gerçekleştirelemiyor!";
                ViewBag.message = message;
                int mID = Convert.ToInt32(Session["musteriId"]);
                var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);


                ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi"); 
            }

            return View();
        }

        public bool Kontrol(int? hesapNo, int? ekNo)
        {
            int mID = Convert.ToInt32(Session["musteriHesapNo"]);
            if (hesapNo == mID)
            {
                return false;
            }
            else
            {
                var model = db.tblMusteriHesap.FirstOrDefault(i => i.hesapNumarasi == hesapNo && i.hesapEkNumarasi == ekNo && i.hesapAcikMi == true);

                return model != null;
            }

            
        }

        public ActionResult İslemTablosu(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var model=new CokluModel();
            int? a = Convert.ToInt32(Session["musteriHesapNo"]);
            model.TblHavales = db.tblHavale.Where(i => i.havaleGonderenHesapNo == a).ToList().ToPagedList(_sayfaNo,5);
            model.TblVirmans = db.tblVirman.Where(i => i.hesapNumarasi == a).ToList().ToPagedList(_sayfaNo,5);
             
            return View(model);
        }
    }
}