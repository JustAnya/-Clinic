using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CLINIC
{
    public static class User
    {
        static public int Id_user;
        static public string Login;
        static public int Access;
        public static Profile GetProfileByIdUser(int Id)
        {
            try
            {
                var sql_con = DB.Get_DB_Connection();
                var table = DB.Get_DataTable("SELECT * FROM [Profile] WHERE [Id_user] = " + Id + ";");
                var a = (Byte[])table.Rows[0]["Image"];
                MemoryStream mStream = new MemoryStream((Byte[])a);
                BitmapImage geti = new BitmapImage();
                geti.BeginInit();
                geti.StreamSource = mStream;
                geti.EndInit();
                Profile pr = new Profile((string)table.Rows[0]["Name"], (string)table.Rows[0]["Surname"], (DateTime)table.Rows[0]["Age"], (string)table.Rows[0]["Phone_number"], (string)table.Rows[0]["Address"], (int)table.Rows[0]["Id_user"], geti, (string)table.Rows[0]["Init"]);
                return pr;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {

            }
        }

        public static int GetNewId()
        {
            int t=0;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [USER];",conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    t = (int)dr[0];
                }
                return (t+1);
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


