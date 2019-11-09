using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankaOtomasyon.Models;
using BankaOtomasyon.ServiceReference1;


namespace BankaOtomasyon.Controllers
{
    public class HGSController : Controller
    {
       private BankaDBEntities db=new BankaDBEntities();
       WebService1SoapClient servis=new WebService1SoapClient();
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult hgsSatis()
        {
            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);

            ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }
        [HttpPost]
        public ActionResult hgsSatis([Bind(Include = "EkNo,Bakiye")] HGSModel model)
        {
            string message = "";
            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);

            #region MyRegion

            if (ModelState.IsValid)
            {
                int msNo = Convert.ToInt32(Session["musteriHesapNo"]);
                var user = db.tblMusteriHesap.FirstOrDefault(i => i.hesapNumarasi == msNo && i.hesapEkNumarasi == model.EkNo);
                if (user.hesapBakiye <= 29)
                {
                    message = "HGS İslemi icin 30 tl den fazla bakiyeniz olmalidir!";
                    ViewBag.message = message;
                    ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
                    return View();
                }
                else
                {
                    long mNo = long.Parse(Session["musteriKimlikNumara"].ToString());
                    var musteriNo = servis.satınAl(mNo);

                    if (musteriNo != 0)
                    {
                        string hgsNo = "";
                        var satis = db.hgsSatis(user.hesapEkNumarasi, user.hesapNumarasi);
                        ViewBag.hgsNo = musteriNo.ToString();
                        ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
                        return View();
                    }
                    else
                    {
                        message = "Hata!Bu kimlik numarasina ait HGS kaydı vardır!";
                        ViewBag.message = message;
                        ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
                    }
                }
            }

            #endregion

            ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }

        [HttpGet]
        public ActionResult hgsIslemleri()
        {

            return View();
        }

        [HttpPost]
        public ActionResult hgsIslemleri(HGSModel model)
        {
            bool a = Kontrol(model.HGSNo);
  
            string message = "";
        
            if (a == true)
            {
               
                TempData["hgsNumara"] = model.HGSNo;
                Session["hgs"] = model.HGSNo;
                return RedirectToAction("hgsBakiyeYukle");
            }
            else
            {
                message = "Hata Böyle bir HGS numarasi bulunmamaktadir!";
                ViewBag.hatamessage = message;
               
                return View();
            }

        }

        [HttpGet]
        public ActionResult hgsBakiyeYukle()
        {

            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
            ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }
        [HttpPost]
        public ActionResult hgsBakiyeYukle(HGSModel model)
        {

            int mID = Convert.ToInt32(Session["musteriId"]);
            var query = db.tblMusteriHesap.Where(i => i.musteriId == mID && i.hesapAcikMi == true);
            if (ModelState.IsValid)
            {
                int a = Convert.ToInt32(Session["hgs"]);
                bool kontrol=servis.BakiyeYukle(model.Bakiye, a);
                if (kontrol)
                {
                    int mNo = Convert.ToInt32(Session["musteriHesapNo"]);
                    db.hgsBakiyeYukle(model.Bakiye, model.EkNo, mNo);
                    return View("Basarili");
                }
                else
                {
                    return View("Basarisiz");
                }
            }

            ViewBag.EkNo = new SelectList(query, "hesapEkNumarasi", "hesapEkNumarasi");
            return View();
        }

        public ActionResult Basarisiz()
        {
            return View();
        }
        public ActionResult Basarili()
        {
            return View();
        }


        public bool Kontrol(int hgsNo)
        {
            bool sonuc=servis.hgsKontrol(hgsNo);
            if (sonuc)
            {
               
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}