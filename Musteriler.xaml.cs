using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Sirket
{
    /// <summary>
    /// Musteriler.xaml etkileşim mantığı
    /// </summary>
    public partial class Musteriler : Window
    {
        SirketEntities sirket = new SirketEntities();
       
      
        public Musteriler()
        {
            InitializeComponent();
       
        }
        public void MusteriListele()
        {
            dgMusteriler.ItemsSource = sirket.Musteriler.ToList();
        }
        public void Temizle()
        {
            txtAdi.Text = txtAldigiHizmet.Text = txtFiyat.Text = dtpTarih.Text= "";
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void dgMusteriler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            DataRowView dataRow = data.SelectedItems as DataRowView;
            if (dataRow != null)
            {
                txtID.Text = dataRow["MusteriID"].ToString();
                txtAdi.Text = dataRow["MusteriAdi"].ToString();
                txtAldigiHizmet.Text = dataRow["AldigiHizmet"].ToString();
                txtFiyat.Text = dataRow["Fiyat"].ToString();
                dtpTarih.Text = dataRow["Aldığı Tarih"].ToString();

            }
        }

      

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            Musteriler musteri = new Musteriler();
            musteri.MusteriAdi = txtAdi.Text;
            musteri.AldigiHizmet = txtAldigiHizmet.Text;
            musteri.Fiyat = int.Parse(txtFiyat.Text);
            musteri.AldigiTarih = dtpTarih.SelectedDate;

            sirket.Musteriler.Add(musteri);
            sirket.SaveChanges();
            MessageBox.Show("Müşteri Bilgileri Kaydoldu.");
            MusteriListele();
            Temizle();
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {

            Musteriler sil = dgMusteriler.SelectedItem as Musteriler;
            sirket.Musteriler.Remove(sirket.Musteriler.First(x => x.MusteriID == sil.MusteriID));
            MessageBox.Show("Musteri Bilgileri Silindi");
            sirket.SaveChanges();
            MusteriListele();
            Temizle();

        /*
            int id = int.Parse(txtID.Text);
            var sil = sirket.Musteriler.Where(x => x.MusteriID == id).FirstOrDefault();
            sirket.Musteriler.Remove(sil);
            MessageBox.Show("Müşteri Bilgileri Silindi");
            MusteriListele();
            Temizle();
        */
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            // int id = int.Parse(txtID.Text);
            //var guncelle = sirket.Musteriler.Where(x => x.MusteriID == id).FirstOrDefault();
            Musteriler guncelle = dgMusteriler.SelectedItem as Musteriler;
            sirket.Musteriler.First(x => x.MusteriID == guncelle.MusteriID);

            guncelle.MusteriAdi = txtAdi.Text;
            guncelle.AldigiHizmet = txtAldigiHizmet.Text;
            guncelle.Fiyat = int.Parse(txtFiyat.Text);
            guncelle.AldigiTarih = dtpTarih.SelectedDate;

            sirket.SaveChanges();
            MessageBox.Show("Musteri Bilgileri Guncellendi.");
            MusteriListele();
            Temizle();
        }

        private void btnListele_Click(object sender, RoutedEventArgs e)
        {
            MusteriListele();
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            Temizle();
        }



        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtID.Text, out int MusteriID))
            {
                var entity = sirket.Musteriler.Find(MusteriID);

                if (entity != null)
                {

                    dgMusteriler.ItemsSource = new[] { entity };
                }
                else
                {
                    MessageBox.Show("Belirtilen ID'ye sahip bir varlık bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir ID değeri girin.");
            }
        }


        private void dgMusteriler_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgMusteriler.SelectedItem is Musteriler musteri)
            {
                txtAdi.Text = musteri.MusteriAdi;
                txtAldigiHizmet.Text = musteri.AldigiHizmet;
                txtFiyat.Text = musteri.Fiyat.ToString();
                dtpTarih.SelectedDate = musteri.AldigiTarih;
            }
        }




    }
}
