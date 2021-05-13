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

namespace ABCInstituteTimetableManagementSystem.SubjectPortal
{
    public partial class Subjects : Form
    {

        public String SubjectCode;
        public int val = 0;

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

        public Subjects()
        {
            InitializeComponent();
        }

        public string connectionString = "Data Source=DESKTOP-5SU6VUS\\SQLEXPRESS;Initial Catalog=ABCInstituteDB;Integrated Security=True";

        //Add Subjects****************************************************************************************
        private void SubjectAddBtn_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(connectionString);

            connect.Open();
            if (connect.State == System.Data.ConnectionState.Open)
            {
                string query = "INSERT INTO Subject (ID,SubjectCode,SubjectName,OfferedYear,OfferedSemester,NoOfLectureHours,NoOfTutorialHours,NoOfLabHours,NoOfEvaluationHours) VALUES (5,@SubjectCode,@SubjectName,@OfferedYear,@OfferedSemester,@NoOfLectureHours,@NoOfTutorialHours,@NoOfLabHours,@NoOfEvaluationHours)";
                SqlCommand cmd = new SqlCommand(query, connect);
                
               
                cmd.Parameters.AddWithValue("@SubjectCode", SubjectTxtSubjectCode.Text);
                cmd.Parameters.AddWithValue("@SubjectName", SubjectTxtSubjectName.Text);
                cmd.Parameters.AddWithValue("@OfferedYear", SubjectSelectYear.SelectedItem);


                if (SubjectRadioBtnSemesterOne.Checked)
                    cmd.Parameters.AddWithValue("@OfferedSemester", 1);
                else
                    cmd.Parameters.AddWithValue("@OfferedSemester", 2);

                cmd.Parameters.AddWithValue("@NoOfLectureHours", SubjectNoOfLecHrs.Value);
                cmd.Parameters.AddWithValue("@NoOfTutorialHours", SubjectNoOfTuteHrs.Value);
                cmd.Parameters.AddWithValue("@NoOfLabHours", SubjectNoOfLabHrs.Value);
                cmd.Parameters.AddWithValue("@NoOfEvaluationHours", SubjectNoOfEvalHrs.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Subject has been Added Successfully...");

                ClearFields();
                SubjectPortalTabControl.SelectedTab = ViewSubjectsTabPage;
            }
        }
        //******************************************************************************************************


        private void Subjects_Load_1(object sender, EventArgs e)
        {
            ViewSubjects();
            SubjectPortalTabControl.SelectedTab = ViewSubjectsTabPage;
        }


       
        //Additional event to refresh********************************************
        private void ViewSubjectsTabPage_Click(object sender, EventArgs e)
        {
            ViewSubjects();
            SubjectPortalTabControl.SelectedTab = ViewSubjectsTabPage;
        }
        //***********************************************************************


        //View Subjects**********************************************************
        private void ViewSubjects()
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from Subject", con);
            DataTable dataTable = new DataTable();

            con.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            con.Close();

            SubjectsDataGridView.AutoGenerateColumns = true;
            SubjectsDataGridView.DataSource = dataTable;


        }

        //************************************************************************************


        //Navigate to Manage Subjects and Auto fill details of the selected item****************************
        private void SubjectsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SubjectCode = SubjectsDataGridView.CurrentRow.Cells[1].Value.ToString();
            TxtBoxSubjectCodeUpdate.Text = SubjectsDataGridView.CurrentRow.Cells[1].Value.ToString();
            TxtBoxSubjectNameUpdate.Text = SubjectsDataGridView.CurrentRow.Cells[2].Value.ToString();
            ComboBoxOfferedYearUpdate.SelectedItem = SubjectsDataGridView.CurrentRow.Cells[3].Value;

            if (SubjectsDataGridView.CurrentRow.Cells[4].Value.ToString() == "1")
            {
                RadioBtnSem1Update.Checked = true;
                RadioBtnSem2Update.Checked = false;
            }
            else
            {
                RadioBtnSem1Update.Checked = false;
                RadioBtnSem2Update.Checked = true;
            }

            NumUpDownLecHrsUpdate.Value = Convert.ToDecimal(SubjectsDataGridView.CurrentRow.Cells[5].Value);
            NumUpDownTuteHrsUpdate.Value = Convert.ToDecimal(SubjectsDataGridView.CurrentRow.Cells[6].Value);
            NumUpDownLabHrsUpdate.Value = Convert.ToDecimal(SubjectsDataGridView.CurrentRow.Cells[7].Value);
            NumUpDownEvalHrsUpdate.Value = Convert.ToDecimal(SubjectsDataGridView.CurrentRow.Cells[8].Value);
            val = 1;
            SubjectPortalTabControl.SelectedTab = ManageSubjectsTabPage;


        }

        //*********************************************************************************************************


        //Update Subject*****************************************************************************************
        private void ManageSubUpdateBtn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                //if (IsValidUpdate())
                //{
                    SqlConnection connection = new SqlConnection(connectionString);

                    SqlCommand sqlCommand = new SqlCommand("UPDATE Subject SET SubjectCode = @UpdatedSubjectCode, SubjectName = @UpdatedSubjectName, OfferedYear = @UpdatedOfferedYear, OfferedSemester = @UpdatedOfferedSemester, NoOfLectureHours = @UpdatedNoOfLectureHours, NoOfTutorialHours = @UpdatedNoOfTutorialHours, NoOfLabHours = @UpdatedNoOfLabHours, NoOfEvaluationHours = @UpdatedNoOfEvalHours WHERE SubjectCode = @SubjectCode", connection);
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.Parameters.AddWithValue("@UpdatedSubjectCode", TxtBoxSubjectCodeUpdate.Text);
                    sqlCommand.Parameters.AddWithValue("@UpdatedSubjectName", TxtBoxSubjectNameUpdate.Text);
                    sqlCommand.Parameters.AddWithValue("@UpdatedOfferedYear", ComboBoxOfferedYearUpdate.Text);

                    if (SubjectRadioBtnSemesterOne.Checked)
                        sqlCommand.Parameters.AddWithValue("@UpdatedOfferedSemester", 1);
                    else
                        sqlCommand.Parameters.AddWithValue("@UpdatedOfferedSemester", 2);


                    sqlCommand.Parameters.AddWithValue("@UpdatedNoOfLectureHours", NumUpDownLecHrsUpdate.Text);
                    sqlCommand.Parameters.AddWithValue("@UpdatedNoOfTutorialHours", NumUpDownTuteHrsUpdate.Text);
                    sqlCommand.Parameters.AddWithValue("@UpdatedNoOfLabHours", NumUpDownLabHrsUpdate.Text);
                    sqlCommand.Parameters.AddWithValue("@UpdatedNoOfEvalHours", NumUpDownEvalHrsUpdate.Text);
                    sqlCommand.Parameters.AddWithValue("@SubjectCode", this.SubjectCode);

                    connection.Open();

                    sqlCommand.ExecuteNonQuery();

                    connection.Close();

                    MessageBox.Show("Subject Information has been Updated Sucessfully", "Confirmation");

                    //GetSubjects();

                    //ClearFieldsAfterUpdate();

                    //tabControlSubjects.SelectedTab = tabPageSubView;
               // }
            }
            else
            {
                MessageBox.Show("Please Select a Subject to Update ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClearUpdateFields();
            SubjectPortalTabControl.SelectedTab = ViewSubjectsTabPage;
        }

        //*******************************************************************************************************************************


        //Delete Subject*****************************************************************************************************************
        private void ManageSubDeleteBtn_Click(object sender, EventArgs e)
        {
            if (val > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);

                if (MessageBox.Show("This will Delete the Subject Permanently. Are You Sure?", "Delete Subject", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM Subject WHERE SubjectCode = @SubjectCode", connection);
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@SubjectCode", this.SubjectCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Subject has been Deleted", "Confirmation");

                    //GetSubjects();

                    //ClearFieldsAfterUpdate();

                   // tabControlSubjects.SelectedTab = tabPageSubView;
                }

            }
            else
            {
                MessageBox.Show("Please Select a Subject to Delete ", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClearUpdateFields();
            SubjectPortalTabControl.SelectedTab = ViewSubjectsTabPage;
        }

        //********************************************************************************************************************************

        private void ViewSubjectsSearchBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand filterLecturers = new SqlCommand("Select * from Subject WHERE SubjectName LIKE '%" + SearchTxtBoxViewSubjects.Text + "%'", con);
            DataTable dataTable = new DataTable();

            con.Open();

            SqlDataReader sqlDataReader = filterLecturers.ExecuteReader();

            dataTable.Load(sqlDataReader);
            con.Close();

            SubjectsDataGridView.AutoGenerateColumns = true;
            SubjectsDataGridView.DataSource = dataTable;
        }

        public void ClearFields()
        {
            SubjectTxtSubjectCode.Clear();
            SubjectTxtSubjectName.Clear();
            SubjectSelectYear.SelectedIndex = -1;
            SubjectRadioBtnSemesterOne.Checked = false;
            SubjectRadioBtnSemesterTwo.Checked = false;
            SubjectNoOfLecHrs.ResetText();
            SubjectNoOfTuteHrs.ResetText();
            SubjectNoOfLabHrs.ResetText();
            SubjectNoOfEvalHrs.ResetText();
        }

        private void SubjectClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        public void ClearUpdateFields()
        {
            TxtBoxSubjectCodeUpdate.Clear();
            TxtBoxSubjectNameUpdate.Clear();
            ComboBoxOfferedYearUpdate.SelectedIndex = -1;
            RadioBtnSem1Update.Checked = false;
            RadioBtnSem2Update.Checked = false;
            NumUpDownLecHrsUpdate.ResetText();
            NumUpDownTuteHrsUpdate.ResetText();
            NumUpDownLabHrsUpdate.ResetText();
            NumUpDownEvalHrsUpdate.ResetText();
        }

        private void ManageSubClearBtn_Click(object sender, EventArgs e)
        {
            ClearUpdateFields();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LecturerPortal.Lecturers navSubPortal = new LecturerPortal.Lecturers();
            navSubPortal.Show();
            this.Hide();
        }
    }
}
