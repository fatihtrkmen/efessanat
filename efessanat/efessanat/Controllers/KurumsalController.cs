using efessanat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efessanat.Controllers
{
    public class KurumsalController : Controller
    {
        efessanatdb db = new efessanatdb();
        // GET: Kurumsal
        public ActionResult Index()
        {
            getAnasayfaIletisimKismi();
            getKurumsalReferences();
            getKurumsalEkipUyeleriFotos();
            getEfesSanatYetenekler();
            getEfesSanatKimdir();
            getKurumsalEkipFotos();
            getEkipTanitimYazisi();
            getKurumsalHosgeldinizYazisi();
            return View();
        }

        private void getKurumsalReferences()
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
        }

        private void getKurumsalEkipUyeleriFotos()
        {
            var ekipUyeleri = db.KurumsalCalisanBilgileri.ToList();

            ViewData.Add("KurumsalCalisanBilgileri", ekipUyeleri);
        }

        private void getEfesSanatYetenekler()
        {
            var EfesSanatYetenekler = db.EfesSanatYetenekler.ToList();
            ViewData.Add("EfesSanatYetenekler", EfesSanatYetenekler);
        }

        private void getEfesSanatKimdir()
        {
            var EfesSanatKimdir = db.EfesSanatKimdir.ToList();
            ViewData.Add("EfesSanatKimdir", EfesSanatKimdir);
        }

        private void getKurumsalEkipFotos()
        {
            string path = Path.Combine(Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipFoto"));
            string[] files = Directory.GetFiles(path);
            List<KurumsalEkipFotos> KurumsalEkipFotos = new List<KurumsalEkipFotos>();
            foreach (string file in files)
            {
                KurumsalEkipFotos reff = new KurumsalEkipFotos();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                KurumsalEkipFotos.Add(reff);
            }
            if (ViewData.ContainsKey("KurumsalEkipFotos"))
                ViewData.Remove("KurumsalEkipFotos");
            ViewData.Add("KurumsalEkipFotos", KurumsalEkipFotos);

        }

        private void getEkipTanitimYazisi()
        {
            KurumsalEkips kurumsalEkips = db.KurumsalEkips.ToList()[0];
            ViewData.Add("KurumsalEkips", kurumsalEkips);
        }

        private void getKurumsalHosgeldinizYazisi()
        {
            KurumsalHosgeldinizs kurumsalHosgeldinizs = db.KurumsalHosgeldinizs.ToList()[0];
            ViewData.Add("KurumsalHosgeldiniz", kurumsalHosgeldinizs);
        }

        private void getAnasayfaIletisimKismi()
        {
            AnasayfaIletisimKismi AnasayfaIletisimKismi = db.AnasayfaIletisimKismi.ToList()[0];
            ViewData.Add("AnasayfaIletisimKismi", AnasayfaIletisimKismi);
        }
    }
}