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
    /// Логика взаимодействия для About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void text_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void text_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (User.Access == 2)
            {
                Menu2 ll = new Menu2();
                ll.Show();
                this.Close();
            }
            else
            {
                Menu1 lll = new Menu1();
                lll.Show();
                this.Close();
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
