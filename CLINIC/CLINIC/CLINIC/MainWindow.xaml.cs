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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(Properties.Settings.Default.Constr);
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                //ищем юзера по логину и паролю в бд
                String querty = "SELECT COUNT(1) FROM [USER] WHERE LOGIN=@LOGIN AND PASSWORD=@PASSWORD";
                SqlCommand sqlCmd = new SqlCommand(querty, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@LOGIN", login.Text);
                sqlCmd.Parameters.AddWithValue("@PASSWORD", Hash((Convert.ToString(password.Text))));
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                DataTable table = new DataTable();
               
                if (count == 1) //если нашли одного такого пользователя
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT TOP(1) * from [USER] where [LOGIN] like '" + login.Text + "'", sqlCon);
                    adapter.Fill(table);
                    int access = (int)table.Rows[0]["ACCESS"];
                    User.Login = login.Text;
                    User.Access = access;
                    User.Id_user = (int)table.Rows[0]["Id_user"];
                    if (access == 1)//если врач
                    {
                        Menu1 aa = new Menu1();
                        this.Close();
                        aa.Show();
                    }
                    else //если пациент
                    {
                        Menu2 bb = new Menu2();
                        this.Close();
                        bb.Show();
                    }
                } 
                else//если не нашли юзера
                {
                    MessageBox.Show("Login or password is incorrect.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
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
