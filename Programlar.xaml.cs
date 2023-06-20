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
    /// Programlar.xaml etkileşim mantığı
    /// </summary>
    public partial class Programlar : Window
    {
        SirketEntities sirket = new SirketEntities();
      
        public Programlar()
        {
            InitializeComponent();
            
        }
        public void ProgramListele()
        {
            dgProgram.ItemsSource = sirket.Program.ToList();
        }
        public void Temizle()
        {
            txtAdi.Text = txtProjeSayisi.Text = txtDepartmanSayisi.Text= dtpTarih.Text = " ";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Anasayfa ana = new Anasayfa();
            ana.Show();
            this.Hide();
        }

        private void dgProgram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid data = (DataGrid)sender;
            DataRowView dataRow = data.SelectedItems as DataRowView;
            if (dataRow != null)
            {
                txtID.Text = dataRow["ProgramID"].ToString();
                txtAdi.Text = dataRow["ProgramAdi"].ToString();
                txtProjeSayisi.Text = dataRow["YapilanProjeSayisi"].ToString();
                txtDepartmanSayisi.Text = dataRow["KullanilanDepartmanSayisi"].ToString();
                dtpTarih.Text = dataRow["Aldığı Tarih"].ToString();


            }
        }

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            Program program = new Program();
            program.ProgramAdi = txtAdi.Text;
            program.YapilanProjeSayisi = int.Parse(txtProjeSayisi.Text);
            program.KullanilanDepartmanSayisi = int.Parse(txtDepartmanSayisi.Text);
            program.BaslangicTarihi = dtpTarih.SelectedDate;

            sirket.Program.Add(program);
            sirket.SaveChanges();
            MessageBox.Show("Program Bilgileri Eklendi.");
            ProgramListele();
            Temizle();
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            Program sil = dgProgram.SelectedItem as Program;
            sirket.Program.Remove(sirket.Program.First(x => x.ProgramID == sil.ProgramID));
            MessageBox.Show("Program Bilgileri Silindi");
            sirket.SaveChanges();
            ProgramListele();
            Temizle();

            /*
            int id = int.Parse(txtID.Text);
            var sil = sirket.Program.Where(x => x.ProgramID == id).FirstOrDefault();
            sirket.Program.Remove(sil);
            MessageBox.Show("Program Bilgileri Silindi");
            ProgramListele();
            Temizle();*/
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            // int id = int.Parse(txtID.Text);
            //var guncelle = sirket.Program.Where(x => x.ProgramID == id).FirstOrDefault();

            Program guncelle = dgProgram.SelectedItem as Program;
            sirket.Program.First(x => x.ProgramID == guncelle.ProgramID);
            guncelle.ProgramAdi = txtAdi.Text;
            guncelle.YapilanProjeSayisi = int.Parse(txtProjeSayisi.Text);
            guncelle.KullanilanDepartmanSayisi = int.Parse(txtDepartmanSayisi.Text);
            guncelle.BaslangicTarihi = dtpTarih.SelectedDate;
            sirket.SaveChanges();
            MessageBox.Show("Program Bilgileri Guncellendi.");
            ProgramListele();
            Temizle();
        }

        private void btnListele_Click(object sender, RoutedEventArgs e)
        {
            ProgramListele();

        }

        private void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            Temizle();
        }

        private void btnAra_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtID.Text, out int ProgramID))
            {
                var entity = sirket.Program.Find(ProgramID);

                if (entity != null)
                {

                    dgProgram.ItemsSource = new[] { entity };
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

        private void dgProgram_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgProgram.SelectedItem is Program program)
            {
                txtAdi.Text = program.ProgramAdi;
                txtProjeSayisi.Text = program.YapilanProjeSayisi.ToString();
                txtDepartmanSayisi.Text = program.KullanilanDepartmanSayisi.ToString();
                dtpTarih.SelectedDate = program.BaslangicTarihi;
            }
        }
    }
}
