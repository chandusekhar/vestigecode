namespace CoreComponent.UI
{
    partial class DashBoard
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPanelList = new System.Windows.Forms.ComboBox();
            this.btnAddPAnel = new System.Windows.Forms.Button();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.tlpBase = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.cmbPanelList);
            this.pnlHeader.Controls.Add(this.btnAddPAnel);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1005, 41);
            this.pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add Panel";
            // 
            // cmbPanelList
            // 
            this.cmbPanelList.FormattingEnabled = true;
            this.cmbPanelList.Location = new System.Drawing.Point(101, 6);
            this.cmbPanelList.Name = "cmbPanelList";
            this.cmbPanelList.Size = new System.Drawing.Size(121, 21);
            this.cmbPanelList.TabIndex = 1;
            // 
            // btnAddPAnel
            // 
            this.btnAddPAnel.Location = new System.Drawing.Point(228, 6);
            this.btnAddPAnel.Name = "btnAddPAnel";
            this.btnAddPAnel.Size = new System.Drawing.Size(75, 23);
            this.btnAddPAnel.TabIndex = 0;
            this.btnAddPAnel.Text = "Add Panel";
            this.btnAddPAnel.UseVisualStyleBackColor = true;
            this.btnAddPAnel.Click += new System.EventHandler(this.btnAddColumns_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.tlpBase);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 41);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1005, 628);
            this.pnlControls.TabIndex = 1;
            // 
            // tlpBase
            // 
            this.tlpBase.ColumnCount = 2;
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBase.Location = new System.Drawing.Point(0, 0);
            this.tlpBase.Name = "tlpBase";
            this.tlpBase.RowCount = 2;
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBase.Size = new System.Drawing.Size(1005, 628);
            this.tlpBase.TabIndex = 2;
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 669);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DashBoard";
            this.Text = "DashBoard";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPanelList;
        private System.Windows.Forms.Button btnAddPAnel;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.TableLayoutPanel tlpBase;
    }
}