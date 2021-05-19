using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SqlClient;

namespace ABCInstituteTimetableManagementSystem.MoreOptionsPortal
{
    public partial class MoreOptions : MetroForm
    {
        public MoreOptions()
        {
            InitializeComponent();
            fetchSessionData();
        }

        Service.SessionInfo sessionInfo;
        public void fetchSessionData()
        {
            try
            {
                Service.MoreOptionService moreOptionService = new Service.MoreOptionService();
                sessionInfo = new Service.SessionInfo();

                sessionInfo = moreOptionService.fetchAllSessions();

                BindingSource bs = new BindingSource();
                BindingSource bs2 = new BindingSource();
                BindingSource bs3 = new BindingSource();
                BindingSource bs4 = new BindingSource();
                BindingSource bs5 = new BindingSource();
                BindingSource bs6 = new BindingSource();

                bs.DataSource = sessionInfo.sessionStringList;
                bs2.DataSource = sessionInfo.sessionStringList;
                bs3.DataSource = sessionInfo.sessionStringList;
                bs4.DataSource = sessionInfo.sessionStringList;
                bs5.DataSource = sessionInfo.sessionStringList;
                bs6.DataSource = sessionInfo.sessionStringList;

                SelectSession1Txt.DataSource = bs;
                SelectSession2Txt.DataSource = bs2;
                NonOvrlpSesCombo.DataSource = bs3;
                NonOvrlpSesCombo2.DataSource = bs4;
                SelctSes1ParaCombo.DataSource = bs5;
                SelctSes2ParaCombo.DataSource = bs6;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private void MoreOptions_Load(object sender, EventArgs e)
        {

        }

        private void StdntGrp_TagTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StdntGrp_tagCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StdntGrp_BtnAddTag_Click(object sender, EventArgs e)
        {

        }

        private void StdntGrp_TagBtnClear_Click(object sender, EventArgs e)
        {

        }

        private void StdntGrp_SrchByTag_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StdntGrp_SrchTag_TextChanged(object sender, EventArgs e)
        {

        }

        private void StdntGrp_tagLbl_Click(object sender, EventArgs e)
        {

        }

        private void btn_Tag_search_Click(object sender, EventArgs e)
        {

        }

        private void StdntGrp_TagBtnDelete_Click(object sender, EventArgs e)
        {

        }

        private void StdntGrp_TagBtnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnAddConSessions_Click(object sender, EventArgs e)
        {
            try
            {
                Service.MoreOptionService service = new Service.MoreOptionService();
                Service.ConsecutiveSession obj = new Service.ConsecutiveSession();

                obj.sessionOne = SelectSession1Txt.SelectedItem.ToString();
                obj.sessionTwo = SelectSession2Txt.SelectedItem.ToString();
                obj.startTime = ConsecStartTimeCombo.SelectedItem.ToString();
                obj.classDay = ConsecDayCombo.SelectedItem.ToString();


                foreach (DictionaryEntry de in sessionInfo.sessionIdMap)
                {
                    if (de.Value.Equals(obj.sessionOne))
                    {
                        obj.s1Id = (int)de.Key;
                    }
                    if (de.Value.Equals(obj.sessionTwo))
                    {
                        obj.s2Id = (int)de.Key;
                    }
                }
                foreach (DictionaryEntry de in sessionInfo.durationMap)
                {
                    if (de.Key.ToString().Equals(obj.s1Id.ToString()))
                    {
                        obj.duration += Double.Parse(de.Value.ToString());
                    }
                    if (de.Key.ToString().Equals(obj.s2Id.ToString()))
                    {
                        obj.duration += Double.Parse(de.Value.ToString());
                    }
                }


                if (service.saveConsecutiveOrParallelSessions(obj, "consecutive"))
                {
                    MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_consec_search_Click(object sender, EventArgs e)
        {
            
                   try
            {

                if (SerchByConSessionscombo.SelectedIndex == -1 || String.IsNullOrEmpty(SerchConSessionstxt.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = SerchByConSessionscombo.SelectedItem.ToString();
                    string value = SerchConSessionstxt.Text.ToString();
                    Service.MoreOptionService service = new Service.MoreOptionService();
                    dataGridViewConSessions.DataSource = service.fetchConsecutiveSessions(prop, value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnAddShoulOver_Click(object sender, EventArgs e)
        {

            try
            {
                Service.MoreOptionService service = new Service.MoreOptionService();
                Service.NonOverlap obj = new Service.NonOverlap();

                obj.sessionOne = NonOvrlpSesCombo.SelectedItem.ToString();
                obj.sessionTwo = NonOvrlpSesCombo2.SelectedItem.ToString();

                if(obj.sessionOne == obj.sessionTwo)
                {
                    MessageBox.Show("Both sessions cannot be same", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    foreach (DictionaryEntry de in sessionInfo.sessionIdMap)
                    {
                        if (de.Value.Equals(obj.sessionOne))
                        {
                            obj.s1Id = (int)de.Key;
                        }
                        if (de.Value.Equals(obj.sessionTwo))
                        {
                            obj.s2Id = (int)de.Key;
                        }
                    }

                    if (service.saveNonOverlappingSessions(obj))
                    {
                        MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

         

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnParlSes_Click(object sender, EventArgs e)
        {
            try
            {
                Service.MoreOptionService service = new Service.MoreOptionService();
                Service.ConsecutiveSession obj = new Service.ConsecutiveSession();

                if (SelctSes1ParaCombo.SelectedIndex == -1 || SelctSes2ParaCombo.SelectedIndex == -1 || DayParlCombo.SelectedIndex == -1 || StartParlCombo.SelectedIndex == -1)
                {
     
                    MessageBox.Show("Please select all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    obj.sessionOne = SelctSes1ParaCombo.SelectedItem.ToString();
                    obj.sessionTwo = SelctSes2ParaCombo.SelectedItem.ToString();
                    obj.startTime = StartParlCombo.SelectedItem.ToString();
                    obj.classDay = DayParlCombo.SelectedItem.ToString();


                    foreach (DictionaryEntry de in sessionInfo.sessionIdMap)
                    {
                        if (de.Value.Equals(obj.sessionOne))
                        {
                            obj.s1Id = (int)de.Key;
                        }
                        if (de.Value.Equals(obj.sessionTwo))
                        {
                            obj.s2Id = (int)de.Key;
                        }
                    }
                    foreach (DictionaryEntry de in sessionInfo.durationMap)
                    {
                        if (de.Key.ToString().Equals(obj.s1Id.ToString()))
                        {
                            obj.duration += Double.Parse(de.Value.ToString());
                        }

                    }

                    if(service.checkOverlapConditionForParallelSessions(obj.s1Id, obj.s2Id))
                    {
                        if (service.saveConsecutiveOrParallelSessions(obj, "parallel"))
                        {
                            MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    } else
                    {
                        MessageBox.Show("These sessions must not overlap", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    
                }
             

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


    private void metroTabPage2_Click(object sender, EventArgs e)
        {

        }

        //Member 3 functions strats here ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        public string conString = (@"Server=tcp:abc-insstitute-server.database.windows.net,1433;Initial Catalog=abcinstitute-datbase;Persist Security Info=False;User ID=dbuser;Password=1qaz!QAZ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        private void metroTile1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(conString);

            con.Open();
            SqlCommand scom = con.CreateCommand();
            scom.CommandType = CommandType.Text;
            scom.CommandText = "Select COUNT(Id) as Id from SetNotAvailableTime";
            scom.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(scom);
            da.Fill(dt);

            int num = 0;

            foreach (DataRow dr in dt.Rows) {
                string inc = dr["Id"].ToString();
                num = Int16.Parse(inc);
                num = ++num;
            }



            SqlConnection connect = new SqlConnection(conString);

            connect.Open();
            if (connect.State == System.Data.ConnectionState.Open)
            {

                string query = "INSERT INTO SetNotAvailableTime (Id,Type,Item,Day,StartTime,EndTime) VALUES (@Increment,@Type,@Item,@Day,@StartTime,@EndTime)";
                SqlCommand cmd = new SqlCommand(query, connect);


                cmd.Parameters.AddWithValue("@Type", selectTypeDropDown.Text);
                cmd.Parameters.AddWithValue("@Item", selectItemDropDown.Text);
                cmd.Parameters.AddWithValue("@Day", DayDropdown.Text);
                cmd.Parameters.AddWithValue("@StartTime", StartTimeDropdown.Text);
                cmd.Parameters.AddWithValue("@EndTime", EndTimeDropdown.Text);
                cmd.Parameters.AddWithValue("@Increment", num);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully set not available time");

                clearFields();
            }
        }

        private void selectTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlConnection connect = new SqlConnection(conString);

            connect.Open();
            if (connect.State == System.Data.ConnectionState.Open)
            {
                if (selectTypeDropDown.Text == "Lecturer")
                {
                    selectItemDropDown.Text = "Select Lecturer";
                    this.selectItemDropDown.DataSource = null;
                    selectItemDropDown.Items.Clear();
                    string query = "select LecturerName from Lecturer";
                    SqlDataAdapter da = new SqlDataAdapter(query, connect);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Lecturer");
                    selectItemDropDown.DisplayMember = "LecturerName";
                    selectItemDropDown.ValueMember = "LecturerID";
                    selectItemDropDown.DataSource = ds.Tables["Lecturer"];
                    selectItemDropDown.Text = "Select Lecturer";
                    connect.Close();
                    selectItemDropDown.SelectedIndex = -1;
                }
                else if (selectTypeDropDown.Text == "Session")
                {
                    selectItemDropDown.Text = "Select Session";
                    this.selectItemDropDown.DataSource = null;
                    selectItemDropDown.Items.Clear();
                    string query = "select ID from Session";
                    SqlDataAdapter da = new SqlDataAdapter(query, connect);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Session");
                    selectItemDropDown.DisplayMember = "ID";
                    selectItemDropDown.ValueMember = "ID";
                    selectItemDropDown.DataSource = ds.Tables["Session"];
                    connect.Close();
                    selectItemDropDown.SelectedIndex = -1;
                }

                else if (selectTypeDropDown.Text == "Groups")
                {
                    selectItemDropDown.Text = "Select SubGroups";
                    this.selectItemDropDown.DataSource = null;
                    selectItemDropDown.Items.Clear();
                    string query = "select groupId from SubGroups";
                    SqlDataAdapter da = new SqlDataAdapter(query, connect);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SubGroups");
                    selectItemDropDown.DisplayMember = "groupId";
                    selectItemDropDown.ValueMember = "groupId";
                    selectItemDropDown.DataSource = ds.Tables["SubGroups"];
                    connect.Close();
                    selectItemDropDown.SelectedIndex = -1;
                }

                else if (selectTypeDropDown.Text == "SubGroups")
                {
                    selectItemDropDown.Text = "Select SubGroups";
                    this.selectItemDropDown.DataSource = null;
                    selectItemDropDown.Items.Clear();
                    string query = "select subGroupId from SubGroups";
                    SqlDataAdapter da = new SqlDataAdapter(query, connect);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "SubGroups");
                    selectItemDropDown.DisplayMember = "subGroupId";
                    selectItemDropDown.ValueMember = "subGroupId";
                    selectItemDropDown.DataSource = ds.Tables["subGroups"];
                    connect.Close();
                    selectItemDropDown.SelectedIndex = -1;
                }
            }

        }


        //created a function to clear text feilds

         public void clearFields()
         {
             DayDropdown.SelectedIndex = -1;
             selectTypeDropDown.SelectedIndex = -1;
             selectItemDropDown.ResetText();
             StartTimeDropdown.ResetText();
             EndTimeDropdown.ResetText();
         }

        private void metroLabel34_Click(object sender, EventArgs e)
        {

        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            viewSNAT();
        }

        //View number of working days and time +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void viewSNAT()
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from SetNotAvailableTime", connection);
            DataTable dataTable = new DataTable();

            connection.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            connection.Close();

            SNATGrid.AutoGenerateColumns = true;
            SNATGrid.DataSource = dataTable;
        }

        private void backMO_Click(object sender, EventArgs e)
        {
            Form1 navSubPortal = new Form1();
            navSubPortal.Show();
            this.Hide();
        }
    }
}
