using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainReservation.Classlar
{
    public class RezervasyonIstek
    {
        public Tren Tren { get; set; }
        public float RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}