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
    /// Логика взаимодействия для HealthCard2.xaml
    /// </summary>
    public partial class HealthCard2 : Window
    {
        List<TimeTable> list_t = new List<TimeTable>();
        List<string> list_s_t = new List<string>();
        public HealthCard2()
        {
            InitializeComponent();

            list_t = TimeTable.GetAllTimetableByIdUserPatient(User.Id_user);//всё расписание кот занято нами
            foreach (TimeTable t in list_t)//и выводим из неё .......
            {
                var i = TimeDoctor.GetTimeByIdTimeDoctor(t.Id_Time_Doctor);
                list_s_t.Add("Day: "+i.Day+"; Time: "+TimeUser.GetTimeById(i.id_time_user).Time+"; Doctor: "+Doctor.GetDoctorByIdDoctor(i.Id_doctor).Init);
            }
            list_talon.ItemsSource = list_s_t;

        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Menu2 ll = new Menu2();
            ll.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
