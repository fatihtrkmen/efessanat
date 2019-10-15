using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class EfesSanatYetenekler
    {
        [Key]
        public int Id { get; set; }
        public string Yetenek { get; set; }
        public string Deger { get; set; }
    }
}