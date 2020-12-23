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
    /// Логика взаимодействия для HealthCard.xaml
    /// </summary>
    public partial class HealthCard : Window
    {
        int id_user = 0;
        Card card_user = new Card();
        public HealthCard()
        {
            InitializeComponent();
        }
        public HealthCard(int id)
        {
            InitializeComponent();

            id_user = id;
            card_user = Card.GetCardByIdUser(id);
            Profile pr = User.GetProfileByIdUser(id);
            txtID.Content = card_user.Id_card;
            txtName.Content = pr.Name;
            txtSurname.Content = pr.Surname;
            txt_age.Content = Convert.ToString(-pr.Birth.Year+DateTime.Now.Year);
            height.Text = Convert.ToString(card_user.Height);
            weight.Text = Convert.ToString(card_user.Weight);
            Chronic.Text = card_user.Chronic_dis;
            Result.Text = card_user.Test_results;
            Diag.Text = card_user.Diagnosis;
            Treatment.Text = card_user.Treatment;

            if(User.Access==1)
            {

            }
            else
            {
                height.IsEnabled = false;
                weight.IsEnabled = false;
                Chronic.IsEnabled = false;
                Result.IsEnabled = false;
                Diag.IsEnabled = false;
                Treatment.IsEnabled = false;
                btnSave.IsEnabled = false;
            }
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (User.Access == 1)
            {
                ListPatients ll = new ListPatients();
                ll.Show();
                this.Close();
            }
            else
            {
                Menu2 ll = new Menu2();
                ll.Show();
                this.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Card.UpdateByIdCard(card_user.Id_card, Convert.ToInt32(height.Text), Convert.ToInt32(weight.Text), Chronic.Text, Result.Text, Diag.Text, Treatment.Text);
            ListPatients ll = new ListPatients();
            ll.Show();
            this.Close();
        }
    }
}
