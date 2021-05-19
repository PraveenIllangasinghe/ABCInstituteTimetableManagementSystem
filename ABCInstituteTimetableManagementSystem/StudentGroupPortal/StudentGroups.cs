using ABCInstituteTimetableManagementSystem.TagPortal;
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
        int progId = -1;
        int grpId = -1;
        int sgrpId = -1;
        

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

        private void StdntGrp_BtnAddProgram_Click(object sender, EventArgs e)
        {
            try
            {
                Service.Program prg = new Service.Program();

                prg.programme = StdntGrp_prgrmTxt.SelectedItem.ToString();
            

                Service.ProgrammeService programmeService = new Service.ProgrammeService();

                if (programmeService.save(prg))
                {
                    MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_prog_search_Click(object sender, EventArgs e)
        {
            try
            {

                if (StdntGrp_SrchByProgram.SelectedIndex == -1 || String.IsNullOrEmpty(StdntGrp_SrchPrgrm.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = StdntGrp_SrchByProgram.SelectedItem.ToString();
                    string value = StdntGrp_SrchPrgrm.Text.ToString();
                    Service.ProgrammeService programmeService = new Service.ProgrammeService();
                    StdntGrp_progrmTable.DataSource = programmeService.fetchProgrammeData(prop, value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void StdntGrp_progrmTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            progId = Convert.ToInt32(StdntGrp_progrmTable.Rows[e.RowIndex].Cells[0].Value.ToString());
            StdntGrp_prgrmTxt.SelectedItem = StdntGrp_progrmTable.Rows[e.RowIndex].Cells[1].Value.ToString();
         
        }

        private void StdntGrp_ProgrmBtnDelete_Click(object sender, EventArgs e)
        {
            // progId is a global variable
            if (progId == -1)
            {
                MessageBox.Show("Please select an entry to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Service.ProgrammeService ProgramService = new Service.ProgrammeService();
                bool done = ProgramService.deleteProgramme(progId);
                if (done)
                {
                    MessageBox.Show("Success", "Selected entry deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void StdntGrp_YearTxt_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StdntGrp_BtnAddGrpNo_Click(object sender, EventArgs e)
        {
            try
            {
                Service.GroupNumbers prg = new Service.GroupNumbers();

                prg.groupNumber = int.Parse(GrpNoCombo.SelectedItem.ToString());


                Service.GroupNumberService groupNumbers = new Service.GroupNumberService();

                if (groupNumbers.save(prg))
                {
                    MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void BtnSearchGN_Click(object sender, EventArgs e)
        {
            try
            {

                if (StdntGrp_SrchByGrpNo.SelectedIndex == -1 || String.IsNullOrEmpty(StdntGrp_SrchByGrpNo.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = StdntGrp_SrchByGrpNo.SelectedItem.ToString();
                    int value = int.Parse(StdntGrp_SrchGrpNo.Text);
                    Service.GroupNumberService GroupNumberService = new Service.GroupNumberService();
                    StdntGrp_GrpNoTable.DataSource = GroupNumberService.fetchGropNoData(prop, value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void StdntGrp_GrpNoTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            grpId = Convert.ToInt32(StdntGrp_GrpNoTable.Rows[e.RowIndex].Cells[0].Value.ToString());
            GrpNoCombo.SelectedItem = StdntGrp_GrpNoTable.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void StdntGrp_BtnDeleteGrpNo_Click(object sender, EventArgs e)
        {
            if (grpId == -1)
            {
                MessageBox.Show("Please select an entry to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Service.GroupNumberService groupNumberService = new Service.GroupNumberService();
                bool done = groupNumberService.deleteGroupNo(grpId);
                if (done)
                {
                    MessageBox.Show("Success", "Selected entry deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void sg_home_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 hm = new Form1();
          
            hm.Show();
        }

        private void sgp_tag_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            Tags  hm = new Tags();

            hm.Show();
        }

        private void StdntGrp_SubGrpNumbtn_Click(object sender, EventArgs e)
        {
            try
            {
                Service.SubGroupNo prg = new Service.SubGroupNo();

                prg.subGroupNo = int.Parse(SuGrpCombo.SelectedItem.ToString());


                Service.SubGroupNoService subgroupNumbers = new Service.SubGroupNoService();

                if (subgroupNumbers.save(prg))
                {
                    MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void StdntGrp_BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Service.YearSemester ys = new Service.YearSemester();

                ys.id = yrSemId;

                ys.year = int.Parse(StdntGrp_YearTxt.SelectedItem.ToString());
                ys.semester = int.Parse(StdntGrp_SemTxt.SelectedItem.ToString());

                Service.YearSemesterService yearSemesterService = new Service.YearSemesterService();
               if (yearSemesterService.update(ys))
                {
                    MessageBox.Show("Success", "Year and Semester update success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenarateGrpID_Click(object sender, EventArgs e)
        {
            string yr = GenrtYearSem.SelectedItem.ToString();
            string sem = GenrtSemCombo.SelectedItem.ToString();
            string program = GenrtProgrm.SelectedItem.ToString();
            string group = GenrtGrpNo.SelectedItem.ToString();
            //  string subGroup = GnrtSubGrpID.SelectedItem.ToString();

            string groupId = $"Y{yr}S{sem}.{group}({program})";
            GnrtGrpIdTxtBox.Text = groupId;
        }

        private void gnrateSubGrpID_Click(object sender, EventArgs e)
        {
            string yr = GenrtYearSem.SelectedItem.ToString();
            string sem = GenrtSemCombo.SelectedItem.ToString();
            string program = GenrtProgrm.SelectedItem.ToString();
            string group = GenrtGrpNo.SelectedItem.ToString();
            string subGroup = GnrtSubGrpID.SelectedItem.ToString();

            string groupId = $"Y{yr}S{sem}.{group}.{subGroup}({program})";
            GnrtSubGroIDTxtbox.Text = groupId;
        }

        private void BtnSaveGrpID_Click(object sender, EventArgs e)
        {
            try
            {
                Service.GroupIDsServices gs = new Service.GroupIDsServices();
                //if (gs.save(GnrtGrpIdTxtBox.Text))
                //{
                //    MessageBox.Show("Success", "Group Id added.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveSubGrpID_Click(object sender, EventArgs e)
        {
            try
            {
                if(String.IsNullOrEmpty(GnrtSubGroIDTxtbox.Text.ToString()) && String.IsNullOrEmpty(GnrtGrpIdTxtBox.Text.ToString()))
                {
                    MessageBox.Show("Success", "Generate group Id and Sub group Id prior saving.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Service.GroupIDsServices subgs = new Service.GroupIDsServices();
                    Service.SubGroupId sub = new Service.SubGroupId();

                    sub.year = int.Parse(GenrtYearSem.SelectedItem.ToString());
                    sub.semester = int.Parse(GenrtSemCombo.SelectedItem.ToString());
                    sub.programme = GenrtProgrm.SelectedItem.ToString();
                    sub.groupNumber = int.Parse(GenrtGrpNo.SelectedItem.ToString());
                    sub.subGroupNo = int.Parse(GnrtSubGrpID.SelectedItem.ToString());
                    sub.groupId = GnrtGrpIdTxtBox.Text.ToString();
                    sub.subgroupId = GnrtSubGroIDTxtbox.Text.ToString();


                    if (subgs.saveSubGrpID(sub))
                    {
                        MessageBox.Show("Success", "Sub Group Id added.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StdntGrp_SrchBySubGrpNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSubGrpNoSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (StdntGrp_SrchBySubGrpNo.SelectedIndex == -1 || String.IsNullOrEmpty(StdntGrp_SrchSubGrpNo.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = StdntGrp_SrchBySubGrpNo.SelectedItem.ToString();
                    int value = int.Parse(StdntGrp_SrchSubGrpNo.Text);
                    Service.SubGroupNoService subGroupNumberService = new Service.SubGroupNoService();
                    StdntGrp_SubGrpNoTable.DataSource = subGroupNumberService.fetchSubGropNoData(prop, value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void StdntGrp_SubGrpNoTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sgrpId = Convert.ToInt32(StdntGrp_SubGrpNoTable.Rows[e.RowIndex].Cells[0].Value.ToString());
            SuGrpCombo.SelectedItem = StdntGrp_SubGrpNoTable.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void StdntGrp_SubGrpNumDlt_Click(object sender, EventArgs e)
        {
            if (sgrpId == -1)
            {
                MessageBox.Show("Please select an entry to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Service.SubGroupNoService sgroupNumberService = new Service.SubGroupNoService();
                bool done = sgroupNumberService.deleteSubGroupNo(sgrpId);
                if (done)
                {
                    MessageBox.Show("Success", "Selected entry deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void StdntGrp_PrgramBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Service.Program ys = new Service.Program();

                ys.id = progId;

                ys.programme = (StdntGrp_prgrmTxt.SelectedItem.ToString());
               

                Service.ProgrammeService PRService = new Service.ProgrammeService();
                if (PRService.update(ys))
                {
                  MessageBox.Show("Success", "Programme update success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StdntGrp_SubGrpNumEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Service.SubGroupNo subg = new Service.SubGroupNo();

                subg.id = sgrpId;

                subg.subGroupNo = int.Parse(SuGrpCombo.SelectedItem.ToString());
                

                Service.SubGroupNoService subService = new Service.SubGroupNoService();
                if (subService.update(subg))
                {
                    MessageBox.Show("Success", "Sub Group Number update success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StdntGrp_BtnEditGrpNo_Click(object sender, EventArgs e)
        {
            try
            {
                Service.GroupNumbers grp = new Service.GroupNumbers();

                grp.id = grpId;

                grp.groupNumber = int.Parse(GrpNoCombo.SelectedItem.ToString());


                Service.GroupNumberService grpService = new Service.GroupNumberService();
                if (grpService.update(grp))
                {
                    MessageBox.Show("Success", "Group Number update success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewDtlsSearchBy.SelectedIndex == -1 || String.IsNullOrEmpty(ViewDtlsSearch.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = ViewDtlsSearchBy.SelectedItem.ToString();


                    string value = ViewDtlsSearch.Text;
                    Service.GroupIDsServices GroupNumberService = new Service.GroupIDsServices();
                    ViewDtlsGridView.DataSource = GroupNumberService.fetchAllDetailsPerField(prop, value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void viewAllBtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                  
                    Service.GroupIDsServices GroupNumberService = new Service.GroupIDsServices();
                    ViewDtlsGridView.DataSource = GroupNumberService.fetchAllDetails();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void StdntGrp_SrchYear_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabYearSem_Click(object sender, EventArgs e)
        {

        }
    }
    }
    

