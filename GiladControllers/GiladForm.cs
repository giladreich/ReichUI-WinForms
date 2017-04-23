using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GiladControllers.Helpers;
using GiladControllers.Helpers.Properties.Events;
using GiladControllers.Helpers.Properties.GiladForm;
using GiladControllers.Properties;

namespace GiladControllers
{
    public class GiladForm : Form
    {
        private readonly Region _oldRegion;



        public GiladForm()
        {
            _oldRegion = Region; // Keeping safe the old region because of the rounded edges property.
            FormBorderStyle = FormBorderStyle.None;
            ResizeRedraw = true;
            DoubleBuffered = true;
            Padding = new Padding(9, 50, 8, 9); // set some padding to make room when panel drops inside the form.

            // Adding events listers to our custom properties.
            FormBorders.ValueChanged += GiladForm_ValueChanged;
            FormBackColor.ValueChanged += GiladForm_ValueChanged;
            AppTitle.ValueChanged += GiladForm_ValueChanged;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);
        }



        #region ---- Style ------------------------------------------------------------------------------------

        private void GiladForm_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            // repainting the form when a value changed within the vs designer.
            if (DesignMode)
                Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Paints the background color. It has to run first, because it fills the form region.
            if (!ClientRectangle.IsEmpty)
                using (var brush = new LinearGradientBrush(ClientRectangle, FormBackColor.GradientColor1,
                    FormBackColor.GradientColor2, FormBackColor.GradientMode))
                {
                    if (RoundEdges)
                    {
                        // TODO: Try to make this not to be done in OnPaint because it costs a lot of performance.
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        using (var path = Utilities.RoundedRect(new Rectangle(0, 0, Width, Height), 16))
                        {
                            // Maybe property to register an event to do OnResize Invalidate()?
                            Region = new Region(path);
                            e.Graphics.FillRegion(brush, Region);
                        }
                    }
                    else
                    {
                        Region = _oldRegion;
                        e.Graphics.FillRectangle(brush, ClientRectangle);
                    }
                }


            if (FormBorders.DrawBorders)
                ControlPaint.DrawBorder(
                    e.Graphics, ClientRectangle,
                    FormBorders.Color, FormBorders.Width, ButtonBorderStyle.Inset,
                    FormBorders.Color, FormBorders.Width, ButtonBorderStyle.Inset,
                    FormBorders.Color, FormBorders.Width, ButtonBorderStyle.Outset,
                    FormBorders.Color, FormBorders.Width, ButtonBorderStyle.Outset
                ); // right, up, left, down


            // Painting grip on the bottom right corner.
            if (ResizeGrip && (WindowState != FormWindowState.Maximized))
                if (RoundEdges)
                    ControlPaint.DrawSizeGrip(e.Graphics, ResizeGripColor,
                        ClientSize.Width - 16, ClientSize.Height - 16, 17, 17);
                else
                    ControlPaint.DrawSizeGrip(e.Graphics, ResizeGripColor,
                        ClientSize.Width - 16, ClientSize.Height - 16, 17, 17);


            if (AppTitle.ShowIcon)
                e.Graphics.DrawIcon(AppTitle.Icon, new Rectangle(AppTitle.IconLocation,
                    AppTitle.IconSize));

            if (AppTitle.ShowTextTitle && AppTitle.ShowIcon)
            {
                e.Graphics.DrawString(AppTitle.Text, AppTitle.TextFont, new SolidBrush(Color.Black),
                    new Point(AppTitle.TextLocation.X, AppTitle.TextLocation.Y - 2)); // shadow.
                e.Graphics.DrawString(AppTitle.Text, AppTitle.TextFont, AppTitle.Brush, AppTitle.TextLocation);
            }
            else
            {
                e.Graphics.DrawString(AppTitle.Text, AppTitle.TextFont, new SolidBrush(Color.Black),
                    new Point(AppTitle.TextLocation.X, AppTitle.TextLocation.Y - 2)); // shadow.
                e.Graphics.DrawString(AppTitle.Text, AppTitle.TextFont, AppTitle.Brush, AppTitle.TextLocation);
            }
        }

        #endregion ---- Style ----------------------------------------------------------------------------------------

        #region ---- Event Handerls ----------------------------------------------------------------------------------

        private Point dragOffset;
        private bool currentlyDragging;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!DraggableForm || (e.Button != MouseButtons.Left)) return;

            if (CustomCursor)
                if (!currentlyDragging && (WindowState != FormWindowState.Maximized) 
                    && DraggableForm)
                {
                    currentlyDragging = true;
                    Cursor = myCursorDrag;
                }

            dragOffset = PointToScreen(e.Location);
            var formLocation = Location;
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
                    Cursor = myCursorNormal;
                }
                else
                    currentlyDragging = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (CustomCursor && !currentlyDragging)
                Cursor = myCursorNormal;

            if (!DraggableForm || (e.Button != MouseButtons.Left)) return;
            var newLocation = PointToScreen(e.Location);
            newLocation.X -= dragOffset.X;
            newLocation.Y -= dragOffset.Y;
            Location = newLocation;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (((MouseEventArgs) e).Button != MouseButtons.Left) return;

            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }


        private const uint  WM_NCHITTEST = 0x0084, WM_MOUSEMOVE = 0x0200, HTLEFT = 10,
                            HTRIGHT = 11, HTBOTTOMRIGHT = 17, HTBOTTOM = 15, HTBOTTOMLEFT = 16,
                            HTTOP = 12,  HTTOPLEFT = 13, HTTOPRIGHT = 14;

        private Size formSize;
        private Point screenPoint;
        private Point clientPoint;
        private Dictionary<uint, Rectangle> boxes;
        private const int RHS = 10; // RESIZE_HANDLE_SIZE
        private bool handled;

        protected override void WndProc(ref Message m)
        {
            if ((WindowState == FormWindowState.Maximized) || !AllowResize)
            {
                base.WndProc(ref m);
                return;
            }

            handled = false;
            if ((m.Msg == WM_NCHITTEST) || (m.Msg == WM_MOUSEMOVE))
            {
                formSize = Size;
                screenPoint = new Point(m.LParam.ToInt32());
                clientPoint = PointToClient(screenPoint);

                boxes = new Dictionary<uint, Rectangle>
                {
                    {HTBOTTOMLEFT, new Rectangle(0, formSize.Height - RHS, RHS, RHS)},
                    {HTBOTTOM, new Rectangle(RHS, formSize.Height - RHS, formSize.Width - 2*RHS, RHS)},
                    {HTBOTTOMRIGHT, new Rectangle(formSize.Width - RHS, formSize.Height - RHS, RHS, RHS)},
                    {HTRIGHT, new Rectangle(formSize.Width - RHS, RHS, RHS, formSize.Height - 2*RHS)},
                    {HTTOPRIGHT, new Rectangle(formSize.Width - RHS, 0, RHS, RHS)},
                    {HTTOP, new Rectangle(RHS, 0, formSize.Width - 2*RHS, RHS)},
                    {HTTOPLEFT, new Rectangle(0, 0, RHS, RHS)},
                    {HTLEFT, new Rectangle(0, RHS, RHS, formSize.Height - 2*RHS)}
                };

                foreach (var hitBox in boxes)
                    if (hitBox.Value.Contains(clientPoint))
                    {
                        m.Result = (IntPtr) hitBox.Key;
                        handled = true;
                        break;
                    }
            }

            if (!handled)
                base.WndProc(ref m);
        }

        #endregion ---- Event Handerls --------------------------------------------------------------------------------

        #region ---- Properties ------------------------------------------------------------------------------------

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        [Category("~Custom Data")]
        [Description("Allows the form to be resized from the sides and corners.")]
        public bool AllowResize { get; set; } = true;

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        [Category("~Custom Data")]
        [Description("Allows the form to be dragged from anywhere in the parent container.")]
        public bool DraggableForm { get; set; } = true;

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

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        private AppTitle _appTitle = new AppTitle();

        [Category("~Custom Data")]
        [Description("Allows to change the icon or text of the application.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public AppTitle AppTitle
        {
            get { return _appTitle; }
            set
            {
                if (_appTitle == value) return;
                _appTitle = value;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        private FormBackColor _formBackColor = new FormBackColor(
            LinearGradientMode.Vertical, Color.Black, Color.Brown);

        [Category("~Custom Data")]
        [Description("Allows you to control the gradient background of the form.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public FormBackColor FormBackColor
        {
            get { return _formBackColor; }
            set
            {
                if (_formBackColor == value) return;
                _formBackColor = value;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        private bool _roundEdges;

        [Category("~Custom Data")]
        [Description("Round the edges of the form. Note that this takes more performance.")]
        public bool RoundEdges
        {
            get { return _roundEdges; }
            set
            {
                _roundEdges = value;
                if (DesignMode) Invalidate();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        private bool _resizeGrip = true;

        [Category("~Custom Data")]
        [Description("Paints the resize grip on the bottom right corner.")]
        public bool ResizeGrip
        {
            get { return _resizeGrip; }
            set
            {
                _resizeGrip = value;
                if (DesignMode) Invalidate();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        private Color _resizeGripColor = DefaultBackColor;

        [Category("~Custom Data")]
        [Description("Resize grip color.")]
        public Color ResizeGripColor
        {
            get { return _resizeGripColor; }
            set
            {
                if (_resizeGripColor == value) return;
                _resizeGripColor = value;
                if (DesignMode) Invalidate();
            }
        }


        private FormBorders _formBorders = new FormBorders();

        [Category("~Custom Data")]
        [Description("Draw borders around the form.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public FormBorders FormBorders
        {
            get { return _formBorders; }
            set
            {
                if (!_formBorders.DrawBorders || (_formBorders == value)) return;
                _formBorders = value;
            }
        }

        #endregion ---- Properties ------------------------------------------------------------------------------------


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
                MessageBox.Show("Failed to retrieve custom cursor from embedded resource!");
            }
        }







    }
}