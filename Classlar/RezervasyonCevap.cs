using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainReservation.Classlar
{
    public class RezervasyonCevap
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; }
        public RezervasyonCevap()
        {
            this.RezervasyonYapilabilir = true;

            YerlesimAyrinti = new List<YerlesimAyrinti>();
        }
    }
}