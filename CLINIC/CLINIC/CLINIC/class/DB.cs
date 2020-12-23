using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CLINIC 
{
    public static class DB
    {
        public static SqlConnection Get_DB_Connection()
        {
            string cn_string = Properties.Settings.Default.Constr;
            SqlConnection cn_connection = new SqlConnection(cn_string);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            return cn_connection;
        }
        public static DataTable Get_DataTable(string query)
        {
            SqlConnection cn_connection = Get_DB_Connection();

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, cn_connection);
            adapter.Fill(table);

            return table;
        }
        public static void Execute_SQL(string query)
        {
            SqlConnection cn_connection = Get_DB_Connection();

            SqlCommand cmd_Command = new SqlCommand(query, cn_connection);
            cmd_Command.ExecuteNonQuery();
        }
        public static void Close_DB_Connection()
        {
            string cn_string = Properties.Settings.Default.Constr;
            SqlConnection cn_connection = new SqlConnection(cn_string);
            if (cn_connection.State != ConnectionState.Closed) cn_connection.Close();
        }
    }
}
