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
using System.Collections;

namespace ABCInstituteTimetableManagementSystem.GenerateTimetablePortal
{
    public partial class GenerateTimetables : Form
    {

        public GenerateTimetables()
        {
            InitializeComponent();
            DataGridLecturerTimeTable.RowTemplate.Height = 60;
            DataGridSGTimeTable.RowTemplate.Height = 60;
        }

        //Connection String
        public string connectionString = (@"Server=tcp:abc-insstitute-server.database.windows.net,1433;Initial Catalog=abcinstitute-datbase;Persist Security Info=False;User ID=dbuser;Password=1qaz!QAZ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        private void GenerateTimetables_Load(object sender, EventArgs e)
        {
            this.ComboBoxLecTimeTB.DataSource = null;
            ComboBoxLecTimeTB.Items.Clear();

            this.ComboBoxSGTimeTB.DataSource = null;
            ComboBoxSGTimeTB.Items.Clear();
        }

        //load lecturer names to the combo box
        public void PopulateComboBoxLecTimeTB()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            ComboBoxLecTimeTB.Items.Clear();
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
                ComboBoxLecTimeTB.Items.Add(dr["LecturerName"].ToString());
            }

            connection.Close();
        }

        private void ComboBoxLecTimeTB_DropDown(object sender, EventArgs e)
        {
            PopulateComboBoxLecTimeTB();
        }


        //Start Time
        public int num_of_hrs = 8;
        public int num_of_mins = 30;
        public int num_of_sec = 0;


        //Time Row Display
        private void TimeRowDisplay(int hours, int minutes, int seconds)
        {

            num_of_sec = num_of_sec + seconds;

            if (num_of_sec > 60)
                {
                    //Increment mins by 1 if no of seconds greater than 60
                    ++num_of_mins;
                    //Take remainder as seconds
                    num_of_sec = num_of_sec - 60;
                }

            num_of_mins = num_of_mins + minutes;

            if (num_of_mins > 60)
                {
                     //Increment hrs by 1 if no of mins greater than 60
                    ++num_of_hrs;
                    //Take remainder as mins
                    num_of_mins = num_of_mins - 60;
                }

            num_of_hrs = num_of_hrs + hours;
        }




        private void GenLecTimeTBbtn_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(connectionString);

            //Timetable Generation**************************************************************************************************************************

            //Initialize at start time 08:30:00
            num_of_sec = 0;
            num_of_mins = 30;
            num_of_hrs = 8;

            String sql = "select SubjectName,GroupID,SubjectCode,Tag,Duration from Session";

            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            DataTable data = new DataTable();

            SqlDataReader reader = cmd.ExecuteReader();
            data.Load(reader);

            con.Close();

            System.IO.StringWriter stringWriter;

            int days = 7;
            string result;

            //Set No of Columns - adtional col to display time
            DataGridLecturerTimeTable.ColumnCount = days+1;

            //Set Column Headers
            DataGridLecturerTimeTable.Columns[0].Name = "Time";
            DataGridLecturerTimeTable.Columns[1].Name = "Monday";
            DataGridLecturerTimeTable.Columns[2].Name = "Tuesday";
            DataGridLecturerTimeTable.Columns[3].Name = "Wednesday";
            DataGridLecturerTimeTable.Columns[4].Name = "Thursday";
            DataGridLecturerTimeTable.Columns[5].Name = "Friday";
            DataGridLecturerTimeTable.Columns[6].Name = "Saturday";
            DataGridLecturerTimeTable.Columns[7].Name = "Sunday";


           
            
            string[,] grid = new string[8, 8];


            for (int row = 0; row < grid.GetLength(0); row++){

                for (int column = 0; column < grid.GetLength(1); column++){

                    //Initialize with blanks cells
                    grid[row, column] = "  ";
                }

            }

            int rw = 1;
            int colm = 0;

            foreach (DataRow row in data.Rows)
            {
                stringWriter = new System.IO.StringWriter();

                foreach (DataColumn col in data.Columns)
                {
                    stringWriter.Write(row[col].ToString());
                }

                result = stringWriter.ToString();

                if (result.Length > 1)
                {
                    result = result.Substring(0, result.Length - 1);
                }

                if (colm == 5)
                {
                    colm = 0;
                    rw++;
                }
                    grid[colm, rw] = result;
                    colm++;

            }

            do
            {
                foreach (DataGridViewRow gdr in DataGridLecturerTimeTable.Rows)
                {
                    try
                    {
                        DataGridLecturerTimeTable.Rows.Remove(gdr);
                    }
                    catch (Exception) { }
                }
            } while (DataGridLecturerTimeTable.Rows.Count > 1);


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var cellContent = new ArrayList();

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    cellContent.Add(grid[i, j]);
                }

                string cell = (string)cellContent[1];
                string cellCon = cell.Substring(cell.Length - 1);

                //Structure of the Cell Display
                string[] row = new string[] {num_of_hrs + ":" + num_of_mins, (string) cellContent[1], (string) cellContent[2], (string) cellContent[3], (string) cellContent[4], (string) cellContent[5], (string) cellContent[6], (string) cellContent[7]};

                try
                {
                    TimeRowDisplay(int.Parse(cellCon.Trim()), 0, 0);
                }
                catch (Exception exception)
                {
                    //MessageBox.Show("Error while Generating...", "Confirmation");
                }

                DataGridLecturerTimeTable.Rows.Add(row);
            }

            //Select slots of given lecturer
            String remv = "select Distinct SubjectName from Session where LecturerName LIKE '%" + ComboBoxLecTimeTB.Text + "%'";

            SqlCommand cmdd = new SqlCommand(remv, con);
            con.Open();
            DataTable data2 = new DataTable();
            SqlDataReader reader2 = cmdd.ExecuteReader();
            data2.Load(reader2);

            con.Close();
            string lec = "";


            foreach (DataRow data_r in data2.Rows)
            {
                lec = data_r["SubjectName"].ToString();
            }

            //set null values
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    //remove other lecturers' slots
                    if (!(grid[i, j].ToString().Contains(lec))) {

                        DataGridLecturerTimeTable.Rows[i].Cells[j].Value = DBNull.Value;

                    }
                    
                }
            }
        }

        private void ComboBoxSGTimeTB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void PopulateComboBoxStuTimeTB()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            ComboBoxSGTimeTB.Items.Clear();
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT groupId FROM SubGroups";
            cmd.ExecuteNonQuery();
            DataTable data3 = new DataTable();
            SqlDataAdapter reader3 = new SqlDataAdapter(cmd);
            reader3.Fill(data3);


            foreach (DataRow data_r in data3.Rows)
            {
                ComboBoxSGTimeTB.Items.Add(data_r["groupId"].ToString());
            }

            connection.Close();
        }

        private void ComboBoxSGTimeTB_DropDown(object sender, EventArgs e)
        {
            PopulateComboBoxStuTimeTB();
        }

        private void GenSGTimeTBbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);

            //Timetable Generation**************************************************************************************************************************

            //Initialize at start time 08:30:00
            num_of_sec = 0;
            num_of_mins = 30;
            num_of_hrs = 8;

            String sql = "select SubjectName,GroupID,SubjectCode,Tag,Duration from Session";

            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            DataTable data = new DataTable();
            SqlDataReader reader = cmd.ExecuteReader();
            data.Load(reader);

            con.Close();

            System.IO.StringWriter stringWriter;
            string result;

            //Set Column Headers
            DataGridSGTimeTable.ColumnCount = 8;
            DataGridSGTimeTable.Columns[0].Name = "";
            DataGridSGTimeTable.Columns[1].Name = "Monday";
            DataGridSGTimeTable.Columns[2].Name = "Tuesday";
            DataGridSGTimeTable.Columns[3].Name = "Wednesday";
            DataGridSGTimeTable.Columns[4].Name = "Thursday";
            DataGridSGTimeTable.Columns[5].Name = "Friday";
            DataGridSGTimeTable.Columns[6].Name = "Saturday";
            DataGridSGTimeTable.Columns[7].Name = "Sunday";


            string[,] grid = new string[5, 8];


            for (int row = 0; row < grid.GetLength(0); row++)
            {

                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    grid[row, column] = "  ";
                }

            }

            int rw = 1;
            int colm = 0;

            foreach (DataRow row in data.Rows)
            {
                stringWriter = new System.IO.StringWriter();

                foreach (DataColumn col in data.Columns)
                {
                    stringWriter.Write(row[col].ToString());
                }

                result = stringWriter.ToString();

                if (result.Length > 2)
                    result = result.Substring(0, result.Length - 2);


                if (colm == 5)
                {
                    colm = 0;
                    rw++;
                }

                    grid[colm, rw] = result;
                    colm++;

             
            }

            do
            {
                foreach (DataGridViewRow gdr in DataGridSGTimeTable.Rows)
                {
                    try
                    {
                        DataGridSGTimeTable.Rows.Remove(gdr);
                    }
                    catch (Exception) { }
                }
            } while (DataGridSGTimeTable.Rows.Count > 1);


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                var cellContent = new ArrayList();

                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    cellContent.Add(grid[i, j]);
                }

                string srrr = (string)cellContent[1];
                string srrr2 = srrr.Substring(srrr.Length - 2);

                //Structure of the Cell Display
                string[] row = new string[] { num_of_hrs + ":" + num_of_mins, (string)cellContent[1], (string)cellContent[2], (string)cellContent[3], (string)cellContent[4], (string)cellContent[5], (string)cellContent[6], (string)cellContent[7] };

                try
                {
                    TimeRowDisplay(int.Parse(srrr2.Trim()), 0, 0);
                }
                catch (Exception exception)
                {
                    //MessageBox.Show("Error while Generating...", "Confirmation");
                }

                DataGridSGTimeTable.Rows.Add(row);
            }

            //Select slots of given lecturer
            String remv = "select Distinct SubjectName from Session where groupId LIKE '%" + ComboBoxSGTimeTB.Text + "%'";

            SqlCommand cmdd = new SqlCommand(remv, con);
            con.Open();
            DataTable data2 = new DataTable();
            SqlDataReader reader2 = cmdd.ExecuteReader();
            data2.Load(reader2);

            con.Close();
            string stug = "";


            foreach (DataRow data_r in data2.Rows)
            {
                stug = data_r["SubjectName"].ToString();
            }

            //set null values
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    //remove other student group slots
                    if (!(grid[row, column].ToString().Contains(stug)))
                    {

                        DataGridSGTimeTable.Rows[row].Cells[column].Value = DBNull.Value;

                    }

                }
            }
        }
    



        //Time table for student groups


    }
    
}
