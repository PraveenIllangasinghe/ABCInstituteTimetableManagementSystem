using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ABCInstituteTimetableManagementSystem.SessionPortal
{
    public partial class Sessions : Form
    {

        //Move Window ************************************

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {

                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;

                    return;

            }

            base.WndProc(ref m);
        }

        //*************************************************


        public Sessions()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=DESKTOP-5SU6VUS\\SQLEXPRESS;Initial Catalog=ABCInstituteDB;Integrated Security=True";

        private void Session_Create_Btn_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("INSERT INTO Session (ID,LecturerName,SubjectCode,SubjectName,Tag,GroupID,StudentCount,Duration) VALUES (8, @LecturerName, @SubjectCode, @SubjectName, @Tag, @GroupID, @StudentCount, @Duration)", connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@LecturerName", CB_Lecturer_Add.Text);
            cmd.Parameters.AddWithValue("@SubjectCode", CB_SubCode_Add.Text);
            cmd.Parameters.AddWithValue("@SubjectName", CB_SubName_Add.Text);
            cmd.Parameters.AddWithValue("@Tag", CB_Tag_Add.Text);
            cmd.Parameters.AddWithValue("@GroupID", CB_Group_Add.Text);
            cmd.Parameters.AddWithValue("@StudentCount", Txt_NoOfStudents_Add.Text);
            cmd.Parameters.AddWithValue("@Duration", Num_Duration_Add.Value);

            connection.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Session has been Created Successfully...");

            ClearFields();
            SessionTabControl.SelectedTab = ViewSessionsTabPage;
        }



        //Clear Fields
        public void ClearFields()
        {
            CB_Lecturer_Add.SelectedIndex = -1;
            CB_SubCode_Add.SelectedIndex = -1;
            CB_SubName_Add.SelectedIndex = -1;
            CB_Tag_Add.SelectedIndex = -1;
            CB_Group_Add.SelectedIndex = -1;
            Txt_NoOfStudents_Add.Clear();
            Num_Duration_Add.ResetText();
        }


        //Load Values to Dropdown Menus
        public void Populate_CB_Lecturer_Add()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            CB_Lecturer_Add.Items.Clear();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT LecturerName FROM Lecturer";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                CB_Lecturer_Add.Items.Add(dr["LecturerName"].ToString());
            }

            connection.Close();
        }


        public void Populate_CB_SubCode_Add()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            CB_SubCode_Add.Items.Clear();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT SubjectCode FROM Subject";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                CB_SubCode_Add.Items.Add(dr["SubjectCode"].ToString());
            }

            connection.Close();
        }


        public void Populate_CB_SubName_Add()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            CB_SubName_Add.Items.Clear();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT SubjectName FROM Subject";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                CB_SubName_Add.Items.Add(dr["SubjectName"].ToString());
            }

            connection.Close();
        }

        public void Populate_CB_Tag_Add()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            CB_Tag_Add.Items.Clear();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TagName FROM Tags";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                CB_Tag_Add.Items.Add(dr["TagName"].ToString());
            }

            connection.Close();
        }


        public void Populate_CB_Group_Add()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            CB_Group_Add.Items.Clear();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT groupId FROM SubGroups";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                CB_Group_Add.Items.Add(dr["groupId"].ToString());
            }

            connection.Close();
        }


        //****************************************************************************************************


        //Call the Populate Combo Box Methods
        private void CB_Lecturer_Add_DropDown(object sender, EventArgs e)
        {
            Populate_CB_Lecturer_Add();
        }

        private void CB_SubCode_Add_DropDown(object sender, EventArgs e)
        {
            Populate_CB_SubCode_Add();
        }

        private void CB_SubName_Add_DropDown(object sender, EventArgs e)
        {
            Populate_CB_SubName_Add();
        }

        private void CB_Tag_Add_DropDown(object sender, EventArgs e)
        {
            Populate_CB_Tag_Add();
        }

        private void CB_Group_Add_DropDown(object sender, EventArgs e)
        {
            Populate_CB_Group_Add();
        }


        //*****************************************************************************************************
    }

}
