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
    /// Логика взаимодействия для Menu1.xaml
    /// </summary>
    public partial class Menu1 : Window
    {
        public Menu1()
        {
            InitializeComponent();
        }

        private void btnProfile1_Click(object sender, RoutedEventArgs e)
        {            
            Profile1 profile1 = new Profile1();
            this.Close();
            profile1.Show();
        }

        private void btnAppointment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            ListPatients l = new ListPatients();
            l.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            this.Close();
            about.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Menu1 = new MainWindow();
            Menu1.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
