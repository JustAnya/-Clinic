using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
    public partial class Profile1 : Window
    {
        Byte[] ava;
        public Profile1()
        {
            InitializeComponent();
            
            if(User.Access==2)
            { 
                addinfo.Visibility = Visibility.Collapsed;
            }

            Profile pr = User.GetProfileByIdUser(User.Id_user);

            if (pr.Name == "") //если это незаполненный пофиль бд, то ничего не вставляем
            {

            }
            else//если профиль уже заполнен в бд то заполняем
            {
                var sql_con = DB.Get_DB_Connection();
                var table = DB.Get_DataTable("SELECT * FROM [Profile] WHERE [Id_user] = " + User.Id_user + ";");
                ava = (Byte[])table.Rows[0]["Image"];
                Photo.Source = byteArrayToBMP(ava);

                Name.Text = pr.Name;
                Surname.Text = pr.Surname;
                dateevent.DisplayDate = pr.Birth;
                Phone.Text = pr.Phone;
                Address.Text = pr.Address;
            }

        }

        private void ButtomSave1_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == ""|| Surname.Text == "" || dateevent.Text =="" || Phone.Text=="" || Address.Text =="" )
            {
                MessageBox.Show("неверно заполнено");
            }
            else
            {
                if (User.Access==1)
                {
                    Profile.UpdateProfile(Name.Text, Surname.Text, (DateTime)dateevent.SelectedDate, Phone.Text, ava, Address.Text);
                    Doctor.UpdateInit(Surname.Text + " "+ Name.Text);
                }
                else
                {
                    Profile.UpdateProfile(Name.Text, Surname.Text, (DateTime)dateevent.SelectedDate, Phone.Text, ava, Address.Text);
                } 
                ButtomSave.Content = "Save!";
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (User.Access == 1)//если вы врач то переход к окну врача
            {
                Menu1 registration = new Menu1();
                this.Close();
                registration.Show();
            }
            else//если пациент то к окну пациента
            {
                Menu2 registration = new Menu2();
                this.Close();
                registration.Show();
            }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog img = new OpenFileDialog();

                BitmapImage bm1 = new BitmapImage();
                img.ShowDialog();
                var s = img.SafeFileName;
                FileStream fs = new FileStream(img.FileName, FileMode.Open);
                ava = new Byte[fs.Length];
                int read = (int)fs.Length;
                fs.Read(ava, 0, read);
                fs.Close();

                bm1.BeginInit();
                bm1.UriSource = new Uri(img.FileName, UriKind.Relative);
                bm1.CacheOption = BitmapCacheOption.OnLoad;
                bm1.EndInit();

                Photo.Source = bm1;
              
            }
            catch
            {
                MessageBox.Show("open error");
            }
        }

        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {

        }
        public static BitmapImage byteArrayToBMP(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            MemoryStream ms2 = new MemoryStream();
            image.Save(ms2, ImageFormat.Png);
            BitmapImage bm1 = new BitmapImage();
            bm1.BeginInit();
            bm1.StreamSource = ms2;
            bm1.EndInit();
            return bm1;
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            AddDocInfo d = new AddDocInfo();
            d.Show();
        }

        private void Phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
            
        }
    }
}
