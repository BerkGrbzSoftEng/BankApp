using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankaOtomasyon.Models;

namespace BankaOtomasyon.Controllers
{
    [LoginController]
    public class HesapController : Controller
    {
        private BankaDBEntities db = new BankaDBEntities();
        // GET: Hesap
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HesapOlustur()
        {
            
            int b = Convert.ToInt32(Session["musteriId"]);
            var model = db.tblMusteriHesap.FirstOrDefault(i => i.musteriId == b);
            return View(model);
        }

        [HttpGet]
        public ActionResult EkHesapOlustur()
        {
            string message = "";
            var hesap = new tblMusteriHesap();


            int a = Convert.ToInt32(Session["musteriHesapNo"]);
            int b = Convert.ToInt32(Session["musteriId"]);
            hesap.hesapNumarasi = a;
            hesap.musteriId = Convert.ToInt32(Session["musteriId"]);
            hesap.hesapAcilisTarihi = DateTime.Now;
            hesap.hesapAcikMi = true;
            hesap.hesapBakiye = 0;
            hesap.hesapEkNumarasi = HesapEkNo();
            var result = db.tblMusteriHesap.Add(hesap);


            if (result != null)
            {
                db.SaveChanges();
                return RedirectToAction("HesapOlustur");
            }

            return View();
        }


        public int HesapEkNo()
        {
            int a = (int)Session["musteriId"];
            var q = db.tblMusteriHesap.FirstOrDefault(i => i.musteriId == a);
            if (q == null)
            {
                return 5001;
            }
            else
            {
                int? b = db.tblMusteriHesap.Where(i => i.musteriId == a).Max(i => i.hesapEkNumarasi);
                int ekno = Convert.ToInt32(b + 1);
                return ekno;
            }

        }

        public PartialViewResult _tblHesap()
        {
            int a = Convert.ToInt32(Session["musteriId"]);
            var model = db.tblMusteriHesap.Where(i => i.musteriId == a).ToList();
            return PartialView("_tblHesap", model);

        }

        public ActionResult HesapKapat(int? id)
        {
            string bakiye = "";
            tblMusteriHesap hesap = db.tblMusteriHesap.Find(id);
            hesap.hesapAcikMi = false;
            hesap.hesapKapanisTarihi = DateTime.Now;
            if (ModelState.IsValid && hesap.hesapBakiye==0)
            {
                db.Entry(hesap).State = EntityState.Modified;
                db.SaveChanges();

            }
            else
            {
                bakiye = "Hesabinizi kapatmak icin bakiyeniz olmamalidir";
                TempData["bakiye"] = bakiye;
            }
            return RedirectToAction("HesapOlustur");
        }

        [HttpPost]
        public ActionResult HesapParaYatir([Bind(Include = "HesapEkNo,Para")]HesapParaModel model)
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            if (ModelState.IsValid)
            {
                db.ParaYatir(model.Para, model.HesapEkNo,mID);
                return RedirectToAction("HesapOlustur");
            }

            return View();
        }

        [HttpGet]
        public ActionResult HesapParaYatir()
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
            ViewBag.HesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }
        [HttpGet]
        public ActionResult HesapParaCek()
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
            ViewBag.HesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }

        [HttpPost]
        public ActionResult HesapParaCek([Bind(Include = "HesapEkNo,Para")]HesapParaModel model)
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            int hesap = model.HesapEkNo;
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
            string message = "";
            var max = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapEkNumarasi == hesap).Max(i => i.hesapBakiye);
            if (ModelState.IsValid && model.Para <= max)
            {
                db.ParaCek(model.Para, model.HesapEkNo,mID);
                return RedirectToAction("HesapOlustur");
            }
            else
            {
                ViewBag.message = "Hesabinizda yeterli miktar yok!";
            }

            ViewBag.HesapEkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View(model);
        }
    }
}