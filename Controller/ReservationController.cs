using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainReservation.Classlar;

namespace TrainReservation.Controller
{
    public class ReservationController : ApiController
    {
        public RezervasyonCevap Post([FromBody] RezervasyonIstek rezervasyonIstek)
        {
            RezervasyonCevap rezervasyonCevap = new RezervasyonCevap();
            List<Vagon> rezervasyonAcilabilirVagonlar = new List<Vagon>();
            float toplamBosYer;

            foreach (Vagon vagon in rezervasyonIstek.Tren.Vagonlar)
            {
                vagon.UygunKoltukHesapla();  //Her bir vagona yerleştirilebilecek maksimum kişi sayısını hesapladık
                if(vagon.RezerveEdilebilirKoltukSayisi > 0)
                    rezervasyonAcilabilirVagonlar.Add(vagon);  //eğer rezervasyon yapılabilir koltuk sayısı 0 üzerindeyse rezervasyon oluşturulabilir vagonlara ekle
            }

            toplamBosYer = rezervasyonAcilabilirVagonlar.Sum(x => x.RezerveEdilebilirKoltukSayisi); //elimizde bulunan toplam boş koltuğu hesapladık

            if (toplamBosYer < rezervasyonIstek.RezervasyonYapilacakKisiSayisi)
                return rezervasyonCevap;  //toplam uygun koltuk sayısı istenilenden az ise olumsuz cevap gönderildi

            if (rezervasyonAcilabilirVagonlar.Any(x => x.RezerveEdilebilirKoltukSayisi >= rezervasyonIstek.RezervasyonYapilacakKisiSayisi))
            {
                YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti(
                    rezervasyonAcilabilirVagonlar
                    .Where(x => x.RezerveEdilebilirKoltukSayisi >= rezervasyonIstek.RezervasyonYapilacakKisiSayisi)  //rezervasyon isteğindeki tüm yolcuları alabilecek bir vagon varsa tüm rezervasyonu bu vagona yapıp cevabı gönderiyorum
                    .FirstOrDefault().Ad, rezervasyonIstek.RezervasyonYapilacakKisiSayisi);
                rezervasyonCevap.YerlesimAyrinti.Add(yerlesimAyrinti);
                return rezervasyonCevap;
            }

            if (!rezervasyonIstek.KisilerFarkliVagonlaraYerlestirilebilir)
                return rezervasyonCevap;  //bir üstteki sorgu sonunda program hala çalışıyorsa ve farklı vagon kabul edilmemişse rezervasyon yapılamaz sonucunu döndür

            foreach (var vagon in rezervasyonAcilabilirVagonlar)
            {
                if (rezervasyonIstek.RezervasyonYapilacakKisiSayisi > vagon.RezerveEdilebilirKoltukSayisi)
                {
                    rezervasyonIstek.RezervasyonYapilacakKisiSayisi = rezervasyonIstek.RezervasyonYapilacakKisiSayisi - vagon.RezerveEdilebilirKoltukSayisi;
                    YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti(vagon.Ad, vagon.RezerveEdilebilirKoltukSayisi);  //her iterasyonda rezervasyon istenilen koltuk sayısı ile vagondaki boş koltuk sayısını karşılaştırıp sonuca göre gerekli işlem yapıyorum. Böylece her iterasyon için gerekli koltuk miktarını elde ediyorum
                    rezervasyonCevap.YerlesimAyrinti.Add(yerlesimAyrinti);                                                      
                }
                else
                {
                    YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti(vagon.Ad, rezervasyonIstek.RezervasyonYapilacakKisiSayisi);
                    rezervasyonCevap.YerlesimAyrinti.Add(yerlesimAyrinti);
                    return rezervasyonCevap;
                }
            }
            return rezervasyonCevap;
        }
    }
}
