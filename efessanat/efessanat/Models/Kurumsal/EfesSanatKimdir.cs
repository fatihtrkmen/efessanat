using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class EfesSanatKimdir
    {
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }

        public string Aciklama { get; set; }
    }
}