using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankaOtomasyon.Models;

namespace BankaOtomasyon.Controllers
{
    [LoginController]
    public class HomeController : Controller
    {
        public ActionResult Anasayfa()
        {
            return View();
        }

       
    }
}