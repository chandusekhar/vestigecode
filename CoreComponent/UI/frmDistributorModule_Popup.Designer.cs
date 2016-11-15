namespace CoreComponent.UI
{
    partial class frmDistributorModule_Popup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDistributors = new System.Windows.Forms.DataGridView();
            this.pnlSubMain = new System.Windows.Forms.Panel();
            this.pnlSubPart1 = new System.Windows.Forms.Panel();
            this.pnlSubPart1_2 = new System.Windows.Forms.Panel();
            this.pnlSubPart1_1 = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlSubPart2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributors)).BeginInit();
            this.pnlSubMain.SuspendLayout();
            this.pnlSubPart1.SuspendLayout();
            this.pnlSubPart1_2.SuspendLayout();
            this.pnlSubPart1_1.SuspendLayout();
            this.pnlSubPart2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDistributors
            // 
            this.dgvDistributors.AllowUserToAddRows = false;
            this.dgvDistributors.AllowUserToDeleteRows = false;
            this.dgvDistributors.AllowUserToResizeColumns = false;
            this.dgvDistributors.AllowUserToResizeRows = false;
            this.dgvDistributors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDistributors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDistributors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistributors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDistributors.Location = new System.Drawing.Point(0, 0);
            this.dgvDistributors.Name = "dgvDistributors";
            this.dgvDistributors.RowHeadersVisible = false;
            this.dgvDistributors.Size = new System.Drawing.Size(559, 180);
            this.dgvDistributors.TabIndex = 0;
            this.dgvDistributors.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDistributors_CellDoubleClick);
            this.dgvDistributors.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDistributors_KeyDown);
            // 
            // pnlSubMain
            // 
            this.pnlSubMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlSubMain.Controls.Add(this.pnlSubPart1);
            this.pnlSubMain.Controls.Add(this.pnlSubPart2);
            this.pnlSubMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubMain.Location = new System.Drawing.Point(12, 10);
            this.pnlSubMain.Name = "pnlSubMain";
            this.pnlSubMain.Size = new System.Drawing.Size(559, 240);
            this.pnlSubMain.TabIndex = 17;
            // 
            // pnlSubPart1
            // 
            this.pnlSubPart1.Controls.Add(this.pnlSubPart1_2);
            this.pnlSubPart1.Controls.Add(this.pnlSubPart1_1);
            this.pnlSubPart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubPart1.Location = new System.Drawing.Point(0, 0);
            this.pnlSubPart1.Name = "pnlSubPart1";
            this.pnlSubPart1.Size = new System.Drawing.Size(559, 210);
            this.pnlSubPart1.TabIndex = 18;
            // 
            // pnlSubPart1_2
            // 
            this.pnlSubPart1_2.Controls.Add(this.dgvDistributors);
            this.pnlSubPart1_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSubPart1_2.Location = new System.Drawing.Point(0, 30);
            this.pnlSubPart1_2.Name = "pnlSubPart1_2";
            this.pnlSubPart1_2.Size = new System.Drawing.Size(559, 180);
            this.pnlSubPart1_2.TabIndex = 20;
            // 
            // pnlSubPart1_1
            // 
            this.pnlSubPart1_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSubPart1_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSubPart1_1.Controls.Add(this.lblPageTitle);
            this.pnlSubPart1_1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubPart1_1.Location = new System.Drawing.Point(0, 0);
            this.pnlSubPart1_1.Name = "pnlSubPart1_1";
            this.pnlSubPart1_1.Size = new System.Drawing.Size(559, 30);
            this.pnlSubPart1_1.TabIndex = 19;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.Location = new System.Drawing.Point(3, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(45, 14);
            this.lblPageTitle.TabIndex = 9;
            this.lblPageTitle.Text = "label1";
            // 
            // pnlSubPart2
            // 
            this.pnlSubPart2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlSubPart2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSubPart2.Controls.Add(this.btnOK);
            this.pnlSubPart2.Controls.Add(this.btnCancel);
            this.pnlSubPart2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSubPart2.Location = new System.Drawing.Point(0, 210);
            this.pnlSubPart2.Name = "pnlSubPart2";
            this.pnlSubPart2.Size = new System.Drawing.Size(559, 30);
            this.pnlSubPart2.TabIndex = 17;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(385, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(472, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmDistributorModule_Popup
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(583, 260);
            this.Controls.Add(this.pnlSubMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDistributorModule_Popup";
            this.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose Distributor";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributors)).EndInit();
            this.pnlSubMain.ResumeLayout(false);
            this.pnlSubPart1.ResumeLayout(false);
            this.pnlSubPart1_2.ResumeLayout(false);
            this.pnlSubPart1_1.ResumeLayout(false);
            this.pnlSubPart1_1.PerformLayout();
            this.pnlSubPart2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDistributors;
        private System.Windows.Forms.Panel pnlSubMain;
        private System.Windows.Forms.Panel pnlSubPart2;
        private System.Windows.Forms.Panel pnlSubPart1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlSubPart1_2;
        private System.Windows.Forms.Panel pnlSubPart1_1;
        private System.Windows.Forms.Label lblPageTitle;
    }
}