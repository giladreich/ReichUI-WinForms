using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace ReichUI.Controls
{


    public enum ButtonState
    {
        Exit          = 0,
        ExitHover     = 1,
        Maximize      = 2,
        MaximizeHover = 3,
        Resize        = 4,
        ResizeHover   = 5,
        Minimize      = 6,
        MinimizeHover = 7
    }


    public partial class ReichTitleBar : UserControl
    {
        Point dragOffset;
        private Dictionary<ButtonState, Image> _images;
        private Color _color1 = Color.White;
        private Color _color2 = Color.Black;
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;



        public ReichTitleBar()
        {
            InitializeComponent();
            InitializeImages();
            this.ResizeRedraw = true;
            this.Dock = DockStyle.Top;
        }


        // TODO: Based on my solution in stackoverflow, add this feature later to resize with the corners and grip:
        // TODO: http://stackoverflow.com/questions/17748446/custom-resize-handle-in-border-less-form-c-sharp/43412244#43412244


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


        [Description("Application title text."), Category("~Custom Data")]
        public string TitleText
        {
            get { return lblAppTitle.Text; }
            set
            {
                if (lblAppTitle.Text == value)
                    return;
                lblAppTitle.Text = value;
                if(DesignMode) this.Invalidate();
            }
        }

        #endregion --- Custom Properties Controls -------------------------------------------------------------------------------------------





        #region --- Events Handlers ---------------------------------------------------------------------------------------------------------

        private void background_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || ParentForm == null) return;

            dragOffset = this.PointToScreen(e.Location);
            var formLocation = ParentForm.Location;
            dragOffset.X -= formLocation.X;
            dragOffset.Y -= formLocation.Y;
        }


        private void background_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || ParentForm == null) return;

            var newLocation = this.PointToScreen(e.Location);
            newLocation.X -= dragOffset.X;
            newLocation.Y -= dragOffset.Y;
            ParentForm.Location = newLocation;
        }


        //private Point dragOffset;
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left && ParentForm != null)
        //    {
        //        dragOffset = this.PointToScreen(e.Location);
        //        var formLocation = FindForm().Location;
        //        dragOffset.X -= formLocation.X;
        //        dragOffset.Y -= formLocation.Y;
        //    }

        //    base.OnMouseDown(e);
        //}


        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left && ParentForm != null)
        //    {
        //        Point newLocation = this.PointToScreen(e.Location);

        //        newLocation.X -= dragOffset.X;
        //        newLocation.Y -= dragOffset.Y;
        //        ParentForm.Location = newLocation;
        //    }

        //    base.OnMouseMove(e);
        //}


        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            pbExit.Image = _images[ButtonState.ExitHover];
        }

        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            pbExit.Image = _images[ButtonState.Exit];
        }

        private void pbMax_MouseEnter(object sender, EventArgs e)
        {
            if (FindForm().WindowState == FormWindowState.Maximized)
                pbMax.Image = _images[ButtonState.ResizeHover];
            else
                pbMax.Image = _images[ButtonState.MaximizeHover];
        }

        private void pbMax_MouseLeave(object sender, EventArgs e)
        {
            if (FindForm().WindowState == FormWindowState.Maximized)
                pbMax.Image = _images[ButtonState.Resize];
            else
                pbMax.Image = _images[ButtonState.Maximize];
        }

        private void pbMin_MouseEnter(object sender, EventArgs e)
        {
            pbMin.Image = _images[ButtonState.MinimizeHover];
        }

        private void pbMin_MouseLeave(object sender, EventArgs e)
        {
            pbMin.Image = _images[ButtonState.Minimize];
        }


        private void pbExit_Click(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse.Button != MouseButtons.Left)
                return;

            FindForm().Close();
        }

        private void pbMax_Click(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse.Button != MouseButtons.Left)
                return;

            if (FindForm().WindowState == FormWindowState.Maximized)
            {
                FindForm().WindowState = FormWindowState.Normal;
                pbMax.Image = _images[ButtonState.Resize];
            }
            else
            {
                FindForm().WindowState = FormWindowState.Maximized;
                pbMax.Image = _images[ButtonState.Maximize];
            }
        }

        private void pbMin_Click(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse.Button != MouseButtons.Left)
                return;

            FindForm().WindowState = FormWindowState.Minimized;
        }

        private void background_DoubleClick(object sender, EventArgs e)
        {
            var mouse = e as MouseEventArgs;
            if (mouse.Button != MouseButtons.Left)
                return;

            if (FindForm().WindowState == FormWindowState.Maximized)
            {
                FindForm().WindowState = FormWindowState.Normal;
                pbMax.Image = _images[ButtonState.Maximize];
            }
            else
            {
                FindForm().WindowState = FormWindowState.Maximized;
                pbMax.Image = _images[ButtonState.Resize];
            }
        }
        #endregion --- Events Handlers ------------------------------------------------------------------------------------------------------





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
            if(DesignMode) this.Invalidate();
            base.OnScroll(se);
        }

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

        #endregion --- Overriding Control Events -----------------------------------------------------------------------------------------




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
