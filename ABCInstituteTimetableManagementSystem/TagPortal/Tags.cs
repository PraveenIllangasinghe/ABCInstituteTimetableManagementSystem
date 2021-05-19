using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace ABCInstituteTimetableManagementSystem.TagPortal
{
    public partial class Tags : MetroForm
    {
        int tagId = -1;
        public Tags()
        {
            InitializeComponent();
        }

        private void Tags_Load(object sender, EventArgs e)
        {

        }

        private void StdntGrp_BtnAddTag_Click(object sender, EventArgs e)
        {
            try
            {
                Service.TagName prg = new Service.TagName();

                prg.tagName = StdntGrp_tagCombo.SelectedItem.ToString();


                Service.TagNameService tagNameService = new Service.TagNameService();

                if (tagNameService.save(prg))
                {
                    MessageBox.Show("Success", "Data Insertion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_Tag_search_Click(object sender, EventArgs e)
        {
            try
            {

                if (StdntGrp_SrchByTag.SelectedIndex == -1 || String.IsNullOrEmpty(StdntGrp_SrchTag.Text.ToString()))
                {
                    MessageBox.Show("Please select a search by property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string prop = StdntGrp_SrchByTag.SelectedItem.ToString();
                    string value = StdntGrp_SrchTag.Text.ToString();
                    Service.TagNameService TagService = new Service.TagNameService();
                    StdntGrp_TagTable.DataSource = TagService.fetchTagData(prop, value);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void StdntGrp_TagTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            tagId = Convert.ToInt32(StdntGrp_TagTable.Rows[e.RowIndex].Cells[0].Value.ToString());
            StdntGrp_tagCombo.SelectedItem = StdntGrp_TagTable.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void StdntGrp_TagBtnDelete_Click(object sender, EventArgs e)
        {
            if (tagId == -1)
            {
                MessageBox.Show("Please select an entry to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Service.TagNameService tagService = new Service.TagNameService();
                bool done = tagService.deleteTag(tagId);
                if (done)
                {
                    MessageBox.Show("Success", "Selected entry deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        
    }

        private void StdntGrp_TagBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Service.TagName ys = new Service.TagName();

                ys.id = tagId;

                
                ys.tagName = (StdntGrp_tagCombo.SelectedItem.ToString());

                Service.TagNameService tagService = new Service.TagNameService();
                if (tagService.update(ys))
                {
                    MessageBox.Show("Success", "Year and Semester update success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TGBack_Click(object sender, EventArgs e)
        {
            Form1 navSubPortal = new Form1();
            navSubPortal.Show();
            this.Hide();
        }
    }
}
