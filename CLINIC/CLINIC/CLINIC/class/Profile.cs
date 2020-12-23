using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace CLINIC
{
    public class Profile
    {
        public string Name;
        public string Surname;
        public string Init;
        public DateTime Birth;
        public string Phone;
        public string Address;
        public int Id_user;
        public Byte[] Image;
        public BitmapImage icon { get; set; }

        public Profile(string N, string S, DateTime B, string P, string A, int Id_u, BitmapImage Ic, string i)
        {
            Name = N;
            Surname = S;
            Birth = B;
            Phone = P;
            Address = A;
            Id_user = Id_u;
            Image = null;
            Init = i;
            icon = Ic;
        }

        public Profile(string N,string S, DateTime B, string P,string A, int Id_u, Byte[] I, string i)
        {
            Name = N;
            Surname = S;
            Birth = B;
            Phone = P;
            Address = A;
            Init = i;
            Id_user = Id_u;
            Image = I;
            icon = null;
        }

        public static string query_insert = "insert into [Profile]([Name],[Surname],[Age],[Phone_number],[Address],[Id_user],[Image], [Init]) values (@name, @surname, @age, @phone_number,@address,  @id, @image,@init)";
        public static string query_update = "update [Profile] set [Init]=@init, [Name]=@Name, [Surname]=@Surname, [Age]=@Age, [Phone_number]=@Phone_number, [Address]=@Address, [Image]=@Image where [Id_user]=@id;";
         
        public static void UpdateProfile(string name, string surname, DateTime age, string phone, Byte[] image, string address)
        {
            try
            { 
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_update, sql_con);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@surname", surname);
                cmd2.Parameters.AddWithValue("@age", age);
                cmd2.Parameters.AddWithValue("@phone_number", phone);
                cmd2.Parameters.AddWithValue("@image", image);
                cmd2.Parameters.AddWithValue("@address", address);
                cmd2.Parameters.AddWithValue("@id", User.Id_user);
                cmd2.Parameters.AddWithValue("@init", surname + " " + name);

                cmd2.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                // clsDB.Close_DB_Connection();
            }
        }
        static public void Set_Profile(string name, string surname, DateTime age, string phone, Byte[] image, string address)
        {
            try
            {
                string Message = "Сохранено";
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@surname", surname);
                cmd2.Parameters.AddWithValue("@age", age);
                cmd2.Parameters.AddWithValue("@phone_number", phone);
                cmd2.Parameters.AddWithValue("@image", image);
                cmd2.Parameters.AddWithValue("@address", address);
                cmd2.Parameters.AddWithValue("@id", User.Id_user);
                cmd2.Parameters.AddWithValue("@init", surname + " " + name);

                cmd2.ExecuteNonQuery();
                System.Windows.MessageBox.Show(Message);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                // clsDB.Close_DB_Connection();
            }
        }

        public static int GetIdUserByInit(string s)
        {
            int t = 0;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Profile];", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    if ((string)dr["init"]==s)
                    {
                        t = (int)dr["Id_user"];
                        break;
                    }                   
                }
                return t;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {

            }
        }

    }
}
