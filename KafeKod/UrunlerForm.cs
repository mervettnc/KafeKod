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
    public partial class UrunlerForm : Form
    {
        KafeVeri db;
        BindingList<Urun> blUrunler;
        public UrunlerForm(KafeVeri kafeVeri)
        {
            db = kafeVeri;
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false;
            blUrunler = new BindingList<Urun>(db.Urunler);
            dgvUrunler.DataSource = blUrunler;
          

        }

        private void bntEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAdi.Text.Trim();
            if (urunAd=="")
            {
                MessageBox.Show("lütfen ürün adı giriniz");
                return;
            }
            blUrunler.Add(new Urun { UrunAd = urunAd, BirimFiyat = nudBirimFiyat.Value });
            db.Urunler.Sort();
        }

        private void dgvUrunler_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("geçersiz değer girdiniz");
        }

        private void dgvUrunler_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //UrunAd'ı düzenliyorsa
            if (e.ColumnIndex == 0)
            {
                if (e.FormattedValue.ToString().Trim()=="")
                {
                    dgvUrunler.Rows[e.RowIndex].ErrorText = "ürün adı boş geçilemez";
                    e.Cancel = true; 
                }
                else
                {
                    dgvUrunler.Rows[e.RowIndex].ErrorText = "";

                }
            }
        }
    }
}
