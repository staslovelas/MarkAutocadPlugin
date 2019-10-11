using System;
using System.Windows.Forms;

namespace MarkAnalyzerAutocadPlagin
{
    public partial class PluginWizard : Form
    {

        int pageFlag = 1;

        public string OpenFileName {
            get
            {
                string name = this.inPathTextbox.Text.Trim();
                if (name.Equals(""))
                {
                    return null;
                }
                return name;
            }
        }

        public string WriteFileName
        {
            get
            {
                string name = this.xlNameTextBox.Text.Trim();
                if (name.Equals(""))
                {
                    return null;
                }
                return name;
            }
        }

        public bool ColorMarks
        {
            get
            {
                return this.colorMarksCB.Checked;
            }
        }

        public bool DeleteMarks
        {
            get
            {
                return this.deleteMarksCB.Checked;
            }
        }
        public bool ExportOnlyGoodMarks
        {
            get
            {
                return this.onlyGoodMarksCB.Checked;
            }
        }

        public bool ExportToExcel
        {
            get
            {
                return this.expResCB.Checked;
            }
        }

        public bool FromAutocad
        {
            get
            {
                return this.drawCB.Checked;
            }
        }

        public bool FromExcel
        {
            get
            {
                return this.excelCB.Checked;
            }
        }

        public string FirstBear
        {
            get
            {
                string first = this.frstBearMark.Text.Trim();
                if (!first.Equals(""))
                {
                    return first;
                }
                return null;
            }
        }

        public string SecondBear
        {
            get
            {
                string second = this.secBearMark.Text.Trim();
                if (!second.Equals(""))
                {
                    return second;
                }
                return null;
            }
        }

        public string ThirdBear
        {
            get
            {
                string third = this.thrdBearMark.Text.Trim();
                if (!third.Equals(""))
                {
                    return third;
                }
                return null;
            }
        }

        public string MlParameter
        {
            get
            {
                string coef = this.mlTextbox.Text.Trim();
                if (coef != null)
                {
                    return coef;
                }
                return null;
            }
        }

        public string MnParameter
        {
            get
            {
                string coef = this.mnTextbox.Text.Trim();
                if (coef != null)
                {
                    return coef;
                }
                return null;
            }
        }

        public PluginWizard()
        {
            InitializeComponent();
        }

        private void InBrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel files(*.xls)|*.xls|Open XML files(*.xlsx)|*.xlsx";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    inPathTextbox.Text = ofd.FileName;
                }
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            switch (pageFlag)
            {
                case 2:
                    settingsPage.Visible = false;
                    backBtn.Enabled = false;
                    pageFlag = 1;
                    break;
                case 3:
                    paramPage.Visible = false;
                    nextBtn.Enabled = true;
                    finishBtn.Enabled = false;
                    pageFlag = 2;
                    break;
                default:
                    break;
            }
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (pageFlag == 1 && FromExcel)
            {
                if (OpenFileName == null)
                {
                    MessageBox.Show("ERROR: Вы не ввели имя файла с исходными данными\n");
                    return;
                }
            }

            if (pageFlag == 2 && ExportToExcel)
            {
                if (WriteFileName == null)
                {
                    MessageBox.Show("ERROR: Вы не ввели имя файла для экспорта данных\n");
                    return;
                }
            }

            switch (pageFlag)
            {
                case 1:
                    settingsPage.Visible = true;
                    backBtn.Enabled = true;
                    pageFlag = 2;
                    break;
                case 2:
                    paramPage.Visible = true;
                    nextBtn.Enabled = false;
                    finishBtn.Enabled = true;
                    pageFlag = 3;
                    break;
                default:
                    break;
            }           
        }

        private void FinishBtn_Click(object sender, EventArgs e)
        {
            if (mnTextbox.Text == "" || mlTextbox.Text == "" || frstBearMark.Text == "" || secBearMark.Text == "" || thrdBearMark.Text == "")
            {
                MessageBox.Show("ERROR: Вы заполнили не все поля параметров\n");
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void DrawCB_CheckedChanged(object sender, EventArgs e)
        {
            if (FromAutocad)
            {
                excelCB.Checked = false;
            }
            else
            {
                excelCB.Checked = true;
            }
        }

        private void ExcelCB_CheckedChanged(object sender, EventArgs e)
        {
            if (FromExcel)
            {
                inBrowseBtn.Enabled = true;
                inPathTextbox.Enabled = true;
                drawCB.Checked = false;
            }
            else
            {
                inBrowseBtn.Enabled = false;
                inPathTextbox.Enabled = false;
                drawCB.Checked = true;
            }
        }

        private void DeleteMarksCB_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteMarks)
            {
                colorMarksCB.Checked = false;
            }
            else
            {
                colorMarksCB.Checked = true;
            }
        }

        private void ColorMarksCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ColorMarks)
            {
                deleteMarksCB.Checked = false;
            }
            else
            {
                deleteMarksCB.Checked = true;
            }
        }

        private void ExpResCB_CheckedChanged(object sender, EventArgs e)
        {
            if (ExportToExcel)
            {
                xlNameTextBox.Enabled = true;
                onlyGoodMarksCB.Enabled = true;
                fileSaveBrowseBtn.Enabled = true;               
            }
            else
            {
                xlNameTextBox.Enabled = false;
                onlyGoodMarksCB.Enabled = false;
                fileSaveBrowseBtn.Enabled = false;
            }
        }

        private void FileSaveBrowseBtn_Click(object sender, EventArgs e)
        {
            string fileName = String.Empty;

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileName = sfd.FileName;
                xlNameTextBox.Text = fileName;
            }
        }

        private void MnTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 46)
            {
                e.Handled = true;
            }
        }

        private void MlTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 46)
            {
                e.Handled = true;
            }
        }
    }
}
