using efessanat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efessanat.Controllers
{
    public class EfesSanatController : Controller
    {
        efessanatdb db = new efessanatdb();
        public ActionResult Index()
        {
            getAnasayfaIletisimKismi();
            getAltFotoLogo();
            getAltFotoTanitimYazisi();
            getAltFoto();
            getYoneticiler();
            getNitelikler();
            getSlides();
            getReferences();
            getAnasayfaHosgeldiniz();
            return View();
        }

        private void getAnasayfaIletisimKismi()
        {
            AnasayfaIletisimKismi AnasayfaIletisimKismi = db.AnasayfaIletisimKismi.ToList()[0];
            ViewData.Add("AnasayfaIletisimKismi", AnasayfaIletisimKismi);
        }

        private void getAltFotoLogo()
        {
            string path = Path.Combine(Server.MapPath("~/AltFotoLogo/"));
            string[] files = Directory.GetDirectories(path);
            List<AltFotoLogo> AltFotoLogo = new List<AltFotoLogo>();
            foreach (string file in files)
            {
                AltFotoLogo reff = new AltFotoLogo();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                AltFotoLogo.Add(reff);
            }
            if (ViewData.ContainsKey("AltFotoLogo"))
                ViewData.Remove("AltFotoLogo");
            if (AltFotoLogo.Count > 0)
                ViewData.Add("AltFotoLogo", AltFotoLogo);
        }

        private void getAltFoto()
        {
            string path = Path.Combine(Server.MapPath("~/AltFoto/"));
            string[] files = Directory.GetDirectories(path);
            List<AltFoto> altfoto = new List<AltFoto>();
            foreach (string file in files)
            {
                AltFoto reff = new AltFoto();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                altfoto.Add(reff);
            }
            if (ViewData.ContainsKey("altfoto"))
                ViewData.Remove("altfoto");
            ViewData.Add("altfoto", altfoto);
        }

        private void getAltFotoTanitimYazisi()
        {
            AltFotoTanitimYazisis altFotoTanitimYazisi = db.AltFotoTanitimYazisis.ToList()[0];
            ViewData.Add("altFotoTanitimYazisi", altFotoTanitimYazisi);
        }

        private void getYoneticiler()
        {
            string path = Path.Combine(Server.MapPath("~/Yoneticiler/"));
            string[] files = Directory.GetDirectories(path);
            List<Yoneticiler> yoneticiler = new List<Yoneticiler>();
            foreach (string file in files)
            {
                Yoneticiler reff = new Yoneticiler();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                yoneticiler.Add(reff);
            }
            if (ViewData.ContainsKey("yoneticiler"))
                ViewData.Remove("yoneticiler");
            ViewData.Add("yoneticiler", yoneticiler);
        }

        private void getNitelikler()
        {
            var nitelikler = db.AnasayfaNiteliklers.ToList();
            ViewData.Add("nitelikler",nitelikler);
        }

        private void getAnasayfaHosgeldiniz()
        {
            AnasayfaHosgeldiniz anasayfaHosgeldiniz = db.AnasayfaHosgeldinizs.ToList()[0];
            ViewData.Add("anasayfaHosgeldiniz", anasayfaHosgeldiniz);
        }

        private void getReferences()
        {
            string path = Path.Combine(Server.MapPath("~/References/"));
            string[] files = Directory.GetDirectories(path);
            List<References> references = new List<References>();
            foreach (string file in files)
            {
                References reff = new References();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                references.Add(reff);
            }
            if (ViewData.ContainsKey("references"))
                ViewData.Remove("references");
            ViewData.Add("references", references);

            ReferansYazisis refYazisi = db.ReferansYazisis.ToList()[0];
            if (ViewData.ContainsKey("refYazisi"))
                ViewData.Remove("refYazisi");
            ViewData.Add("refYazisi", refYazisi);
        }

        private void getSlides()
        {
            List<Slides> slides = db.Slides.ToList();
            ViewData.Add("slaytlar", slides);
        }



    }
}