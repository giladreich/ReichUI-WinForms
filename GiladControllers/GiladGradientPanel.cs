using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using GiladControllers.Helpers;
using GiladControllers.Properties;
using MessageBox = System.Windows.Forms.MessageBox;

namespace GiladControllers
{
    public partial class GiladGradientPanel : Panel
    {
        private Color _color1 = Color.White;
        private Color _color2 = Color.Black;
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;


        public GiladGradientPanel()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);

            InitCustomCursor();
        }









        private Point dragOffset;
        private bool currentlyDragging;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!DraggableForm || (e.Button != MouseButtons.Left)) return;

            if (CustomCursor)
                if (!currentlyDragging && (FindForm()?.WindowState != FormWindowState.Maximized)
                    && DraggableForm)
                {
                    currentlyDragging = true;
                    this.Cursor = myCursorDrag;
                }

            dragOffset = FindForm().PointToScreen(e.Location);
            var formLocation = FindForm().Location;
            dragOffset.X -= formLocation.X;
            dragOffset.Y -= formLocation.Y;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!DraggableForm || (e.Button != MouseButtons.Left)) return;
            if (CustomCursor)
                if (currentlyDragging)
                {
                    currentlyDragging = false;
                    this.Cursor = myCursorNormal;
                }
                else
                    currentlyDragging = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (CustomCursor && !currentlyDragging)
                this.Cursor = myCursorNormal;

            if (!DraggableForm || (e.Button != MouseButtons.Left)) return;
            var newLocation = FindForm().PointToScreen(e.Location);
            newLocation.X -= dragOffset.X;
            newLocation.Y -= dragOffset.Y;
            FindForm().Location = newLocation;
        }



        [Category("~Custom Data")]
        [Description("Allows you to drag the form within the panel region.")]
        public bool DraggableForm { get; set; } = false;



        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        private bool _customCursor;
        [Category("~Custom Data")]
        [Description("Allows you to enable custom cursor when the cursor is in the region of the form.")]
        public bool CustomCursor
        {
            get { return _customCursor; }
            set
            {
                _customCursor = value;
                if (_customCursor)
                {
                    InitCustomCursor();
                }
                else
                {
                    myCursorNormal?.Dispose();
                    myCursorDrag?.Dispose();
                }
            }
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



        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }


        #region --- Overriding Control Events -------------------------------------------------------------------------------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.ClientRectangle.IsEmpty)
            {
                using (var brush = new LinearGradientBrush(this.ClientRectangle,
                    Color1, Color2, GradientMode))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }

            base.OnPaint(e);
        }
        protected override void OnScroll(ScrollEventArgs se)
        {
            if(DesignMode) this.Invalidate();
            base.OnScroll(se);
        }

        #endregion --- Overriding Control Events -----------------------------------------------------------------------------------------




        private Cursor myCursorDrag;
        private Cursor myCursorNormal;
        private void InitCustomCursor()
        {
            try
            {
                myCursorDrag = LoadCursor.CreateCurFromEmbRc(Resources.cur_form_drag);
                myCursorNormal = LoadCursor.CreateCurFromEmbRc(Resources.cur_form_normal);
            }
            catch
            {
                throw new ApplicationException("Failed to retrieve custom cursor from embedded resource.");
            }
        }
    }
}
