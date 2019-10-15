using efessanat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efessanat.Controllers
{
    public class EgitimlerController : Controller
    {
        efessanatdb db = new efessanatdb();
        // GET: Egitimler
        public ActionResult Index()
        {
            getEgitimYazisi(); 
            getAnasayfaIletisimKismi();
            return View();
        }

        private void getEgitimYazisi()
        {
            EgitimYazisis EgitimYazisis = db.EgitimYazisis.ToList()[0];
            ViewData.Add("EgitimYazisis", EgitimYazisis);
        }

        private void getAnasayfaIletisimKismi()
        {
            AnasayfaIletisimKismi AnasayfaIletisimKismi = db.AnasayfaIletisimKismi.ToList()[0];
            ViewData.Add("AnasayfaIletisimKismi", AnasayfaIletisimKismi);
        }
    }
}