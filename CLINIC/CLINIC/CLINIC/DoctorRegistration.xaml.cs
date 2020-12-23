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

    public partial class DoctorRegistration : Window
    {
        List<Speciality> list_spec = new List<Speciality>();
        List<Doctor> list_doc = new List<Doctor>();
        List<Doctor> list_doc_spec = new List<Doctor>();
        List<TimeDoctor> list_time = new List<TimeDoctor>();
        List<TimeDoctor> list_t_u = new List<TimeDoctor>();
        string cur_time_user = "";
        List<string> l_s = new List<string>();
        List<string> l_d = new List<string>();
        List<string> l_t = new List<string>();

        Speciality cur_spec = new Speciality();
        Doctor cur_doc = new Doctor();
        int cur_day = 1;
        public DoctorRegistration()
        {
            InitializeComponent();

            list_spec = Speciality.GetListSpeciality();
            foreach(Speciality ss in list_spec)
            {
                l_s.Add(ss.Name_specialty);
            }
            combo_spec.ItemsSource = l_s;

            list_doc = Doctor.GetListAllDoctor();
            
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Menu2 ll = new Menu2();
            ll.Show();
            this.Close();
        }

        private void ButtomSave_Click(object sender, RoutedEventArgs e)
        {
            if (combo_time.SelectedItem!=null)
            {
                int id_cur_time = TimeUser.GetIdByTime((string)(combo_time.SelectedItem));//gлучаем айди выбранного времени
                TimeDoctor i = TimeDoctor.GetTimeDoctorByIdTimeByDayByIdDoctor(id_cur_time, cur_day, cur_doc.Id_doctor);//получаем конкретный приём конкретного врача
                if (TimeTable.isRecordingYetByIdTimeDoctor(i.Id_time_doctor))//проверка на занятость 
                {
                    MessageBox.Show("Это время уже занято");
                }
                else
                {
                    TimeTable.InsertRecord((Card.GetCardByIdUser(User.Id_user)).Id_card, i.Id_time_doctor);
                }
            }
            else
            {
                MessageBox.Show("Выберите время");
            }
        }

        private void text_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void text_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void combo_spec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            cur_spec = list_spec[combo_spec.SelectedIndex];
            combo_doc.ItemsSource = null;
            l_d.Clear();
            if (cur_spec==null) { }
            else
            {
                label_info.Text = cur_spec.About_specialty;

                foreach(Doctor d in list_doc)
                {
                    if (d.Id_specialty==cur_spec.Id_specialty)
                    {
                        list_doc_spec.Add(d);
                        l_d.Add(d.Init);
                    }
                }
                combo_doc.ItemsSource = l_d;
            }
        }

        private void combo_doc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cur_doc = list_doc_spec[combo_doc.SelectedIndex];
            combo_time.ItemsSource = null;
            list_t_u.Clear();
            if (cur_doc==null) {}
            else
            {
                label_info2.Text = cur_doc.About_doctor; 
                list_t_u = TimeDoctor.GetByDayByIdDoctor(1, cur_doc.Id_doctor); 
                foreach (var d in list_t_u)
                { 
                    l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time); 
                }
                combo_time.ItemsSource = l_t;
            }

        }

        private void calend_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //DateTime ddd = new DateTime();
            //if (((DateTime)calend.SelectedDate).Day<=25)
            //{
            //    DateTime dd = new DateTime(((DateTime)calend.SelectedDate).Year, ((DateTime)calend.SelectedDate).Month, ((DateTime)calend.SelectedDate).Day + 7);
            //    ddd = dd;
            //}
            //else
            //{
            //    DateTime dd = new DateTime(((DateTime)calend.SelectedDate).Year, ((DateTime)calend.SelectedDate).Month+1, 1);
            //    ddd = dd;
            //}
            //TimeD ttt = new TimeD();
            combo_time.ItemsSource = null;
            l_t.Clear();
            if(calend.SelectedDate<=DateTime.Now /*|| calend.SelectedDate>=ddd*/)
            {
                MessageBox.Show("Выберите корректную дату.");
            }
            else
            {
                if(((DateTime)calend.SelectedDate).DayOfWeek ==DayOfWeek.Monday)
                {
                    cur_day = 1;
                    list_t_u = TimeDoctor.GetByDayByIdDoctor(cur_day, cur_doc.Id_doctor);//
                    foreach (var d in list_t_u)
                    {
                        l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time);
                    }
                    combo_time.ItemsSource = l_t;
                }
                else if (((DateTime)calend.SelectedDate).DayOfWeek == DayOfWeek.Tuesday)
                {
                    cur_day = 2;
                    list_t_u = TimeDoctor.GetByDayByIdDoctor(cur_day, cur_doc.Id_doctor);
                    foreach (var d in list_t_u)
                    {
                        l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time);
                    }
                    combo_time.ItemsSource = l_t;
                }
                else if (((DateTime)calend.SelectedDate).DayOfWeek == DayOfWeek.Wednesday)
                {
                    cur_day = 3;
                    list_t_u = TimeDoctor.GetByDayByIdDoctor(cur_day, cur_doc.Id_doctor);
                    foreach (var d in list_t_u)
                    {
                        l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time);
                    }
                    combo_time.ItemsSource = l_t;
                }
                else if (((DateTime)calend.SelectedDate).DayOfWeek == DayOfWeek.Thursday)
                {
                    cur_day = 4;
                    list_t_u = TimeDoctor.GetByDayByIdDoctor(cur_day, cur_doc.Id_doctor);
                    foreach (var d in list_t_u)
                    {
                        l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time);
                    }
                    combo_time.ItemsSource = l_t;
                }
                else if (((DateTime)calend.SelectedDate).DayOfWeek == DayOfWeek.Friday)
                {
                    cur_day = 5;
                    list_t_u = TimeDoctor.GetByDayByIdDoctor(cur_day, cur_doc.Id_doctor);
                    foreach (var d in list_t_u)
                    {
                        l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time);
                    }
                    combo_time.ItemsSource = l_t;
                }
                else if (((DateTime)calend.SelectedDate).DayOfWeek == DayOfWeek.Saturday)
                {
                    cur_day = 6;
                    list_t_u = TimeDoctor.GetByDayByIdDoctor(cur_day, cur_doc.Id_doctor);
                    foreach (var d in list_t_u)
                    {
                        l_t.Add(TimeUser.GetTimeById(d.id_time_user).Time);
                    }
                    combo_time.ItemsSource = l_t;
                }
                else if (((DateTime)calend.SelectedDate).DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show("Воскресенье - выходной. Вызывайте скорую.");
                }  
            }
        }
    }
}
