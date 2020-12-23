using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLINIC
{
    public class TimeDoctor
    {
        public int Id_time_doctor;
        public int id_time_user;
        public int Day;
        public int Id_doctor;

        public TimeDoctor(int Id_t_doc, int id_t_u, int Dayt, int id_doc)
        {
            Id_time_doctor = Id_t_doc;
            id_time_user = id_t_u;
            Day = Dayt;
            Id_doctor = id_doc;
        }
        public TimeDoctor() { }

        public static bool isContainByDayByDoctorId(int day, int id)
        {
            bool f = false;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeDoctor] where [Id_doctor]=" + id + " and [Day]="+day+";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    f = true;
                    break;
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

            }
        }

        public static string query_insert = "insert into [TimeDoctor]([Id_time],[Day],[Id_doctor]) values(@id_time, @day, @id_doc)";//все приёмы на которые записались к врачу
        public static List<TimeDoctor> GetListByIdDoctor(int id_doc)
        {
            List<TimeDoctor> td = new List<TimeDoctor>();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeDoctor] where [Id_doctor]=" + id_doc + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    TimeDoctor tt = new TimeDoctor((int)dr[0], (int)dr[1], (int)dr[2], (int)dr[3]);
                    td.Add(tt);
                }
                return td;
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
        public static void DeleteTimeByIdTimeDoctor(int id)
        {
            try
            {
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand("delete from [TimeDoctor] where [Id_time_doctor]=" + id + ";", sql_con); 
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

        public static void DeleteTimeByIdDoctorByDay(int id_doc, int day)
        {
            try
            {
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand("delete from [TimeDoctor] where [Id_doctor]=" + id_doc + " and [Day]="+day+";", sql_con);
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
        public static void SetTime(int id_t, int day, int id_doc)//вносим запись о времени работы доктора
        {
            try
            {
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@day", day);
                cmd2.Parameters.AddWithValue("@id_time", id_t);
                cmd2.Parameters.AddWithValue("@id_doc", id_doc);
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
        public static TimeDoctor GetTimeByIdTimeDoctor(int id)
        {
            TimeDoctor tt = new TimeDoctor();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeDoctor] where [Id_time_doctor]=" + id+";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    TimeDoctor td = new TimeDoctor((int)dr[0], (int)dr[1], (int)dr[2], (int)dr[3]);
                    tt = td;
                    break;
                }
                return tt;
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

        public static List<TimeDoctor> GetByDayByIdDoctor(int day, int id_doc)//список приёмов доктора
        {
            List<TimeDoctor> list_d = new List<TimeDoctor>();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeDoctor] where [Id_doctor]=" + id_doc + " and [Day]=" + day + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    TimeDoctor tt = new TimeDoctor((int)dr[0], (int)dr[1], (int)dr[2], (int)dr[3]);
                    list_d.Add(tt);
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
        public static TimeDoctor GetTimeDoctorByIdTimeByDayByIdDoctor(int id_t, int day, int id_doc)//на основании дня, времени, и айди доктора получаем конкретный приём
        {
            TimeDoctor c = new TimeDoctor();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeDoctor] where [Id_doctor]=" + id_doc + " and  [Day]=" + day + " and [Id_time]="+id_t+ ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    TimeDoctor cc = new TimeDoctor((int)dr[0], (int)dr[1], (int)dr[2], (int)dr[3]);
                    c = cc;
                }
                return c;
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

        public static List<TimeDoctor> GetUniqByDayByIdDoctor(int day, int id_doc)//получить список приёмов этого врача без повторов
        {
            List<TimeDoctor> list_d = new List<TimeDoctor>();
            try
            {
                int i = 0; 
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeDoctor] where [Id_doctor]=" + id_doc + " and [Day]=" + day + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    if (i == (int)dr[2]) { }
                    else
                    {
                        i = (int)dr[2];
                        TimeDoctor tt = new TimeDoctor((int)dr[0], (int)dr[1], (int)dr[2], (int)dr[3]);
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

            }

        }

        public static List<TimeDoctor> GetUniqByIdDoctor(int id_doc)//получаем список приёмов врача без повтора дня
        {
            List<TimeDoctor> list_d = new List<TimeDoctor>();
            try
            { 
                for (int i=1; i<=7;i++)
                {
                    var list = TimeDoctor.GetUniqByDayByIdDoctor(i, id_doc);
                    if ( list!= null)
                    {
                        foreach (var l in list)
                        {
                            list_d.Add(l);
                        }
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

            }

        }
    }
}
