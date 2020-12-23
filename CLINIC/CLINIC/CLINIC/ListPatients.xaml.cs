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
    /// Логика взаимодействия для ListPatients.xaml
    /// </summary>
    public partial class ListPatients : Window
    {
        List<string> list_p = new List<string>();
        public ListPatients()
        {
            InitializeComponent();
            FillList();
        }

        public void FillList()
        {
            list_patient.ItemsSource = null;//обнуляем старый список
            list_p.Clear();
            var l_s = TimeTable.GetListInitPatientByIdDoctor(Doctor.GetDoctorByIdUser(User.Id_user).Id_doctor);//взяли нашу айдишку и вывели пациентом
            foreach (var s in l_s)
            {
                list_p.Add(s);
            }
            list_patient.ItemsSource = list_p;
        }

        private void ButtomLog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) //закрыть
        {
            Menu1 ll = new Menu1();
            ll.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e) //сворачиваем
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)//назад
        {
            Menu1 ll = new Menu1();
            ll.Show();
            this.Close();
        }

        private void list_patient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(list_patient==null)
            {
                MessageBox.Show("Выберите пациента");
            }
            else
            {
                int i = Profile.GetIdUserByInit((string)(list_patient.SelectedItem));
                HealthCard d = new HealthCard(i);
                d.Show();
                this.Close();
            }
        }
    }
}
