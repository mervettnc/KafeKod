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
    public partial class SiparisForm : Form
    {
        private KafeVeri db;
        private Siparis siparis;
        BindingList<SiparisDetay> blSiparisDetaylar;



        public SiparisForm(KafeVeri kafeVeri, Siparis siparis)
        {
            db = kafeVeri;
            this.siparis = siparis;
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar); 
            InitializeComponent();
            MasaNoGuncelle();
            TutarGuncelle();
            cboUrun.DataSource = db.Urunler;
            cboUrun.SelectedItem = null; //ilk önce boş gelsin bir şey yazmasın diye

            dgvSiparisDetay.DataSource = blSiparisDetaylar;

        }

        private void TutarGuncelle()
        {
            lblTutar.Text = siparis.ToplamTutarTL;
        }

        private void MasaNoGuncelle()
        {
            Text = "Masa " + siparis.MasaNo;
            lblMasaNo.Text = siparis.MasaNo.ToString("00");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cboUrun.SelectedItem==null)
            {
                MessageBox.Show("lütfen bir ürün seçiniz!");
                return;
            }
            Urun seciliUrun = (Urun)cboUrun.SelectedItem;
            var sd = new SiparisDetay
            {  
                UrunAd=seciliUrun.UrunAd,
                BirimFiyat=seciliUrun.BirimFiyat,
                Adet=(int)nudAdet.Value
            };
            blSiparisDetaylar.Add(sd);
            //cboUrun.SelectedItem = null;
            nudAdet.Value = 1;
            TutarGuncelle();
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void bntSiparisIptal_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(" Sipariş iptali onaylıyor musunuz? ",
                "Sipariş İptal Onayı ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (dr== DialogResult.Yes)
            {
                 siparis.Durum = SiparisDurum.Iptal;
                siparis.KapanisZamani = DateTime.Now;
                Close();

            }
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(" Ödeme alındıysa masanın hesabı kapatılacaktır.Onaylıyor musunuz? ",
            "Masa Kapatma Onayı ",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.Yes)
            {
                siparis.Durum = SiparisDurum.Odendi;
                siparis.KapanisZamani = DateTime.Now;
                siparis.OdenenTutar = siparis.ToplamTutar();
               
                Close();

            }
        }
    }
}
