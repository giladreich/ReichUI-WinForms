namespace ReichUI.Controls
{
    public class GiladButtonDesigner : System.Windows.Forms.Design.ControlDesigner
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
            components = new System.ComponentModel.Container();
        }


        protected override void PostFilterProperties(System.Collections.IDictionary Properties)
        {
            Properties.Remove("CausesValidation");
            Properties.Remove("Locked");
            Properties.Remove("GenerateMember");
            Properties.Remove("RightToLeft");
            Properties.Remove("ForeColor");
            Properties.Remove("BackgroundImage");
            Properties.Remove("BackColor");
            Properties.Remove("BackgroundImageLayout");
            Properties.Remove("UseWaitCursor");
            Properties.Remove("AccessibleRole");
            Properties.Remove("AccessibleName");
            Properties.Remove("AccessibleDescription");
            Properties.Remove("AllowDrop");
            Properties.Remove("Text");
            Properties.Remove("Font");
            Properties.Remove("ContextMenu");
            Properties.Remove("FlatStyle");
            Properties.Remove("Image");
            Properties.Remove("ImageAlign");
            Properties.Remove("ImageIndex");
            Properties.Remove("ImageList");
            Properties.Remove("TextAlign");
            Properties.Remove("Enabled");
            Properties.Remove("Dock");

            base.PostFilterProperties(Properties);
        }
        #endregion
    }
}
