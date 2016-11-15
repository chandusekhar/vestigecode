namespace CoreComponent.Hierarchies.UI
{
    partial class frmTree
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
            this.tvHierarchy = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tvHierarchy
            // 
            this.tvHierarchy.Location = new System.Drawing.Point(8, 12);
            this.tvHierarchy.Name = "tvHierarchy";
            this.tvHierarchy.Size = new System.Drawing.Size(152, 223);
            this.tvHierarchy.TabIndex = 99;
            this.tvHierarchy.TabStop = false;
            this.tvHierarchy.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvHierarchy_AfterSelect);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(40, -1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Visible = false;
            // 
            // frmTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(169, 247);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tvHierarchy);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTree";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hierarchy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvHierarchy;
        private System.Windows.Forms.TextBox textBox1;
    }
}