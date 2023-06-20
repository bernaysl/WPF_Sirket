using System;
using System.Collections.Generic;
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

namespace WPF_Sirket
{
    /// <summary>
    /// Anasayfa.xaml etkileşim mantığı
    /// </summary>
    public partial class Anasayfa : Window
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        SirketEntities sirket = new SirketEntities();

        private void btnCalisanlar_Click(object sender, RoutedEventArgs e)
        {
            Calisanlar calisanlar = new Calisanlar();
            calisanlar.Show();
            this.Hide();
        }

        private void btnDepartmanlar_Click(object sender, RoutedEventArgs e)
        {
            Departmanlar departmanlar = new Departmanlar();
            departmanlar.Show();
            this.Hide();

        }

        private void btnProgramlar_Click(object sender, RoutedEventArgs e)
        {
            Programlar program = new Programlar();
            program.Show();
            this.Hide();
        }

        private void btnMusteriler_Click(object sender, RoutedEventArgs e)
        {
            Musteriler musteri= new Musteriler();
            musteri.Show();
            this.Hide();
        }

       
    }
}
