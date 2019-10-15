using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class EgitimKategorileris
    {
        [Key]
        public int Id { get; set; }
        public string Yazi { get; set; }
    }
}