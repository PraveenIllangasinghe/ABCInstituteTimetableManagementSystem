using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.StudentGroupPortal.Service
{
    class GroupIDsServices
    {

        public bool save(SubGroupId allSubDetails)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "insert into GroupID (groupId) values (@Group)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                //cmd1.Parameters.AddWithValue("@Group", grpNo);



                cmd1.ExecuteNonQuery();
                temp1 = true;

            }
            catch (Exception ew)
            {

                throw;
            }


            return temp1;
        }
                           

            public DataTable fetchAllDetailsPerField(string prop, string value)
        {
          
            try
            {
                DataTable dt = new DataTable();
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();
                string q;
                if (prop == "programme")
                {
                    q = $"select * from SubGroups where programme = '{value}'";
                } else
                {
                    int alteredValue = int.Parse(value);

                    q = $"select * from SubGroups where {prop} = {value}";
                }
                

                SqlCommand cmd1 = new SqlCommand(q, con1.getConnection());


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


        public DataTable fetchAllDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "select * from SubGroups";

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
        public bool saveSubGrpID(SubGroupId allSubDetails)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "insert into SubGroups (year, semester, programme, groupNo, subGroupNo, groupId, subGroupId) values (@year, @semester, @programme, @groupNo, @subGroupNo, @groupId, @subGroupId)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());


                cmd1.Parameters.AddWithValue("@year", allSubDetails.year);

                cmd1.Parameters.AddWithValue("@semester", allSubDetails.semester);

                cmd1.Parameters.AddWithValue("@programme", allSubDetails.programme);

                cmd1.Parameters.AddWithValue("@subGroupNo", allSubDetails.subGroupNo);

                cmd1.Parameters.AddWithValue("@groupNo", allSubDetails.groupNumber);


                cmd1.Parameters.AddWithValue("@groupId", allSubDetails.groupId);
                cmd1.Parameters.AddWithValue("@subGroupId", allSubDetails.subgroupId);



                cmd1.ExecuteNonQuery();
                temp1 = true;

            }
            catch (Exception ew)
            {

                throw;
            }


            return temp1;
        }


    }
}





