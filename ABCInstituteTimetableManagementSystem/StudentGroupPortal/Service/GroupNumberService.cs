using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.StudentGroupPortal.Service
{
    class GroupNumberService
    {
        public bool update(GroupNumbers grp)
        {
            bool temp1 = false;
            try
            {

                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "update  GroupNo set groupNumber = @groupNumber where id = @id";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                cmd1.Parameters.AddWithValue("@groupNumber", grp.groupNumber);
               
                cmd1.Parameters.AddWithValue("@id", grp.id);


                cmd1.ExecuteNonQuery();
                temp1 = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return temp1;
        }
            public bool save(GroupNumbers grpNo)
            {

                bool temp1 = false;
                try
                {
                    Database.dbConnect con1 = new Database.dbConnect();
                    con1.openConnection();

                    string q1 = "insert into GroupNo (groupNumber) values (@groupNumber)";

                    SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                    cmd1.Parameters.AddWithValue("@groupNumber", grpNo.groupNumber);
                    


                    cmd1.ExecuteNonQuery();
                    temp1 = true;

                }
                catch (Exception ew)
                {

                    throw;
                }


                return temp1;
            }
            public DataTable fetchGropNoData(string prop, int value)
            {
                try
                {
                    DataTable dt = new DataTable();
                    Database.dbConnect con1 = new Database.dbConnect();
                    con1.openConnection();

                    string q1 = $"select * from GroupNo where {prop} = {value}";

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
            public bool deleteGroupNo(int id)
            {
                bool temp1 = false;
                try
                {
                    Database.dbConnect con1 = new Database.dbConnect();
                    con1.openConnection();

                    string q1 = "delete  GroupNo where id = @id";

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

