namespace MarkAnalyzerAutocadPlagin
{
    partial class PluginWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginWizard));
            this.nextBtn = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.startPageIntro = new System.Windows.Forms.Label();
            this.drawCB = new System.Windows.Forms.CheckBox();
            this.excelCB = new System.Windows.Forms.CheckBox();
            this.inPathTextbox = new System.Windows.Forms.TextBox();
            this.inBrowseBtn = new System.Windows.Forms.Button();
            this.settingsPage = new System.Windows.Forms.Panel();
            this.fileSaveBrowseBtn = new System.Windows.Forms.Button();
            this.onlyGoodMarksCB = new System.Windows.Forms.CheckBox();
            this.xlNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.expResCB = new System.Windows.Forms.CheckBox();
            this.colorMarksCB = new System.Windows.Forms.CheckBox();
            this.deleteMarksCB = new System.Windows.Forms.CheckBox();
            this.introSettingsPage = new System.Windows.Forms.Label();
            this.paramPage = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.thrdBearMark = new System.Windows.Forms.TextBox();
            this.secBearMark = new System.Windows.Forms.TextBox();
            this.frstBearMark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mlTextbox = new System.Windows.Forms.TextBox();
            this.mnTextbox = new System.Windows.Forms.TextBox();
            this.introCoefPage = new System.Windows.Forms.Label();
            this.backBtn = new System.Windows.Forms.Button();
            this.finishBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.settingsPage.SuspendLayout();
            this.paramPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextBtn
            // 
            this.nextBtn.Location = new System.Drawing.Point(296, 356);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(75, 23);
            this.nextBtn.TabIndex = 1;
            this.nextBtn.Text = "Далее >";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.NextBtn_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox.ErrorImage = null;
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(193, 367);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // startPageIntro
            // 
            this.startPageIntro.AutoSize = true;
            this.startPageIntro.Location = new System.Drawing.Point(217, 21);
            this.startPageIntro.Name = "startPageIntro";
            this.startPageIntro.Size = new System.Drawing.Size(267, 13);
            this.startPageIntro.TabIndex = 3;
            this.startPageIntro.Text = "Пожалуйста, выберите источник исходных данных:";
            // 
            // drawCB
            // 
            this.drawCB.AutoSize = true;
            this.drawCB.Checked = true;
            this.drawCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawCB.Location = new System.Drawing.Point(217, 51);
            this.drawCB.Name = "drawCB";
            this.drawCB.Size = new System.Drawing.Size(112, 17);
            this.drawCB.TabIndex = 4;
            this.drawCB.Text = "Чертеж AutoCAD";
            this.drawCB.UseVisualStyleBackColor = true;
            this.drawCB.CheckedChanged += new System.EventHandler(this.DrawCB_CheckedChanged);
            // 
            // excelCB
            // 
            this.excelCB.AutoSize = true;
            this.excelCB.Location = new System.Drawing.Point(217, 74);
            this.excelCB.Name = "excelCB";
            this.excelCB.Size = new System.Drawing.Size(128, 17);
            this.excelCB.TabIndex = 5;
            this.excelCB.Text = "Файл Excel (xls, xlsx)";
            this.excelCB.UseVisualStyleBackColor = true;
            this.excelCB.CheckedChanged += new System.EventHandler(this.ExcelCB_CheckedChanged);
            // 
            // inPathTextbox
            // 
            this.inPathTextbox.Enabled = false;
            this.inPathTextbox.Location = new System.Drawing.Point(217, 97);
            this.inPathTextbox.Name = "inPathTextbox";
            this.inPathTextbox.Size = new System.Drawing.Size(231, 20);
            this.inPathTextbox.TabIndex = 6;
            // 
            // inBrowseBtn
            // 
            this.inBrowseBtn.Enabled = false;
            this.inBrowseBtn.Location = new System.Drawing.Point(454, 96);
            this.inBrowseBtn.Name = "inBrowseBtn";
            this.inBrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.inBrowseBtn.TabIndex = 7;
            this.inBrowseBtn.Text = "Выбрать";
            this.inBrowseBtn.UseVisualStyleBackColor = true;
            this.inBrowseBtn.Click += new System.EventHandler(this.InBrowseBtn_Click);
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.fileSaveBrowseBtn);
            this.settingsPage.Controls.Add(this.onlyGoodMarksCB);
            this.settingsPage.Controls.Add(this.xlNameTextBox);
            this.settingsPage.Controls.Add(this.label1);
            this.settingsPage.Controls.Add(this.expResCB);
            this.settingsPage.Controls.Add(this.colorMarksCB);
            this.settingsPage.Controls.Add(this.deleteMarksCB);
            this.settingsPage.Controls.Add(this.introSettingsPage);
            this.settingsPage.Location = new System.Drawing.Point(217, 21);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Size = new System.Drawing.Size(312, 329);
            this.settingsPage.TabIndex = 8;
            this.settingsPage.Visible = false;
            // 
            // fileSaveBrowseBtn
            // 
            this.fileSaveBrowseBtn.Enabled = false;
            this.fileSaveBrowseBtn.Location = new System.Drawing.Point(237, 161);
            this.fileSaveBrowseBtn.Name = "fileSaveBrowseBtn";
            this.fileSaveBrowseBtn.Size = new System.Drawing.Size(75, 23);
            this.fileSaveBrowseBtn.TabIndex = 7;
            this.fileSaveBrowseBtn.Text = "Выбрать";
            this.fileSaveBrowseBtn.UseVisualStyleBackColor = true;
            this.fileSaveBrowseBtn.Click += new System.EventHandler(this.FileSaveBrowseBtn_Click);
            // 
            // onlyGoodMarksCB
            // 
            this.onlyGoodMarksCB.AutoSize = true;
            this.onlyGoodMarksCB.Enabled = false;
            this.onlyGoodMarksCB.Location = new System.Drawing.Point(0, 198);
            this.onlyGoodMarksCB.Name = "onlyGoodMarksCB";
            this.onlyGoodMarksCB.Size = new System.Drawing.Size(285, 17);
            this.onlyGoodMarksCB.TabIndex = 6;
            this.onlyGoodMarksCB.Text = "Оставить в файле только неотбракованные марки";
            this.onlyGoodMarksCB.UseVisualStyleBackColor = true;
            // 
            // xlNameTextBox
            // 
            this.xlNameTextBox.Enabled = false;
            this.xlNameTextBox.Location = new System.Drawing.Point(0, 162);
            this.xlNameTextBox.Name = "xlNameTextBox";
            this.xlNameTextBox.Size = new System.Drawing.Size(231, 20);
            this.xlNameTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Введите имя файла (формат xls):";
            // 
            // expResCB
            // 
            this.expResCB.AutoSize = true;
            this.expResCB.Location = new System.Drawing.Point(0, 114);
            this.expResCB.Name = "expResCB";
            this.expResCB.Size = new System.Drawing.Size(273, 17);
            this.expResCB.TabIndex = 3;
            this.expResCB.Text = "Экспортировать результаты вычислений в Excel";
            this.expResCB.UseVisualStyleBackColor = true;
            this.expResCB.CheckedChanged += new System.EventHandler(this.ExpResCB_CheckedChanged);
            // 
            // colorMarksCB
            // 
            this.colorMarksCB.AutoSize = true;
            this.colorMarksCB.Location = new System.Drawing.Point(0, 53);
            this.colorMarksCB.Name = "colorMarksCB";
            this.colorMarksCB.Size = new System.Drawing.Size(206, 17);
            this.colorMarksCB.TabIndex = 2;
            this.colorMarksCB.Text = "Выделить марки разными цветами";
            this.colorMarksCB.UseVisualStyleBackColor = true;
            this.colorMarksCB.CheckedChanged += new System.EventHandler(this.ColorMarksCB_CheckedChanged);
            // 
            // deleteMarksCB
            // 
            this.deleteMarksCB.AutoSize = true;
            this.deleteMarksCB.Checked = true;
            this.deleteMarksCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteMarksCB.Location = new System.Drawing.Point(0, 30);
            this.deleteMarksCB.Name = "deleteMarksCB";
            this.deleteMarksCB.Size = new System.Drawing.Size(230, 17);
            this.deleteMarksCB.TabIndex = 1;
            this.deleteMarksCB.Text = "Удалить не прошедшие проверку марки";
            this.deleteMarksCB.UseVisualStyleBackColor = true;
            this.deleteMarksCB.CheckedChanged += new System.EventHandler(this.DeleteMarksCB_CheckedChanged);
            // 
            // introSettingsPage
            // 
            this.introSettingsPage.AutoSize = true;
            this.introSettingsPage.Location = new System.Drawing.Point(0, 0);
            this.introSettingsPage.Name = "introSettingsPage";
            this.introSettingsPage.Size = new System.Drawing.Size(299, 13);
            this.introSettingsPage.TabIndex = 0;
            this.introSettingsPage.Text = "Укажите, как именно отобразить результат вычислений:";
            // 
            // paramPage
            // 
            this.paramPage.Controls.Add(this.label8);
            this.paramPage.Controls.Add(this.label7);
            this.paramPage.Controls.Add(this.label6);
            this.paramPage.Controls.Add(this.label3);
            this.paramPage.Controls.Add(this.thrdBearMark);
            this.paramPage.Controls.Add(this.secBearMark);
            this.paramPage.Controls.Add(this.frstBearMark);
            this.paramPage.Controls.Add(this.label5);
            this.paramPage.Controls.Add(this.label4);
            this.paramPage.Controls.Add(this.mlTextbox);
            this.paramPage.Controls.Add(this.mnTextbox);
            this.paramPage.Controls.Add(this.introCoefPage);
            this.paramPage.Location = new System.Drawing.Point(217, 21);
            this.paramPage.Name = "paramPage";
            this.paramPage.Size = new System.Drawing.Size(312, 329);
            this.paramPage.TabIndex = 6;
            this.paramPage.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(195, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "3:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(96, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "2:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-3, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Введите имена опорных марок:";
            // 
            // thrdBearMark
            // 
            this.thrdBearMark.Location = new System.Drawing.Point(217, 185);
            this.thrdBearMark.Name = "thrdBearMark";
            this.thrdBearMark.Size = new System.Drawing.Size(44, 20);
            this.thrdBearMark.TabIndex = 8;
            // 
            // secBearMark
            // 
            this.secBearMark.Location = new System.Drawing.Point(118, 185);
            this.secBearMark.Name = "secBearMark";
            this.secBearMark.Size = new System.Drawing.Size(44, 20);
            this.secBearMark.TabIndex = 7;
            // 
            // frstBearMark
            // 
            this.frstBearMark.Location = new System.Drawing.Point(19, 185);
            this.frstBearMark.Name = "frstBearMark";
            this.frstBearMark.Size = new System.Drawing.Size(44, 20);
            this.frstBearMark.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "ML - СКО измерения расстояния*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mn - предельная СКО определения координат*";
            // 
            // mlTextbox
            // 
            this.mlTextbox.Location = new System.Drawing.Point(0, 118);
            this.mlTextbox.Name = "mlTextbox";
            this.mlTextbox.Size = new System.Drawing.Size(174, 20);
            this.mlTextbox.TabIndex = 2;
            this.mlTextbox.Text = "0.001";
            this.mlTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MlTextbox_KeyPress);
            // 
            // mnTextbox
            // 
            this.mnTextbox.Location = new System.Drawing.Point(0, 51);
            this.mnTextbox.Name = "mnTextbox";
            this.mnTextbox.Size = new System.Drawing.Size(175, 20);
            this.mnTextbox.TabIndex = 1;
            this.mnTextbox.Text = "0.005";
            this.mnTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MnTextbox_KeyPress);
            // 
            // introCoefPage
            // 
            this.introCoefPage.AutoSize = true;
            this.introCoefPage.Location = new System.Drawing.Point(0, 0);
            this.introCoefPage.Name = "introCoefPage";
            this.introCoefPage.Size = new System.Drawing.Size(179, 13);
            this.introCoefPage.TabIndex = 0;
            this.introCoefPage.Text = "Пожалуйста, введите параметры:";
            // 
            // backBtn
            // 
            this.backBtn.Enabled = false;
            this.backBtn.Location = new System.Drawing.Point(217, 356);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(75, 23);
            this.backBtn.TabIndex = 9;
            this.backBtn.Text = "< Назад";
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // finishBtn
            // 
            this.finishBtn.Enabled = false;
            this.finishBtn.Location = new System.Drawing.Point(454, 356);
            this.finishBtn.Name = "finishBtn";
            this.finishBtn.Size = new System.Drawing.Size(75, 23);
            this.finishBtn.TabIndex = 10;
            this.finishBtn.Text = "Посчитать";
            this.finishBtn.UseVisualStyleBackColor = true;
            this.finishBtn.Click += new System.EventHandler(this.FinishBtn_Click);
            // 
            // PluginWizard
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 391);
            this.Controls.Add(this.paramPage);
            this.Controls.Add(this.finishBtn);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.settingsPage);
            this.Controls.Add(this.inBrowseBtn);
            this.Controls.Add(this.inPathTextbox);
            this.Controls.Add(this.excelCB);
            this.Controls.Add(this.drawCB);
            this.Controls.Add(this.startPageIntro);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.nextBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(557, 430);
            this.MinimumSize = new System.Drawing.Size(557, 430);
            this.Name = "PluginWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mark Analyzer Wizard";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            this.paramPage.ResumeLayout(false);
            this.paramPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label startPageIntro;
        private System.Windows.Forms.CheckBox drawCB;
        private System.Windows.Forms.CheckBox excelCB;
        private System.Windows.Forms.TextBox inPathTextbox;
        private System.Windows.Forms.Button inBrowseBtn;
        private System.Windows.Forms.Panel settingsPage;
        private System.Windows.Forms.Panel paramPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mlTextbox;
        private System.Windows.Forms.TextBox mnTextbox;
        private System.Windows.Forms.Label introCoefPage;
        private System.Windows.Forms.CheckBox expResCB;
        private System.Windows.Forms.CheckBox colorMarksCB;
        private System.Windows.Forms.CheckBox deleteMarksCB;
        private System.Windows.Forms.Label introSettingsPage;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button finishBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xlNameTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox thrdBearMark;
        private System.Windows.Forms.TextBox secBearMark;
        private System.Windows.Forms.TextBox frstBearMark;
        private System.Windows.Forms.CheckBox onlyGoodMarksCB;
        private System.Windows.Forms.Button fileSaveBrowseBtn;
    }
}