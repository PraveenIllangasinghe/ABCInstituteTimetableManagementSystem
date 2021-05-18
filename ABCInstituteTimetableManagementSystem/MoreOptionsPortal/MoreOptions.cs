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

     
    }
}
