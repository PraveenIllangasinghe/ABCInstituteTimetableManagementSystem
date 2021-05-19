using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABCInstituteTimetableManagementSystem
{
    public partial class Form1 : Form
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

        public Form1()
        {
            InitializeComponent();
        }

        private void HomeLecManageBtn_Click(object sender, EventArgs e)
        {

            LecturerPortal.Lecturers navLecPortal = new LecturerPortal.Lecturers();
            navLecPortal.Show();
            this.Hide();

        }

        private void HomeSGManageBtn_Click(object sender, EventArgs e)
        {

            StudentGroupPortal.StudentGroups navSGPortal = new StudentGroupPortal.StudentGroups();
            navSGPortal.Show();
            this.Hide();

        }

        private void HomeSubManageBtn_Click(object sender, EventArgs e)
        {

            SubjectPortal.Subjects navSubPortal = new SubjectPortal.Subjects();
            navSubPortal.Show();
            this.Hide();

        }

        private void HomeWDManageBtn_Click(object sender, EventArgs e)
        {

            WorkingDaysPortal.WorkingDays navWDPortal = new WorkingDaysPortal.WorkingDays();
            navWDPortal.Show();
            this.Hide();

        }

        private void HomeLocManageBtn_Click(object sender, EventArgs e)
        {

            LocationPortal.AddandManageLocations navLocPortal = new LocationPortal.AddandManageLocations();
            navLocPortal.Show();
            this.Hide();

        }

        private void HomeTagManageBtn_Click(object sender, EventArgs e)
        {

            TagPortal.Tags navTagPortal = new TagPortal.Tags();
            navTagPortal.Show();
            this.Hide();

        }

        private void HomeStatManageBtn_Click(object sender, EventArgs e)
        {

            StatisticsPortal.Visualizingstatistic navStatPortal = new StatisticsPortal.Visualizingstatistic();
            navStatPortal.Show();
            this.Hide();

        }

        private void HomeRoomManageBtn_Click(object sender, EventArgs e)
        {

            RoomPortal.ManageRoomsSessions navRoomPortal = new RoomPortal.ManageRoomsSessions();
            navRoomPortal.Show();
            this.Hide();

        }

        private void HomeSessManageBtn_Click(object sender, EventArgs e)
        {

            SessionPortal.Sessions navSessPortal = new SessionPortal.Sessions();
            navSessPortal.Show();
            this.Hide();

        }

        private void HomeMOManageBtn_Click(object sender, EventArgs e)
        {

            MoreOptionsPortal.MoreOptions navMOPortal = new MoreOptionsPortal.MoreOptions();
            navMOPortal.Show();
            this.Hide();

        }

        private void HomeTBManageBtn_Click(object sender, EventArgs e)
        {

           GenerateTimetablePortal.GenerateTimetables navTBPortal = new GenerateTimetablePortal.GenerateTimetables();
            navTBPortal.Show();
            this.Hide();

        }

        private void HomeUGManageBtn_Click(object sender, EventArgs e)
        {

            UserGuide.UserGuide navUGPortal = new UserGuide.UserGuide();
            navUGPortal.Show();
            this.Hide();

        }

        private void HomeCloseBtn_Click(object sender, EventArgs e)
        {

            //Close App******************************************

            Application.Exit();

            //***************************************************

        }

        private void HomeMinimizeBtn_Click(object sender, EventArgs e)
        {


            //Minimize App***************************************

            this.WindowState = FormWindowState.Minimized;

            //***************************************************

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
