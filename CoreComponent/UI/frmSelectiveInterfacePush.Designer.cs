namespace CoreComponent.UI
{
    partial class frmSelectiveInterfacePush
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
            this.pnlBodyMain = new System.Windows.Forms.Panel();
            this.grpRecordToAdd = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cmbLocType = new System.Windows.Forms.ComboBox();
            this.lblLocType = new System.Windows.Forms.Label();
            this.cmbAction = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblLocCode = new System.Windows.Forms.Label();
            this.txtID5 = new System.Windows.Forms.TextBox();
            this.lblPUC = new System.Windows.Forms.Label();
            this.txtID4 = new System.Windows.Forms.TextBox();
            this.lblPushType = new System.Windows.Forms.Label();
            this.txtID3 = new System.Windows.Forms.TextBox();
            this.lblID1 = new System.Windows.Forms.Label();
            this.txtID2 = new System.Windows.Forms.TextBox();
            this.lblID2 = new System.Windows.Forms.Label();
            this.txtID1 = new System.Windows.Forms.TextBox();
            this.lblID3 = new System.Windows.Forms.Label();
            this.cmbPushType = new System.Windows.Forms.ComboBox();
            this.lblID4 = new System.Windows.Forms.Label();
            this.cmbInterfaceProcess = new System.Windows.Forms.ComboBox();
            this.lblID5 = new System.Windows.Forms.Label();
            this.cmbPUC = new System.Windows.Forms.ComboBox();
            this.lblInterface = new System.Windows.Forms.Label();
            this.cmbLocCode = new System.Windows.Forms.ComboBox();
            this.lblUnprocessedRecords = new System.Windows.Forms.Label();
            this.dgvUnprocessedRecords = new System.Windows.Forms.DataGridView();
            this.errprovValidate = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBase.SuspendLayout();
            this.pnlBodyMain.SuspendLayout();
            this.grpRecordToAdd.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnprocessedRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errprovValidate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Location = new System.Drawing.Point(292, 3);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExit.BackgroundImage = global::CoreComponent.Properties.Resources.exit;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExit.Location = new System.Drawing.Point(935, 3);
            this.btnExit.Size = new System.Drawing.Size(75, 29);
            // 
            // pnlBodyMain
            // 
            this.pnlBodyMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBodyMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pnlBodyMain.Controls.Add(this.grpRecordToAdd);
            this.pnlBodyMain.Controls.Add(this.lblUnprocessedRecords);
            this.pnlBodyMain.Controls.Add(this.dgvUnprocessedRecords);
            this.pnlBodyMain.Location = new System.Drawing.Point(0, 43);
            this.pnlBodyMain.Name = "pnlBodyMain";
            this.pnlBodyMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBodyMain.Size = new System.Drawing.Size(1013, 627);
            this.pnlBodyMain.TabIndex = 0;
            // 
            // grpRecordToAdd
            // 
            this.grpRecordToAdd.Controls.Add(this.panel2);
            this.grpRecordToAdd.Controls.Add(this.cmbLocType);
            this.grpRecordToAdd.Controls.Add(this.lblLocType);
            this.grpRecordToAdd.Controls.Add(this.cmbAction);
            this.grpRecordToAdd.Controls.Add(this.lblAction);
            this.grpRecordToAdd.Controls.Add(this.lblLocCode);
            this.grpRecordToAdd.Controls.Add(this.txtID5);
            this.grpRecordToAdd.Controls.Add(this.lblPUC);
            this.grpRecordToAdd.Controls.Add(this.txtID4);
            this.grpRecordToAdd.Controls.Add(this.lblPushType);
            this.grpRecordToAdd.Controls.Add(this.txtID3);
            this.grpRecordToAdd.Controls.Add(this.lblID1);
            this.grpRecordToAdd.Controls.Add(this.txtID2);
            this.grpRecordToAdd.Controls.Add(this.lblID2);
            this.grpRecordToAdd.Controls.Add(this.txtID1);
            this.grpRecordToAdd.Controls.Add(this.lblID3);
            this.grpRecordToAdd.Controls.Add(this.cmbPushType);
            this.grpRecordToAdd.Controls.Add(this.lblID4);
            this.grpRecordToAdd.Controls.Add(this.cmbInterfaceProcess);
            this.grpRecordToAdd.Controls.Add(this.lblID5);
            this.grpRecordToAdd.Controls.Add(this.cmbPUC);
            this.grpRecordToAdd.Controls.Add(this.lblInterface);
            this.grpRecordToAdd.Controls.Add(this.cmbLocCode);
            this.grpRecordToAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpRecordToAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRecordToAdd.Location = new System.Drawing.Point(10, 10);
            this.grpRecordToAdd.Name = "grpRecordToAdd";
            this.grpRecordToAdd.Size = new System.Drawing.Size(993, 216);
            this.grpRecordToAdd.TabIndex = 0;
            this.grpRecordToAdd.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnReset);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 181);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(987, 32);
            this.panel2.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(837, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.BackgroundImage = global::CoreComponent.Properties.Resources.button;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(183)))), ((int)(((byte)(222)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(912, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cmbLocType
            // 
            this.cmbLocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocType.FormattingEnabled = true;
            this.cmbLocType.Location = new System.Drawing.Point(287, 17);
            this.cmbLocType.Name = "cmbLocType";
            this.cmbLocType.Size = new System.Drawing.Size(117, 21);
            this.cmbLocType.TabIndex = 0;
            this.cmbLocType.SelectedIndexChanged += new System.EventHandler(this.cmbLocType_SelectedIndexChanged);
            // 
            // lblLocType
            // 
            this.lblLocType.AutoSize = true;
            this.lblLocType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocType.Location = new System.Drawing.Point(183, 20);
            this.lblLocType.Name = "lblLocType";
            this.lblLocType.Size = new System.Drawing.Size(98, 13);
            this.lblLocType.TabIndex = 1;
            this.lblLocType.Text = "Location Type:*";
            this.lblLocType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbAction
            // 
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.FormattingEnabled = true;
            this.cmbAction.Location = new System.Drawing.Point(287, 122);
            this.cmbAction.Name = "cmbAction";
            this.cmbAction.Size = new System.Drawing.Size(117, 21);
            this.cmbAction.TabIndex = 4;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(227, 125);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(54, 13);
            this.lblAction.TabIndex = 10;
            this.lblAction.Text = "Action:*";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLocCode
            // 
            this.lblLocCode.AutoSize = true;
            this.lblLocCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocCode.Location = new System.Drawing.Point(181, 47);
            this.lblLocCode.Name = "lblLocCode";
            this.lblLocCode.Size = new System.Drawing.Size(100, 13);
            this.lblLocCode.TabIndex = 2;
            this.lblLocCode.Text = "Location Code:*";
            this.lblLocCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID5
            // 
            this.txtID5.Location = new System.Drawing.Point(609, 148);
            this.txtID5.Name = "txtID5";
            this.txtID5.Size = new System.Drawing.Size(205, 21);
            this.txtID5.TabIndex = 10;
            // 
            // lblPUC
            // 
            this.lblPUC.AutoSize = true;
            this.lblPUC.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPUC.Location = new System.Drawing.Point(245, 73);
            this.lblPUC.Name = "lblPUC";
            this.lblPUC.Size = new System.Drawing.Size(36, 13);
            this.lblPUC.TabIndex = 3;
            this.lblPUC.Text = "PUC:";
            this.lblPUC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID4
            // 
            this.txtID4.Location = new System.Drawing.Point(609, 122);
            this.txtID4.Name = "txtID4";
            this.txtID4.Size = new System.Drawing.Size(205, 21);
            this.txtID4.TabIndex = 9;
            // 
            // lblPushType
            // 
            this.lblPushType.AutoSize = true;
            this.lblPushType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPushType.Location = new System.Drawing.Point(203, 99);
            this.lblPushType.Name = "lblPushType";
            this.lblPushType.Size = new System.Drawing.Size(78, 13);
            this.lblPushType.TabIndex = 4;
            this.lblPushType.Text = "Push Type:*";
            this.lblPushType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID3
            // 
            this.txtID3.Location = new System.Drawing.Point(609, 96);
            this.txtID3.Name = "txtID3";
            this.txtID3.Size = new System.Drawing.Size(205, 21);
            this.txtID3.TabIndex = 8;
            // 
            // lblID1
            // 
            this.lblID1.AutoSize = true;
            this.lblID1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID1.Location = new System.Drawing.Point(566, 47);
            this.lblID1.Name = "lblID1";
            this.lblID1.Size = new System.Drawing.Size(37, 13);
            this.lblID1.TabIndex = 5;
            this.lblID1.Text = "ID 1:";
            this.lblID1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID2
            // 
            this.txtID2.Location = new System.Drawing.Point(609, 70);
            this.txtID2.Name = "txtID2";
            this.txtID2.Size = new System.Drawing.Size(205, 21);
            this.txtID2.TabIndex = 7;
            // 
            // lblID2
            // 
            this.lblID2.AutoSize = true;
            this.lblID2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID2.Location = new System.Drawing.Point(566, 73);
            this.lblID2.Name = "lblID2";
            this.lblID2.Size = new System.Drawing.Size(37, 13);
            this.lblID2.TabIndex = 6;
            this.lblID2.Text = "ID 2:";
            this.lblID2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID1
            // 
            this.txtID1.Location = new System.Drawing.Point(609, 44);
            this.txtID1.Name = "txtID1";
            this.txtID1.Size = new System.Drawing.Size(205, 21);
            this.txtID1.TabIndex = 6;
            // 
            // lblID3
            // 
            this.lblID3.AutoSize = true;
            this.lblID3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID3.Location = new System.Drawing.Point(566, 99);
            this.lblID3.Name = "lblID3";
            this.lblID3.Size = new System.Drawing.Size(37, 13);
            this.lblID3.TabIndex = 7;
            this.lblID3.Text = "ID 3:";
            this.lblID3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPushType
            // 
            this.cmbPushType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPushType.FormattingEnabled = true;
            this.cmbPushType.Location = new System.Drawing.Point(287, 96);
            this.cmbPushType.Name = "cmbPushType";
            this.cmbPushType.Size = new System.Drawing.Size(117, 21);
            this.cmbPushType.TabIndex = 3;
            this.cmbPushType.SelectedIndexChanged += new System.EventHandler(this.cmbPushType_SelectedIndexChanged);
            // 
            // lblID4
            // 
            this.lblID4.AutoSize = true;
            this.lblID4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID4.Location = new System.Drawing.Point(566, 125);
            this.lblID4.Name = "lblID4";
            this.lblID4.Size = new System.Drawing.Size(37, 13);
            this.lblID4.TabIndex = 8;
            this.lblID4.Text = "ID 4:";
            this.lblID4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbInterfaceProcess
            // 
            this.cmbInterfaceProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterfaceProcess.FormattingEnabled = true;
            this.cmbInterfaceProcess.Location = new System.Drawing.Point(609, 17);
            this.cmbInterfaceProcess.Name = "cmbInterfaceProcess";
            this.cmbInterfaceProcess.Size = new System.Drawing.Size(205, 21);
            this.cmbInterfaceProcess.TabIndex = 5;
            this.cmbInterfaceProcess.SelectedIndexChanged += new System.EventHandler(this.cmbInterface_SelectedIndexChanged);
            // 
            // lblID5
            // 
            this.lblID5.AutoSize = true;
            this.lblID5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID5.Location = new System.Drawing.Point(566, 151);
            this.lblID5.Name = "lblID5";
            this.lblID5.Size = new System.Drawing.Size(37, 13);
            this.lblID5.TabIndex = 9;
            this.lblID5.Text = "ID 5:";
            this.lblID5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPUC
            // 
            this.cmbPUC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPUC.FormattingEnabled = true;
            this.cmbPUC.Location = new System.Drawing.Point(287, 70);
            this.cmbPUC.Name = "cmbPUC";
            this.cmbPUC.Size = new System.Drawing.Size(184, 21);
            this.cmbPUC.TabIndex = 2;
            // 
            // lblInterface
            // 
            this.lblInterface.AutoSize = true;
            this.lblInterface.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInterface.Location = new System.Drawing.Point(484, 20);
            this.lblInterface.Name = "lblInterface";
            this.lblInterface.Size = new System.Drawing.Size(119, 13);
            this.lblInterface.TabIndex = 11;
            this.lblInterface.Text = "Interface Process:*";
            this.lblInterface.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbLocCode
            // 
            this.cmbLocCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocCode.FormattingEnabled = true;
            this.cmbLocCode.Location = new System.Drawing.Point(287, 44);
            this.cmbLocCode.Name = "cmbLocCode";
            this.cmbLocCode.Size = new System.Drawing.Size(184, 21);
            this.cmbLocCode.TabIndex = 1;
            this.cmbLocCode.SelectedIndexChanged += new System.EventHandler(this.cmbLocCode_SelectedIndexChanged);
            // 
            // lblUnprocessedRecords
            // 
            this.lblUnprocessedRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUnprocessedRecords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(117)))), ((int)(((byte)(186)))));
            this.lblUnprocessedRecords.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnprocessedRecords.ForeColor = System.Drawing.Color.White;
            this.lblUnprocessedRecords.Location = new System.Drawing.Point(10, 231);
            this.lblUnprocessedRecords.Name = "lblUnprocessedRecords";
            this.lblUnprocessedRecords.Size = new System.Drawing.Size(993, 27);
            this.lblUnprocessedRecords.TabIndex = 23;
            this.lblUnprocessedRecords.Text = "Unprocessed Records";
            this.lblUnprocessedRecords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvUnprocessedRecords
            // 
            this.dgvUnprocessedRecords.AllowUserToAddRows = false;
            this.dgvUnprocessedRecords.AllowUserToDeleteRows = false;
            this.dgvUnprocessedRecords.AllowUserToResizeColumns = false;
            this.dgvUnprocessedRecords.AllowUserToResizeRows = false;
            this.dgvUnprocessedRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUnprocessedRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnprocessedRecords.Location = new System.Drawing.Point(10, 261);
            this.dgvUnprocessedRecords.Name = "dgvUnprocessedRecords";
            this.dgvUnprocessedRecords.RowHeadersVisible = false;
            this.dgvUnprocessedRecords.Size = new System.Drawing.Size(993, 357);
            this.dgvUnprocessedRecords.TabIndex = 1;
            this.dgvUnprocessedRecords.TabStop = false;
            this.dgvUnprocessedRecords.SelectionChanged += new System.EventHandler(this.dgvUnprocessedRecords_SelectionChanged);
            // 
            // errprovValidate
            // 
            this.errprovValidate.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errprovValidate.ContainerControl = this;
            // 
            // frmSelectiveInterfacePush
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1013, 707);
            this.ControlBox = false;
            this.Controls.Add(this.pnlBodyMain);
            this.DoubleBuffered = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectiveInterfacePush";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.frmSelectiveInterfacePush_Load);
            this.Controls.SetChildIndex(this.pnlBase, 0);
            this.Controls.SetChildIndex(this.pnlBodyMain, 0);
            this.pnlBase.ResumeLayout(false);
            this.pnlBase.PerformLayout();
            this.pnlBodyMain.ResumeLayout(false);
            this.grpRecordToAdd.ResumeLayout(false);
            this.grpRecordToAdd.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnprocessedRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errprovValidate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBodyMain;
        private System.Windows.Forms.DataGridView dgvUnprocessedRecords;
        private System.Windows.Forms.Label lblID1;
        private System.Windows.Forms.Label lblPushType;
        private System.Windows.Forms.Label lblPUC;
        private System.Windows.Forms.Label lblLocCode;
        private System.Windows.Forms.Label lblLocType;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblID5;
        private System.Windows.Forms.Label lblID4;
        private System.Windows.Forms.Label lblID3;
        private System.Windows.Forms.Label lblID2;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.TextBox txtID1;
        private System.Windows.Forms.ComboBox cmbPushType;
        private System.Windows.Forms.ComboBox cmbInterfaceProcess;
        private System.Windows.Forms.ComboBox cmbPUC;
        private System.Windows.Forms.ComboBox cmbLocCode;
        private System.Windows.Forms.ComboBox cmbLocType;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.TextBox txtID5;
        private System.Windows.Forms.TextBox txtID4;
        private System.Windows.Forms.TextBox txtID3;
        private System.Windows.Forms.TextBox txtID2;
        private System.Windows.Forms.Label lblUnprocessedRecords;
        private System.Windows.Forms.GroupBox grpRecordToAdd;
        private System.Windows.Forms.ErrorProvider errprovValidate;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel2;
    }
}