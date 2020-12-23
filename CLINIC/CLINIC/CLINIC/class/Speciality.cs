using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLINIC
{
    class Speciality
    {
        public int Id_specialty;
        public string Name_specialty;
        public string About_specialty;

        public Speciality(int i, string s, string s2)
        {
            Id_specialty = i;
            Name_specialty = s;
            About_specialty = s2;
        }
        public Speciality() { }

        public static List<Speciality> GetListSpeciality()
        {
            List<Speciality> list_sp = new List<Speciality>();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Specialty] ;", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//rider
                {
                    Speciality spec = new Speciality((int)dr[0], (string)dr[1], (string)dr[2]);
                    list_sp.Add(spec);
                }
                return list_sp;
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

        public static string GetNameSpecialityByIDSpeciality(int id)
        {
            string s = "";
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Specialty] ;", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//rider
                {
                    if ((int)dr[0] == id)
                    {
                        s = (string)dr[1];
                        break;
                    }
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

        public static int GetIdSpecialityByNameSpeciality(string s)
        {
            int i=0;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Specialty] ;", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//rider
                {
                    if ((string)dr[1] == s)
                    {
                        i = (int)dr[0];
                        break;
                    }
                }
                return i;
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
