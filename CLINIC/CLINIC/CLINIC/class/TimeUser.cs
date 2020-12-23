using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLINIC 
{
    public class TimeUser
    {
        public int id;
        public string AllTime;
        public string Time;

        public TimeUser(int id_time, string all_time, string cur_time)
        {
            id = id_time;
            AllTime = all_time;
            Time = cur_time;
        }
        public TimeUser() { }

        public static int GetIdByTime(string tim)
        {
            int t = 0;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeUser];", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    if ((string)dr[2] == tim)
                    {
                        t = (int)dr[0];
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

        public static List<TimeUser> GetListTimeByAllTime(string all)
        {
            List<TimeUser> t = new List<TimeUser>();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeUser];", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    if ((string)dr[1] == all)
                    {
                        TimeUser tt = new TimeUser((int)dr[0], (string)dr[1], (string)dr[2]);
                        t.Add(tt);
                    }
                }
                return t;
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

        public static TimeUser GetTimeById(int id_t)
        {
            TimeUser t = new TimeUser();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [TimeUser] where [Id]=" + id_t+ ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    TimeUser tt = new TimeUser((int)dr[0], (string)dr[1], (string)dr[2]);
                    t = tt;
                }
                return t;
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
