using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainReservation.Classlar
{
    public class Vagon
    {
        public string Ad { get; set; }
        public float Kapasite { get; set; }
        public float DoluKoltukAdet { get; set; }
        public float RezerveEdilebilirKoltukSayisi { get; set; }
        public void UygunKoltukHesapla()
        {
            this.Kapasite = (this.Kapasite / 100) * 70;
            if (Kapasite > DoluKoltukAdet)
                RezerveEdilebilirKoltukSayisi = Kapasite - DoluKoltukAdet;
            else
                RezerveEdilebilirKoltukSayisi = 0;
        }
    }
   
}