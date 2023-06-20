using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using System.Data.SqlClient;
using System.Data.Entity;
using WPF_Sirket.Model;


namespace WPF_Sirket
{
    /// <summary>
    /// Calisanlar.xaml etkileşim mantığı
    /// </summary>
    public partial class Calisanlar : Window
    {
        SirketEntities sirket=new SirketEntities();
        

        public void Temizle()
        {
            txtAdi.Text = txtDepartman.Text = txtProgram.Text = txtTecrube.Text = "";
        }

        public Calisanlar()
        {
            InitializeComponent();
            
        }
        public void CalisanListele() => dgCalisanlar.ItemsSource = sirket.Calisanlar.ToList();
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void dgCalisanlar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data=(DataGrid) sender;
            DataRowView dataRow = data.SelectedItems as DataRowView;
            if (dataRow != null)
            {
                txtID.Text= dataRow["CalisanID"].ToString();
                txtAdi.Text = dataRow["CalisanAdi"].ToString();
                txtDepartman.Text = dataRow["DepartmanAdi"].ToString();
                txtProgram.Text = dataRow["KullandigiProgram"].ToString();
                txtTecrube.Text = dataRow["Tecrübe"].ToString();
            }
        }

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            Calisanlar calisan=new Calisanlar();
            calisan.CalisanAdi = txtAdi.Text;
            calisan.DepartmanAdi=txtDepartman.Text;
            calisan.KullandigiProgram = txtProgram.Text;
            calisan.Tecrübe=txtTecrube.Text;
            sirket.Calisanlar.Add(calisan);
            sirket.SaveChanges();
            MessageBox.Show("Calisan Bilgileri Kaydoldu.");
            CalisanListele();
            Temizle();
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            
            Calisanlar sil = dgCalisanlar.SelectedItem as Calisanlar;
            sirket.Calisanlar.Remove(sirket.Calisanlar.First(x => x.CalisanID == sil.CalisanID));
            MessageBox.Show("Calisan Bilgileri Silindi");
            sirket.SaveChanges();
            CalisanListele();
            Temizle();
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {

            //int id = int.Parse(txtID.Text);
            // var guncelle=sirket.Calisanlar.Where(x => x.CalisanID == id).FirstOrDefault();

            //bu kısım calısıyor, diğer buonlara ve formlara uygulaaaaaa
            Calisanlar guncelle = dgCalisanlar.SelectedItem as Calisanlar;
            sirket.Calisanlar.First(x => x.CalisanID == guncelle.CalisanID);
            guncelle.CalisanAdi = txtAdi.Text;
            guncelle.DepartmanAdi = txtDepartman.Text;
            guncelle.KullandigiProgram = txtProgram.Text;
            guncelle.Tecrübe = txtTecrube.Text;
            sirket.SaveChanges();
            MessageBox.Show("Calisan Bilgileri Guncellendi.");
            CalisanListele();
            Temizle();
        }

        private void btnListele_Click(object sender, RoutedEventArgs e)
        {
            CalisanListele();
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            /* Calisanlar calisan = new Calisanlar();
             calisan.CalisanID = int.Parse(txtID.Text);
             Calisanlar ara = dgCalisanlar.SelectedItem as Calisanlar;
             sirket.Calisanlar.Find(sirket.Calisanlar.First(x => x.CalisanID == ara.CalisanID));
             MessageBox.Show("Arama başarılı.");
             sirket.SaveChanges();
             CalisanListele();
             Temizle();*/

            if (int.TryParse(txtID.Text, out int CalisanID))
            {
                var entity = sirket.Calisanlar.Find(CalisanID); 

                if (entity != null)
                {
                    
                    dgCalisanlar.ItemsSource = new[] { entity };
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

        private void dgCalisanlar_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgCalisanlar.SelectedItem is Calisanlar calisan)
            {
             
                txtAdi.Text = calisan.CalisanAdi;
                txtDepartman.Text = calisan.DepartmanAdi;
                txtProgram.Text = calisan.KullandigiProgram;
                txtTecrube.Text = calisan.Tecrübe;
            }
        }
    }
}
