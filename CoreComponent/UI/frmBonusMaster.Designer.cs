namespace CoreComponent.UI
{
    partial class frmBonusMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBonusMaster));
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpFrequency = new System.Windows.Forms.GroupBox();
            this.rdoDaily = new System.Windows.Forms.RadioButton();
            this.rdoMonthly = new System.Windows.Forms.RadioButton();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkCloseMonth = new System.Windows.Forms.CheckBox();
            this.chkBonus = new System.Windows.Forms.CheckBox();
            this.chkLevels = new System.Windows.Forms.CheckBox();
            this.chkPVBV = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlSearchHeader.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpFrequency.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGridSearch
            // 
            this.pnlGridSearch.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlGridSearch.Location = new System.Drawing.Point(0, 279);
            this.pnlGridSearch.Size = new System.Drawing.Size(1007, 370);
            // 
            // pnlSearchHeader
            // 
            this.pnlSearchHeader.Controls.Add(this.panel1);
            this.pnlSearchHeader.Size = new System.Drawing.Size(1007, 280);
            this.pnlSearchHeader.Controls.SetChildIndex(this.panel1, 0);
            this.pnlSearchHeader.Controls.SetChildIndex(this.pnlButtons, 0);
            // 
            // lblSearchResult
            // 
            this.lblSearchResult.Dock = System.Windows.Forms.DockStyle.None;
            this.lblSearchResult.Location = new System.Drawing.Point(0, 281);
            this.lblSearchResult.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.Location = new System.Drawing.Point(838, 0);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Process";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSearch.Location = new System.Drawing.Point(763, 0);
            this.btnSearch.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.Location = new System.Drawing.Point(913, 0);
            this.btnReset.TabIndex = 2;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.None;
            this.pnlButtons.Location = new System.Drawing.Point(7, 213);
            this.pnlButtons.Size = new System.Drawing.Size(988, 34);
            this.pnlButtons.Controls.SetChildIndex(this.btnReset, 0);
            this.pnlButtons.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlButtons.Controls.SetChildIndex(this.btnSearch, 0);
            this.pnlButtons.Controls.SetChildIndex(this.btnExport, 0);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.grpFrequency);
            this.panel1.Controls.Add(this.grpOptions);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 250);
            this.panel1.TabIndex = 0;
            // 
            // grpFrequency
            // 
            this.grpFrequency.Controls.Add(this.rdoDaily);
            this.grpFrequency.Controls.Add(this.rdoMonthly);
            this.grpFrequency.Location = new System.Drawing.Point(259, 8);
            this.grpFrequency.Name = "grpFrequency";
            this.grpFrequency.Size = new System.Drawing.Size(479, 60);
            this.grpFrequency.TabIndex = 0;
            this.grpFrequency.TabStop = false;
            this.grpFrequency.Text = "Batch Frequency";
            // 
            // rdoDaily
            // 
            this.rdoDaily.AutoSize = true;
            this.rdoDaily.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoDaily.Location = new System.Drawing.Point(163, 24);
            this.rdoDaily.Name = "rdoDaily";
            this.rdoDaily.Size = new System.Drawing.Size(54, 17);
            this.rdoDaily.TabIndex = 1;
            this.rdoDaily.TabStop = true;
            this.rdoDaily.Text = "Daily";
            this.rdoDaily.UseVisualStyleBackColor = true;
            this.rdoDaily.CheckedChanged += new System.EventHandler(this.rdoDaily_CheckedChanged);
            // 
            // rdoMonthly
            // 
            this.rdoMonthly.AutoSize = true;
            this.rdoMonthly.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMonthly.Location = new System.Drawing.Point(61, 24);
            this.rdoMonthly.Name = "rdoMonthly";
            this.rdoMonthly.Size = new System.Drawing.Size(69, 17);
            this.rdoMonthly.TabIndex = 0;
            this.rdoMonthly.TabStop = true;
            this.rdoMonthly.Text = "Monthly";
            this.rdoMonthly.UseVisualStyleBackColor = true;
            this.rdoMonthly.CheckedChanged += new System.EventHandler(this.rdoMonthly_CheckedChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkCloseMonth);
            this.grpOptions.Controls.Add(this.chkBonus);
            this.grpOptions.Controls.Add(this.chkLevels);
            this.grpOptions.Controls.Add(this.chkPVBV);
            this.grpOptions.Location = new System.Drawing.Point(258, 69);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(480, 130);
            this.grpOptions.TabIndex = 1;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Batch Processing Options";
            // 
            // chkCloseMonth
            // 
            this.chkCloseMonth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCloseMonth.Location = new System.Drawing.Point(62, 97);
            this.chkCloseMonth.Name = "chkCloseMonth";
            this.chkCloseMonth.Size = new System.Drawing.Size(156, 17);
            this.chkCloseMonth.TabIndex = 5;
            this.chkCloseMonth.Text = "Close Current Month";
            this.chkCloseMonth.UseVisualStyleBackColor = true;
            // 
            // chkBonus
            // 
            this.chkBonus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBonus.Location = new System.Drawing.Point(62, 74);
            this.chkBonus.Name = "chkBonus";
            this.chkBonus.Size = new System.Drawing.Size(156, 17);
            this.chkBonus.TabIndex = 4;
            this.chkBonus.Text = "Calculate Bonus Points";
            this.chkBonus.UseVisualStyleBackColor = true;
            // 
            // chkLevels
            // 
            this.chkLevels.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLevels.Location = new System.Drawing.Point(62, 51);
            this.chkLevels.Name = "chkLevels";
            this.chkLevels.Size = new System.Drawing.Size(156, 17);
            this.chkLevels.TabIndex = 3;
            this.chkLevels.Text = "Assign Levels";
            this.chkLevels.UseVisualStyleBackColor = true;
            // 
            // chkPVBV
            // 
            this.chkPVBV.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPVBV.Location = new System.Drawing.Point(62, 28);
            this.chkPVBV.Name = "chkPVBV";
            this.chkPVBV.Size = new System.Drawing.Size(156, 17);
            this.chkPVBV.TabIndex = 2;
            this.chkPVBV.Text = "Distribute PV/BV";
            this.chkPVBV.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(723, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(115, 32);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export/Import";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmBonusMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(1013, 703);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmBonusMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Bonus Master Batch";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmBonusMaster_Load);
            this.pnlSearchHeader.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpFrequency.ResumeLayout(false);
            this.grpFrequency.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpFrequency;
        private System.Windows.Forms.RadioButton rdoDaily;
        private System.Windows.Forms.RadioButton rdoMonthly;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkCloseMonth;
        private System.Windows.Forms.CheckBox chkBonus;
        private System.Windows.Forms.CheckBox chkLevels;
        private System.Windows.Forms.CheckBox chkPVBV;
        private System.Windows.Forms.Button btnExport;


    }
}