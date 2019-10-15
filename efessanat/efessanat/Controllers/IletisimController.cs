using efessanat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efessanat.Controllers
{
    public class IletisimController : Controller
    {
        efessanatdb db = new efessanatdb();
        // GET: İletisim
        public ActionResult Index()
        {
            getAnasayfaIletisimKismi();
            return View();
        }

        private void getAnasayfaIletisimKismi()
        {
            AnasayfaIletisimKismi AnasayfaIletisimKismi = db.AnasayfaIletisimKismi.ToList()[0];
            ViewData.Add("AnasayfaIletisimKismi", AnasayfaIletisimKismi);
        }
    }
}