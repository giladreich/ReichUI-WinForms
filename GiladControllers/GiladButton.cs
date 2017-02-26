using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiladControllers
{
    public enum CustomButtonDirection
    {
        Forward = 0,
        Back    = 1,
    }

    public enum ButtonImages
    {
        Forward         = 0,
        ForwardHover    = 1,
        ForwardClicked  = 2,
        ForwardDisabled = 3,
        Back            = 4,
        BackHover       = 5,
        BackClicked     = 6,
        BackDisabled    = 7
    }


    public enum ButtonForwardAnim
    {
        Forward1 = 0,
        Forward2 = 1,
        Forward3 = 2,
        Forward4 = 3,
        Forward5 = 4,
        Forward6 = 5
    }

    public enum ButtonBackAnim
    {
        Back1 = 0,
        Back2 = 1,
        Back3 = 2,
        Back4 = 3,
        Back5 = 4,
        Back6 = 5
    }


    public enum CustomButtonCase
    {
        Default         = 0,
        Hover           = 1,
        Clicked         = 2,
        DisabledEnabled = 3,
        SpecialCase     = 4
    }




    [Description("Gilad Button Control")]
    [ToolboxBitmap(typeof(Control))]
    [Designer(typeof(GiladButtonDesigner))]
    public partial class GiladButton : Control
    {
        // Properties
        private CustomButtonDirection _buttonDirection = CustomButtonDirection.Forward;
        private bool _buttonEnabled                    = true;
        private bool _animateHover                     = false;
        private bool _handCursorHover                  = false;
        private bool _reverse                          = false; // flag for the animated button.


        // Variables
        private Dictionary<ButtonImages, Image> _btnImages;
        private Dictionary<ButtonForwardAnim, Image> _btnForwardAnimImages;
        private Dictionary<ButtonBackAnim, Image> _btnBackAnimImages;
        private MemoryStream _memCursor        = new MemoryStream(Properties.Resources.hand_cursor);
        private MemoryStream _memCursorClicked = new MemoryStream(Properties.Resources.hand_clicked);
        private Cursor _handCursor;
        private Cursor _handCursorClicked;
        private static Thread _threadAnimatorBack;
        private static Thread _threadAnimatorForward;


        public GiladButton()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint  |
                     ControlStyles.UserPaint             |
                     ControlStyles.SupportsTransparentBackColor
                     , true);

            //InitializeComponent();
            InitializeImages();
            this.Size                  = new Size(100, 100);
            this.BackgroundImage       = _btnImages[0];
            this.BackColor             = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }




        #region --------------------------------------- Custom Properties Controls ---------------------------------------

        [Description("Sets the button direction Back or Forward."), Category("~GiladButton Custom Data")]
        public CustomButtonDirection ButtonDirection
        {
            get { return _buttonDirection; }
            set
            {
                if (_buttonDirection == value)
                    return;

                _buttonDirection = value;
                UpdateButtonImage(CustomButtonCase.DisabledEnabled);
                this.Invalidate();
            }
        }


        [Description("Animating the button hover event."), Category("~GiladButton Custom Data")]
        public bool AnimateHover
        {
            get { return _animateHover; }
            set
            {
                if (_animateHover == value)
                    return;

                _animateHover = value;
                this.Invalidate();
            }
        }


        [Description("Hand Cursor will display while hovering button."), Category("~GiladButton Custom Data")]
        public bool HandCursorHover
        {
            get { return _handCursorHover; }
            set
            {
                if (_handCursorHover == value)
                    return;

                _handCursorHover = value;
                if (_handCursorHover)
                    InitializeHandCursor();
                else
                    this.Cursor = DefaultCursor;
                this.Invalidate();
            }
        }

        [Description("Enable or Disable Button"), Category("~GiladButton Custom Data")]
        public bool ButtonEnabled
        {
            get { return _buttonEnabled; }
            set
            {
                if (_buttonEnabled == value)
                    return;

                _buttonEnabled = value;
                UpdateButtonImage(CustomButtonCase.DisabledEnabled);
                this.Invalidate();
            }
        }
        #endregion --------------------------------------- Custom Properties Controls ---------------------------------------




        #region ------------------------------ Overriding Parents Class Events ------------------------------ 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnMouseEnter
        /// \param 
        /// \return 
        ///
        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_buttonEnabled)
                return;

            UpdateButtonImage(CustomButtonCase.Hover);

            if (_handCursorHover && _handCursor != null)
                this.Cursor = _handCursor;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnMouseLeave
        /// \param 
        /// \return 
        ///
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!_buttonEnabled)
                return;

            UpdateButtonImage(CustomButtonCase.Default);

            if (_handCursorHover && _handCursor != null)
                this.Cursor = Cursors.Default;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnMouseClick
        /// \param 
        /// \return 
        ///
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!_buttonEnabled)
                return;

            // added this check since i want only the left click to work.
            if ((e.Button & MouseButtons.Left) == 0)
                return;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnClick
        /// \param 
        /// \return 
        ///
        protected override void OnClick(EventArgs e)
        {
            if (!_buttonEnabled)
                return;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnMouseDown
        /// \param 
        /// \return 
        ///
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_buttonEnabled)
                return;

            if ((e.Button & MouseButtons.Left) == 0)
                return;

            UpdateButtonImage(CustomButtonCase.Clicked);

            if (HandCursorHover && _handCursor != null)
                this.Cursor = _handCursorClicked;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnMouseUp
        /// \param 
        /// \return 
        ///
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!_buttonEnabled)
                return;

            if ((e.Button & MouseButtons.Left) == 0)
                return;

            UpdateButtonImage(CustomButtonCase.SpecialCase);

            if (HandCursorHover && _handCursor != null)
                this.Cursor = _handCursor;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief Control::OnPaint
        /// \param 
        /// \return 
        ///
        protected override void OnPaint(PaintEventArgs pe)
        {
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(-1, -1, this.Width, this.Height)); // rounding the region of the control.

                this.Region = new Region(gp);
            }

            base.OnPaint(pe);
        }


        #endregion ------------------------------ Overriding Parents Class Events ------------------------------ 


        #region --------------------------------------- Custom Methods ---------------------------------------
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief GiladButton::InitializeImages
        /// \param 
        /// \return 
        ///
        private void InitializeImages()
        {
            _btnImages = new Dictionary<ButtonImages, Image>()
            {
                {ButtonImages.Forward        , Properties.Resources.forward},
                {ButtonImages.ForwardHover   , Properties.Resources.forward_5}, // shiny image for default hover.
                {ButtonImages.ForwardClicked , Properties.Resources.forward_clicked},
                {ButtonImages.ForwardDisabled, Properties.Resources.forward_disabled},
                {ButtonImages.Back           , Properties.Resources.back},
                {ButtonImages.BackHover      , Properties.Resources.back_5},
                {ButtonImages.BackClicked    , Properties.Resources.back_clicked},
                {ButtonImages.BackDisabled   , Properties.Resources.back_disabled}
            };


            _btnForwardAnimImages = new Dictionary<ButtonForwardAnim, Image>()
            {
                {ButtonForwardAnim.Forward1, Properties.Resources.forward},
                {ButtonForwardAnim.Forward2, Properties.Resources.forward_2},
                {ButtonForwardAnim.Forward3, Properties.Resources.forward_3},
                {ButtonForwardAnim.Forward4, Properties.Resources.forward_4},
                {ButtonForwardAnim.Forward5, Properties.Resources.forward_5},
                {ButtonForwardAnim.Forward6, Properties.Resources.forward_6}
            };


            _btnBackAnimImages = new Dictionary<ButtonBackAnim, Image>()
            {
                {ButtonBackAnim.Back1, Properties.Resources.back},
                {ButtonBackAnim.Back2, Properties.Resources.back_2},
                {ButtonBackAnim.Back3, Properties.Resources.back_3},
                {ButtonBackAnim.Back4, Properties.Resources.back_4},
                {ButtonBackAnim.Back5, Properties.Resources.back_5},
                {ButtonBackAnim.Back6, Properties.Resources.back_6}
            };
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief GiladButton::InitializeHandCursor
        /// \param 
        /// \return 
        ///
        private void InitializeHandCursor()
        {
            try
            {
                _handCursor        = new Cursor(_memCursor);
                _handCursorClicked = new Cursor(_memCursorClicked);
            }
            catch (Exception)
            {
                MessageBox.Show("[InitializeHandCursor] - Could not allocate custom cursor images.", "ERROR", MessageBoxButtons.OK);
            }

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief GiladButton::AnimateButton
        /// \param 
        /// \return 
        ///
        public void AnimateButton()
        {
            if (_reverse)
            {
                if (CustomButtonDirection.Forward == _buttonDirection)
                {
                    foreach (var btnImage in _btnForwardAnimImages.Reverse())
                    {
                        this.BackgroundImage = btnImage.Value;
                        Thread.Sleep(40);
                    }
                }
                else
                {
                    foreach (var btnImage in _btnBackAnimImages.Reverse())
                    {
                        this.BackgroundImage = btnImage.Value;
                        Thread.Sleep(40);
                    }
                }

                _threadAnimatorBack.Abort();
            }
            else
            {
                if (CustomButtonDirection.Forward == _buttonDirection)
                {
                    foreach (var btnImage in _btnForwardAnimImages)
                    {
                        if (_reverse) // added this checks to avoid 2 threads running at the same time and updating images.
                            _threadAnimatorForward.Abort();

                        this.BackgroundImage = btnImage.Value;
                        Thread.Sleep(40);
                    }

                }
                else
                {
                    foreach (var btnImage in _btnBackAnimImages)
                    {
                        if (_reverse)                     
                            _threadAnimatorForward.Abort();

                        this.BackgroundImage = btnImage.Value;
                        Thread.Sleep(40);
                    }
                }

                _threadAnimatorForward.Abort();
            }

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief GiladButton::UpdateButtonImage
        /// \param
        /// \return
        ///
        private void UpdateButtonImage(CustomButtonCase buttonCase)
        {
            if (_animateHover)
            {
                switch (buttonCase)
                {
                    case CustomButtonCase.Default:
                        _reverse = true;

                        try
                        {
                            _threadAnimatorBack = new Thread(new ThreadStart(AnimateButton));
                            _threadAnimatorBack.Start();
                        }
                        catch (Exception)
                        {
                            _threadAnimatorBack.Abort();
                        }
                        break;

                    case CustomButtonCase.Hover:
                        // NOTE: In order to make it animated, a BackgroundImage object does not support gif, 
                        // But a PictureBox Image support which is implemented in PictureBox that disabling us from
                        // overriding some of the functions. 
                        // If a BackgroundImage is not supporting gif, let's make it do ^_^            
                        // This seems to do the job :-)
                        _reverse = false;

                        try
                        {
                            _threadAnimatorForward = new Thread(new ThreadStart(AnimateButton));
                            _threadAnimatorForward.Start();
                        }
                        catch (Exception)
                        {
                            _threadAnimatorForward.Abort();
                        }
                        break;

                    case CustomButtonCase.Clicked:
                        if (CustomButtonDirection.Forward == _buttonDirection)
                            this.BackgroundImage = _btnImages[ButtonImages.ForwardClicked];
                        else
                            this.BackgroundImage = _btnImages[ButtonImages.BackClicked];
                        break;

                    case CustomButtonCase.DisabledEnabled:
                        if (_buttonEnabled)
                        {
                            if (CustomButtonDirection.Forward == _buttonDirection)
                                this.BackgroundImage = _btnImages[ButtonImages.Forward];
                            else
                                this.BackgroundImage = _btnImages[ButtonImages.Back];
                        }
                        else
                        {
                            if (CustomButtonDirection.Forward == _buttonDirection)
                                this.BackgroundImage = _btnImages[ButtonImages.ForwardDisabled];
                            else
                                this.BackgroundImage = _btnImages[ButtonImages.BackDisabled];
                        }
                        break;

                    case CustomButtonCase.SpecialCase:
                        if (CustomButtonDirection.Forward == _buttonDirection)
                            this.BackgroundImage = _btnForwardAnimImages[ButtonForwardAnim.Forward5];
                                // means the last image with glowing effect.
                        else
                            this.BackgroundImage = _btnBackAnimImages[ButtonBackAnim.Back5];
                        break;

                    default:
                        MessageBox.Show("Unable to detect button type. AnimateHover -> True");
                        break;
                }
            } // End if
            else
            {
                switch (buttonCase)
                {
                    case CustomButtonCase.Default:
                        if (CustomButtonDirection.Forward == _buttonDirection)
                            this.BackgroundImage = _btnImages[ButtonImages.Forward];
                        else
                            this.BackgroundImage = _btnImages[ButtonImages.Back];
                        break;

                    case CustomButtonCase.Hover:
                        if (CustomButtonDirection.Forward == _buttonDirection)
                            this.BackgroundImage = _btnImages[ButtonImages.ForwardHover];
                        else
                            this.BackgroundImage = _btnImages[ButtonImages.BackHover];
                        break;

                    case CustomButtonCase.Clicked:
                        if (CustomButtonDirection.Forward == _buttonDirection)
                            this.BackgroundImage = _btnImages[ButtonImages.ForwardClicked];
                        else
                            this.BackgroundImage = _btnImages[ButtonImages.BackClicked];
                        break;

                    case CustomButtonCase.DisabledEnabled:
                        if (_buttonEnabled)
                        {
                            if (CustomButtonDirection.Forward == _buttonDirection)
                                this.BackgroundImage = _btnImages[ButtonImages.Forward];
                            else
                                this.BackgroundImage = _btnImages[ButtonImages.Back];
                        }
                        else
                        {
                            if (CustomButtonDirection.Forward == _buttonDirection)
                                this.BackgroundImage = _btnImages[ButtonImages.ForwardDisabled];
                            else
                                this.BackgroundImage = _btnImages[ButtonImages.BackDisabled];
                        }
                        break;

                    case CustomButtonCase.SpecialCase:
                        if (CustomButtonDirection.Forward == _buttonDirection)
                            this.BackgroundImage = _btnForwardAnimImages[ButtonForwardAnim.Forward5]; // means the last image with glowing effect.
                        else
                            this.BackgroundImage = _btnBackAnimImages[ButtonBackAnim.Back5];
                        break;

                    default:
                        MessageBox.Show("Unable to detect button type. AnimateHover -> False");
                        break;
                }
            } // End else

        } // End switch


        #endregion --------------------------------------- Custom Methods ---------------------------------------

    }
}
