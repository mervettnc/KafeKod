using KafeKod.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeKod
{
    public partial class Form1 : Form
    {
        KafeVeri db;  //kafeveri kullanabilmek için referan a sağ tık ile add referans diyip projeye ekleriz.
      

        public Form1()
        {

            VeriOku();
            InitializeComponent();
            MasalariOlustur();
            //MasaBul(5).Text = "senibuldum";
            //MasaBul(6).Text = "pikaçuuuuuuu";

        }

        private void VeriOku()
        {
            try
            {
                string json = File.ReadAllText("veri.json");
                db = JsonConvert.DeserializeObject <KafeVeri>(json);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void OrnekVerileriYukle()
        {
            db.Urunler = new List<Urun>
            {
                new Urun { UrunAd="kola",BirimFiyat=6.99m},
                new Urun{ UrunAd="çay",BirimFiyat=2.50m} 
            };
            db.Urunler.Sort();
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
            for (int i = 1; i <=db.MasaAdet; i++)
            {
                lvi = new ListViewItem("Masa" + i);
                //i değeriyle kayıtlı bir sipariş varmı
                Siparis sip = db.AktifSiparisler.FirstOrDefault(x => x.MasaNo == i);
                if (sip == null)
                {
                    lvi.Tag = i;
                    lvi.ImageKey = "bos";

                }
                else 
                {
                    lvi.Tag = sip;
                    lvi.ImageKey = "dolu";
                }
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
                frmSiparis.MasaTasiniyor += FrmSiparis_Masatasindi;
                frmSiparis.ShowDialog();

                if (sip.Durum == SiparisDurum.Odendi || sip.Durum==SiparisDurum.Iptal)  //masayı temizleme 
                {
                    lvi.Tag = sip.MasaNo;
                    lvi.ImageKey = "bos";
                    db.AktifSiparisler.Remove(sip);
                    db.GecmisSiparisler.Add(sip);

                }

            }
        }

        private void FrmSiparis_Masatasindi(object sender, MasaTasimaEventArgs e)
        {
            //adim1: eski masayi bosalt
            ListViewItem lviEskiMasa = MasaBul(e.EskiMasaNo);
                lviEskiMasa.Tag = e.EskiMasaNo;
                lviEskiMasa.ImageKey = "bos";

            //adim2: yeni masaya siparisi koy
            ListViewItem lviYeniMasa = MasaBul(e.YeniMasaNo);
            lviYeniMasa.Tag = e.TasinanSiparis;
            lviYeniMasa.ImageKey = "dolu";

        }

        private void tsmiGecmisSiparisler_Click(object sender, EventArgs e)
        {
            var frm = new GecmisSiparislerForm(db);
            frm.ShowDialog();
        }

        private void tsmiUrunler_Click(object sender, EventArgs e)
        {
            var frm = new UrunlerForm(db);
            frm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string json = JsonConvert.SerializeObject(db);
            File.WriteAllText("veri.json", json);
            
        }
        private ListViewItem MasaBul(int masaNo)
        {

            foreach (ListViewItem item in lvwMasalar.Items)
            {
                if (item.Tag is int && (int )item.Tag==masaNo)
                {
                    return item;
                }
                else if (item.Tag is Siparis && ((Siparis)item.Tag).MasaNo==masaNo)
                { return item; }
            }
            return null;
        }
    }
}
