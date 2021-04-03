using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABCInstituteTimetableManagementSystem.StudentGroupPortal
{
    public partial class StudentGroups : Form
    {
        int yrSemId = -1;

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

        public StudentGroups()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }


        private void testtab_Load(object sender, EventArgs e)
        {

        }

        private void tabGrpNo_Click(object sender, EventArgs e)
        {

        }

        private void metroLabel14_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void BtnAddYearSem_Click(object sender, EventArgs e)
        {
            try
            {
                Service.YearSemester yrSem = new Service.YearSemester();

                yrSem.year = int.Parse(StdntGrp_YearTxt.SelectedItem.ToString());
                yrSem.semester = int.Parse(StdntGrp_SemTxt.SelectedItem.ToString());

                Service.YearSemesterService yearSemesterService = new Service.YearSemesterService();

                if (yearSemesterService.save(yrSem))
                {
                    MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
        }

        private void btn_Yr_search_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (StdntGrp_SrchByYear.SelectedIndex == -1 || String.IsNullOrEmpty(StdntGrp_SrchYear.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = StdntGrp_SrchByYear.SelectedItem.ToString();
                    int value = int.Parse(StdntGrp_SrchYear.Text);
                    Service.YearSemesterService yearSemesterService = new Service.YearSemesterService();
                    StdntGrp_YearTable.DataSource = yearSemesterService.fetchYearSemData(prop, value);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void StdntGrp_YearTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            yrSemId = Convert.ToInt32(StdntGrp_YearTable.Rows[e.RowIndex].Cells[0].Value.ToString());
            StdntGrp_YearTxt.SelectedItem = StdntGrp_YearTable.Rows[e.RowIndex].Cells[1].Value.ToString();
            StdntGrp_SemTxt.SelectedItem = StdntGrp_YearTable.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void StdntGrp_BtnDelete_Click(object sender, EventArgs e)
        {
            if(yrSemId == -1)
            {
                MessageBox.Show("Please select an entry to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                Service.YearSemesterService yearSemesterService = new Service.YearSemesterService();
                bool done = yearSemesterService.deleteYearSem(yrSemId);
                if (done)
                {
                    MessageBox.Show("Success", "Selected entry deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
