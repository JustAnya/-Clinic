using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLINIC
{
    class Doctor
    {
        public int Id_doctor;
        public string About_doctor;
        public int Id_specialty;
        public int Id_user;
        public string Init;

        public Doctor(string init, int id, string about, int id_s, int id_u)
        {
            Init = init;
            Id_doctor = id;
            About_doctor = about;
            Id_specialty = id_s;
            Id_user = id_u;
        }
        public Doctor() { }

        public static string query_insert = "insert into [DOCTOR] ([About_doctor],[Id_specialty],[Id_user],[Initial_doc]) values (@about,@i_s,@i_u,@init);";
        public static string query_update = "update [DOCTOR] set [About_doctor]=@ab, [Id_specialty]=@id where [Id_user]=@idd;";

        static public void SetDoctor(string about, int id_s, int id_u, string init)
        {
            try
            {
                string Message = "Сохранено";
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@about", about);
                cmd2.Parameters.AddWithValue("@i_s", id_s);
                cmd2.Parameters.AddWithValue("@i_u", id_u);
                cmd2.Parameters.AddWithValue("@init", init); 
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
        public static Doctor GetDoctorByIdUser(int id)
        {
            Doctor s = new Doctor();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [DOCTOR] where [Id_user]=" + id + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    Doctor spec = new Doctor((string)dr[4], (int)dr[0], (string)dr[1], (int)dr[2], (int)dr[3]);
                    s = spec;
                }
                return s;
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
        public static Doctor GetDoctorByIdDoctor(int id)
        {
            Doctor s = new Doctor();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [DOCTOR] where [Id_doctor]=" + id + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    Doctor spec = new Doctor((string)dr[4], (int)dr[0], (string)dr[1], (int)dr[2], (int)dr[3]);
                    s = spec;
                }
                return s;
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

        public static void UpdateInit(string init)
        {
            try
            {
                string Message = "Сохранено";
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand("update [DOCTOR] set [Initial_doc]=@ab where [Id_user]=@idd;", sql_con);
                cmd2.Parameters.AddWithValue("@idd", User.Id_user); 
                cmd2.Parameters.AddWithValue("@ab", init);
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
        public static void UpdateByAboutBySpec(string about, int id)
        {
            try
            {
                string Message = "Сохранено";
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_update, sql_con);
                cmd2.Parameters.AddWithValue("@idd", User.Id_user);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.Parameters.AddWithValue("@ab", about);
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


        public static List<Doctor> GetListAllDoctor()
        {
            List<Doctor> list_d = new List<Doctor>();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [DOCTOR];", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    Doctor spec = new Doctor((string)dr[4], (int)dr[0], (string)dr[1], (int)dr[2], (int)dr[3]);
                    list_d.Add(spec);
                }
                return list_d;
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
    }
}

