namespace WBL_II
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.cbCollege = new System.Windows.Forms.ComboBox();
            this.collegesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wBLDB_CollegeDS = new WBL_II.WBLDB_CollegeDS();
            this.cbProgram = new System.Windows.Forms.ComboBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkCollege = new System.Windows.Forms.CheckBox();
            this.chkProgram = new System.Windows.Forms.CheckBox();
            this.lblCollege = new System.Windows.Forms.Label();
            this.lblProgram = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.collegesTableAdapter = new WBL_II.WBLDB_CollegeDSTableAdapters.CollegesTableAdapter();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.lblTerm = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.clbTerm = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.collegesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wBLDB_CollegeDS)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCollege
            // 
            this.cbCollege.DataSource = this.collegesBindingSource;
            this.cbCollege.DisplayMember = "College";
            this.cbCollege.FormattingEnabled = true;
            this.cbCollege.Location = new System.Drawing.Point(13, 65);
            this.cbCollege.Name = "cbCollege";
            this.cbCollege.Size = new System.Drawing.Size(280, 21);
            this.cbCollege.TabIndex = 0;
            this.cbCollege.SelectedIndexChanged += new System.EventHandler(this.cbCollege_SelectedIndexChanged);
            // 
            // collegesBindingSource
            // 
            this.collegesBindingSource.DataMember = "Colleges";
            this.collegesBindingSource.DataSource = this.wBLDB_CollegeDS;
            // 
            // wBLDB_CollegeDS
            // 
            this.wBLDB_CollegeDS.DataSetName = "WBLDB_CollegeDS";
            this.wBLDB_CollegeDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cbProgram
            // 
            this.cbProgram.FormattingEnabled = true;
            this.cbProgram.Location = new System.Drawing.Point(323, 65);
            this.cbProgram.Name = "cbProgram";
            this.cbProgram.Size = new System.Drawing.Size(280, 21);
            this.cbProgram.TabIndex = 1;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(13, 120);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(70, 17);
            this.chkAll.TabIndex = 2;
            this.chkAll.Text = "Select All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckStateChanged += new System.EventHandler(this.chkAll_CheckStateChanged);
            // 
            // chkCollege
            // 
            this.chkCollege.AutoSize = true;
            this.chkCollege.Location = new System.Drawing.Point(89, 120);
            this.chkCollege.Name = "chkCollege";
            this.chkCollege.Size = new System.Drawing.Size(94, 17);
            this.chkCollege.TabIndex = 3;
            this.chkCollege.Text = "Select College";
            this.chkCollege.UseVisualStyleBackColor = true;
            this.chkCollege.CheckStateChanged += new System.EventHandler(this.chkCollege_CheckStateChanged);
            // 
            // chkProgram
            // 
            this.chkProgram.AutoSize = true;
            this.chkProgram.Location = new System.Drawing.Point(195, 120);
            this.chkProgram.Name = "chkProgram";
            this.chkProgram.Size = new System.Drawing.Size(98, 17);
            this.chkProgram.TabIndex = 4;
            this.chkProgram.Text = "Select Program";
            this.chkProgram.UseVisualStyleBackColor = true;
            this.chkProgram.CheckStateChanged += new System.EventHandler(this.chkProgram_CheckStateChanged);
            // 
            // lblCollege
            // 
            this.lblCollege.AutoSize = true;
            this.lblCollege.Location = new System.Drawing.Point(12, 49);
            this.lblCollege.Name = "lblCollege";
            this.lblCollege.Size = new System.Drawing.Size(78, 13);
            this.lblCollege.TabIndex = 5;
            this.lblCollege.Text = "Select College:";
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(320, 49);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(82, 13);
            this.lblProgram.TabIndex = 6;
            this.lblProgram.Text = "Select Program:";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(15, 165);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(278, 23);
            this.pbProgress.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(327, 165);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(252, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 33);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Course Offerings";
            // 
            // collegesTableAdapter
            // 
            this.collegesTableAdapter.ClearBeforeFill = true;
            // 
            // lblTerm
            // 
            this.lblTerm.AutoSize = true;
            this.lblTerm.Location = new System.Drawing.Point(624, 49);
            this.lblTerm.Name = "lblTerm";
            this.lblTerm.Size = new System.Drawing.Size(67, 13);
            this.lblTerm.TabIndex = 11;
            this.lblTerm.Text = "Select Term:";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(15, 144);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(21, 13);
            this.lblPercent.TabIndex = 12;
            this.lblPercent.Text = "0%";
            // 
            // clbTerm
            // 
            this.clbTerm.FormattingEnabled = true;
            this.clbTerm.Location = new System.Drawing.Point(627, 65);
            this.clbTerm.Name = "clbTerm";
            this.clbTerm.Size = new System.Drawing.Size(114, 124);
            this.clbTerm.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 207);
            this.Controls.Add(this.clbTerm);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblTerm);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.lblProgram);
            this.Controls.Add(this.lblCollege);
            this.Controls.Add(this.chkProgram);
            this.Controls.Add(this.chkCollege);
            this.Controls.Add(this.chkAll);
            this.Controls.Add(this.cbProgram);
            this.Controls.Add(this.cbCollege);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.collegesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wBLDB_CollegeDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCollege;
        private System.Windows.Forms.ComboBox cbProgram;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkCollege;
        private System.Windows.Forms.CheckBox chkProgram;
        private System.Windows.Forms.Label lblCollege;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private WBLDB_CollegeDS wBLDB_CollegeDS;
        private System.Windows.Forms.BindingSource collegesBindingSource;
        private WBLDB_CollegeDSTableAdapters.CollegesTableAdapter collegesTableAdapter;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Label lblTerm;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.CheckedListBox clbTerm;
    }
}

