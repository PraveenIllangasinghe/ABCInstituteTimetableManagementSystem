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

        public int val = 0;
        public String SessionID;

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

        public string connectionString = (@"Server=tcp:abc-insstitute-server.database.windows.net,1433;Initial Catalog=abcinstitute-datbase;Persist Security Info=False;User ID=dbuser;Password=1qaz!QAZ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

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


        //Load Values to Dropdown Menus in Add Session
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
            cmd.Parameters.AddWithValue("@SubjectCode", CB_SubCode_Add.Text);
            cmd.CommandText = "SELECT SubjectName FROM Subject WHERE SubjectCode = @SubjectCode";
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


        //******************************************************************************************************


        
        private void ViewSessionsTabPage_Click(object sender, EventArgs e)
        {
            ViewSessions();
            SessionTabControl.SelectedTab = ViewSessionsTabPage;
        }

        //View Sesions**********************************************************
        private void ViewSessions()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from Session", con);
            DataTable dataTable = new DataTable();

            con.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            con.Close();

            Session_DataGridView.AutoGenerateColumns = true;
            Session_DataGridView.DataSource = dataTable;


        }

        //********************************************************************************************


        //Cell Click to Auto Fill
        private void Session_DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            Populate_MCB_Lecturer_Name();
            Populate_MCB_Sub_Code();
            Populate_MCB_SubName();
            Populate_MCB_Tag_Sess();
            Populate_MCB_Group_ID_Sess();

            SessionID = Session_DataGridView.CurrentRow.Cells[0].Value.ToString();
            MCB_Lecturer_Name.SelectedItem = Session_DataGridView.CurrentRow.Cells[1].Value;
            MCB_Sub_Code.SelectedItem = Session_DataGridView.CurrentRow.Cells[2].Value;
            MCB_SubName.SelectedItem = Session_DataGridView.CurrentRow.Cells[3].Value;
            MCB_Tag_Sess.SelectedItem = Session_DataGridView.CurrentRow.Cells[4].Value;
            MCB_Group_ID_Sess.SelectedItem = Session_DataGridView.CurrentRow.Cells[5].Value;
            Mtxt_NoOfStudents.Text = Session_DataGridView.CurrentRow.Cells[6].Value.ToString();
            M_num_duration.Value = Convert.ToDecimal(Session_DataGridView.CurrentRow.Cells[7].Value);


            val = 1;
            SessionTabControl.SelectedTab = ManageSessionsTabPage;
        }
        //*****************************************************************************************************




        //Populate Manage Session fields
        public void Populate_MCB_Lecturer_Name()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            MCB_Lecturer_Name.Items.Clear();
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
                MCB_Lecturer_Name.Items.Add(dr["LecturerName"].ToString());
            }

            connection.Close();
        }


        public void Populate_MCB_Sub_Code()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            MCB_Sub_Code.Items.Clear();
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
                MCB_Sub_Code.Items.Add(dr["SubjectCode"].ToString());
            }

            connection.Close();
        }


        public void Populate_MCB_SubName()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            MCB_SubName.Items.Clear();
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
                MCB_SubName.Items.Add(dr["SubjectName"].ToString());
            }

            connection.Close();
        }

        public void Populate_MCB_Tag_Sess()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            MCB_Tag_Sess.Items.Clear();
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
                MCB_Tag_Sess.Items.Add(dr["TagName"].ToString());
            }

            connection.Close();
        }


        public void Populate_MCB_Group_ID_Sess()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            MCB_Group_ID_Sess.Items.Clear();
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
                MCB_Group_ID_Sess.Items.Add(dr["groupId"].ToString());
            }

            connection.Close();
        }

        //****************************************************************************************************


        //Update Session Information
        private void Manage_Session_Upd_Btn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                
                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand("UPDATE Session SET LecturerName = @UpdatedLecturerName, SubjectCode = @UpdatedSubjectCode, SubjectName = @UpdatedSubjectName, Tag = @UpdatedTag, GroupID = @UpdatedGroupID, StudentCount = @UpdatedStudentCount, Duration = @UpdatedDuration WHERE ID = @SessionID", connection);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@UpdatedLecturerName", MCB_Lecturer_Name.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedSubjectCode", MCB_Sub_Code.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedSubjectName", MCB_SubName.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedTag", MCB_Tag_Sess.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedGroupID", MCB_Group_ID_Sess.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedStudentCount", Mtxt_NoOfStudents.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedDuration", M_num_duration.Text);
                sqlCommand.Parameters.AddWithValue("@SessionID", this.SessionID);

                connection.Open();

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Session Information has been Updated Sucessfully", "Confirmation");

            }
            else
            {
                MessageBox.Show("Please Select a Session to Update ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ClearUpdateFields();
            SessionTabControl.SelectedTab = ViewSessionsTabPage;
        }

        //*****************************************************************************************************


        //Delete Session
        private void Manage_Session_Del_Btn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if (MessageBox.Show("This will Delete the Session Permanently. Are You Sure?", "Delete Session", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Session WHERE ID = @SessID", connection);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@SessID", this.SessionID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Session has been Deleted", "Confirmation");

                }

            }
            else
            {
                MessageBox.Show("Please Select a Subject to Delete ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //ClearUpdateFields();
            SessionTabControl.SelectedTab = ViewSessionsTabPage;
        }


        //****************************************************************************************************

        private void SessionCloseBtn_Click(object sender, EventArgs e)
        {
            //Close App******************************************

            Application.Exit();

            //***************************************************
        }

        private void SessionMinBtn_Click(object sender, EventArgs e)
        {
            //Minimize App***************************************

            this.WindowState = FormWindowState.Minimized;

            //***************************************************
        }
    }

}
