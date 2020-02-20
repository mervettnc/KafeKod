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

        public event EventHandler<MasaTasimaEventArgs> MasaTasiniyor;

        public SiparisForm(KafeVeri kafeVeri, Siparis siparis)
        {
            db = kafeVeri;
            this.siparis = siparis;
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);
            InitializeComponent();
            MasaNoYukle();
            MasaNoGuncelle();
            TutarGuncelle();

            cboUrun.DataSource = db.Urunler;
            cboUrun.SelectedItem = null; //ilk önce boş gelsin bir şey yazmasın diye

            dgvSiparisDetay.DataSource = blSiparisDetaylar;
            
        }

        private void MasaNoYukle()
        {
            cboMasaNo.Items.Clear();
            for (int i = 1; i <= db.MasaAdet; i++)
            {
                if (!db.AktifSiparisler.Any(x => x.MasaNo == i))
                {
                    cboMasaNo.Items.Add(i);
                }
            }
            cboMasaNo.SelectedIndex = 0;
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
            if (cboUrun.SelectedItem == null)
            {
                MessageBox.Show("lütfen bir ürün seçiniz!");
                return;
            }
            Urun seciliUrun = (Urun)cboUrun.SelectedItem;
            var sd = new SiparisDetay
            {
                UrunAd = seciliUrun.UrunAd,
                BirimFiyat = seciliUrun.BirimFiyat,
                Adet = (int)nudAdet.Value
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

            if (dr == DialogResult.Yes)
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

        private void dgvSiparisDetay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int rowindex = dgvSiparisDetay.HitTest(e.X, e.Y).RowIndex;
                if (rowindex > -1)
                {
                    dgvSiparisDetay.ClearSelection();
                    dgvSiparisDetay.Rows[rowindex].Selected = true;
                    cmsSiparisDetay.Show(MousePosition); //ya da Cursor.Position kullanılır
                }

            }
        }

        private void tsmiSiparirisSil_Click(object sender, EventArgs e)
        {
            //seçili elemanı kaldır
            if (dgvSiparisDetay.SelectedRows.Count > 0)
            {
                var seciliSatir = dgvSiparisDetay.SelectedRows[0];
                var sipDetay = (SiparisDetay)seciliSatir.DataBoundItem;
                blSiparisDetaylar.Remove(sipDetay);
            }
            TutarGuncelle();
        }

        private void bntMasaTasi_Click(object sender, EventArgs e)
        {
            if (cboMasaNo.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir masa no seçiniz");
                return;
            }
            int eskiMasaNo = siparis.MasaNo;
            int hedefMasaNo = (int)cboMasaNo.SelectedItem;
            if (MasaTasiniyor != null)
            {
                var args = new MasaTasimaEventArgs
                {
                    TasinanSiparis = siparis,
                    EskiMasaNo = eskiMasaNo,
                    YeniMasaNo = hedefMasaNo
                };

                MasaTasiniyor(this, args);
            }
            siparis.MasaNo = hedefMasaNo;
            MasaNoGuncelle();
            MasaNoYukle();

          
        }
    }
    public class MasaTasimaEventArgs : EventArgs
    {
        public Siparis TasinanSiparis { get; set; }
        public int EskiMasaNo { get; set; }
        public int YeniMasaNo { get; set; }
    }
}
