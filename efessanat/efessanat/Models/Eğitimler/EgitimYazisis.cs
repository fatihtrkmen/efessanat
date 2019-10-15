using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class EgitimYazisis
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Yazi { get; set; }
    }
}