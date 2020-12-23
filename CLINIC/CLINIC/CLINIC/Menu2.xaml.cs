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

namespace CLINIC
{
    /// <summary>
    /// Логика взаимодействия для Menu2.xaml
    /// </summary>
    public partial class Menu2 : Window
    {
        public Menu2()
        {
            InitializeComponent();
        }

        private void btnHealthc_Click(object sender, RoutedEventArgs e)
        {
            HealthCard helcard = new HealthCard(User.Id_user);
             this.Close();
            helcard.Show();
        }

        private void btnmedrec_Click(object sender, RoutedEventArgs e)
        {
            DoctorRegistration record = new DoctorRegistration();
            this.Close();
            record.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            HealthCard2 about = new HealthCard2();
            this.Close();
            about.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Menu2 = new MainWindow();
            Menu2.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile1 mm = new Profile1();
            mm.Show();
            this.Close();
        }
    }
}
