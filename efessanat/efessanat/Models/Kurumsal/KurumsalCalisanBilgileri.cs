using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class KurumsalCalisanBilgileri
    {
        [Key]
        public int Id { get; set; }
        public string isim { get; set; }
        public string pozisyon { get; set; }
        public string detay { get; set; }
        public string fb { get; set; }
        public string twitter { get; set; }
        public string gmail { get; set; }
        public string filename { get; set; }
    }
}