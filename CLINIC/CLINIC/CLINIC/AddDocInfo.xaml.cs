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
    /// Логика взаимодействия для AddDocInfo.xaml
    /// </summary>
    public partial class AddDocInfo : Window
    {
        Doctor doc = new Doctor();
        List<Speciality> list_s = new List<Speciality>();
        List<TimeDoctor> list_a_t = new List<TimeDoctor>();
        List<string> list_s_s = new List<string>();
        List<string> list_d_s = new List<string>();
        List<string> list_a_t_s = new List<string>();

        public AddDocInfo()
        {
            InitializeComponent();

            list_s = Speciality.GetListSpeciality();
            doc = Doctor.GetDoctorByIdUser(User.Id_user);
            ab_d.Text = doc.About_doctor;

            foreach (Speciality s in list_s)
            {
                list_s_s.Add(s.Name_specialty);
            }
            list_spec.ItemsSource = list_s_s;

            for (int i=1; i<=7; i++)
            {
                list_d_s.Add(Convert.ToString(i));
            }
            list_day.ItemsSource = list_d_s;

            FillListTime();          

            for (int i = 0; i < list_s.Count; i++)
            {
                if (list_s[i].Name_specialty == Speciality.GetNameSpecialityByIDSpeciality(doc.Id_specialty))
                {
                    list_spec.SelectedIndex = i;
                    break;
                }
            }
        }

        public void FillListTime()
        {
            list_all_time.ItemsSource = null;
            list_a_t_s.Clear();
            list_a_t.Clear();
            list_a_t = TimeDoctor.GetUniqByIdDoctor(doc.Id_doctor); 
            foreach (var t in list_a_t)
            {
                list_a_t_s.Add(t.Day + " " + TimeUser.GetTimeById(t.id_time_user).AllTime);
            }
            list_all_time.ItemsSource = list_a_t_s;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtomReg_Click(object sender, RoutedEventArgs e)
        {
            if (list_day.SelectedItem != null)
            {
                if ((bool)sm1.IsChecked)
                {
                    if (TimeDoctor.isContainByDayByDoctorId((list_day.SelectedIndex + 1), doc.Id_doctor))
                    {
                        MessageBox.Show("Этот день уже есть в расписании.");
                    }
                    else
                    {
                        var list_time = TimeUser.GetListTimeByAllTime((string)sm1.Content);
                        foreach (var l in list_time)
                        {
                            TimeDoctor.SetTime(l.id, (list_day.SelectedIndex + 1), doc.Id_doctor);
                        }
                        FillListTime();
                    }
                }
                else if ((bool)sm2.IsChecked)
                {
                    if (TimeDoctor.isContainByDayByDoctorId((list_day.SelectedIndex + 1), doc.Id_doctor))
                    {
                        MessageBox.Show("Этот день уже есть в расписании.");
                    }
                    else
                    {
                        var list_time = TimeUser.GetListTimeByAllTime((string)sm2.Content);
                        foreach (var l in list_time)
                        {
                            TimeDoctor.SetTime(l.id, (list_day.SelectedIndex + 1), doc.Id_doctor);
                        }
                        FillListTime();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите день недели.");
            }
        }
        

        private void Butt(object sender, RoutedEventArgs e)
        {
            Doctor.UpdateByAboutBySpec(ab_d.Text, Speciality.GetIdSpecialityByNameSpeciality(Convert.ToString(list_spec.SelectedItem))); 
            MessageBox.Show("Сохранено");
            Close();
        }

        private void ButtomR_Click(object sender, RoutedEventArgs e)
        {
            if (list_all_time.SelectedItem!=null)
            {
                foreach(var t in list_a_t)
                {
                    string s = t.Day + " " + TimeUser.GetTimeById(t.id_time_user).AllTime;
                    if ((string)list_all_time.SelectedItem==s)
                    {
                        TimeDoctor.DeleteTimeByIdDoctorByDay(Doctor.GetDoctorByIdUser(User.Id_user).Id_doctor, t.Day); 
                    }
                }
                FillListTime();
            }
            else
            {
                MessageBox.Show("Выберите время из списка.");
            }
        }

        private void list_all_time_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
