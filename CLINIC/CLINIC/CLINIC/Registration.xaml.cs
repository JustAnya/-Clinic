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
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace CLINIC
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        Byte[] Data= null;
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtomReg_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.Constr);
            try
            {
                if (login.Text == "" || password.Text == "")
                {
                    MessageBox.Show("Не все поля заполнены.");
                }
                else
                {
                    if (password.Text.Length < 2)
                    {
                        MessageBox.Show("Недостаточная длина пароля");
                    }
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();
                    //поиск записей в бд с таким логином
                    String querty = "SELECT COUNT(1) FROM [USER] WHERE LOGIN=@LOGIN";
                    SqlCommand sqlCmd = new SqlCommand(querty, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@LOGIN", login.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    DataTable table = new DataTable();

                    if (count == 0)//если не нашли ни одной записи в бд с таким логином
                    {
                        var tablei = DB.Get_DataTable("SELECT * FROM [Profile] WHERE [Id_user]=1;");
                        Data = (Byte[])tablei.Rows[0]["Image"];
                        String query = "Insert into [USER] (Login, Password,Access) values (@Login, @Password, @Access)";
                        SqlCommand sqlCmd2 = new SqlCommand(query, sqlCon);
                        sqlCmd2.CommandType = CommandType.Text;
                        sqlCmd2.Parameters.AddWithValue("@Login", login.Text);
                        sqlCmd2.Parameters.AddWithValue("@Password", Hash((Convert.ToString(password.Text))));
                        if ((bool)doctor.IsChecked)//если регается доктор
                        {
                            sqlCmd2.Parameters.AddWithValue("@Access", "1");
                            User.Access = 1;
                            sqlCmd2.ExecuteNonQuery();
                            User.Login = login.Text;
                            User.Id_user = User.GetNewId() - 1;
                            Profile.Set_Profile("", "", DateTime.Now, "", Data, "");
                            Doctor.SetDoctor("", 1, User.Id_user, "");
                            Card.SetCard(User.Id_user, 0, 0, "no", "no", "no", "no");
                            MainWindow registration = new MainWindow();//перехоим в главное меню
                            registration.Show();
                            this.Close();
                        }
                        else if ((bool)pation.IsChecked)//если регается пациент
                        {
                            sqlCmd2.Parameters.AddWithValue("@Access", "2");
                            User.Access = 2;
                            sqlCmd2.ExecuteNonQuery();
                            User.Login = login.Text;
                            User.Id_user = User.GetNewId() - 1;
                            Profile.Set_Profile("", "", DateTime.Now, "", Data, "");
                            Card.SetCard(User.Id_user, 0, 0, "no", "no", "no", "no");
                            MainWindow registration = new MainWindow();//перехоим в главное меню
                            registration.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Выберите тип пользователя.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такой логин уже есть");
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                //sqlCon.Close();
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            MainWindow l = new MainWindow();
            l.Show();
            this.Close();
        }

        public static string Hash(string input)//хеширование пароля
        {
            byte[] hash = Encoding.ASCII.GetBytes(input);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string output = "";
            foreach (var b in hashenc)
            {
                output += b.ToString("x2");
            }
            return output;
        }
    }
}
