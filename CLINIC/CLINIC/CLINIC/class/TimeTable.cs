using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLINIC
{
    public class TimeTable
    {
        public int Id_timetable;
        public int Id_card;
        public int Id_Time_Doctor;
        //public TimeDoctor info_t;
        //public TimeUser timeUser;

        public TimeTable(int Id_t, int Id_t_d, int Id_c)
        {
            Id_timetable = Id_t;
            Id_card = Id_c;
            Id_Time_Doctor = Id_t_d;
        } 
        public TimeTable() { }
        public static string query_insert = "insert into [Timetable]([Id_card],[Id_time_doctor]) values(@card, @time)";

        public static TimeTable GetTimetableByIdTimeDoctorByIdCard(int id_t_d, int id_c)
        {
            TimeTable list_d = new TimeTable();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Timetable] where [Id_time_doctor]="+id_t_d+" and [Id_card]="+id_c+";", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TimeTable t = new TimeTable((int)dr[0], (int)dr[1], (int)dr[2]);
                    list_d = t;
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
                // clsDB.Close_DB_Connection();
            }
        }
        public static bool isRecordingYetByIdTimeDoctor(int id_t_d)
        {
            bool f = true;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT count(1) FROM [Timetable] where [Id_time_doctor]="+id_t_d+";", conn);
                int b = (int)cmd.ExecuteScalar();
                if (b==0)
                {
                    f = false;
                } 
                return f;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                // clsDB.Close_DB_Connection();
            }
        }
        public static List<string> GetListInitPatientByIdDoctor(int id)
        {
            List<string> list_d = new List<string>();
            try
            {
                var list_time = TimeDoctor.GetListByIdDoctor(id);
                foreach (TimeDoctor t in list_time)
                {
                    var conn = DB.Get_DB_Connection();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [Timetable];", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (t.Id_time_doctor == dr.GetInt32(1))
                        {
                            int id_user = Card.GetIdUserByIdCard((int)dr[2]);
                            Profile p = User.GetProfileByIdUser(id_user);
                            if (list_d.Contains(p.Init))
                            { }
                            else { list_d.Add(p.Init); }
                        }
                    }
                    DB.Close_DB_Connection();
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
                // clsDB.Close_DB_Connection();
            }
        }
        public static void InsertRecord(int Id_car, int Id_Tim_d)
        {
            try
            {
                string Message = "Талон добавлен к зарезервированным.";
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@card", Id_car);
                cmd2.Parameters.AddWithValue("@time", Id_Tim_d);
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

        public static List<TimeTable> GetAllTimetableByIdUserPatient(int id)
        {
            List<TimeTable> list_d = new List<TimeTable>();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Timetable];", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                int cc = Card.GetCardByIdUser(id).Id_card;
                while (dr.Read())
                {
                    if ((int)dr["Id_card"] == cc)
                    { 
                        TimeTable tt = new TimeTable((int)dr[0], (int)dr[1], (int)dr[2]);
                        list_d.Add(tt);
                    }
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
                // clsDB.Close_DB_Connection();
            }
        }
    }
}
 