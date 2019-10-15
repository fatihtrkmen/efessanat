using efessanat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace efessanat.Controllers
{
    public class PanelController : Controller
    {
        efessanatdb db = new efessanatdb();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            getSlides();
            return View();
        }
        private void getSlides()
        {
            List<Slides> slides = db.Slides.ToList();
            ViewData.Add("slides", slides);
        }
        public ActionResult SlaytEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SlaytEkle_(FormCollection _fc)
        {
            Slides _slide = new Slides();
            string type = String.Empty;
            string selectedRadio = _fc["hidden"];
            if (selectedRadio=="1")
            {
                type = "1"; 
                if (String.IsNullOrEmpty(_fc["slaytadi"])  || String.IsNullOrEmpty(_fc["youtubeLinki"]) || String.IsNullOrEmpty(_fc["metin1Text"]) || String.IsNullOrEmpty(_fc["metin2Text"]) || String.IsNullOrEmpty(_fc["metin3Text"]) || String.IsNullOrEmpty(_fc["metin4Text"]) || String.IsNullOrEmpty(_fc["metin5Text"]) || String.IsNullOrEmpty(_fc["metin6Text"]) || String.IsNullOrEmpty(_fc["metin7Text"]) || !r1uploadercheck())
                {
                    _slide.Name = _fc["slaytadi"];
                    _slide.ButtonText = _fc["ButtonText"];
                    _slide.Frame = _fc["youtubeLinki"];
                    _slide.Text1 = _fc["metin1Text"];
                    _slide.Text2 = _fc["metin2Text"];
                    _slide.Text3 = _fc["metin3Text"];
                    _slide.Text4 = _fc["metin4Text"];
                    _slide.Text5 = _fc["metin5Text"];
                    _slide.Text6 = _fc["metin6Text"];
                    _slide.Text7 = _fc["metin7Text"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("SlaytEkle", _slide);

                }
            }
            else if (selectedRadio == "2")
            {
                type = "2";
                if (String.IsNullOrEmpty(_fc["slaytadi"]) || String.IsNullOrEmpty(_fc["ButtonText"]) || String.IsNullOrEmpty(_fc["metin1Text"]) || String.IsNullOrEmpty(_fc["metin2Text"]) || !r2uploadercheck())
                {
                    _slide.Name = _fc["slaytadi"];
                    _slide.ButtonText = _fc["ButtonText"];
                    _slide.Text1 = _fc["metin1Text"];
                    _slide.Text2 = _fc["metin2Text"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("SlaytEkle", _slide);

                }

            }
            else
            {
                type = "3";
                if (String.IsNullOrEmpty(_fc["slaytadi"]) || String.IsNullOrEmpty(_fc["metin1Text"]) || String.IsNullOrEmpty(_fc["metin2Text"]) || String.IsNullOrEmpty(_fc["metin3Text"]) || String.IsNullOrEmpty(_fc["metin4Text"]) || String.IsNullOrEmpty(_fc["metin5Text"]) || String.IsNullOrEmpty(_fc["metin6Text"]) || String.IsNullOrEmpty(_fc["metin7Text"]) || !r3uploadercheck())
                {
                    _slide.Name = _fc["slaytadi"];
                    _slide.Text1 = _fc["metin1Text"];
                    _slide.Text2 = _fc["metin2Text"];
                    _slide.Text3 = _fc["metin3Text"];
                    _slide.Text4 = _fc["metin4Text"];
                    _slide.Text5 = _fc["metin5Text"];
                    _slide.Text6 = _fc["metin6Text"];
                    _slide.Text7 = _fc["metin7Text"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("SlaytEkle", _slide);

                }

            }  
                

                string slaytadi = _fc["slaytadi"];
                efessanatdb db = new efessanatdb();
                var slides = db.Slides.Where(s => s.Name == slaytadi).ToList();
                
                if (slides.Count() == 0)
                {
                    string buttontext = _fc["ButtonText"];
                    string metin1Text = _fc["metin1Text"];
                    string metin2Text = _fc["metin2Text"];
                    string metin3Text = _fc["metin3Text"];
                    string metin4Text = _fc["metin4Text"];
                    string metin5Text = _fc["metin5Text"];
                    string metin6Text = _fc["metin6Text"];
                    string metin7Text = _fc["metin7Text"];
                    string youtubeLink = _fc["youtubeLinki"];

                    youtubeLink = youtubeLink.Replace("watch?v=", "embed/");

                slaytadi = correctSlaytName(slaytadi);

                string path = Server.MapPath("~/Slides/" + slaytadi + "/");
                    Directory.CreateDirectory(path);
                    path = Server.MapPath("~/Slides/" + slaytadi + "/arkaplan");
                    Directory.CreateDirectory(path);

                    _slide.Type = type;
                _slide.Name = slaytadi;
                    _slide.ButtonText = buttontext;
                    _slide.Text1 = metin1Text;
                    _slide.Text2 = metin2Text;
                    _slide.Text3 = metin3Text;
                    _slide.Text4 = metin4Text;
                    _slide.Text5 = metin5Text;
                    _slide.Text6 = metin6Text;
                    _slide.Text7 = metin7Text;
                    _slide.Frame = youtubeLink;

                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                        db.Database.Connection.Open();

                    db.Entry(_slide).State = EntityState.Added;
                    try
                    {
                        db.SaveChanges();
                        ModelState.AddModelError("", "Yeni Slayt Eklendi.");
                    }
                    catch (Exception ex) { }


                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength != 0)
                        {

                            if (hpf != null && hpf.ContentLength > 0)
                            {

                                if (file == "input-file-preview1")
                                {
                                    path = Server.MapPath("~/Slides/" + slaytadi + "/arkaplan");
                                    Directory.CreateDirectory(path);
                                    hpf.SaveAs(Path.Combine(path, "arkaplan."+hpf.FileName.Split('.')[1]));
                                }
                                //else if (file == "input-file-preview2")
                                //{
                                //    path = Server.MapPath("~/Slides/" + slaytadi + "/frame");
                                //    Directory.CreateDirectory(path);
                                //    hpf.SaveAs(Path.Combine(path, hpf.FileName));
                                //}
                                else if (file == "input-file-preview3")
                                {
                                    path = Server.MapPath("~/Slides/" + slaytadi + "/foto_1");
                                    Directory.CreateDirectory(path);
                                    hpf.SaveAs(Path.Combine(path, "foto_1.jpg"));
                                }
                                else
                                {
                                    path = Server.MapPath("~/Slides/" + slaytadi + "/foto_2");
                                    Directory.CreateDirectory(path);
                                    hpf.SaveAs(Path.Combine(path, "foto_2.jpg"));
                                }

                            }

                        }
                    }



                }
                else
                {
                    ModelState.AddModelError("", "Slayt adı kullanımda.");
                }


            return View("SlaytEkle", _slide);
        }

        private string correctSlaytName(string slaytadi)
        {
            do
            {
                slaytadi = slaytadi.Replace(" ", "");
            }
            while (slaytadi.Contains(" "));
            return slaytadi;
        }

        public bool r1uploadercheck()
        {
            bool res = true;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {

                    if (hpf == null && hpf.ContentLength == 0)
                    {

                        if (file == "input-file-preview1")
                        {
                            res = false;
                        }
                    }
                }
                else
                {
                    if (file == "input-file-preview1")
                    {
                        res = false;
                    }
                }
            }
            return res;
        }
        public bool r2uploadercheck()
        {
            bool res = true;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {

                    if (hpf == null && hpf.ContentLength == 0)
                    {

                        if (file == "input-file-preview1")
                        {
                            res = false;
                        }
                    }
                }
                else
                {
                    if (file == "input-file-preview1")
                    {
                        res = false;
                    }
                }
            }
            return res;
        }
        public bool r3uploadercheck()
        {
            bool res = true;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {

                    if (hpf == null && hpf.ContentLength == 0)
                    {

                        if (file == "input-file-preview3" || file == "input-file-preview4")
                        {
                            res = false;
                        }
                    }
                }
                else
                {
                    if (file == "input-file-preview3" || file == "input-file-preview4")
                    {
                        res = false;
                    }
                }
            }
            return res;
        }


        public ActionResult SlaytDetaylari(string _id)
       {
            int id = Convert.ToInt32(_id);
            Slides temp = db.Slides
                         .Where(u => u.Id == id).ToList()[0];
            return View(temp);
        }

        [HttpPost]
        public ActionResult SlaytDuzenle(FormCollection _fc)
        {
            Slides _slide = new Slides();
            string type = String.Empty;
            string selectedRadio = _fc["hidden"];
            if (selectedRadio == "1")
            {
                type = "1";
                if (String.IsNullOrEmpty(_fc["slaytadi"]) || String.IsNullOrEmpty(_fc["metin1Text"]) || String.IsNullOrEmpty(_fc["metin2Text"]) || String.IsNullOrEmpty(_fc["metin3Text"]) || String.IsNullOrEmpty(_fc["metin4Text"]) || String.IsNullOrEmpty(_fc["metin5Text"]) || String.IsNullOrEmpty(_fc["metin6Text"]) || String.IsNullOrEmpty(_fc["metin7Text"]) || !r1uploadercheck())
                {
                    _slide.Name = _fc["slaytadi"];
                    _slide.ButtonText = _fc["ButtonText"];
                    _slide.Text1 = _fc["metin1Text"];
                    _slide.Text2 = _fc["metin2Text"];
                    _slide.Text3 = _fc["metin3Text"];
                    _slide.Text4 = _fc["metin4Text"];
                    _slide.Text5 = _fc["metin5Text"];
                    _slide.Text6 = _fc["metin6Text"];
                    _slide.Text7 = _fc["metin7Text"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("SlaytEkle", _slide);

                }
            }
            else if (selectedRadio == "2")
            {
                type = "2";
                if (String.IsNullOrEmpty(_fc["slaytadi"]) || String.IsNullOrEmpty(_fc["ButtonText"]) || String.IsNullOrEmpty(_fc["metin1Text"]) || String.IsNullOrEmpty(_fc["metin2Text"]) || !r2uploadercheck())
                {
                    _slide.Name = _fc["slaytadi"];
                    _slide.ButtonText = _fc["ButtonText"];
                    _slide.Text1 = _fc["metin1Text"];
                    _slide.Text2 = _fc["metin2Text"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("SlaytEkle", _slide);

                }

            }
            else
            {
                type = "3";
                if (String.IsNullOrEmpty(_fc["slaytadi"]) || String.IsNullOrEmpty(_fc["metin1Text"]) || String.IsNullOrEmpty(_fc["metin2Text"]) || String.IsNullOrEmpty(_fc["metin3Text"]) || String.IsNullOrEmpty(_fc["metin4Text"]) || String.IsNullOrEmpty(_fc["metin5Text"]) || String.IsNullOrEmpty(_fc["metin6Text"]) || String.IsNullOrEmpty(_fc["metin7Text"]) || !r3uploadercheck())
                {
                    _slide.Name = _fc["slaytadi"];
                    _slide.Text1 = _fc["metin1Text"];
                    _slide.Text2 = _fc["metin2Text"];
                    _slide.Text3 = _fc["metin3Text"];
                    _slide.Text4 = _fc["metin4Text"];
                    _slide.Text5 = _fc["metin5Text"];
                    _slide.Text6 = _fc["metin6Text"];
                    _slide.Text7 = _fc["metin7Text"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("SlaytEkle", _slide);

                }

            }


            string slaytadi = _fc["slaytadi"];
            efessanatdb db = new efessanatdb();
            var slides = db.Slides.Where(s => s.Name == slaytadi).ToList();

            if (slides.Count() == 0)
            {
                string buttontext = _fc["ButtonText"];
                string metin1Text = _fc["metin1Text"];
                string metin2Text = _fc["metin2Text"];
                string metin3Text = _fc["metin3Text"];
                string metin4Text = _fc["metin4Text"];
                string metin5Text = _fc["metin5Text"];
                string metin6Text = _fc["metin6Text"];
                string metin7Text = _fc["metin7Text"];

                string path = Server.MapPath("~/Slides/" + slaytadi + "/");
                Directory.CreateDirectory(path);
                path = Server.MapPath("~/Slides/" + slaytadi + "/arkaplan");
                Directory.CreateDirectory(path);

                _slide.Type = type;
                _slide.Name = slaytadi;
                _slide.ButtonText = buttontext;
                _slide.Text1 = metin1Text;
                _slide.Text2 = metin2Text;
                _slide.Text3 = metin3Text;
                _slide.Text4 = metin4Text;
                _slide.Text5 = metin5Text;
                _slide.Text6 = metin6Text;
                _slide.Text7 = metin7Text;

                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.Entry(_slide).State = EntityState.Added;
                try
                {
                    db.SaveChanges();
                    ModelState.AddModelError("", "Yeni Slayt Eklendi.");
                }
                catch (Exception ex) { }


                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                path = Server.MapPath("~/Slides/" + slaytadi + "/arkaplan");
                                Directory.CreateDirectory(path);
                                hpf.SaveAs(Path.Combine(path, hpf.FileName));
                            }
                            else if (file == "input-file-preview2")
                            {
                                path = Server.MapPath("~/Slides/" + slaytadi + "/frame");
                                Directory.CreateDirectory(path);
                                hpf.SaveAs(Path.Combine(path, hpf.FileName));
                            }
                            else if (file == "input-file-preview3")
                            {
                                path = Server.MapPath("~/Slides/" + slaytadi + "/foto_1");
                                Directory.CreateDirectory(path);
                                hpf.SaveAs(Path.Combine(path, hpf.FileName));
                            }
                            else
                            {
                                path = Server.MapPath("~/Slides/" + slaytadi + "/foto_2");
                                Directory.CreateDirectory(path);
                                hpf.SaveAs(Path.Combine(path, hpf.FileName));
                            }

                        }

                    }
                }



            }
            else
            {
                ModelState.AddModelError("", "Slayt adı kullanımda.");
            }


            return View("SlaytEkle", _slide);
        }
        
        public ActionResult SlaytSil(string _slaytid)
        {
            int slaytid = Convert.ToInt32(_slaytid);
            Slides secilenSlayt = db.Slides
                         .Where(u => u.Id == slaytid).ToList()[0];
            try
            {
                db.Slides.Remove(secilenSlayt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            string path = Path.Combine(Server.MapPath("~/Content/Photos/" + _slaytid));
            string destinationDirectory = Path.Combine(Server.MapPath("~/DeletedDirectories/" + _slaytid));
            try
            {
                Directory.Move(path, destinationDirectory);
            }
            catch (Exception ex) { }


            if (secilenSlayt.Name == "DevamEdiyor")
            {
                string pathtobedeleted = Path.Combine(Server.MapPath("~/Views/DevamEdenProjeler/" + secilenSlayt.Name + ".cshtml"));
                System.IO.File.Delete(pathtobedeleted);
            }
            else
            {
                string pathtobedeleted = Path.Combine(Server.MapPath("~/Views/BitenProjeler/" + secilenSlayt.Name + ".cshtml"));
                System.IO.File.Delete(pathtobedeleted);
            }


            return Redirect("/nurzenadmin");
        }
        public ActionResult DeletePhoto(string _id, string _photo)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/Content/Photos/" + _id + "/" + _photo));
                System.IO.File.Delete(path);
            }
            catch (Exception ex)
            {
                string dbconfig = System.Web.HttpContext.Current.Server.MapPath("~/Log/" + "Log.txt");
                System.IO.File.AppendAllText(dbconfig, ex.ToString());
            }

            return Redirect("/nurzenadmin/ProjeDuzenle?_id=" + _id);
        }
        public ActionResult Referanslar()
        {
            getReferencces();
            return View();
        }
        private void getReferencces()
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
        public ActionResult AddReference()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddReference_(FormCollection _fc)
        {
            References _reference = new References();
            string referenceadi = _fc["referenceadi"];
            _reference.Name = referenceadi;
            string guid = Guid.NewGuid().ToString();
            string path = Path.Combine(Server.MapPath("~/References/"+ guid + "/"));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                string fileDir = Path.Combine(path, "logo." + hpf.FileName.Split('.')[1]);
                                hpf.SaveAs(fileDir);
                                using (var image = Image.FromFile(fileDir))
                                {
                                    if (image.Size.Width > 137 || image.Size.Height > 77)
                                    {
                                        var newImage = ScaleImage(image, 137, 77);

                                        if (hpf.FileName.Split('.')[1] == "png")
                                            newImage.Save(fileDir.Replace("logo", "logo1"), ImageFormat.Png);
                                        else if (hpf.FileName.Split('.')[1] == "jpg")
                                            newImage.Save(fileDir.Replace("logo", "logo1"), ImageFormat.Jpeg);

                                    }
                                    }
                                if (System.IO.File.Exists(fileDir.Replace("logo", "logo1")))
                                {
                                    System.IO.File.Delete(fileDir);
                                    System.IO.File.Move(fileDir.Replace("logo", "logo1"), fileDir);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen logo seçiniz.");
                        }

                    }
                }



            }
            else
            {
                ModelState.AddModelError("", "Referans adı kullanımda.");
            }


            return Redirect("/panel/Referanslar");
        }
        public ActionResult DeleteReference(string _refid)
        {
            string path = Path.Combine(Server.MapPath("~/References/"+ _refid + "/"));

            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
            else
            {
                ModelState.AddModelError("", "Referans bulunamadi.");
            }

            return Redirect("/panel/Referanslar");
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public ActionResult ReferansYazisi()
        {
            ReferansYazisis referans = db.ReferansYazisis.ToList()[0];
            return View(referans);
        }

        [ValidateInput(false)]
        public ActionResult referansYazisiGuncelle(ReferansYazisis _referans)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Entry(_referans).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Referans yazisi güncellendi!");
                db.Entry(_referans).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return View("ReferansYazisi", _referans);
        }

        public ActionResult KurumsalHosgeldiniz()
        {
            KurumsalHosgeldinizs hosgeldiniz = db.KurumsalHosgeldinizs.ToList()[0];
            return View(hosgeldiniz);
        }

        public ActionResult KurumsalEkipFotosx(string _id)
        {
            string path = Path.Combine(Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipFoto/"+_id));

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else
            {
                ModelState.AddModelError("", "Yoneticiler bulunamadi.");
            }
            return Redirect("KurumsalEkipFoto");
        }

        public ActionResult KurumsalEkipFotosEkle()
        {
            return View();
        }

        public ActionResult KurumsalEkipFotosEkle_(FormCollection _fc)
        {
            string newFileName = String.Empty;
            
            //if (!System.IO.File.Exists(path))
            //{
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                        //input - file - preview1
                        string extension = hpf.FileName.Split('.')[1];
                        string fileName= hpf.FileName.Split('.')[0];
                        string guid = Guid.NewGuid().ToString();
                        string path = Path.Combine(Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipFoto/" + guid+"."+extension));

                        if (file == "input-file-preview1")
                            {
                                hpf.SaveAs(path);
                                using (var image = Image.FromFile(path))
                                {
                                    if (image.Size.Width != 555 || image.Size.Height != 311)
                                    {
                                        var newImage = ScaleImage(image, 555, 311);

                                    if (hpf.FileName.Split('.')[1] == "png")
                                    {
                                        newImage.Save(path.Replace(guid, guid + "1"), ImageFormat.Png);
                                    }
                                    else if (hpf.FileName.Split('.')[1] == "jpg")
                                    {
                                        newImage.Save(path.Replace(guid, guid + "1"), ImageFormat.Jpeg);

                                    }

                                    }
                                }
                                if (System.IO.File.Exists(path.Replace(guid, guid + "1")))
                                {
                                    System.IO.File.Delete(path);
                                    System.IO.File.Move(path.Replace(guid, guid + "1"), path);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen yonetici seçiniz.");
                        }

                    }
                }



            //}
            //else
            //{
            //    ModelState.AddModelError("", "Referans adı kullanımda.");
            //}


            return Redirect("KurumsalEkipFoto");
        }

        public ActionResult KurumsalCalisanBilgileri()
        {
            var KurumsalCalisanBilgileri = db.KurumsalCalisanBilgileri.ToList();
            ViewData.Add("KurumsalCalisanBilgileri", KurumsalCalisanBilgileri);
            return View();
        }
        public ActionResult KurumsalCalisanBilgileriEkle()
        {
            return View();
        }
        public ActionResult KurumsalCalisanBilgileriEkle_(FormCollection _fc)
        {
            KurumsalCalisanBilgileri _kurumsalCalisanBilgileri = new KurumsalCalisanBilgileri();
            if (String.IsNullOrEmpty(_fc["isim"]) || String.IsNullOrEmpty(_fc["pozisyon"]) || String.IsNullOrEmpty(_fc["detay"]) || String.IsNullOrEmpty(_fc["fb"]) || String.IsNullOrEmpty(_fc["twitter"]) || String.IsNullOrEmpty(_fc["gmail"]) || !r1uploadercheck())
            {
                ModelState.AddModelError("", "Tüm alanları doldurun.");
                return View("KurumsalCalisanBilgileriEkle", _kurumsalCalisanBilgileri);
            }

            string calisanadi = _fc["isim"];
            var calisanlar = db.KurumsalCalisanBilgileri.Where(s => s.isim == calisanadi).ToList();
            string isim = _fc["isim"];
            string pozisyon = _fc["pozisyon"];
            string detay = _fc["detay"];
            string fb = _fc["fb"];
            string twitter = _fc["twitter"];
            string gmail = _fc["gmail"];
            if (calisanlar.Count() == 0)
            {


                _kurumsalCalisanBilgileri.isim = isim;
                _kurumsalCalisanBilgileri.pozisyon = pozisyon;
                _kurumsalCalisanBilgileri.detay = detay;
                _kurumsalCalisanBilgileri.fb = fb;
                _kurumsalCalisanBilgileri.twitter = twitter;
                _kurumsalCalisanBilgileri.gmail = gmail;



                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.Entry(_kurumsalCalisanBilgileri).State = EntityState.Added;
                try
                {
                    db.SaveChanges();
                    db.Entry(_kurumsalCalisanBilgileri).GetDatabaseValues();
                    ModelState.AddModelError("", "Çalışan bilgisi eklendi.");
                }
                catch (Exception ex) { }
            }
            else
            {
                ModelState.AddModelError("", "Çalışan zaten mevcut");
            }

            if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                db.Database.Connection.Close();


            foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            //input - file - preview1
                            string extension = hpf.FileName.Split('.')[1];
                            string fileName = hpf.FileName.Split('.')[0];
                            string guid = isim;
                            string path = Path.Combine(Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipUyeleri/" + _kurumsalCalisanBilgileri.Id+"/"+ guid + "." + extension));
                             Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipUyeleri/" + _kurumsalCalisanBilgileri.Id)));
                            if (file == "input-file-preview3")
                            {
                                hpf.SaveAs(path);
                                using (var image = Image.FromFile(path))
                                {
                                    if (image.Size.Width != 320 || image.Size.Height != 417)
                                    {
                                        var newImage = ScaleImage(image, 320, 417);

                                        if (hpf.FileName.Split('.')[1].ToLower() == "png")
                                        {
                                            newImage.Save(path.Replace(guid, guid + "1"), ImageFormat.Png);
                                        }
                                        else if (hpf.FileName.Split('.')[1].ToLower() == "jpg")
                                        {
                                            newImage.Save(path.Replace(guid, guid + "1"), ImageFormat.Jpeg);

                                        }

                                    }
                                }
                                if (System.IO.File.Exists(path.Replace(guid, guid + "1")))
                                {
                                    System.IO.File.Delete(path);
                                    System.IO.File.Move(path.Replace(guid, guid + "1"), path);
                                }

                            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                                db.Database.Connection.Open();

                            _kurumsalCalisanBilgileri.filename = isim+"."+extension;
                            db.Entry(_kurumsalCalisanBilgileri).State = EntityState.Modified;
                            try
                            {
                                db.SaveChanges();
                                db.Entry(_kurumsalCalisanBilgileri).GetDatabaseValues();
                            }
                            catch (Exception ex) { }

                            if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                                db.Database.Connection.Close();


                        }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen yonetici seçiniz.");
                        }

                    }
                }





            //return View("SlaytEkle", _slide);
            return View("KurumsalCalisanBilgileriEkle", _kurumsalCalisanBilgileri);
        }

        public ActionResult KurumsalCalisanBilgileriSil(string _id)
        {
            int id = Convert.ToInt32(_id);
            string path = Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipUyeleri/" + _id + "/");

            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }


            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            KurumsalCalisanBilgileri calisan = db.KurumsalCalisanBilgileri.Where(c => c.Id == id).ToList()[0];
            db.Entry(calisan).State = EntityState.Deleted;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Çalışan bilgisi eklendi.");
            }
            catch (Exception ex) { }

            return Redirect("KurumsalCalisanBilgileri");
        }

        public ActionResult AnasayfaHosgeldiniz()
        {
            AnasayfaHosgeldiniz referans = db.AnasayfaHosgeldinizs.ToList()[0];
            return View(referans);
        }

        [ValidateInput(false)]
        public ActionResult anasayfahosgeldinizguncelle(AnasayfaHosgeldiniz _anasayfaHosgeldiniz)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Entry(_anasayfaHosgeldiniz).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Hoşgeldiniz yazisi güncellendi!");
                db.Entry(_anasayfaHosgeldiniz).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return View("AnasayfaHosgeldiniz", _anasayfaHosgeldiniz);
        }

        public ActionResult KurumsalEkip()
        {
            KurumsalEkips kurumsalEkips = db.KurumsalEkips.ToList()[0];
            return View(kurumsalEkips);
        }

        public ActionResult KurumsalEkipFoto()
        {
            getKurumsalEkipFoto();
            return View();
        }

        private void getKurumsalEkipFoto()
        {
            string path = Path.Combine(Server.MapPath("~/Assets/Fotograflar/Kurumsal/EkipFoto/"));
            string[] files = Directory.GetFiles(path);
            List<KurumsalEkipFotos> KurumsalEkipFotos = new List<KurumsalEkipFotos>();
            foreach (string file in files)
            {
                KurumsalEkipFotos reff = new KurumsalEkipFotos();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                KurumsalEkipFotos.Add(reff);
            }
            if (ViewData.ContainsKey("KurumsalEkipFoto"))
                ViewData.Remove("KurumsalEkipFoto");
            if(KurumsalEkipFotos.Count>0)
            ViewData.Add("KurumsalEkipFoto", KurumsalEkipFotos);
        }

        [ValidateInput(false)]
        public ActionResult KurumsalEkip_(KurumsalEkips _kurumsalEkips)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Entry(_kurumsalEkips).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Ekip tanıtım güncellendi!");
            }
            catch (Exception ex) { }

            return View("KurumsalEkip", _kurumsalEkips);
        }


        [ValidateInput(false)]
        public ActionResult KurumsalHosgeldiniz_(KurumsalHosgeldinizs _kurumsalHosgeldinizs)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Entry(_kurumsalHosgeldinizs).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Hoşgeldiniz yazisi güncellendi!");
            }
            catch (Exception ex) { }

            return View("KurumsalHosgeldiniz", _kurumsalHosgeldinizs);
        }

        public ActionResult AnasayfaNitelikler()
        {
            var referans = db.AnasayfaNiteliklers.ToList();
            ViewData.Add("AnasayfaNitelikler",referans);
            return View();
        }

        public ActionResult AnasayfaNiteliklerEkle()
        {
            return View();
        }

        public ActionResult AnasayfaNiteliklerEkle_(AnasayfaNiteliklers _anasayfaNiteliklers)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Entry(_anasayfaNiteliklers).State = EntityState.Added;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Nitelik Eklendi!");
                db.Entry(_anasayfaNiteliklers).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return View("AnasayfaNiteliklerEkle", _anasayfaNiteliklers);
        }
        public ActionResult anasayfaniteliklerguncelle(AnasayfaNiteliklers _anasayfaNiteliklers)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();

            db.Entry(_anasayfaNiteliklers).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Hoşgeldiniz yazisi güncellendi!");
                db.Entry(_anasayfaNiteliklers).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return View("AnasayfaHosgeldiniz", _anasayfaNiteliklers);
        }
        public ActionResult AnasayfaNitelikDetaylari(string _id)
        {
            int id = Convert.ToInt32(_id);
            AnasayfaNiteliklers nitelik = db.AnasayfaNiteliklers.Where(u=>u.Id==id).ToList()[0];
            return View(nitelik);
        }
        public ActionResult AnasayfaNitelikDetaylari_(AnasayfaNiteliklers _anasayfaNiteliklers)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_anasayfaNiteliklers).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Nitelik bilgisi güncellendi!");
                db.Entry(_anasayfaNiteliklers).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return View("AnasayfaNitelikDetaylari", _anasayfaNiteliklers);
        }
        public ActionResult AnasayfaNitelikSil(string _id)
        {
            int id = Convert.ToInt32(_id);
            AnasayfaNiteliklers nitelik = db.AnasayfaNiteliklers.Where(u => u.Id == id).ToList()[0];
            try
            {
                db.AnasayfaNiteliklers.Remove(nitelik);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return Redirect("AnasayfaNitelikler");
        }

        public ActionResult Yoneticiler()
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

            return View();
        }
        public ActionResult YoneticiSil(string _name)
        {
            string path = Path.Combine(Server.MapPath("~/Yoneticiler/" + _name + "/"));

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                ModelState.AddModelError("", "Yoneticiler bulunamadi.");
            }
            return Redirect("Yoneticiler");
        }

        public ActionResult AnasayfaYoneticiEkle()
        {
            return View();
        }
        public ActionResult YoneticiEkle_(FormCollection _fc)
        {
            References _yonetici = new References();
            string yoneticiadi = _fc["yoneticiadi"];
            _yonetici.Name = yoneticiadi;
            string guid = Guid.NewGuid().ToString();
            string path = Path.Combine(Server.MapPath("~/Yoneticiler/" + guid + "/"));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                string fileDir = Path.Combine(path, "yonetici." + hpf.FileName.Split('.')[1]);
                                hpf.SaveAs(fileDir);
                                using (var image = Image.FromFile(fileDir))
                                {
                                    if (image.Size.Width != 555 || image.Size.Height != 693)
                                    {
                                        var newImage = ScaleImage(image, 555, 693);

                                        if (hpf.FileName.Split('.')[1] == "png")
                                            newImage.Save(fileDir.Replace("yonetici", "yonetici1"), ImageFormat.Png);
                                        else if (hpf.FileName.Split('.')[1] == "jpg")
                                            newImage.Save(fileDir.Replace("yonetici", "yonetici1"), ImageFormat.Jpeg);

                                    }
                                }
                                if (System.IO.File.Exists(fileDir.Replace("yonetici", "yonetici1")))
                                {
                                    System.IO.File.Delete(fileDir);
                                    System.IO.File.Move(fileDir.Replace("yonetici", "yonetici1"), fileDir);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen yonetici seçiniz.");
                        }

                    }
                }



            }
            else
            {
                ModelState.AddModelError("", "Referans adı kullanımda.");
            }


            return Redirect("Yoneticiler");
        }

    
        public ActionResult AltFotoTanitimYazisi()
        {
            AltFotoTanitimYazisis altFotoTanitimYazisi = db.AltFotoTanitimYazisis.ToList()[0];
            return View(altFotoTanitimYazisi);
        }

        [ValidateInput(false)]
        public ActionResult _AltFotoTanitimYazisiGuncelle(AltFotoTanitimYazisis _altFotoTanitimYazisi)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_altFotoTanitimYazisi).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Tanıtım yazısı güncellendi!");
                db.Entry(_altFotoTanitimYazisi).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return Redirect("AltFotoTanitimYazisi");
        }

        public ActionResult AltFoto()
        {
            getAltFoto();
            return View();
        }

        private void getAltFoto()
        {
            string path = Path.Combine(Server.MapPath("~/AltFoto/"));
            string[] files = Directory.GetDirectories(path);
            List<AltFoto> AltFoto = new List<AltFoto>();
            foreach (string file in files)
            {
                AltFoto reff = new AltFoto();
                reff.Name = file.Remove(0, file.LastIndexOf('\\') + 1);
                AltFoto.Add(reff);
            }
            if (ViewData.ContainsKey("AltFoto"))
                ViewData.Remove("AltFoto");
            ViewData.Add("AltFoto", AltFoto);
        }

        public ActionResult AltFotoEkle()
        {
            return View();
        }



        public ActionResult AltFotoDuzenle(string _id)
        {
            AltFoto altFoto = new AltFoto();
            altFoto.Name = _id;

            return View(altFoto);
        }


        public ActionResult AltFotoDuzenle_(AltFoto _altFoto)
        {
            string guid = _altFoto.Name;
            string path = Path.Combine(Server.MapPath("~/AltFoto/" + guid + "/"));
            AltFoto altfoto = new AltFoto();
            altfoto.Name = guid;

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                string fileDir = Path.Combine(path, "logonew." + hpf.FileName.Split('.')[1]);
                                hpf.SaveAs(fileDir);
                                //using (var image = Image.FromFile(fileDir))
                                //{
                                //    if (image.Size.Width != 1879 || image.Size.Height != 1597)
                                //    {
                                //        var newImage = ScaleImage(image, 1879, 1597);

                                //        if (hpf.FileName.Split('.')[1] == "png")
                                //            newImage.Save(fileDir, ImageFormat.Png);
                                //        else if (hpf.FileName.Split('.')[1] == "jpg")
                                //            newImage.Save(fileDir, ImageFormat.Jpeg);

                                //    }
                                //}
                                if (System.IO.File.Exists(fileDir.Replace("logonew", "logo")))
                                {
                                    System.IO.File.Delete(fileDir.Replace("logonew", "logo"));
                                    System.IO.File.Move(fileDir, fileDir.Replace("logonew", "logo"));
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen logo seçiniz.");
                        }

                    }
                }




            return Redirect("/panel/AltFoto");
        }

        public ActionResult AltFotoLogo()
        {
            getAltFotoLogo();
            return View();
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
            if(AltFotoLogo.Count>0)
            ViewData.Add("AltFotoLogo", AltFotoLogo);
        }

        public ActionResult AltFotoLogoDuzenle(string _id)
        {
            AltFotoLogo altFotoLogo = new AltFotoLogo();
            altFotoLogo.Name = _id;

            return View(altFotoLogo);
        }


        public ActionResult AltFotoLogoDuzenle_(AltFotoLogo _altFotoLogo)
        {
            string guid = _altFotoLogo.Name;
            string path = Path.Combine(Server.MapPath("~/AltFotoLogo/" + guid + "/"));
            AltFotoLogo altfoto = new AltFotoLogo();
            altfoto.Name = guid;

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength != 0)
                {

                    if (hpf != null && hpf.ContentLength > 0)
                    {

                        if (file == "input-file-preview1")
                        {
                            string fileDir = Path.Combine(path, "logonew." + hpf.FileName.Split('.')[1]);
                            hpf.SaveAs(fileDir);
                            //using (var image = Image.FromFile(fileDir))
                            //{
                            //    if (image.Size.Width != 1879 || image.Size.Height != 1597)
                            //    {
                            //        var newImage = ScaleImage(image, 1879, 1597);

                            //        if (hpf.FileName.Split('.')[1] == "png")
                            //            newImage.Save(fileDir, ImageFormat.Png);
                            //        else if (hpf.FileName.Split('.')[1] == "jpg")
                            //            newImage.Save(fileDir, ImageFormat.Jpeg);

                            //    }
                            //}
                            if (System.IO.File.Exists(fileDir.Replace("logonew", "logo")))
                            {
                                System.IO.File.Delete(fileDir.Replace("logonew", "logo"));
                                System.IO.File.Move(fileDir, fileDir.Replace("logonew", "logo"));
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lütfen logo seçiniz.");
                    }

                }
            }




            return Redirect("/panel/AltFotoLogo");
        }

        public ActionResult AltFotoEkle_(FormCollection _fc)
        {
         
            string guid = Guid.NewGuid().ToString();
            string path = Path.Combine(Server.MapPath("~/AltFoto/" + guid + "/"));
            AltFoto altfoto = new AltFoto();
            altfoto.Name = guid;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                string fileDir = Path.Combine(path, "logo." + hpf.FileName.Split('.')[1]);
                                hpf.SaveAs(fileDir);
                                using (var image = Image.FromFile(fileDir))
                                {
                                    if (image.Size.Width !=1879 || image.Size.Height != 1597)
                                    {
                                        var newImage = ScaleImage(image, 1879, 1597);

                                        if (hpf.FileName.Split('.')[1] == "png")
                                            newImage.Save(fileDir.Replace("logo", "logo1"), ImageFormat.Png);
                                        else if (hpf.FileName.Split('.')[1] == "jpg")
                                            newImage.Save(fileDir.Replace("logo", "logo1"), ImageFormat.Jpeg);

                                    }
                                }
                                if (System.IO.File.Exists(fileDir.Replace("logo", "logo1")))
                                {
                                    System.IO.File.Delete(fileDir);
                                    System.IO.File.Move(fileDir.Replace("logo", "logo1"), fileDir);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen logo seçiniz.");
                        }

                    }
                }



            }
            else
            {
                ModelState.AddModelError("", "Referans adı kullanımda.");
            }


            return Redirect("/panel/AltFoto");
        }

        public ActionResult AltFotoLogoEkle()
        {
            return View();
        }

        public ActionResult AltFotoLogoEkle_()
        {

            string guid = Guid.NewGuid().ToString();
            string path = Path.Combine(Server.MapPath("~/AltFotoLogo/" + guid + "/"));
            AltFotoLogo AltFotoLogo = new AltFotoLogo();
            AltFotoLogo.Name = guid;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                string fileDir = Path.Combine(path, "logo." + hpf.FileName.Split('.')[1]);
                                hpf.SaveAs(fileDir);
                                using (var image = Image.FromFile(fileDir))
                                {
                                    if (image.Size.Width != 1879 || image.Size.Height != 1597)
                                    {
                                        var newImage = ScaleImage(image, 1879, 1597);

                                        if (hpf.FileName.Split('.')[1] == "png")
                                            newImage.Save(fileDir.Replace("logo", "logo1"), ImageFormat.Png);
                                        else if (hpf.FileName.Split('.')[1] == "jpg")
                                            newImage.Save(fileDir.Replace("logo", "logo1"), ImageFormat.Jpeg);

                                    }
                                }
                                if (System.IO.File.Exists(fileDir.Replace("logo", "logo1")))
                                {
                                    System.IO.File.Delete(fileDir);
                                    System.IO.File.Move(fileDir.Replace("logo", "logo1"), fileDir);
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Lütfen logo seçiniz.");
                        }

                    }
                }



            }
            else
            {
                ModelState.AddModelError("", "Referans adı kullanımda.");
            }


            return Redirect("/panel/AltFotoLogo");
        }

        public ActionResult AltFotoLogoSil(string _id)
        {
            string path = Path.Combine(Server.MapPath("~/AltFotoLogo/" + _id + "/"));

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            else
            {
                ModelState.AddModelError("", "Yoneticiler bulunamadi.");
            }
            return Redirect("/panel/AltFotoLogo");
        }

        public ActionResult AnasayfaIletisimKismi()
        {
            AnasayfaIletisimKismi anasayfaIletisimKismi = db.AnasayfaIletisimKismi.ToList()[0]; 
            return View(anasayfaIletisimKismi);
        }
        [ValidateInput(false)]
        public ActionResult AnasayfaIletisimKismi_(AnasayfaIletisimKismi _anasayfaIletisimKismi)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_anasayfaIletisimKismi).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Nitelik bilgisi güncellendi!");
                db.Entry(_anasayfaIletisimKismi).GetDatabaseValues();

            }
            catch (Exception ex) { }

            return Redirect("/panel/AnasayfaIletisimKismi");
        }

        public ActionResult AltLogo()
        {
            return View();
        }

        public ActionResult AltLogoEkle()
        {
            return View();
        }
        public ActionResult AltLogoEkle_(FormCollection _fc)
        {
            AltLogoYazisis altLogoYazisis = new AltLogoYazisis();
                if (String.IsNullOrEmpty(_fc["LogoYazisi"]) || !r1uploadercheck())
                {
                altLogoYazisis.Yazi = _fc["LogoYazisi"];
                    ModelState.AddModelError("", "Tüm alanları doldurun.");
                    return View("AltLogoEkle", altLogoYazisis);

                }


            string logoYazisi = _fc["LogoYazisi"];
            efessanatdb db = new efessanatdb();
            var slides = db.Slides.Where(s => s.Name == logoYazisi).ToList();
            string uniqueId = Guid.NewGuid().ToString();
            if (slides.Count() == 0)
            {
                logoYazisi = correctSlaytName(logoYazisi);
                altLogoYazisis.İsim= _fc["İsim"];
                string path = Server.MapPath("~/AltLogo/" + logoYazisi + "/");
                Directory.CreateDirectory(path);
                path = Server.MapPath("~/AltLogo/" + logoYazisi);


                if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                    db.Database.Connection.Open();

                db.Entry(altLogoYazisis).State = EntityState.Added;
                try
                {
                    db.SaveChanges();
                    ModelState.AddModelError("", "Yeni Slayt Eklendi.");
                }
                catch (Exception ex) { }


                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength != 0)
                    {

                        if (hpf != null && hpf.ContentLength > 0)
                        {

                            if (file == "input-file-preview1")
                            {
                                path = Server.MapPath("~/AltLogo/" + altLogoYazisis.İsim);
                                Directory.CreateDirectory(path);
                                hpf.SaveAs(Path.Combine(path, "arkaplan." + hpf.FileName.Split('.')[1]));
                            }
                        }

                    }
                }



            }
            else
            {
                ModelState.AddModelError("", "Slayt adı kullanımda.");
            }


            return View("SlaytEkle", altLogoYazisis);
        }

        public ActionResult EfesSanatKimdir()
        {
            getEfesSanatKimdir();
            return View();
        }

        public ActionResult EfesSanatYetenekler()
        {
            getEfesSanatYetenekler();
            return View();
        }

        private void getEfesSanatYetenekler()
        {
            var EfesSanatYetenekler = db.EfesSanatYetenekler.ToList();

            if (ViewData.ContainsKey("EfesSanatYetenekler"))
                ViewData.Remove("EfesSanatYetenekler");
            ViewData.Add("EfesSanatYetenekler", EfesSanatYetenekler);
        }

        public ActionResult EfesSanatYeteneklerEkle()
        {
            return View();
        }

        public ActionResult EfesSanatYeteneklerEkle_(EfesSanatYetenekler _efesSanatYetenekler)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_efesSanatYetenekler).State = EntityState.Added;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Yetenek Ekledi!");

                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
            catch (Exception ex) { }


            return View("EfesSanatYeteneklerEkle", _efesSanatYetenekler);
        }

        private void getEfesSanatKimdir()
        {
            var EfesSanatKimdir = db.EfesSanatKimdir.ToList();

            if (ViewData.ContainsKey("EfesSanatKimdir"))
                ViewData.Remove("EfesSanatKimdir");
            ViewData.Add("EfesSanatKimdir", EfesSanatKimdir);
        }

        //public ActionResult EfesSanatKimdirDuzenle_(string _id)
        //{

        //}
        public ActionResult EfesSanatYeteneklerDuzenle(string _id)
        {
            int id = Convert.ToInt32(_id);
            EfesSanatYetenekler efesSanatYetenekler = db.EfesSanatYetenekler.Where(u => u.Id == id).ToList()[0];
            return View(efesSanatYetenekler);
        }
        public ActionResult EfesSanatYeteneklerSil(string _id)
        {
            int id = Convert.ToInt32(_id);
            EfesSanatYetenekler efesSanatYetenekler = db.EfesSanatYetenekler.Where(u => u.Id == id).ToList()[0];
            return View(efesSanatYetenekler);
        }
        public ActionResult EfesSanatYeteneklerDuzenle_(EfesSanatYetenekler _efesSanatYetenekler)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_efesSanatYetenekler).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "EfesSanat Kimdir Metni Güncellendi!");

                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
            catch (Exception ex) { }


            return View("EfesSanatYeteneklerDuzenle", _efesSanatYetenekler);
        }
        public ActionResult EfesSanatKimdirDuzenle(string _id)
        {
            int id = Convert.ToInt32(_id);
            EfesSanatKimdir efesSanatKimdir = db.EfesSanatKimdir.Where(u => u.Id == id).ToList()[0];
            return View(efesSanatKimdir);
        }

        public ActionResult EfesSanatKimdirEkle()
        {
            return View();

        }

        public ActionResult EfesSanatKimdirEkle_(EfesSanatKimdir _efesSanatKimdir)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_efesSanatKimdir).State = EntityState.Added;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "EfesSanat Kimdir Metni Eklendi!");

                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
            catch (Exception ex) { }


            return View("EfesSanatKimdirEkle", _efesSanatKimdir);
        }
        public ActionResult EfesSanatKimdirDuzenle_(EfesSanatKimdir _efesSanatKimdir)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_efesSanatKimdir).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "EfesSanat Kimdir Metni Güncellendi!");

                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
            catch (Exception ex) { }


            return View("EfesSanatKimdir", _efesSanatKimdir);
        }
        public ActionResult EfesSanatKimdirSil(string _id)
        {
            int id = Convert.ToInt32(_id);
            EfesSanatKimdir efesSanatKimdir = db.EfesSanatKimdir.Where(u => u.Id == id).ToList()[0];
            try
            {
                db.EfesSanatKimdir.Remove(efesSanatKimdir);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return Redirect("EfesSanatKimdir");
        }
        public ActionResult EgitimlerYazi()
        {
            EgitimYazisis egitimYazisis = db.EgitimYazisis.ToList()[0];
            return View(egitimYazisis);
        }
        [ValidateInput(false)]
        public ActionResult EgitimlerYaziDuzenle(EgitimYazisis _egitimYazisis)
        {
            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_egitimYazisis).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Egitim Metni Güncellendi!");

                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
            catch (Exception ex) { }


            return View("EgitimlerYazi", _egitimYazisis);
        }
        public ActionResult EgitimKategorileri()
        {
            //EgitimKategorileris EgitimKategorileris = db.EgitimKategorileris.ToList()[0];
            //return View(EgitimKategorileris);

            var EgitimKategorileri = db.EgitimKategorileris.ToList();

            if (ViewData.ContainsKey("EgitimKategorileri"))
                ViewData.Remove("EgitimKategorileri");
            ViewData.Add("EgitimKategorileri", EgitimKategorileri);

            return View();
        }
        
        public ActionResult EgitimKategorilerSil(string _slaytid)
        {
            int slaytid = Convert.ToInt32(_slaytid);
            EgitimKategorileris egitimKategorileris = db.EgitimKategorileris
                         .Where(u => u.Id == slaytid).ToList()[0];
            try
            {
                db.EgitimKategorileris.Remove(egitimKategorileris);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return Redirect("/EgitimKategorileri");
        }
        public ActionResult EgitimlerKategoriEkle()
        {
            return View();

        }
        public ActionResult EgitimKategorilerEkle_(FormCollection _fc)
        {

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            string kategori = _fc["kategoriadi"].ToString();
            var kategories = db.EgitimKategorileris.Where(c => c.Yazi == kategori).ToList();

            EgitimKategorileris egitimKategorileris = new EgitimKategorileris();
            if (kategories.Count()==0) {

                egitimKategorileris.Yazi = kategori;

                db.Entry(egitimKategorileris).State = EntityState.Added;
                try
                {
                    db.SaveChanges();
                    ModelState.AddModelError("", "Yeni Kategori Eklendi.");
                }
                catch (Exception ex) { }
            }
            else
            {
                ModelState.AddModelError("", "Kategori zaten mevcut.");
            }

            //foreach (string file in Request.Files)
            //{
            //    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
            //    if (hpf.ContentLength != 0)
            //    {

            //        if (hpf != null && hpf.ContentLength > 0)
            //        {

            //            if (file == "input-file-preview1")
            //            {
            //                path = Server.MapPath("~/Slides/" + slaytadi + "/arkaplan");
            //                Directory.CreateDirectory(path);
            //                hpf.SaveAs(Path.Combine(path, "arkaplan." + hpf.FileName.Split('.')[1]));
            //            }
            //            //else if (file == "input-file-preview2")
            //            //{
            //            //    path = Server.MapPath("~/Slides/" + slaytadi + "/frame");
            //            //    Directory.CreateDirectory(path);
            //            //    hpf.SaveAs(Path.Combine(path, hpf.FileName));
            //            //}
            //            else if (file == "input-file-preview3")
            //            {
            //                path = Server.MapPath("~/Slides/" + slaytadi + "/foto_1");
            //                Directory.CreateDirectory(path);
            //                hpf.SaveAs(Path.Combine(path, "foto_1.jpg"));
            //            }
            //            else
            //            {
            //                path = Server.MapPath("~/Slides/" + slaytadi + "/foto_2");
            //                Directory.CreateDirectory(path);
            //                hpf.SaveAs(Path.Combine(path, "foto_2.jpg"));
            //            }

            //        }

            //    }
            //}



            //}
            //else
            //{
            //    ModelState.AddModelError("", "Slayt adı kullanımda.");
            //}

            //return View();
            return View("EgitimlerKategoriEkle", egitimKategorileris);
        }


        public ActionResult EgitimKategorileriDuzenle(string _id)
        {
            int id = Convert.ToInt32(_id);
            EgitimKategorileris egitimKategorileri = db.EgitimKategorileris
                         .Where(u => u.Id == id).ToList()[0];
            return View(egitimKategorileri);
        }
        public ActionResult EgitimKategorileriDuzenle_(EgitimKategorileris _egitimKategorileris)
        {

            if (db.Database.Connection.State == System.Data.ConnectionState.Closed)
                db.Database.Connection.Open();
            db.Entry(_egitimKategorileris).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                ModelState.AddModelError("", "Egitim Metni Güncellendi!");

                if (db.Database.Connection.State == System.Data.ConnectionState.Open)
                    db.Database.Connection.Close();
            }
            catch (Exception ex) { }


            return View("EgitimKategorileriDuzenle", _egitimKategorileris);

        }
    }
}


