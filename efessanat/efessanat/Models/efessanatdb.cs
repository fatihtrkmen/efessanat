using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace efessanat.Models
{
    public class efessanatdb:DbContext
    {
        public efessanatdb()
        : base("efessanatdb")
        {
        }
        public virtual DbSet<Users> user { get; set; }
        public virtual DbSet<Slides> Slides { get; set; }
        public virtual DbSet<ReferansYazisis> ReferansYazisis { get; set; }
        public virtual DbSet<AnasayfaHosgeldiniz> AnasayfaHosgeldinizs { get; set; }
        public virtual DbSet<AnasayfaNiteliklers> AnasayfaNiteliklers { get; set; }
        public virtual DbSet<AltFotoTanitimYazisis> AltFotoTanitimYazisis { get; set; }
        public virtual DbSet<AnasayfaIletisimKismi> AnasayfaIletisimKismi { get; set; }
        public virtual DbSet<AltLogoYazisis> AltLogoYazisis { get; set; }
        public virtual DbSet<KurumsalHosgeldinizs> KurumsalHosgeldinizs { get; set; }
        public virtual DbSet<KurumsalEkips> KurumsalEkips { get; set; }
        public virtual DbSet<EfesSanatKimdir> EfesSanatKimdir { get; set; }
        public virtual DbSet<EfesSanatYetenekler> EfesSanatYetenekler { get; set; }
        public virtual DbSet<KurumsalCalisanBilgileri> KurumsalCalisanBilgileri { get; set; }
        public virtual DbSet<EgitimYazisis> EgitimYazisis { get; set; }
        public virtual DbSet<EgitimKategorileris> EgitimKategorileris { get; set; }

    }
}