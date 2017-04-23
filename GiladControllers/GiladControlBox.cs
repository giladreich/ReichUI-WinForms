using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace GiladControllers
{
    public sealed partial class GiladControlBox : UserControl
    {
        private Dictionary<ButtonState, Image> _images;
        private Color _color1 = Color.White;
        private Color _color2 = Color.Black;
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;



        public GiladControlBox()
        {
            InitializeComponent();
            InitializeImages();
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
                if(DesignMode) this.Invalidate();
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
                if(DesignMode) this.Invalidate();
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
                if(DesignMode) this.Invalidate();
            }
        }


        #endregion --- Custom Properties Controls -------------------------------------------------------------------------------------------





        #region --- Events Handlers ---------------------------------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (Control control in Controls) // reflection to sort flickering.
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, control, new object[] { true });
            }
        }


        private void pbExit_Click(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse?.Button != MouseButtons.Left)
                return;

            ParentForm?.Close();
        }

        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            pbExit.Image = _images[ButtonState.ExitHover];
        }

        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            pbExit.Image = _images[ButtonState.Exit];
        }

        private void pbMax_Click(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse?.Button != MouseButtons.Left)
                return;

            if (ParentForm?.WindowState == FormWindowState.Maximized)
            {
                ParentForm.WindowState = FormWindowState.Normal;
                pbMax.Image = _images[ButtonState.Resize];
            }
            else
            {
                ParentForm.WindowState = FormWindowState.Maximized;
                pbMax.Image = _images[ButtonState.Maximize];
            }
        }

        private void pbMax_MouseEnter(object sender, EventArgs e)
        {
            if (ParentForm?.WindowState == FormWindowState.Maximized)
                pbMax.Image = _images[ButtonState.ResizeHover];
            else
                pbMax.Image = _images[ButtonState.MaximizeHover];
        }

        private void pbMax_MouseLeave(object sender, EventArgs e)
        {
            if (ParentForm?.WindowState == FormWindowState.Maximized)
                pbMax.Image = _images[ButtonState.Resize];
            else
                pbMax.Image = _images[ButtonState.Maximize];
        }

        private void pbMin_Click(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse.Button != MouseButtons.Left)
                return;

            ParentForm.WindowState = FormWindowState.Minimized;
        }

        private void pbMin_MouseEnter(object sender, EventArgs e)
        {
            pbMin.Image = _images[ButtonState.MinimizeHover];
        }

        private void pbMin_MouseLeave(object sender, EventArgs e)
        {
            pbMin.Image = _images[ButtonState.Minimize];
        }

        #endregion --- Events Handlers ------------------------------------------------------------------------------------------------------



        private void InitializeImages()
        {
            _images = new Dictionary<ButtonState, Image>()
            {
                {ButtonState.Exit         , Properties.Resources.exit          },
                {ButtonState.ExitHover    , Properties.Resources.exit_hover    },
                {ButtonState.Maximize     , Properties.Resources.maximize      },
                {ButtonState.MaximizeHover, Properties.Resources.maximize_hover},
                {ButtonState.Resize       , Properties.Resources.resize        },
                {ButtonState.ResizeHover  , Properties.Resources.resize_hover  },
                {ButtonState.Minimize     , Properties.Resources.minimize      },
                {ButtonState.MinimizeHover, Properties.Resources.minimize_hover}
            };
        }
    }
}
