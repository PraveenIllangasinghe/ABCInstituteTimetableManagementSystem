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

namespace ABCInstituteTimetableManagementSystem.WorkingDaysPortal
{
    public partial class WorkingDays : Form
    {

        public int WorkingDaysId = 1;
        public int WorkingTimeId = 1;
        public int TimeSlotId = 1;
        public int value = 0;

        //Strat of Move window ---------------------------------------------------------------------------------------------

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

        //End of Move window ------------------------------------------------------------------------------------------------

        public WorkingDays()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-IM68I3A;Initial Catalog=ABCInstituteDB;Integrated Security=True";


        //View working days +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void viewWorkingDays()
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from WorkingDays", connection);
            DataTable dataTable = new DataTable();

            connection.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            connection.Close();

            wdGrid.AutoGenerateColumns = true;
            wdGrid.DataSource = dataTable;
        }
        
        private void metroLabel38_Click(object sender, EventArgs e)
        {

        }

        //View number of working days and time +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public void viewWorkingTime()
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from WorkingTime", connection);
            DataTable dataTable = new DataTable();

            connection.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            connection.Close();

            wtGrid.AutoGenerateColumns = true;
            wtGrid.DataSource = dataTable;
        }


        public void viewTimeSlot()
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand readSubjectsQuery = new SqlCommand("Select * from TimeSlot", connection);
            DataTable dataTable = new DataTable();

            connection.Open();

            SqlDataReader sqlDataReader = readSubjectsQuery.ExecuteReader();

            dataTable.Load(sqlDataReader);
            connection.Close();

            tsGrid.AutoGenerateColumns = true;
            tsGrid.DataSource = dataTable;
        }

        //created a function to clear all kinds of text feilds

        public void clearFields()
        {
            dayDropdownTS.SelectedIndex = -1;
            timeslotDropdownTS.SelectedIndex = -1;
            noWorkingDaysNUD.ResetText();
            workingTimeHrsNUD.ResetText();
            workingTimeMinsNUD.ResetText();
            monCB.Checked = false;
            tueCB.Checked = false;
            wedCB.Checked = false;
            thuCB.Checked = false;
            friCB.Checked = false;
            satCB.Checked = false;
            sunCB.Checked = false;
        }


        //Add number of working days and time +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button34_Click_1(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(conString);

            connect.Open();
            if (connect.State == System.Data.ConnectionState.Open)
            {

                string query = "INSERT INTO WorkingTime (wtId,NumberOfWorkingDays,WorkingTimePerDayHours,WorkingTimePerDayMins) VALUES (1,@NoWorkingDays,@WorkingTimeHrs,@WorkingTimeMins)";
                SqlCommand cmd = new SqlCommand(query, connect);


                cmd.Parameters.AddWithValue("@NoWorkingDays", noWorkingDaysNUD.Text);
                cmd.Parameters.AddWithValue("@WorkingTimeHrs", workingTimeHrsNUD.Text);
                cmd.Parameters.AddWithValue("@WorkingTimeMins", workingTimeMinsNUD.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Number of Working days and time have been added Successfully");


                clearFields();
            }

        }

        //Add working days  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button17_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(conString);

            connect.Open();
            if (connect.State == System.Data.ConnectionState.Open)
            {

                string query = "INSERT INTO WorkingDays (Id,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday) VALUES (1,@mon,@tue,@wed,@thu,@fri,@sat,@sun)";
                SqlCommand cmd = new SqlCommand(query, connect);


                cmd.Parameters.AddWithValue("@mon", monCB.Checked);
                cmd.Parameters.AddWithValue("@tue", tueCB.Checked);
                cmd.Parameters.AddWithValue("@wed", wedCB.Checked);
                cmd.Parameters.AddWithValue("@thu", thuCB.Checked);
                cmd.Parameters.AddWithValue("@fri", friCB.Checked);
                cmd.Parameters.AddWithValue("@sat", satCB.Checked);
                cmd.Parameters.AddWithValue("@sun", sunCB.Checked);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Working days has been Added Successfully");

                clearFields();
            }
        }

        //Update working days  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button16_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand("UPDATE WorkingDays SET Monday = @mon, Tuesday = @tue, Wednesday = @wed, Thursday = @thu, Friday = @fri, Saturday = @sat, Sunday = @sun WHERE Id = 1", connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@mon", monCB.Checked);
            cmd.Parameters.AddWithValue("@tue", tueCB.Checked);
            cmd.Parameters.AddWithValue("@wed", wedCB.Checked);
            cmd.Parameters.AddWithValue("@thu", thuCB.Checked);
            cmd.Parameters.AddWithValue("@fri", friCB.Checked);
            cmd.Parameters.AddWithValue("@sat", satCB.Checked);
            cmd.Parameters.AddWithValue("@sun", sunCB.Checked);

            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Working days have been Updated Sucessfully", "Successfully");

            clearFields();

        }

        //Delete working days  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button15_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conString);

            if (MessageBox.Show("DELETE WORKING DAYS?", "Delete WorkingDays", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM WorkingDays WHERE Id = @WorkingDaysId", connection);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@WorkingDaysId", this.WorkingDaysId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Working days have been Deleted", "Successfully");

            }
        }

        //Update number of working days and time +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button13_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand("UPDATE WorkingTime SET NumberOfWorkingDays = @NoWorkingDays, WorkingTimePerDayHours = @WorkingTimeHrs, WorkingTimePerDayMins = @WorkingTimeMins WHERE wtId = 1", connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@NoWorkingDays", noWorkingDaysNUD.Text);
            cmd.Parameters.AddWithValue("@WorkingTimeHrs", workingTimeHrsNUD.Text);
            cmd.Parameters.AddWithValue("@WorkingTimeMins", workingTimeMinsNUD.Text);

            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Number of working days and time have been updated sucessfully", "Successfully");

            clearFields();
        }

        //Delete number of working days and time +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button14_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conString);

            if (MessageBox.Show("DELETE NUMBER OF WORKING DAYS AND TIME?", "Delete WorkingTime", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM WorkingTime WHERE wtId = @WorkingTimeId", connection);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@WorkingTimeId", this.WorkingTimeId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Number of working days and time have been Deleted", "Successfully");

            }
        }

        //Add time slot +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button21_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(conString);

            connect.Open();
            if (connect.State == System.Data.ConnectionState.Open)
            {

                string query = "INSERT INTO TimeSlot (slotId,Day,TimeSlot) VALUES (1,@dayTS,@timeslotTS)";
                SqlCommand cmd = new SqlCommand(query, connect);


                cmd.Parameters.AddWithValue("@dayTS", dayDropdownTS.Text);
                cmd.Parameters.AddWithValue("@timeslotTS", timeslotDropdownTS.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Time slot been added Successfully");

                clearFields();
            }
        }

        //Update time slot +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button18_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand("UPDATE TimeSlot SET Day = @dayTS, TimeSlot = @timeslotTS WHERE slotId = 1", connection);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@dayTS", dayDropdownTS.Text);
            cmd.Parameters.AddWithValue("@timeslotTS", timeslotDropdownTS.Text);

            connection.Open();

            cmd.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Time slot has been updated sucessfully", "Successfully");

            clearFields();
        }

        //Delete time slot +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void button22_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(conString);

            if (MessageBox.Show("DELETE TIME SLOT?", "Delete TimeSlot", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                SqlCommand command = new SqlCommand("DELETE FROM TimeSlot WHERE slotId = @TimeSlotId", connection);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@TimeSlotId", this.TimeSlotId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Time slot has been Deleted", "Successfully");

            }
        }

        private void refreshWD_Click(object sender, EventArgs e)
        {
            viewWorkingDays();
            viewWorkingTime();
            viewTimeSlot();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form1 navSubPortal = new Form1();
            navSubPortal.Show();
            this.Hide();
        }
    }
}