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
using WPF_Sirket.Model;

namespace WPF_Sirket
{
    /// <summary>
    /// Departmanlar.xaml etkileşim mantığı
    /// </summary>
    public partial class Departmanlar : Window
    {
        SirketEntities sirket = new SirketEntities();
        
        public Departmanlar()
        {
            InitializeComponent();
           
        }
        public void DepartmanListele()
        {
            dgDepartman.ItemsSource = sirket.Departman.ToList();
        }
        public void Temizle()
        {
            txtAdi.Text = txtElemanSayisi.Text = txtProjeSayisi.Text = txtButce.Text = "";
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }
        private void dgDepartman_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            DataRowView dataRow = data.SelectedItems as DataRowView;
            if (dataRow != null) //bu kısımlardaki to stringi düzeltmek gerekebilir, sql'de int yaptım nvarchar degil
            {
                txtID.Text = dataRow["DepartmanD"].ToString();
                txtAdi.Text = dataRow["DepartmanAdi"].ToString(); 
                txtElemanSayisi.Text = dataRow["ElemanSayisi"].ToString();
                txtProjeSayisi.Text = dataRow["ProjeSayisi"].ToString();
                txtButce.Text = dataRow["Butce"].ToString();
            }
        }
        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            Departman dp = new Departman();
            dp.DepartmanAdi = txtAdi.Text;
            dp.ElemanSayisi = int.Parse(txtElemanSayisi.Text);
            dp.ProjeSayisi =int.Parse(txtProjeSayisi.Text);
            dp.Butce = int.Parse(txtButce.Text);
            sirket.Departman.Add(dp);
            sirket.SaveChanges();
            MessageBox.Show("Departman Bilgileri Kaydoldu.");
            DepartmanListele();
            Temizle();
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            Departman sil = dgDepartman.SelectedItem as Departman;
            sirket.Departman.Remove(sirket.Departman.First(x => x.DepartmanID == sil.DepartmanID));
            MessageBox.Show("Departman Bilgileri Silindi");
            sirket.SaveChanges();
            DepartmanListele();
            Temizle();

            /*
            int id = int.Parse(txtID.Text);
            var sil = sirket.Departman.Where(x => x.DepartmanID == id).FirstOrDefault();
            sirket.Departman.Remove(sil);
            MessageBox.Show("Departman Bilgileri Silindi");
            DepartmanListele();
            Temizle();*/
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            // int id = int.Parse(txtID.Text);
            //var guncelle = sirket.Departman.Where(x => x.DepartmanID == id).FirstOrDefault();

            Departman guncelle = dgDepartman.SelectedItem as Departman;
            sirket.Departman.First(x => x.DepartmanID == guncelle.DepartmanID);

            guncelle.DepartmanAdi = txtAdi.Text;
            guncelle.ElemanSayisi = int.Parse(txtElemanSayisi.Text);
            guncelle.ProjeSayisi = int.Parse(txtProjeSayisi.Text);
            guncelle.Butce = int.Parse(txtButce.Text);
            sirket.SaveChanges();
            MessageBox.Show("Departman Bilgileri Guncellendi.");
            DepartmanListele();
            Temizle();
        }

        private void btnListele_Click(object sender, RoutedEventArgs e)
        {
            DepartmanListele();
        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtID.Text, out int DepartmanID))
            {
                var entity = sirket.Departman.Find(DepartmanID);

                if (entity != null)
                {

                    dgDepartman.ItemsSource = new[] { entity };
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

        //tekrar bak
        private void dgDepartman_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgDepartman.SelectedItem is Departman departman)
            {
                txtAdi.Text = departman.DepartmanAdi;
                txtElemanSayisi.Text = departman.ElemanSayisi.ToString();
                txtProjeSayisi.Text = departman.ProjeSayisi.ToString();
                txtButce.Text = departman.Butce.ToString();
            }
        }
    }
}
