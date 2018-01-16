namespace ReichUI.Controls
{
    partial class ReichCheckBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCheckBox = new System.Windows.Forms.Label();
            this.pbCheckBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCheckBox
            // 
            this.lblCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckBox.Location = new System.Drawing.Point(38, 0);
            this.lblCheckBox.Name = "lblCheckBox";
            this.lblCheckBox.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblCheckBox.Size = new System.Drawing.Size(145, 34);
            this.lblCheckBox.TabIndex = 1;
            this.lblCheckBox.Text = "CheckBox Text";
            // 
            // pbCheckBox
            // 
            this.pbCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.pbCheckBox.Image = global::ReichUI.Properties.Resources.CB_DefaultState;
            this.pbCheckBox.Location = new System.Drawing.Point(3, 3);
            this.pbCheckBox.Name = "pbCheckBox";
            this.pbCheckBox.Size = new System.Drawing.Size(29, 27);
            this.pbCheckBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCheckBox.TabIndex = 0;
            this.pbCheckBox.TabStop = false;
            this.pbCheckBox.Click += new System.EventHandler(this.pbCheckBox_Click);
            this.pbCheckBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbCheckBox_MouseClick);
            this.pbCheckBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCheckBox_MouseDown);
            this.pbCheckBox.MouseEnter += new System.EventHandler(this.pbCheckBox_MouseEnter);
            this.pbCheckBox.MouseLeave += new System.EventHandler(this.pbCheckBox_MouseLeave);
            this.pbCheckBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCheckBox_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pbCheckBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCheckBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(186, 34);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // GiladCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(186, 34);
            this.Name = "GiladCheckBox";
            this.Size = new System.Drawing.Size(186, 34);
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label lblCheckBox;
        public System.Windows.Forms.PictureBox pbCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
