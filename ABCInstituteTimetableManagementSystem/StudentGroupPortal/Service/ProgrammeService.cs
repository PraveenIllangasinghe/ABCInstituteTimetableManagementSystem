using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.StudentGroupPortal.Service
{
    class ProgrammeService
    {
        public bool update(Program program)
        {
            bool temp1 = false;
            try
            {

                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "update Progrmme set programme = @programme where id = @id";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@programme", program.programme);
                cmd1.Parameters.AddWithValue("@id", program.id);


                cmd1.ExecuteNonQuery();
                temp1 = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return temp1;
        }

        public bool save(Program prgrm)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "insert into Progrmme (programme) values (@programme)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@programme", prgrm.programme);
               


                cmd1.ExecuteNonQuery();
                con1.closeConnection();
                temp1 = true;

            }
            catch (Exception ew)
            {

                throw;
            }


            return temp1;
        }
        public DataTable fetchProgrammeData(string prop, string value)
        {
            try
            {
                DataTable dt = new DataTable();
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = $"select * from Progrmme where {prop} = '{value}'";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());
                SqlDataAdapter ada = new SqlDataAdapter(cmd1);
                ada.Fill(dt);
                con1.closeConnection();
                return dt;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool deleteProgramme(int id)
        {
            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "delete  Progrmme where id = @id";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@id", id);


                cmd1.ExecuteNonQuery();
                con1.closeConnection();
                temp1 = true;
            }
            catch (Exception ex)
            {

                temp1 = false;
            }
            return temp1;
        }
    }
}
