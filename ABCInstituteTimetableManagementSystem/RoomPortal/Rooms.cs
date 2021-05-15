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

namespace ABCInstituteTimetableManagementSystem.RoomPortal
{
    public partial class ManageRoomsSessions : Form
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


        public ManageRoomsSessions()
        {
            InitializeComponent();
            tag_cmb();
            Room_combo();
            Lecturer_combo();
            LecturerRoom_combo();
            subject_combo();
            subtagcombo();
            subRoomcombo();
            groupcombo();
            subgroupcombo();
            Roomcombo();
            sessionpagecombo();
            sessionRoomcombo();
            AddRoomForRESERVABLETime();
        }

        SqlConnection con = new SqlConnection(@"Data Source = ELECTRA\SQLSERVER; Initial Catalog = LocationDB; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader dr;
        String sql;
        SqlDataAdapter da;



        private void tag_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageRoomsSessions_Load(object sender, EventArgs e)
        {
            sessionpagecombo();
            RoomForconsecutiveSession();
            AddRoomForConsecutive();
            clearDayTimeFields();
        }

        private void tag_cmb()
        {

            tag_combo_box.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT TagName FROM  TagTable";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            da1.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                tag_combo_box.Items.Add(dr["TagName"].ToString());
            }

            con.Close();

        }
        private void Room_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Room_combo()
        {
            Room_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Room_combo_box.Items.Add(dataRow["Room_Name"].ToString());
            }
        }

        private void Allocate_room_button_Click(object sender, EventArgs e)
        {

            String addtag = tag_combo_box.SelectedItem.ToString();
            String addroom = Room_combo_box.SelectedItem.ToString();


            if ((tag_combo_box.Text != string.Empty) && (Room_combo_box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select Tag, Room from Tag_Rooms where Tag = '" + tag_combo_box.Text + "' and Room = '" + Room_combo_box.Text + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The selected room is already allocated!");
                }
                else if ((tag_combo_box.SelectedItem.ToString() != string.Empty) && (Room_combo_box.SelectedItem.ToString() != string.Empty))
                {


                    sql = "insert into Tag_Rooms (Tag,Room)values(@addtag,@addroom)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addtag", addtag);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();

                }

            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void clearRoomFieldForTag()
        {
            tag_combo_box.SelectedIndex = -1;
        }

        private void Clear_Tag_Button_Click(object sender, EventArgs e)
        {
            tag_combo_box.SelectedIndex = -1;
            Room_combo_box.SelectedIndex = -1;
        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }


        //SUITABLE ROOM FOR LECTURER

        //selecting a lecturer

        private void lecturer_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Lecturer_combo()
        {

            lecturer_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select LecturerName from Lecturer ", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                lecturer_combo_box.Items.Add(dataRow["LecturerName"].ToString());
            }
        }

        private void LecturerRoom_combo()
        {

            lecroom_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                lecroom_combo_box.Items.Add(dataRow["Room_Name"].ToString());
            }
        }

        private void allocatelecturer_room_btn_Click(object sender, EventArgs e)
        {


            String addlecturer = lecturer_combo_box.SelectedItem.ToString();
            String addroom = lecroom_combo_box.SelectedItem.ToString();


            if ((lecturer_combo_box.Text != string.Empty) && (lecroom_combo_box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select LecturerName, Room from Lecturer_Rooms where LecturerName = '" + lecturer_combo_box.Text + "' and Room = '" + lecroom_combo_box.Text + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected lecturer!");
                }
                else if ((lecturer_combo_box.SelectedItem.ToString() != string.Empty) && (lecroom_combo_box.SelectedItem.ToString() != string.Empty))
                {


                    sql = "insert into Lecturer_Rooms (LecturerName,Room)values(@addlecturer,@addroom)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addlecturer", addlecturer);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();
                    clearRoomFieldForLecturer();

                }

            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void clearRoomFieldForLecturer()
        {

            lecturer_combo_box.SelectedIndex = -1;
        }

        private void clrLecturerroom_btn_Click(object sender, EventArgs e)
        {
            lecturer_combo_box.SelectedIndex = -1;
            lecroom_combo_box.SelectedIndex = -1;
        }


        //SUITABLE ROOMS FOR SUBJECT AND TAG

        //drop down for selecting a subject

        private void subject_combo()
        {


            subject_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT SubjectName from Subject", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                subject_combo_box.Items.Add(dataRow["SubjectName"].ToString());
            }
        }

        private void subtagcombo()
        {

            subTag_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT DISTINCT TagName FROM  TagTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                subTag_combo_box.Items.Add(dataRow["TagName"].ToString());
            }

        }

        private void subRoomcombo()
        {
            subRoom_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                subRoom_combo_box.Items.Add(dataRow["Room_Name"].ToString());
            }


        }

        private void subRoom_allocate_btn_Click(object sender, EventArgs e)
        {


            String addSubject = subject_combo_box.SelectedItem.ToString();
            String addTag = subTag_combo_box.SelectedItem.ToString();
            String addroom = subRoom_combo_box.SelectedItem.ToString();


            if ((subject_combo_box.Text != string.Empty) && (subTag_combo_box.Text != string.Empty) && (subRoom_combo_box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select Subject, Tag, Room from SubjectTag_Rooms where Subject = '" + subject_combo_box.Text + "'and (Tag = '" + subTag_combo_box.Text + "'  and Room = '" + subRoom_combo_box.Text + "')", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected subject and tag!");
                }
                else if ((subject_combo_box.SelectedItem.ToString() != string.Empty) && (subTag_combo_box.SelectedItem.ToString() != string.Empty) && (subRoom_combo_box.Text != string.Empty))
                {


                    sql = "insert into SubjectTag_Rooms (Subject,Tag,Room)values(@addSubject,@addTag,@addroom)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addSubject", addSubject);
                    cmd.Parameters.AddWithValue("@addTag", addTag);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();
                    clearRoomFieldForLecturer();

                }

            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void clearRoomFieldForTagSub()
        {
            subTag_combo_box.SelectedIndex = -1;
        }

        private void subRoom_clear_btn_Click(object sender, EventArgs e)
        {
            subject_combo_box.SelectedIndex = -1;
            subTag_combo_box.SelectedIndex = -1;
            subRoom_combo_box.SelectedIndex = -1;
        }



        //SUITABLE ROOM FOR GROUP/SUB GROUP

        private void groupcombo()
        {

            group_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select groupId from SubGroups", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                group_combo_box.Items.Add(dataRow["groupId"].ToString());
            }

        }

        private void subgroupcombo()
        {

            subGroup_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select subGroupId from SubGroups", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                subGroup_combo_box.Items.Add(dataRow["subGroupId"].ToString());
            }
        }

        private void Roomcombo()
        {

            GroupRoom_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                GroupRoom_combo_box.Items.Add(dataRow["Room_Name"].ToString());
            }

        }

        private void allocateBtn_Room_box_Click(object sender, EventArgs e)
        {



            String addgroup = group_combo_box.SelectedItem.ToString();
            String addsub = subGroup_combo_box.SelectedItem.ToString();
            String addroom = GroupRoom_combo_box.SelectedItem.ToString();


            if ((group_combo_box.Text != string.Empty) && (subGroup_combo_box.Text != string.Empty) && (GroupRoom_combo_box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select GroupNum, SubGroupNum, Room from GroupSubGroup_Rooms" +
                    " where GroupNum = '" + group_combo_box.Text + "' and  SubGroupNum = '" + subGroup_combo_box.Text + "'and  Room = '" + GroupRoom_combo_box.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected group and sub group!");
                }
                else if ((group_combo_box.SelectedItem.ToString() != string.Empty) && (subGroup_combo_box.SelectedItem.ToString() != string.Empty) && (GroupRoom_combo_box.Text != string.Empty))
                {


                    sql = "insert into GroupSubGroup_Rooms (GroupNum,SubGroupNum,Room)values(@addgroup,@addsub,@addroom)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addgroup", addgroup);
                    cmd.Parameters.AddWithValue("@addsub", addsub);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();
                    clearRoomFieldForLecturer();

                }

            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void clearBtn_Room_box_Click(object sender, EventArgs e)
        {

            group_combo_box.SelectedIndex = -1;
            subGroup_combo_box.SelectedIndex = -1;
            GroupRoom_combo_box.SelectedIndex = -1;
        }




        //SUITABLE ROOM FOR A SESSION

        //selecting a session

        private void sessionpagecombo()
        {

            session_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select LecturerName, SubjectCode, SubjectName, Tag from Session", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //session_cmb.Items.Add(dataRow["SessionID"].ToString());
                session_combo_box.Items.Add(dataRow["LecturerName"].ToString() + " - " + dataRow["SubjectCode"].ToString() + " - " + dataRow["SubjectName"].ToString() + " - " + dataRow["Tag"].ToString());
            }

        }

        private void sessionRoomcombo()
        {

            session_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                sessionRoom_combo_box.Items.Add(dataRow["Room_Name"].ToString());
            }
        }

        private void session_allocate_Btn_Click(object sender, EventArgs e)
        {


            String addsession = session_combo_box.SelectedItem.ToString();
            String addroom = sessionRoom_combo_box.SelectedItem.ToString();


            if ((session_combo_box.Text != string.Empty) && (sessionRoom_combo_box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select SessionID, Room from Session_Rooms where SessionID= '" + session_combo_box.Text + "' and Room = '" + sessionRoom_combo_box.Text + "' ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("The room is already allocated for the selected session");
                }
                else if ((session_combo_box.SelectedItem.ToString() != string.Empty) && (sessionRoom_combo_box.SelectedItem.ToString() != string.Empty))
                {


                    sql = "insert into Session_Rooms (SessionID,Room)values(@addsession,@addroom)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addsession", addsession);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();
                    clearRoomFieldForLecturer();

                }

            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //clear Room Field For Session
        public void clearRoomFieldForSession()
        {
            session_combo_box.SelectedIndex = -1;
        }

        private void session_clear_Btn_Click(object sender, EventArgs e)
        {
            session_combo_box.SelectedIndex = -1;
            sessionRoom_combo_box.SelectedIndex = -1;
        }

        private void session_combo_box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void RoomForconsecutiveSession()
        {

            consecutiveRoom_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                consecutiveRoom_combo_box.Items.Add(dataRow["Room_Name"].ToString());
            }

        }

        public void AddRoomForConsecutive() {

            Consecutive_session_combo_box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select Id,sessionOne, sessionTwo from consecutive ", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Consecutive_session_combo_box.Items.Add(dataRow["Id"].ToString() + " - " + dataRow["sessionOne"].ToString() + " - " + dataRow["sessionTwo"].ToString());

            }


        }

        private void allocate_consecutive_Btn_Click(object sender, EventArgs e)
        {
            String addconsecutive = Consecutive_session_combo_box.SelectedItem.ToString();
            String addroom = consecutiveRoom_combo_box.SelectedItem.ToString();

            if ((consecutiveRoom_combo_box.Text != string.Empty) && (Consecutive_session_combo_box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select Room,ConsecutiveSessionId from ConsecutiveSession_Rooms where Room = '" + consecutiveRoom_combo_box.Text + "' and  ConsecutiveSessionId = '" + Consecutive_session_combo_box.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("Room is already allocated for the selected consecutive session! Please choose another");
                }
                else if ((consecutiveRoom_combo_box.SelectedItem.ToString() != string.Empty) && (Consecutive_session_combo_box.SelectedItem.ToString() != string.Empty))
                {


                    sql = "insert into ConsecutiveSession_Rooms (Room,ConsecutiveSessionId)values(@addroom,@addconsecutive)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addconsecutive", addconsecutive);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();
                    clearRoomFieldForLecturer();

                }
               

            }
            else
            {
                MessageBox.Show("Please fill all the fields", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clear_consecutive_Btn_Click(object sender, EventArgs e)
        {
            consecutiveRoom_combo_box.SelectedIndex = -1;
            Consecutive_session_combo_box.SelectedIndex = -1;
        }


        // ALLOCATING NON RESERVABLE TIME FOR A ROOM

        //drop down for room

        public void AddRoomForRESERVABLETime()
        {

            timeRoom_Box.Items.Clear();
            SqlDataAdapter sda = new SqlDataAdapter("select DISTINCT Room_Name from RoomTable", con);
            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);
            foreach (DataRow dataRow in dataTable.Rows)
            {
                timeRoom_Box.Items.Add(dataRow["Room_Name"].ToString());
            }
        }

        private void Allocate_time_Btn_Click(object sender, EventArgs e)
        {
            String addroom = timeRoom_Box.SelectedItem.ToString();
            String day = selectDay_Box.SelectedItem.ToString();
            String starttime = startTime_Box.SelectedItem.ToString();
            String endtime = endTime_Box.SelectedItem.ToString();

            if ((timeRoom_Box.Text != string.Empty) && (selectDay_Box.Text != string.Empty) && (startTime_Box.Text != string.Empty))
            {
                //check duplicate before save
                SqlDataAdapter da = new SqlDataAdapter("Select Room, Day,StartTime,EndTime from NonReservableTime_Rooms where Room = '" + timeRoom_Box.Text + "' " +
                    "and  Day = '" + selectDay_Box.Text + "' and StartTime = '" + startTime_Box.Text + "' and EndTime = '" + endTime_Box.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("Time is already allocated as non reservable!");
                    clearDayTimeFields();
                }
                else if ((timeRoom_Box.SelectedItem.ToString() != string.Empty) && (selectDay_Box.SelectedItem.ToString() != string.Empty) && (startTime_Box.SelectedItem.ToString() != string.Empty))
                {


                    sql = "insert into NonReservableTime_Rooms (Room,Day,StartTime,EndTime)values(@addroom,@day,@starttime,@endtime)";
                    con.Open();
                    cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@addroom", addroom);
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@starttime", starttime);
                    cmd.Parameters.AddWithValue("@endtime", endtime);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Room Allocated");
                    con.Close();
                    clearDayTimeFields();

                }

            }
            else
            {
                MessageBox.Show("All fields must be filled", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void clearDayTimeFields()
        {
            selectDay_Box.Text = " ";
            startTime_Box.Text = " ";
            endTime_Box.Text = " "; 
        }

        private void clear_time_Btn_Click(object sender, EventArgs e)
        {
            timeRoom_Box.Text = " ";
            clearDayTimeFields();
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

        //Navigate Location
        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            LocationPortal.AddandManageLocations lcbtn = new LocationPortal.AddandManageLocations();
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
