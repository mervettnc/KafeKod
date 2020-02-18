using KafeKod.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeKod
{
    public partial class Form1 : Form
    {
        KafeVeri db;  //kafeveri kullanabilmek için referan a sağ tık ile add referans diyip projeye ekleriz.
        int masaAdet = 20;

        public Form1()
        {
            db = new KafeVeri();
            OrnekVerileriYukle();
            InitializeComponent();
            MasalariOlustur();
        }

        private void OrnekVerileriYukle()
        {
            db.Urunler = new List<Urun>
            {
                new Urun { UrunAd="kola",BirimFiyat=6.99m},
                new Urun{ UrunAd="çay",BirimFiyat=2.50m} 
            };
        }

        private void MasalariOlustur()
        {
            #region ListView imajlarının hazırlığı
            ImageList img = new ImageList();
            img.Images.Add("bos", Properties.Resources.bosmasa);
            img.Images.Add("dolu", Properties.Resources.dolumasa);
            img.ImageSize = new Size(64, 64);
            lvwMasalar.LargeImageList = img;

            #endregion

            ListViewItem lvi;
            for (int i = 1; i <=masaAdet; i++)
            {
                lvi = new ListViewItem("Masa" + i);
                lvi.Tag = i;
                lvi.ImageKey = "bos";
                lvwMasalar.Items.Add(lvi);

            }
        }

        private void lvwMasalar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                var lvi = lvwMasalar.SelectedItems[0];
                lvi.ImageKey = "dolu";
                Siparis sip;

                //doluysa olanı almasa, boşsa yeni sipariş oluştur
                if (lvi.Tag is Siparis)  
                {
                   
                    sip = (Siparis)lvi.Tag;

                }
                else  
                {
                    sip = new Siparis();
                    sip.MasaNo = (int)lvi.Tag;
                    sip.AcilisZamani = DateTime.Now;
                    lvi.Tag = sip;
                    db.AktifSiparisler.Add(sip);
                }
                SiparisForm frmSiparis = new SiparisForm(db,sip);
                frmSiparis.ShowDialog();

                if (sip.Durum != SiparisDurum.Aktif)  //masayı temizleme
                {
                    lvi.Tag = sip.MasaNo;
                    lvi.ImageKey = "bos";

                }

            }
        }
    }
}
