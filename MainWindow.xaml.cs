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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Sirket
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SirketEntities sirket = new SirketEntities();
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SirketEntities sirket = new SirketEntities();

            string usr = txt_username.Text;
            string ps=txt_password.Password.ToString();

            bool bul=sirket.Login.Any(user => user.KullaniciAdi==usr && user.Sifre==ps);
            if (bul)
            {
                Anasayfa ana = new Anasayfa();
                ana.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
        }
    }
}
