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

namespace ABCInstituteTimetableManagementSystem.LocationPortal
{
    public partial class AddandManageLocations : Form
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

        public AddandManageLocations()
        {
            InitializeComponent();
            RoomAutono();
            editroom_cmb_DropDown();
        }

        //established the connection
        SqlConnection con = new SqlConnection(@"Data Source = ELECTRA\SQLSERVER; Initial Catalog = LocationDB; Integrated Security = True");
        SqlCommand cmd;
        SqlDataReader dr;
        String sql;
        SqlDataAdapter da;
        //String buildingID;
        String roomID;


        //create Auto increment number for building table............

        public void RoomAutono()
        {

            sql = "select Room_ID from RoomTable order by Room_ID desc";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int id = int.Parse(dr[0].ToString()) + 1;
                roomID = id.ToString("0000");

            }
            else if (Convert.IsDBNull(dr))
            {
                roomID = ("0001");
            }
            else
            {
                roomID = ("0001");
            }

            Room_ID_textbox.Text = roomID.ToString();
            con.Close();


        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void ClearLocationData()
        {
            Building_Combo_Box.SelectedIndex = -1;
            Room_Combo_Box.SelectedIndex = -1;
            AddCapacity_cmb.Value = 0;
            Room_Combo_Box.SelectedIndex = -1;

        }

        private void Save_Location_Button_Click(object sender, EventArgs e)
        {

            String addbuild = Building_Combo_Box.SelectedItem.ToString();
            String addroom = Room_Combo_Box.SelectedItem.ToString();
            String addroomty = AddRoomType_cmb.SelectedItem.ToString();
            String addcapacity = AddCapacity_cmb.Value.ToString();


            if ((Building_Combo_Box.SelectedItem.ToString() != string.Empty) && (Room_Combo_Box.SelectedItem.ToString() != string.Empty) && (AddRoomType_cmb.SelectedItem.ToString() != string.Empty)
                && (AddCapacity_cmb.Value.ToString() != string.Empty))

            {
                sql = "insert into LocationTable (BuildingName,RoomName,RoomType,Capacity)values(@addbuild,@addroom,@addroomty,@addcapacity)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@addbuild", addbuild);
                cmd.Parameters.AddWithValue("@addroom", addroom);
                cmd.Parameters.AddWithValue("@addroomty", addroomty);
                cmd.Parameters.AddWithValue("@addcapacity", addcapacity);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added");
                con.Close();

                LoadLocations();
                ClearLocationData();
                Loc_TabControl.SelectedTab = Load_Location_Page;
            }



        }


        private void ClearBuildingTextBox()
        {
            Add_Building_Name.Text = " ";


        }

        private void SaveBuilding_Name_Click(object sender, EventArgs e)
        {
            String addbuildingname = Add_Building_Name.Text;


            if ((Add_Building_Name.Text != string.Empty))
            {
                sql = "insert into BuildingsNameTable (Buildingnames)values(@addbuildingname)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@addbuildingname", addbuildingname);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Building successfuly Added!");
                con.Close();

                building_cmb();
                ClearBuildingTextBox();

            }
        }


        private void building_cmb()
        {

            sql = "select * from BuildingsNameTable";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            BuildingNameComboBox.Items.Clear();

            while (dr.Read())
            {
                BuildingNameComboBox.Items.Add(dr[0]);

            }

            con.Close();
        }



        //Insert operation to Add Room for Room table.................

        private void ClearRoomsTextBox()
        {
            Room_NametextBox.Text = " ";
            Room_ID_textbox.Text = " ";
            BuildingNameComboBox.SelectedIndex = -1;


        }

        private void SaveRoom_Name_Click(object sender, EventArgs e)
        {
            String addroomname = Room_NametextBox.Text;
            String addroomid = Room_ID_textbox.Text;
            String addBuildName = BuildingNameComboBox.Text;


            if ((Room_NametextBox.Text != string.Empty) && (BuildingNameComboBox.Text != string.Empty))
            {
                sql = "insert into RoomTable (Room_ID,Room_Name,BuildingName)values(@addroomid,@addroomname,@addBuildName)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@addroomname", addroomname);
                cmd.Parameters.AddWithValue("@addroomid", addroomid);
                cmd.Parameters.AddWithValue("@addBuildName", addBuildName);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Room successfuly Added!");
                con.Close();

                ClearRoomsTextBox();
                Refresh_building_from_Room_Table();

                RoomAutono();

            }
        }


        private void LoadBuilding()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Buildingnames FROM BuildingsNameTable", con);
            DataTable dt = new DataTable();



            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);


            //loc_dgridv.AutoGenerateColumns = true;
            //loc_dgridv.DataSource = dt;
            con.Close();
        }


        public void cmbdisp()
        {

           BuildingNameComboBox.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT Buildingnames FROM BuildingsNameTable";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            da1.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                BuildingNameComboBox.Items.Add(dr["Buildingnames"].ToString());
            }

            con.Close();

        }


        public void Select_Building_ComboBox()
        {


            Building_Combo_Box.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT DISTINCT BuildingName FROM RoomTable";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            da1.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Building_Combo_Box.Items.Add(dr["BuildingName"].ToString());
            }

            con.Close();

        }


        //When the Add the Building name to room table added value show through the Location form building combo box-------------------
        private void Refresh_building_from_Room_Table()
        {

            sql = "select DISTINCT BuildingName from RoomTable";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            Building_Combo_Box.Items.Clear();

            while (dr.Read())
            {
                Building_Combo_Box.Items.Add(dr[0]);

            }

            con.Close();

        }

        private void Refresh_Room_from_Room_Table()
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {
            cmbdisp();
            Select_Building_ComboBox();
            Refresh_Room_from_Room_Table();
            LoadLocations();

        }

        private void Add_Location_tab_Click(object sender, EventArgs e)
        {
            cmbdisp();
            Select_Building_ComboBox();
            Refresh_Room_from_Room_Table();
            LoadLocations();
        }

        private void Refresh_Room_Btn_Click(object sender, EventArgs e)
        {
            sql = "SELECT DISTINCT Room_Name from RoomTable where BuildingName ='" + Building_Combo_Box.Text + "'";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            Room_Combo_Box.Items.Clear();

            while (dr.Read())
            {
                Room_Combo_Box.Items.Add(dr[0]);

            }

            con.Close();
        }

        private void Location_Grid_View_Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from LocationTable", con);
            DataTable dt = new DataTable();



            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);


            Location_Grid_View_Table.AutoGenerateColumns = true;
            Location_Grid_View_Table.DataSource = dt;
            con.Close();


            editBuilding_name_text_Box.Text = Location_Grid_View_Table.CurrentRow.Cells[0].Value.ToString();
            editRoomcombo_box.Text = Location_Grid_View_Table.CurrentRow.Cells[1].Value.ToString();
            editCapacity.Text = Location_Grid_View_Table.CurrentRow.Cells[2].Value.ToString();
            editRoomType_cmb.Text = Location_Grid_View_Table.CurrentRow.Cells[3].Value.ToString();
        }

        private void LoadLocations()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from LocationTable", con);
            DataTable dt = new DataTable();



            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);


            Location_Grid_View_Table.AutoGenerateColumns = true;
            Location_Grid_View_Table.DataSource = dt;
            con.Close();
        }


        private void Load_Location_Page_Click(object sender, EventArgs e)
        {
            LoadLocations();
        }

        //Edit Locations...................

        private void ClearUpdateLocDetails()
        {
            editRoomcombo_box.SelectedIndex = -1;
            editBuilding_name_text_Box.Clear();
            editCapacity.Value = 0;
            editRoomType_cmb.SelectedIndex = -1;
        }

        private void Location_Load(object sender, EventArgs e)
        {
            LoadLocations();

        }

        private void Location_Grid_View_Table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            editBuilding_name_text_Box.Text = Location_Grid_View_Table.CurrentRow.Cells[0].Value.ToString();
            editRoomcombo_box.Text = Location_Grid_View_Table.CurrentRow.Cells[1].Value.ToString();
            editCapacity.Text = Location_Grid_View_Table.CurrentRow.Cells[2].Value.ToString();
            editRoomType_cmb.Text = Location_Grid_View_Table.CurrentRow.Cells[3].Value.ToString();
        }


        private void editroom_cmb_DropDown()
        {
            editRoomcombo_box.Items.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT RoomName FROM LocationTable";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                editRoomcombo_box.Items.Add(dr["RoomName"].ToString());
            }

            con.Close();


        }

        private void editRoomcombo_box_SelectedIndexChanged(object sender, EventArgs e)
        {


        }



        //update Button code............

        private void metroTile4_Click(object sender, EventArgs e)
        {
           
        }


        //Delete location Button...................

        private void DeleteLocation_Btn_Click(object sender, EventArgs e)
        {

          



        }

        //Search Part.................................


        private void loc_dgridv_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Location_Grid_View_Table.Sort(Location_Grid_View_Table.Columns[0], ListSortDirection.Ascending);
            Location_Grid_View_Table.Sort(Location_Grid_View_Table.Columns[1], ListSortDirection.Ascending);
            Location_Grid_View_Table.Sort(Location_Grid_View_Table.Columns[2], ListSortDirection.Descending);
            Location_Grid_View_Table.Sort(Location_Grid_View_Table.Columns[3], ListSortDirection.Ascending);
        }



        private void search_txt_box_Click(object sender, EventArgs e)
        {


        }



        //edit page Room Refresh Button--------------------------------------------------

        private void Edit_Refresh_Button_Click(object sender, EventArgs e)
        {
            
        }



        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void updateLocation_Btn_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE LocationTable SET Capacity ='" + editCapacity.Text + "',RoomType = '" + editRoomType_cmb.Text + "' WHERE RoomName ='" + editRoomcombo_box.Text + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Location Updated!");
            con.Close();

            LoadLocations();
            ClearUpdateLocDetails();
            Loc_TabControl.SelectedTab = Load_Location_Page;


        }

        private void DeleteLocation_Btn_Click_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM LocationTable WHERE RoomName = '" + editRoomcombo_box.Text + "'";
            cmd.ExecuteNonQuery();
            MessageBox.Show("Location Deleted!");
            con.Close();


            LoadLocations();
            ClearUpdateLocDetails();
            Loc_TabControl.SelectedTab = Load_Location_Page;


        }

        private void editRoomcombo_box_SelectedIndexChanged_1(object sender, EventArgs e)
        {


            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM LocationTable WHERE RoomName = '" + editRoomcombo_box.Text + "'";
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string r_building = (string)dr["BuildingName"].ToString();
                editBuilding_name_text_Box.Text = r_building;
                //editbuil_cmb.Text= r_building;

                string r_capacity = (string)dr["Capacity"].ToString();
                editCapacity.Text = r_capacity;

                string r_type = (string)dr["RoomType"].ToString();
                editRoomType_cmb.Text = r_type;
                //editbuil_cmb.Text = r_type;
            }
            con.Close();
        }

        private void search_txt_box_Click_1(object sender, EventArgs e)
        {

            if (searchby_cmb.Text == "Building")
            {


                string query = "SELECT * FROM LocationTable WHERE BuildingName LIKE '%" + search_txt_box.Text + "%'";
                con.Open();
                SqlCommand Command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(Command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Location_Grid_View_Table.DataSource = dt;
                con.Close();

            }


            if (searchby_cmb.Text == "Room")
            {

                string query = "SELECT * FROM LocationTable WHERE RoomName LIKE '%" + search_txt_box.Text + "%'";
                con.Open();
                SqlCommand Command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(Command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Location_Grid_View_Table.DataSource = dt;
                con.Close();

            }

            if (searchby_cmb.Text == "Capacity")
            {


                string query = "SELECT * FROM LocationTable WHERE Capacity LIKE '%" + search_txt_box.Text + "%'";
                con.Open();
                SqlCommand Command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(Command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Location_Grid_View_Table.DataSource = dt;
                con.Close();

            }

            if (searchby_cmb.Text == "Room Type")
            {

                string query = "SELECT * FROM LocationTable WHERE RoomType LIKE '%" + search_txt_box.Text + "%'";
                con.Open();
                SqlCommand Command = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(Command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                Location_Grid_View_Table.DataSource = dt;
                con.Close();


            }

        }

        private void Edit_Refresh_Button_Click_1(object sender, EventArgs e)
        {
            editRoomcombo_box.Items.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT RoomName FROM LocationTable";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                editRoomcombo_box.Items.Add(dr["RoomName"].ToString());
            }

            con.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatisticsPortal.Visualizingstatistic visual = new StatisticsPortal.Visualizingstatistic();
            visual.ShowDialog();

        }
    }
}
