namespace GiladControllers
{
    partial class Gilad4CheckBox
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
            this.cbOption1 = new GiladControllers.GiladCheckBox();
            this.cbOption2 = new GiladControllers.GiladCheckBox();
            this.cbOption3 = new GiladControllers.GiladCheckBox();
            this.cbOption4 = new GiladControllers.GiladCheckBox();
            this.SuspendLayout();
            // 
            // cbOption1
            // 
            this.cbOption1.CheckBoxEnabled = true;
            this.cbOption1.Checked = false;
            this.cbOption1.HandCursorHover = false;
            this.cbOption1.Location = new System.Drawing.Point(3, 3);
            this.cbOption1.Name = "cbOption1";
            this.cbOption1.Size = new System.Drawing.Size(434, 52);
            this.cbOption1.TabIndex = 0;
            this.cbOption1.WrongAnswer = false;
            // 
            // cbOption2
            // 
            this.cbOption2.CheckBoxEnabled = true;
            this.cbOption2.Checked = false;
            this.cbOption2.HandCursorHover = false;
            this.cbOption2.Location = new System.Drawing.Point(3, 61);
            this.cbOption2.Name = "cbOption2";
            this.cbOption2.Size = new System.Drawing.Size(434, 52);
            this.cbOption2.TabIndex = 0;
            this.cbOption2.WrongAnswer = false;
            // 
            // cbOption3
            // 
            this.cbOption3.CheckBoxEnabled = true;
            this.cbOption3.Checked = false;
            this.cbOption3.HandCursorHover = false;
            this.cbOption3.Location = new System.Drawing.Point(3, 119);
            this.cbOption3.Name = "cbOption3";
            this.cbOption3.Size = new System.Drawing.Size(434, 52);
            this.cbOption3.TabIndex = 0;
            this.cbOption3.WrongAnswer = false;
            // 
            // cbOption4
            // 
            this.cbOption4.CheckBoxEnabled = true;
            this.cbOption4.Checked = false;
            this.cbOption4.HandCursorHover = false;
            this.cbOption4.Location = new System.Drawing.Point(0, 177);
            this.cbOption4.Name = "cbOption4";
            this.cbOption4.Size = new System.Drawing.Size(434, 52);
            this.cbOption4.TabIndex = 0;
            this.cbOption4.WrongAnswer = false;
            // 
            // Gilad4CheckBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbOption4);
            this.Controls.Add(this.cbOption3);
            this.Controls.Add(this.cbOption2);
            this.Controls.Add(this.cbOption1);
            this.Name = "Gilad4CheckBox";
            this.Size = new System.Drawing.Size(440, 234);
            this.ResumeLayout(false);

        }

        #endregion
        public GiladCheckBox cbOption1;
        public GiladCheckBox cbOption2;
        public GiladCheckBox cbOption3;
        public GiladCheckBox cbOption4;
    }
}
