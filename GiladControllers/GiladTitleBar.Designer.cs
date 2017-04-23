namespace GiladControllers
{
    partial class GiladTitleBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GiladTitleBar));
            this.pbMin = new System.Windows.Forms.PictureBox();
            this.pbMax = new System.Windows.Forms.PictureBox();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pbExit = new System.Windows.Forms.PictureBox();
            this.lblAppTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMax)).BeginInit();
            this.tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMin
            // 
            this.pbMin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbMin.BackColor = System.Drawing.Color.Transparent;
            this.pbMin.Image = Properties.Resources.minimize;
            this.pbMin.Location = new System.Drawing.Point(151, 0);
            this.pbMin.Margin = new System.Windows.Forms.Padding(0);
            this.pbMin.Name = "pbMin";
            this.pbMin.Size = new System.Drawing.Size(39, 34);
            this.pbMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMin.TabIndex = 0;
            this.pbMin.TabStop = false;
            this.pbMin.Click += new System.EventHandler(this.pbMin_Click);
            this.pbMin.MouseEnter += new System.EventHandler(this.pbMin_MouseEnter);
            this.pbMin.MouseLeave += new System.EventHandler(this.pbMin_MouseLeave);
            // 
            // pbMax
            // 
            this.pbMax.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbMax.BackColor = System.Drawing.Color.Transparent;
            this.pbMax.Image = Properties.Resources.maximize;
            this.pbMax.Location = new System.Drawing.Point(190, 0);
            this.pbMax.Margin = new System.Windows.Forms.Padding(0);
            this.pbMax.Name = "pbMax";
            this.pbMax.Size = new System.Drawing.Size(39, 34);
            this.pbMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMax.TabIndex = 0;
            this.pbMax.TabStop = false;
            this.pbMax.Click += new System.EventHandler(this.pbMax_Click);
            this.pbMax.MouseEnter += new System.EventHandler(this.pbMax_MouseEnter);
            this.pbMax.MouseLeave += new System.EventHandler(this.pbMax_MouseLeave);
            // 
            // tableLayout
            // 
            this.tableLayout.BackColor = System.Drawing.Color.Transparent;
            this.tableLayout.ColumnCount = 4;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayout.Controls.Add(this.pbExit, 3, 0);
            this.tableLayout.Controls.Add(this.pbMax, 2, 0);
            this.tableLayout.Controls.Add(this.pbMin, 1, 0);
            this.tableLayout.Controls.Add(this.lblAppTitle, 0, 0);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 1;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.Size = new System.Drawing.Size(268, 35);
            this.tableLayout.TabIndex = 2;
            this.tableLayout.DoubleClick += new System.EventHandler(this.background_DoubleClick);
            this.tableLayout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.background_MouseDown);
            this.tableLayout.MouseMove += new System.Windows.Forms.MouseEventHandler(this.background_MouseMove);
            // 
            // pbExit
            // 
            this.pbExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbExit.BackColor = System.Drawing.Color.Transparent;
            this.pbExit.Image = Properties.Resources.exit;
            this.pbExit.Location = new System.Drawing.Point(229, 0);
            this.pbExit.Margin = new System.Windows.Forms.Padding(0);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(39, 34);
            this.pbExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbExit.TabIndex = 0;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseEnter += new System.EventHandler(this.pbExit_MouseEnter);
            this.pbExit.MouseLeave += new System.EventHandler(this.pbExit_MouseLeave);
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblAppTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.Location = new System.Drawing.Point(3, 9);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(104, 16);
            this.lblAppTitle.TabIndex = 1;
            this.lblAppTitle.Text = "Application Title";
            this.lblAppTitle.DoubleClick += new System.EventHandler(this.background_DoubleClick);
            this.lblAppTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.background_MouseDown);
            this.lblAppTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.background_MouseMove);
            // 
            // GiladBorderLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayout);
            this.MinimumSize = new System.Drawing.Size(200, 35);
            this.Name = "GiladBorderLayout";
            this.Size = new System.Drawing.Size(268, 35);
            this.DoubleClick += new System.EventHandler(this.background_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.background_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.background_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pbMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMax)).EndInit();
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbMin;
        private System.Windows.Forms.PictureBox pbMax;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.PictureBox pbExit;
        private System.Windows.Forms.Label lblAppTitle;
    }
}
