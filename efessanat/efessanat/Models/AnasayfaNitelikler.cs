using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class AnasayfaNiteliklers
    {
        [Key]
        public int Id { get; set; }
        public string NitelikBaslik { get; set; }
        public string NitelikYazisi { get; set; }
    }
}