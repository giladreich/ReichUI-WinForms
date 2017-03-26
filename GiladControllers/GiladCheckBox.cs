using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiladControllers
{
    #region --- Some Enums ---
    public enum CheckBoxState
    {
        DefaultState = 0,
        DefaultStateHover,
        Disabled,
        Checked,
        CheckedHover,
        ClickCheck,
        ClickUncheck,
        WrongAnswer
    }

    public enum CursorState
    {
        Default,
        Hover,
        Click
    }

    public enum ControlViewMode
    {
        Active,
        Inactive
    }
    #endregion --- Some Enums ---


    public partial class GiladCheckBox : UserControl
    {
        #region --- Local Varibles ---
        private bool _checkBoxEnabled          = true;
        private bool _handCursorHover          = false;
        private bool _checked                  = false;
        private bool _wrongAnswer              = false;
        private Color _labelForeColor          = Color.Black;
        private Color _labelForeColorHover     = Color.White;
        private Color _labelForeColorDisabled  = Color.Gray;
        private ControlViewMode _viewMode      = ControlViewMode.Active;

        private Dictionary<CheckBoxState, Image> _boxImages;
        private MemoryStream _memCursor;
        private MemoryStream _memCursorClicked;
        private Cursor _handCursor;
        private Cursor _handCursorClicked;
        #endregion --- Local Varibles ---


        public GiladCheckBox()
        {
            InitializeComponent();
            InitializeImages();
        }





        #region --- Custom Properties Controls ---
        [Description("You can register and unregister all events. " +
                     "By setting Inactive, all events would be unregistered " +
                     "and Active will register them back."), Category("~Custom Data")]
        public ControlViewMode ViewModeState
        {
            get { return _viewMode; }
            set
            {
                if (_viewMode == value)
                    return;

                _viewMode = value;

                if (_viewMode == ControlViewMode.Active)
                {
                    // Register the events
                    this.pbCheckBox.MouseEnter += pbCheckBox_MouseEnter;
                    this.pbCheckBox.MouseLeave += pbCheckBox_MouseLeave;
                    this.pbCheckBox.MouseClick += pbCheckBox_MouseClick;
                    this.pbCheckBox.Click      += pbCheckBox_Click;
                    this.pbCheckBox.MouseDown  += pbCheckBox_MouseDown;
                    this.pbCheckBox.MouseUp    += pbCheckBox_MouseUp;
                }
                else
                {
                    // Unregister the events
                    this.pbCheckBox.MouseEnter -= pbCheckBox_MouseEnter;
                    this.pbCheckBox.MouseLeave -= pbCheckBox_MouseLeave;
                    this.pbCheckBox.MouseClick -= pbCheckBox_MouseClick;
                    this.pbCheckBox.Click      -= pbCheckBox_Click;
                    this.pbCheckBox.MouseDown  -= pbCheckBox_MouseDown;
                    this.pbCheckBox.MouseUp    -= pbCheckBox_MouseUp;
                }
            }
        }

        [Description("Hand Cursor will display while hovering."), Category("~Custom Data")]
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
                {
                    this.Cursor = DefaultCursor;
                    _memCursor.Dispose();
                    _memCursorClicked.Dispose();
                    _handCursor.Dispose();
                    _handCursorClicked.Dispose();
                }
            }
        }

        [Description("Sets/Gets the color of the label."), Category("~Custom Data")]
        public Color LabelForeColor
        {
            get { return _labelForeColor; }
            set
            {
                if (_labelForeColor == value)
                    return;
                lblCheckBox.ForeColor = _labelForeColor = value;
                this.Invalidate();
            }
        }

        [Description("CheckBox Label color when it's being hovered."), Category("~Custom Data")]
        public Color LabelForeColorHover
        {
            get { return _labelForeColorHover; }
            set
            {
                if (_labelForeColorHover == value)
                    return;

                _labelForeColorHover = value;
            }
        }

        [Description("Sets/Gets the color of the label when it's disabled."), Category("~Custom Data")]
        public Color LabelForeColorDisabled
        {
            get { return _labelForeColorDisabled; }
            set
            {
                if (_labelForeColorDisabled == value)
                    return;
                _labelForeColorDisabled = value;
            }
        }

        [Description("Enable or Disable the CheckBox."), Category("~Custom Data")]
        public bool CheckBoxEnabled
        {
            get { return _checkBoxEnabled; }
            set
            {
                if (_checkBoxEnabled == value)
                    return;

                _checkBoxEnabled = value;
                if (_checkBoxEnabled)
                {
                    lblCheckBox.ForeColor = _labelForeColor;
                    UpdateCheckBoxImage(CheckBoxState.DefaultState);
                }
                else
                {
                    lblCheckBox.ForeColor = _labelForeColorDisabled;
                    UpdateCheckBoxImage(CheckBoxState.Disabled);
                }
                this.Invalidate();
            }
        }

        [Description("Boolean flag for CheckBox check."), Category("~Custom Data")]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (_checked == value)
                    return;

                _checked = value;
                if (_checked)
                    UpdateCheckBoxImage(CheckBoxState.Checked);
                else
                    UpdateCheckBoxImage(CheckBoxState.DefaultState);
                this.Invalidate();
            }
        }

        [Description("Changes the check box image as wrong answer."), Category("~Custom Data")]
        public bool WrongAnswer
        {
            get { return _wrongAnswer; }
            set
            {
                if (_wrongAnswer == value)
                    return;

                _wrongAnswer = value;
                if (_wrongAnswer)
                {
                    _checkBoxEnabled = false;
                    UpdateCheckBoxImage(CheckBoxState.WrongAnswer);
                }
                else
                {
                    _checkBoxEnabled = true;
                    UpdateCheckBoxImage(CheckBoxState.DefaultState);
                }
                this.Invalidate();
            }
        }
        #endregion --- Custom Properties Controls ---





        private void InitializeImages()
        {
            _boxImages = new Dictionary<CheckBoxState, Image>()
            {
                {CheckBoxState.DefaultState     , Properties.Resources.CB_DefaultState},
                {CheckBoxState.DefaultStateHover, Properties.Resources.CB_DefaultStateHover},
                {CheckBoxState.Disabled         , Properties.Resources.CB_Disabled},
                {CheckBoxState.Checked          , Properties.Resources.CB_Checked},
                {CheckBoxState.CheckedHover     , Properties.Resources.CB_CheckedHover},
                {CheckBoxState.ClickCheck       , Properties.Resources.CB_ClickCheck},
                {CheckBoxState.ClickUncheck     , Properties.Resources.CB_ClickUncheck},
                {CheckBoxState.WrongAnswer      , Properties.Resources.CB_WrongAnswer}
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
                _memCursor         = new MemoryStream(Properties.Resources.hand_cursor_cb);
                _memCursorClicked  = new MemoryStream(Properties.Resources.hand_clicked_cb);
                _handCursor        = new Cursor(_memCursor);
                _handCursorClicked = new Cursor(_memCursorClicked);
            }
            catch (Exception)
            {
                MessageBox.Show("[InitializeHandCursor] - Could not allocate custom cursor images.", "ERROR", MessageBoxButtons.OK);
            }
        }


        private void UpdateCursor(CursorState cursorState)
        {
            switch (cursorState)
            {
                case CursorState.Hover:
                    this.Cursor = _handCursor;
                    break;

                case CursorState.Click:
                    this.Cursor = _handCursorClicked;
                    break;

                default:
                    this.Cursor = DefaultCursor;
                    break;
            }
        }


        private void UpdateCheckBoxImage(CheckBoxState state)
        {
            // Running this in switch statement if further implementation will be needed for each case.
            switch (state)
            {
                case CheckBoxState.DefaultState:
                    pbCheckBox.Image = _boxImages[CheckBoxState.DefaultState];
                    break;

                case CheckBoxState.DefaultStateHover:
                    pbCheckBox.Image = _boxImages[CheckBoxState.DefaultStateHover];
                    break;

                case CheckBoxState.Disabled:
                    pbCheckBox.Image = _boxImages[CheckBoxState.Disabled];
                    break;

                case CheckBoxState.Checked:

                    pbCheckBox.Image = _boxImages[CheckBoxState.Checked];
                    break;

                case CheckBoxState.CheckedHover:
                    pbCheckBox.Image = _boxImages[CheckBoxState.CheckedHover];
                    break;

                case CheckBoxState.ClickCheck:
                    pbCheckBox.Image = _boxImages[CheckBoxState.ClickCheck];
                    break;

                case CheckBoxState.ClickUncheck:
                    pbCheckBox.Image = _boxImages[CheckBoxState.ClickUncheck];
                    break;

                case CheckBoxState.WrongAnswer:
                    pbCheckBox.Image = _boxImages[CheckBoxState.WrongAnswer];
                    break;

                default:
                    pbCheckBox.Image = _boxImages[CheckBoxState.DefaultState];
                    MessageBox.Show("[UpdateCheckBoxImage] - Unmatched Switch case, setting to default state.");
                    break;
            }
        }




        #region Event Handlers
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief UserControl::OnMouseEnter
        /// \param 
        /// \return 
        ///
        private void pbCheckBox_MouseEnter(object sender, EventArgs e)
        {
            if (!_checkBoxEnabled)
                return;

            UpdateCheckBoxImage(_checked ? CheckBoxState.CheckedHover : CheckBoxState.DefaultStateHover);
            if (_handCursorHover && _handCursor != null)
                UpdateCursor(CursorState.Hover);

            lblCheckBox.ForeColor = LabelForeColorHover;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief UserControl::OnMouseLeave
        /// \param 
        /// \return 
        ///
        private void pbCheckBox_MouseLeave(object sender, EventArgs e)
        {
            if (!_checkBoxEnabled)
                return;

            UpdateCheckBoxImage(_checked ? CheckBoxState.Checked : CheckBoxState.DefaultState);
            if (_handCursorHover && _handCursor != null)
                UpdateCursor(CursorState.Default);

            lblCheckBox.ForeColor = LabelForeColor;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief UserControl::OnMouseClick
        /// \param 
        /// \return 
        ///
        private void pbCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            // added this check since i want only the left click to work.
            if (!_checkBoxEnabled || (e.Button & MouseButtons.Left) == 0)
                return;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief UserControl::OnClick
        /// \param 
        /// \return 
        ///
        private void pbCheckBox_Click(object sender, EventArgs e)
        {
            // Casting to MouseEventArgs to be able to check if left or other keys clicked.
            MouseEventArgs eMouse = (MouseEventArgs) e;
            if (!_checkBoxEnabled || (eMouse.Button & MouseButtons.Left) == 0 || eMouse.Clicks == 2)
                return;

            if (_checked)
            {
                _checked = false;
                UpdateCheckBoxImage(CheckBoxState.DefaultStateHover);
            }
            else
            {
                _checked = true;;
                UpdateCheckBoxImage(CheckBoxState.CheckedHover);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief UserControl::OnMouseDown
        /// \param 
        /// \return 
        ///
        private void pbCheckBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_checkBoxEnabled || (e.Button & MouseButtons.Left) == 0 || e.Clicks == 2)
                return;

            UpdateCheckBoxImage(_checked ? CheckBoxState.ClickUncheck : CheckBoxState.ClickCheck);
            if (HandCursorHover && _handCursor != null)
                this.Cursor = _handCursorClicked;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \brief UserControl::OnMouseUp
        /// \param 
        /// \return 
        ///
        private void pbCheckBox_MouseUp(object sender, MouseEventArgs e)
        {
            // !ClientRectangle.Contains(PointToClient(Control.MousePosition)) checks if leaves the area of the control.
            if (!_checkBoxEnabled || (e.Button & MouseButtons.Left) == 0)
                return;

            if (HandCursorHover && _handCursor != null)
                this.Cursor = _handCursor;
        }

        #endregion Event Handlers
    }
}
