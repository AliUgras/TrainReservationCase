using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainReservation.Classlar
{
    public class YerlesimAyrinti
    {
        public string VagonAdi { get; set; }
        public float KisiSayisi { get; set; }
        public YerlesimAyrinti(string vagonAdi, float kisiSayisi)
        {
            VagonAdi = vagonAdi;
            KisiSayisi = kisiSayisi;
        }
    }
}