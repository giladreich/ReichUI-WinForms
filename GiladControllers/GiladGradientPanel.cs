using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiladControllers
{
    public partial class GiladGradientPanel : UserControl
    {
        private Color _color1 = Color.White;
        private Color _color2 = Color.Black;
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;


        public GiladGradientPanel()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }

        #region --- Custom Properties Controls -------------------------------------------------------------------------------------------

        [Description("Color1 for gradient design will be mixed with Color2."), Category("~Custom Data")]

        public Color Color1
        {
            get { return _color1; }
            set
            {
                if (_color1 == value)
                    return;
                _color1 = value;
                this.Invalidate();
            }
        }

        [Description("Color2 for gradient design will be mixed with Color1."), Category("~Custom Data")]

        public Color Color2
        {
            get { return _color2; }
            set
            {
                if (_color2 == value)
                    return;
                _color2 = value;
                this.Invalidate();
            }
        }


        [Description("Gradient mode postition the way the gradient is set on the control."), Category("~Custom Data")]
        public LinearGradientMode GradientMode
        {
            get { return _gradientMode; }
            set
            {
                if (_gradientMode == value)
                    return;
                _gradientMode = value;
                this.Invalidate();
            }
        }

        #endregion --- Custom Properties Controls -------------------------------------------------------------------------------------------






        #region --- Overriding Control Events -------------------------------------------------------------------------------------------

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(this.ClientRectangle,
                       Color1, Color2, GradientMode))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Invalidate();
            base.OnScroll(se);
        }
        #endregion --- Overriding Control Events -----------------------------------------------------------------------------------------

    }
}
