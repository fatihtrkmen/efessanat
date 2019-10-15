using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class AnasayfaIletisimKismi
    {
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Adres { get; set; }
        public string Numara { get; set; }
        public string Mail { get; set; }
        [DataType(DataType.MultilineText)]
        public string Yazi { get; set; }
    }
}