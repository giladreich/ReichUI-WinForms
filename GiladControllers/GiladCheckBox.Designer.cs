namespace GiladControllers
{
    partial class GiladCheckBox
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
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCheckBox
            // 
            this.lblCheckBox.AutoSize = true;
            this.lblCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.lblCheckBox.Location = new System.Drawing.Point(35, 11);
            this.lblCheckBox.Name = "lblCheckBox";
            this.lblCheckBox.Size = new System.Drawing.Size(80, 13);
            this.lblCheckBox.TabIndex = 1;
            this.lblCheckBox.Text = "CheckBox Text";
            // 
            // pbCheckBox
            // 
            this.pbCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.pbCheckBox.Image = global::GiladControllers.Properties.Resources.CB_DefaultState;
            this.pbCheckBox.Location = new System.Drawing.Point(3, 4);
            this.pbCheckBox.Name = "pbCheckBox";
            this.pbCheckBox.Size = new System.Drawing.Size(27, 27);
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
            // GiladCheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCheckBox);
            this.Controls.Add(this.pbCheckBox);
            this.Name = "GiladCheckBox";
            this.Size = new System.Drawing.Size(173, 33);
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lblCheckBox;
        public System.Windows.Forms.PictureBox pbCheckBox;
    }
}
