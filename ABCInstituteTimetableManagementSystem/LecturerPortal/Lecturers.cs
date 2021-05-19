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

namespace ABCInstituteTimetableManagementSystem.LecturerPortal
{
    public partial class Lecturers : Form
    {
        public int Lec_Rank;
        public int level;
        public String EmployeeID;
        public int val = 0;
        public int ID;


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



        public Lecturers()
        {
            InitializeComponent();
        }

        public string connectionString = (@"Server=tcp:abc-insstitute-server.database.windows.net,1433;Initial Catalog=abcinstitute-datbase;Persist Security Info=False;User ID=dbuser;Password=1qaz!QAZ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        private void AddLecturerBtn_Click(object sender, EventArgs e)
        {

            //Auto Increment Table Records****************************************************************************************************************************

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand comm = connection.CreateCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT COUNT(ID) AS ID FROM Lecturer";
            comm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);

            int nxt=0;
            foreach (DataRow dr in dt.Rows)
            {
                string next = dr["ID"].ToString();
                nxt = Int16.Parse(next);
                nxt = ++nxt;
            }
            connection.Close();

            //*********************************************************************************************************************************************************

            SqlCommand cmd = new SqlCommand("INSERT INTO Lecturer (ID,LecturerName,EmployeeID,Level,Building,Department,Faculty,Center,Rank) VALUES (@AIVal, @LecturerName, @EmployeeID, @Level, @Building, @Department, @Faculty, @Center, @Rank)", connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@LecturerName", TxtBoxLecturerNameAdd.Text);
            cmd.Parameters.AddWithValue("@EmployeeID", TxtBoxEmpIDAdd.Text);
            //cmd.Parameters.AddWithValue("@Level", ComboBoxLevelAdd.Text);
            cmd.Parameters.AddWithValue("@Building", ComboBoxBuildingAdd.Text);
            cmd.Parameters.AddWithValue("@Department", ComboBoxDepartmentAdd.Text);
            cmd.Parameters.AddWithValue("@Faculty", ComboBoxFacultyAdd.Text);
            cmd.Parameters.AddWithValue("@Center", ComboBoxCenterAdd.Text);
            cmd.Parameters.AddWithValue("@AIVal", nxt);

            if (ComboBoxLevelAdd.Text.Equals("Professor"))
            {
                level = 1;
            }
            else if (ComboBoxLevelAdd.Text.Equals("Assistant Professor"))
            {
                level = 2;
            }
            else if (ComboBoxLevelAdd.Text.Equals("Senior Lecturer(HG)"))
            {
                level = 3;
            }
            else if (ComboBoxLevelAdd.Text.Equals("Senior Lecturer"))
            {
                level = 4;
            }
            else if (ComboBoxLevelAdd.Text.Equals("Lecturer"))
            {
                level = 5;
            }
            else if (ComboBoxLevelAdd.Text.Equals("Assistant Lecturer"))
            {
                level = 6;
            }


            cmd.Parameters.AddWithValue("@Level", level);
            cmd.Parameters.AddWithValue("@Rank", TxtBoxRankAdd.Text);

            connection.Open();
            cmd.ExecuteNonQuery();

            MessageBox.Show("Lecturer has been Added Successfully...");

            ClearFields();
            LecturerPortalTabControl.SelectedTab = ViewLecturersTabPage;
        }

        private void LecRankGenBtn_Click(object sender, EventArgs e)
        {
            String ConcatLevel = "";
            String ConcatEmpID;
            String ConcatRank;

            if (ComboBoxLevelAdd.Text.Equals("Professor"))
            {
                ConcatLevel = "1";
            }
            else if (ComboBoxLevelAdd.Text.Equals("Assistant Professor"))
            {
                ConcatLevel = "2";
            }
            else if (ComboBoxLevelAdd.Text.Equals("Senior Lecturer(HG)"))
            {
                ConcatLevel = "3";
            }
            else if (ComboBoxLevelAdd.Text.Equals("Senior Lecturer"))
            {
                ConcatLevel = "4";
            }
            else if (ComboBoxLevelAdd.Text.Equals("Lecturer"))
            {
                ConcatLevel = "5";
            }
            else if (ComboBoxLevelAdd.Text.Equals("Assistant Lecturer"))
            {
                ConcatLevel = "6";
            }


            ConcatEmpID = TxtBoxEmpIDAdd.Text;
            ConcatRank = ConcatLevel + "." + ConcatEmpID;

            TxtBoxRankAdd.Text = ConcatRank;

        }


        private void ViewLecturersTabPage_Click(object sender, EventArgs e)
        {
            ViewLecturers();
            LecturerPortalTabControl.SelectedTab = ViewLecturersTabPage;
        }

        private void Lecturers_Load(object sender, EventArgs e)
        {
            ViewLecturers();
            LecturerPortalTabControl.SelectedTab = ViewLecturersTabPage;
        }


        //View Lecturers**********************************************************
        private void ViewLecturers()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from Lecturer", con);
            DataTable dataTable = new DataTable();

            con.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            con.Close();

            LecturersDataGridView.AutoGenerateColumns = true;
            LecturersDataGridView.DataSource = dataTable;


        }

        //************************************************************************************

        private void LecturersDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EmployeeID = LecturersDataGridView.CurrentRow.Cells[2].Value.ToString();
            TxtBoxLecNameUpdate.Text = LecturersDataGridView.CurrentRow.Cells[1].Value.ToString();
            TxtBoxEmpIDUpdate.Text = LecturersDataGridView.CurrentRow.Cells[2].Value.ToString();
            
            if (LecturersDataGridView.CurrentRow.Cells[3].Value.Equals(1))
            {
                ComboBoxLevelUpdate.SelectedItem = "Professor";
            }
            else if (LecturersDataGridView.CurrentRow.Cells[3].Value.Equals(2))
            {
                ComboBoxLevelUpdate.SelectedItem = "Assistant Professor";
            }
            else if (LecturersDataGridView.CurrentRow.Cells[3].Value.Equals(3))
            {
                ComboBoxLevelUpdate.SelectedItem = "Senior Lecturer(HG)";
            }
            else if (LecturersDataGridView.CurrentRow.Cells[3].Value.Equals(4))
            {
                ComboBoxLevelUpdate.SelectedItem = "Senior Lecturer";
            }
            else if (LecturersDataGridView.CurrentRow.Cells[3].Value.Equals(5))
            {
                ComboBoxLevelUpdate.SelectedItem = "Lecturer";
            }
            else if (LecturersDataGridView.CurrentRow.Cells[3].Value.Equals(6))
            {
                ComboBoxLevelUpdate.SelectedItem = "Assistant Lecturer";
            }


            //ComboBoxLevelUpdate.SelectedItem = LecturersDataGridView.CurrentRow.Cells[3].Value;
            ComboBoxBuildingUpdate.SelectedItem = LecturersDataGridView.CurrentRow.Cells[4].Value;
            ComboBoxDeptUpdate.SelectedItem = LecturersDataGridView.CurrentRow.Cells[5].Value;
            ComboBoxFacultyUpdate.SelectedItem = LecturersDataGridView.CurrentRow.Cells[6].Value;
            ComboBoxCenterUpdate.SelectedItem = LecturersDataGridView.CurrentRow.Cells[7].Value;
            TxtBoxRankUpdate.Text = LecturersDataGridView.CurrentRow.Cells[8].Value.ToString();
            val = 1;
            LecturerPortalTabControl.SelectedTab = ManageLecturersTabPage;
        }

        private void ManageLecUpdateBtn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                //if (IsValidUpdate())
                //{
                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand("UPDATE Lecturer SET LecturerName = @UpdatedLecturerName, EmployeeID = @UpdatedEmployeeID, Level = @UpdatedLevel, Building = @UpdatedBuilding, Department = @UpdatedDepartment, Faculty = @UpdatedFaculty, Center = @UpdatedCenter, Rank = @UpdatedRank WHERE EmployeeID = @EmpID", connection);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@UpdatedLecturerName", TxtBoxLecNameUpdate.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedEmployeeID", TxtBoxEmpIDUpdate.Text);

                if (ComboBoxLevelUpdate.Text.Equals("Professor"))
                {
                    level = 1;
                }
                else if (ComboBoxLevelUpdate.Text.Equals("Assistant Professor"))
                {
                    level = 2;
                }
                else if (ComboBoxLevelUpdate.Text.Equals("Senior Lecturer(HG)"))
                {
                    level = 3;
                }
                else if (ComboBoxLevelUpdate.Text.Equals("Senior Lecturer"))
                {
                    level = 4;
                }
                else if (ComboBoxLevelUpdate.Text.Equals("Lecturer"))
                {
                    level = 5;
                }
                else if (ComboBoxLevelUpdate.Text.Equals("Assistant Lecturer"))
                {
                    level = 6;
                }

                sqlCommand.Parameters.AddWithValue("@UpdatedLevel", level);
                sqlCommand.Parameters.AddWithValue("@UpdatedBuilding", ComboBoxBuildingUpdate.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedDepartment", ComboBoxDeptUpdate.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedFaculty", ComboBoxFacultyUpdate.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedCenter", ComboBoxCenterUpdate.Text);
                sqlCommand.Parameters.AddWithValue("@UpdatedRank", TxtBoxRankUpdate.Text);
                sqlCommand.Parameters.AddWithValue("@EmpID", this.EmployeeID); 

                connection.Open();

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Lecturer Information has been Updated Sucessfully", "Confirmation");

                //GetSubjects();

                //ClearFieldsAfterUpdate();

                //tabControlSubjects.SelectedTab = tabPageSubView;
                // }


                ClearUpdateFields();
                LecturerPortalTabControl.SelectedTab = ViewLecturersTabPage;
            }
            else
            {
                MessageBox.Show("Please Select a Lecturer to Update ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RankGenUpdateBtn_Click(object sender, EventArgs e)
        {
            String ConcatLevel = "";
            String ConcatEmpID;
            String ConcatRank;

            if (ComboBoxLevelUpdate.Text.Equals("Professor"))
            {
                ConcatLevel = "1";
            }
            else if (ComboBoxLevelUpdate.Text.Equals("Assistant Professor"))
            {
                ConcatLevel = "2";
            }
            else if (ComboBoxLevelUpdate.Text.Equals("Senior Lecturer(HG)"))
            {
                ConcatLevel = "3";
            }
            else if (ComboBoxLevelUpdate.Text.Equals("Senior Lecturer"))
            {
                ConcatLevel = "4";
            }
            else if (ComboBoxLevelUpdate.Text.Equals("Lecturer"))
            {
                ConcatLevel = "5";
            }
            else if (ComboBoxLevelUpdate.Text.Equals("Assistant Lecturer"))
            {
                ConcatLevel = "6";
            }


            ConcatEmpID = TxtBoxEmpIDUpdate.Text;
            ConcatRank = ConcatLevel + "." + ConcatEmpID;

            TxtBoxRankUpdate.Text = ConcatRank;
        }

        private void ManageLecDeleteBtn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if (MessageBox.Show("This wiil Delete the Lecturer permanently. Are You Sure?", "Delete Lecturer", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Lecturer WHERE EmployeeID = @EmpID", connection);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@EmpID", this.EmployeeID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Lecturer has been Deleted", "Confirmation");

                    //GetSubjects();

                    //ClearFieldsAfterUpdate();

                    // tabControlSubjects.SelectedTab = tabPageSubView;

                }

            }
            else
            {
                MessageBox.Show("Please Select a Lecturer to Delete ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            ClearUpdateFields();
            LecturerPortalTabControl.SelectedTab = ViewLecturersTabPage;

        }

        private void ManageLecRefreshBtn_Click(object sender, EventArgs e)
        {
            ViewLecturers();
        }

        private void ComboBoxLecNameAATime_DropDown(object sender, EventArgs e)
        {
            PopulateLecNameAATime_ComboBox();
        }


        public void PopulateLecNameAATime_ComboBox()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            ComboBoxLecNameAATime.Items.Clear();
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
                ComboBoxLecNameAATime.Items.Add(dr["LecturerName"].ToString());
            }

            connection.Close();
        }

        private void FullDayBtn_Click(object sender, EventArgs e)
        {
            ComboBoxStartTimeAATime.SelectedItem = "8.30";
            ComboBoxEndTimeAATime.SelectedItem = "5.30";
        }

        //Add Available Time****************************************************************************************
        private void AATimeSaveBtn_Click(object sender, EventArgs e)
        {

            //Auto Increment Table Records****************************************************************************************************************************

            SqlConnection sqlcon = new SqlConnection(connectionString);

            sqlcon.Open();
            SqlCommand comm = sqlcon.CreateCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT COUNT(ID) AS ID FROM LecturerAvailableTime";
            comm.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(dt);

            int aat_nxt = 0;
            foreach (DataRow dr in dt.Rows)
            {
                string aat_next = dr["ID"].ToString();
                aat_nxt = Int16.Parse(aat_next);
                aat_nxt = ++aat_nxt;
            }
            sqlcon.Close();

            //*********************************************************************************************************************************************************


            SqlConnection connect = new SqlConnection(connectionString);

                connect.Open();
                if (connect.State == System.Data.ConnectionState.Open)
                {
                    string query = "INSERT INTO LecturerAvailableTime (ID,LecturerName,Day,StartTime,EndTime) VALUES (@AutoIncAAT,@LecName,@Day,@StartTime,@EndTime)";
                    SqlCommand cmd = new SqlCommand(query, connect);


                    cmd.Parameters.AddWithValue("@LecName", ComboBoxLecNameAATime.SelectedItem);
                    cmd.Parameters.AddWithValue("@Day", ComboBoxDayAATime.SelectedItem);
                    cmd.Parameters.AddWithValue("@StartTime", ComboBoxStartTimeAATime.SelectedItem);
                    cmd.Parameters.AddWithValue("@EndTime", ComboBoxEndTimeAATime.SelectedItem);
                    cmd.Parameters.AddWithValue("@AutoIncAAT", aat_nxt);

                cmd.ExecuteNonQuery();
                    MessageBox.Show("Lecturer Available Time has been Saved Successfully...");

                    ViewAvailableTime();

                }
            
        }
        //******************************************************************************************************


        private void AddAv_Click(object sender, EventArgs e)
        {
            ViewAvailableTime();
        }


        //View Subjects**********************************************************
        private void ViewAvailableTime()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from LecturerAvailableTime", con);
            DataTable dataTable = new DataTable();

            con.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            con.Close();

            AddAvailableTimeDataGridView.AutoGenerateColumns = true;
            AddAvailableTimeDataGridView.DataSource = dataTable;


        }

        private void AddAvailableTimeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PopulateLecNameAATime_ComboBox();

            ID = Convert.ToInt32(AddAvailableTimeDataGridView.CurrentRow.Cells[0].Value.ToString());
            ComboBoxLecNameAATime.SelectedItem = AddAvailableTimeDataGridView.CurrentRow.Cells[1].Value;
            ComboBoxDayAATime.SelectedItem = AddAvailableTimeDataGridView.CurrentRow.Cells[2].Value;
            ComboBoxStartTimeAATime.SelectedItem = AddAvailableTimeDataGridView.CurrentRow.Cells[3].Value;
            ComboBoxEndTimeAATime.SelectedItem = AddAvailableTimeDataGridView.CurrentRow.Cells[4].Value;
            val = 1;
        }

        private void AATimeUpdateBtn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {

                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand("UPDATE LecturerAvailableTime SET LecturerName = @LecNameUpdated, Day = @DayUpdated, StartTime = @StartTimeUpdated, EndTime = @EndTimeUpdated WHERE ID = @ID", connection);
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@LecNameUpdated", ComboBoxLecNameAATime.SelectedItem);
                sqlCommand.Parameters.AddWithValue("@DayUpdated", ComboBoxDayAATime.SelectedItem);
                sqlCommand.Parameters.AddWithValue("@StartTimeUpdated", ComboBoxStartTimeAATime.SelectedItem);
                sqlCommand.Parameters.AddWithValue("@EndTimeUpdated", ComboBoxEndTimeAATime.SelectedItem);
                sqlCommand.Parameters.AddWithValue("@ID", this.ID);

                connection.Open();

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Lecturer Available Time has been Updated Sucessfully", "Confirmation");

                
            }
            else
            {
                MessageBox.Show("Please Select an Available Time to Update ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AATimeDeleteBtn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if (MessageBox.Show("This will Delete this Available Time permanently. Are You Sure?", "Delete Available Time", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM LecturerAvailableTime WHERE ID = 4", connection);
                    command.CommandType = CommandType.Text;

                    //command.Parameters.AddWithValue("@ID", this.ID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Lecturer Available Time has been Deleted", "Confirmation");

                }

            }
            else
            {
                MessageBox.Show("Please Select an Available Time to Delete ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ViewAvailableTime();
        }

        private void ComboBoxSearchViewLec_DropDown(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            ComboBoxSearchViewLec.Items.Clear();
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
                ComboBoxSearchViewLec.Items.Add(dr["LecturerName"].ToString());
            }

            connection.Close();
        }

        private void SearchBtnViewLec_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand filterLecturers = new SqlCommand("Select * from Lecturer WHERE LecturerName = @LecturerSearchName", con);
            DataTable dataTable = new DataTable();
            filterLecturers.Parameters.AddWithValue("@LecturerSearchName", ComboBoxSearchViewLec.SelectedItem);

            con.Open();

            SqlDataReader sqlDataReader = filterLecturers.ExecuteReader();

            dataTable.Load(sqlDataReader);
            con.Close();

            LecturersDataGridView.AutoGenerateColumns = true;
            LecturersDataGridView.DataSource = dataTable;
        }

        public void ClearFields()
        {
            TxtBoxEmpIDAdd.Clear();
            TxtBoxLecturerNameAdd.Clear();
            ComboBoxFacultyAdd.SelectedIndex = -1;
            ComboBoxDepartmentAdd.SelectedIndex = -1;
            ComboBoxCenterAdd.SelectedIndex = -1;
            ComboBoxBuildingAdd.SelectedIndex = -1;
            ComboBoxLevelAdd.SelectedIndex = -1;
            TxtBoxRankAdd.Clear();
        }

        private void AddLecturerClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        public void ClearUpdateFields()
        {
            TxtBoxEmpIDUpdate.Clear();
            TxtBoxLecNameUpdate.Clear();
            ComboBoxFacultyUpdate.SelectedIndex = -1;
            ComboBoxDeptUpdate.SelectedIndex = -1;
            ComboBoxCenterUpdate.SelectedIndex = -1;
            ComboBoxBuildingUpdate.SelectedIndex = -1;
            ComboBoxLevelUpdate.SelectedIndex = -1;
            TxtBoxRankUpdate.Clear();
        }

        private void ManageLecClearBtn_Click(object sender, EventArgs e)
        {
            ClearUpdateFields();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubjectPortal.Subjects navSubPortal = new SubjectPortal.Subjects();
            navSubPortal.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Close App******************************************

            Application.Exit();

            //***************************************************
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Minimize App***************************************

            this.WindowState = FormWindowState.Minimized;

            //***************************************************
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Back to Home***************************************

            Form1 navSubPortal = new Form1();
            navSubPortal.Show();
            this.Hide();

            //****************************************************
        }
    }
}
