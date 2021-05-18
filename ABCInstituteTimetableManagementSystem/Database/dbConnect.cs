using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.Database
{
    class dbConnect
    {
        private static string conString;
        SqlConnection con;
        private SqlCommand cmd;

       public dbConnect()
        {
            conString = @"Data Source=(localdb)\Ishini;Initial Catalog=ABCInstitute;Integrated Security=True";
        }

        public void openConnection()
        {
            con = new SqlConnection(conString);
            con.Open();
        }

        public SqlConnection getConnection()
        {
            return con;
        }
        public void closeConnection()
        {
            con.Close();
        }

        public void addSqlParameter(string key, object valor)
        {
            this.cmd.Parameters.AddWithValue(key, valor);
        }
        public void ExecuteQueries(string query)
        {
            cmd = new SqlCommand(query, con);


            cmd.ExecuteNonQuery();

        }



        public SqlDataReader dataReader(string query)
        {
            //  cmd = new SqlCommand(query, con);
            this.cmd.CommandText = query;
            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public object showDataInGridView(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(conString, con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            object datanum = ds.Tables[0];
            return datanum;

        }
    }
}
