using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLINIC
{
    public class Card
    {
        public int Id_card;
        public int Id_user;
        public int Height;
        public int Weight;
        public string Chronic_dis;
        public string Test_results;
        public string Diagnosis;
        public string Treatment;

        public Card(int card, int us, int H, int W, string dis, string res, string diag,string Treat)
        {
            Id_card = card;
            Id_user = us;
            Height = H;
            Weight = W;
            Chronic_dis = dis;
            Test_results = res;
            Diagnosis = diag;
            Treatment = Treat;
        }
        public Card() { }
        public static string query_update = "update [Health_card] set [Height]=@height, [Weight]=@weight, [Chronic_disease]=@chronic, [Test_results]=@test, [Diagnosis]=@diag, [Treatment]=@treat where [Id_card]=@id;";
        public static string query_insert = "insert into [Health_card]([Id_user],[Height],[Weight], [Chronic_disease],[Test_results],[Diagnosis],[Treatment]) values(@id_u, @hei, @wei, @chron,@test, @diag, @treat)";
        static public void SetCard(int id_user, int hei, int wei, string chron, string test, string dia, string treat)
        {
            try
            {
                string Message = "Сохранено";
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_insert, sql_con);
                cmd2.Parameters.AddWithValue("@hei", hei);
                cmd2.Parameters.AddWithValue("@wei", wei);
                cmd2.Parameters.AddWithValue("@chron", chron);
                cmd2.Parameters.AddWithValue("@test", test);
                cmd2.Parameters.AddWithValue("@diag", dia);
                cmd2.Parameters.AddWithValue("@treat", treat);
                cmd2.Parameters.AddWithValue("@id_u", id_user);
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

        public static void UpdateByIdCard(int id_card, int hei, int wei, string chron, string test, string dia, string treat)
        {
            try
            {
                var sql_con = DB.Get_DB_Connection();
                SqlCommand cmd2 = new SqlCommand(query_update, sql_con);
                cmd2.Parameters.AddWithValue("@height", hei);
                cmd2.Parameters.AddWithValue("@weight", wei);
                cmd2.Parameters.AddWithValue("@chronic", chron);
                cmd2.Parameters.AddWithValue("@test", test);
                cmd2.Parameters.AddWithValue("@diag", dia);
                cmd2.Parameters.AddWithValue("@treat", treat);
                cmd2.Parameters.AddWithValue("@id", id_card ); 
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

        public static int GetIdUserByIdCard(int id)
        {
            int id_user = 0;
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Health_card] where [Id_card]=" + id + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())//reader
                {
                    id_user = (int)dr[1];
                }
                return id_user;
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

        public static Card GetCardByIdUser(int id)
        {
            Card cc = new Card();
            try
            {
                var conn = DB.Get_DB_Connection();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Health_card] where [Id_user]=" + id + ";", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())//reader
                {
                    Card c = new Card((int)dr[0], (int)dr[1], (int)dr[2], (int)dr[3], (string)dr[4], (string)dr[5],(string)dr[6] ,(string)dr[7]);
                    cc = c;
                }
                return cc;
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

