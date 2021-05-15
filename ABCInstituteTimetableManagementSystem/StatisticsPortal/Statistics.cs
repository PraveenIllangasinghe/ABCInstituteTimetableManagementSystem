using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace ABCInstituteTimetableManagementSystem.StatisticsPortal
{
    public partial class Visualizingstatistic : Form
    {

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


        public Visualizingstatistic()
        {
            InitializeComponent();
        }


        //established the connection
        SqlConnection con = new SqlConnection(@"Data Source = ELECTRA\SQLSERVER; Initial Catalog = LocationDB; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader dr;
        String sql;
        SqlDataAdapter da;

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Visualizingstatistic_Load(object sender, EventArgs e)
        {
            LoadProgramme_Std_Group_Chart();
            total_Std_GrpCount();
            LoadSubject_YearChart();
            Load_Lecturer_Faculty_Chart();
            totalLecturer_Count();
            total_Subject_Count();
        }


        private void total_Std_GrpCount()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(GenGroupNumber) as grpcount from GenGroupNum_Table";
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string grp_count = (string)dr["grpcount"].ToString();
                StudentGroupCount_Label.Text = grp_count;


            }
            con.Close();

        }


        private void LoadProgramme_Std_Group_Chart()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;

            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select p.ProgrammeName as Programme, count(g.GenGroupNumber) as Grpcount  " +
                "from Programme_Table p, GenGroupNum_Table g where p.id = g.ProgrammeRef group by p.ProgrammeName", con);
            adapt.Fill(ds, "Grpcount");
            Programme_GropCount_Chart.DataSource = ds.Tables["Grpcount"];


            Programme_GropCount_Chart.Series["Programme"].XValueMember = "Programme";
            Programme_GropCount_Chart.Series["Programme"].YValueMembers = "Grpcount";
            Programme_GropCount_Chart.Series["Programme"].ChartType = SeriesChartType.Bar;


            Programme_GropCount_Chart.DataBind();
            con.Close();
        }



        private void LoadSubject_YearChart()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;

            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Year ,COUNT(Code) as subyrcount from Subjects_Table GROUP BY Year", con);
            adapt.Fill(ds, "subyrcount");
            Subject_Year_Chart.DataSource = ds.Tables["subyrcount"];


            Subject_Year_Chart.Series["Academic Year"].XValueMember = "Year";
            Subject_Year_Chart.Series["Academic Year"].YValueMembers = "subyrcount";
            Subject_Year_Chart.Series["Academic Year"].ChartType = SeriesChartType.Bar;


            Subject_Year_Chart.DataBind();
            con.Close();
        }


        //Calculating total subject count
        private void total_Subject_Count()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(Code) as subCount FROM Subjects_Table";
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string sub_count = (string)dr["subCount"].ToString();
                Subject_Count_Label.Text = sub_count;


            }
            con.Close();

        }


        //Lecture Faculty Chart...................................


        public void Load_Lecturer_Faculty_Chart()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = con;

            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Faculty,COUNT(ID) as c from Lecturer_table GROUP BY Faculty", con);
            adapt.Fill(ds, "Faculty");
            Lecture_Faculty_Chart.DataSource = ds.Tables["Faculty"];

            Lecture_Faculty_Chart.Series["Faculty"].XValueMember = "Faculty";
            Lecture_Faculty_Chart.Series["Faculty"].YValueMembers = "c";
            Lecture_Faculty_Chart.Series["Faculty"].ChartType = SeriesChartType.Bar;


            Lecture_Faculty_Chart.DataBind();
            con.Close();

        }



        // calculating total lecturer count---------------------------

        private void totalLecturer_Count()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(ID) as lecCount FROM Lecturer_table";
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string lec_count = (string)dr["lecCount"].ToString();
                Lecturer_count_label.Text = lec_count;


            }
            con.Close();

        }




        //Location Navigation Button---------------------------

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            LocationPortal.AddandManageLocations addandManageLocations = new LocationPortal.AddandManageLocations();
            addandManageLocations.ShowDialog();
        }

        //BTN Navigate Home
        private void button1_Click(object sender, EventArgs e)
        {

            //home back  

        }

        //BTN Navigate LEcture
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LecturerPortal.Lecturers lcbtn = new LecturerPortal.Lecturers();
            lcbtn.ShowDialog();


        }

        //Navigate Students
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentGroupPortal.StudentGroups lcbtn = new StudentGroupPortal.StudentGroups();
            lcbtn.ShowDialog();
        }

        //Navigate subjects
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            SubjectPortal.Subjects lcbtn = new SubjectPortal.Subjects();
            lcbtn.ShowDialog();
        }

        //Navigate working days
        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            WorkingDaysPortal.WorkingDays lcbtn = new WorkingDaysPortal.WorkingDays();
            lcbtn.ShowDialog();
        }

        //Navigate Tags
        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            TagPortal.Tags lcbtn = new TagPortal.Tags();
            lcbtn.ShowDialog();
        }

        //Statics Navigation
        private void button13_Click(object sender, EventArgs e)
        {

            this.Hide();
            StatisticsPortal.Visualizingstatistic lcbtn = new StatisticsPortal.Visualizingstatistic();
            lcbtn.ShowDialog();
        }

        //Navigate Room
        private void button11_Click(object sender, EventArgs e)
        {

            this.Hide();
            RoomPortal.ManageRoomsSessions lcbtn = new RoomPortal.ManageRoomsSessions();
            lcbtn.ShowDialog();
        }

        //Navigate Session
        private void button14_Click(object sender, EventArgs e)
        {

            this.Hide();
            SessionPortal.Sessions lcbtn = new SessionPortal.Sessions();
            lcbtn.ShowDialog();
        }

        //Navigate More-Option
        private void button15_Click(object sender, EventArgs e)
        {
            this.Hide();
            MoreOptionsPortal.MoreOptions lcbtn = new MoreOptionsPortal.MoreOptions();
            lcbtn.ShowDialog();
        }

        //Navigate Timetables
        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            GenerateTimetablePortal.GenerateTimetables lcbtn = new GenerateTimetablePortal.GenerateTimetables();
            lcbtn.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //home back
        }
    }
}
