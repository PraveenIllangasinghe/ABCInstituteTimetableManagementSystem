using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABCInstituteTimetableManagementSystem.StudentGroupPortal.Service
{
    class YearSemesterService
    {
        public bool update(YearSemester yrSem)
        {
            bool temp1 = false;
            try
            {

                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "update  YearSem set year = @year, semester = @semester where id = @id";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@year", yrSem.year);
                cmd1.Parameters.AddWithValue("@semester", yrSem.semester);
                cmd1.Parameters.AddWithValue("@id", yrSem.id);


                cmd1.ExecuteNonQuery();
                temp1 = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return temp1;
        }
        public bool save(YearSemester yrSem)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "insert into YearSem (year, semester) values (@year,@semester)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@year", yrSem.year);
                cmd1.Parameters.AddWithValue("@semester",yrSem.semester );
            

                cmd1.ExecuteNonQuery();
                temp1 = true;

            }
            catch (Exception ew)
            {

                throw;
            }


            return temp1;
        }
        public DataTable fetchYearSemData(string prop, int value)
        {
            try
            {
                DataTable dt = new DataTable();
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = $"select * from YearSem where {prop} = {value}";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

              //  cmd1.Parameters.AddWithValue("@year", yrSem.year);
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
        public bool deleteYearSem(int id)
        {
            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "delete  YearSem where id = @id";

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
