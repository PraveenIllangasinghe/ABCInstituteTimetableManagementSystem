using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.TagPortal.Service
{
    class TagNameService
    {
        public bool update(TagName tagName)
        {
            bool temp1 = false;
            try
            {

                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "update  Tags set TagName = @TagName where ID = @ID";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@TagName", tagName.tagName);
                cmd1.Parameters.AddWithValue("@ID", tagName.id);


                cmd1.ExecuteNonQuery();
                temp1 = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return temp1;
        }

        public bool save(TagName tagName)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "insert into Tags (TagName) values (@TagName)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@TagName", tagName.tagName);



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
        public DataTable fetchTagData(string prop, string value)
        {
            try
            {
                DataTable dt = new DataTable();
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = $"select * from Tags where {prop} = '{value}'";

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
        public bool deleteTag(int id)
        {
            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "delete  Tags where ID = @ID";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@ID", id);


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