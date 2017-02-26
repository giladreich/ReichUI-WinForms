namespace GiladControllers
{
    partial class TestForm
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
            this.giladButton2 = new GiladControllers.GiladButton();
            this.giladButton1 = new GiladControllers.GiladButton();
            this.SuspendLayout();
            // 
            // giladButton2
            // 
            this.giladButton2.AnimateHover = true;
            this.giladButton2.ButtonDirection = GiladControllers.CustomButtonDirection.Back;
            this.giladButton2.ButtonEnabled = false;
            this.giladButton2.HandCursorHover = true;
            this.giladButton2.Location = new System.Drawing.Point(12, 235);
            this.giladButton2.Name = "giladButton2";
            this.giladButton2.Size = new System.Drawing.Size(99, 102);
            this.giladButton2.TabIndex = 0;
            // 
            // giladButton1
            // 
            this.giladButton1.AnimateHover = true;
            this.giladButton1.ButtonDirection = GiladControllers.CustomButtonDirection.Forward;
            this.giladButton1.ButtonEnabled = true;
            this.giladButton1.HandCursorHover = true;
            this.giladButton1.Location = new System.Drawing.Point(369, 235);
            this.giladButton1.Name = "giladButton1";
            this.giladButton1.Size = new System.Drawing.Size(99, 102);
            this.giladButton1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(480, 349);
            this.Controls.Add(this.giladButton1);
            this.Controls.Add(this.giladButton2);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private GiladButton giladButton2;
        private GiladButton giladButton1;
    }
}