using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiladControllers
{
    public partial class Gilad4CheckBox : UserControl
    {
        private bool _handCursorHover          = false;

        private Color _labelForeColor          = Color.Black;
        private Color _labelForeColorDisabled  = Color.Gray;
        private readonly GiladCheckBox[] _checkBoxes;


        public Gilad4CheckBox()
        {
            InitializeComponent();
            RegisterSubEventHandlers();

            _checkBoxes       = new GiladCheckBox[4]{ cbOption1, cbOption2, cbOption3, cbOption4 };
            CheckBoxAutoSize  = false;
            CheckBoxSize      = new Size(435, 55);
            CheckBoxLabelSize = new Size(400, 40);
        }


        #region ----------------------------------- Properties -----------------------------------
        [Description("Hand Cursor will display while hovering button."), Category("~Custom Data")]
        public bool HandCursorHover
        {
            get { return _handCursorHover; }
            set
            {
                if (_handCursorHover == value)
                    return;

                _handCursorHover = value;
                foreach (var checkBox in _checkBoxes)
                    checkBox.HandCursorHover = value;
            }
        }

        [Description("Set/Get all the check boxes size."), Category("~Custom Data")]
        public Size CheckBoxSize
        {
            get { return cbOption1.Size; }
            set
            {
                if (cbOption1.Size == value)
                    return;
                foreach (var checkBox in _checkBoxes)
                    checkBox.Size = value;
                this.Invalidate();
            }
        }

        [Description("Set/Get all the check boxes Labels size."), Category("~Custom Data")]
        public Size CheckBoxLabelSize
        {
            get { return cbOption1.lblCheckBox.Size; }
            set
            {
                if (cbOption1.lblCheckBox.Size == value)
                    return;
                foreach (var checkBox in _checkBoxes)
                    checkBox.lblCheckBox.Size = value;
                this.Invalidate();
            }
        }

        [Description("Set/Get all the check boxes Font attributes."), Category("~Custom Data")]
        public Font CheckBoxFont
        {
            get { return cbOption1.lblCheckBox.Font; }
            set
            {
                if (Equals(cbOption1.lblCheckBox.Font, value))
                    return;
                foreach (var checkBox in _checkBoxes)
                    checkBox.lblCheckBox.Font = value;
                this.Invalidate();
            }
        }

        [Description("Set/Get AutSize of the labels. It's particle when text is too long so it jumps a row down."), Category("~Custom Data")]
        public bool CheckBoxAutoSize
        {
            get { return cbOption1.lblCheckBox.AutoSize; }
            set
            {
                if (cbOption1.lblCheckBox.AutoSize == value)
                    return;
                foreach (var checkBox in _checkBoxes)
                    checkBox.lblCheckBox.AutoSize = value;
                this.Invalidate();
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
                foreach (var checkBox in _checkBoxes)
                    _labelForeColor = checkBox.LabelForeColor = value;
                this.Invalidate();
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
                foreach (var checkBox in _checkBoxes)
                    _labelForeColorDisabled = checkBox.LabelForeColorDisabled = value;
            }
        }

        [Description("Returns the selected CheckBox index from the 4 boxes starting from 0 to 3.")]
        public int SelectedCheckBoxIndex => GetEnabledCheckBox();

        [Description("Set/Get CheckBox 1 Text."), Category("~Custom Data")]
        public string CheckBox1Text
        {
            get { return cbOption1.lblCheckBox.Text; }
            set
            {
                if (cbOption1.lblCheckBox.Text == value)
                    return;
                cbOption1.lblCheckBox.Text = value;
                this.Invalidate();
            }
        }

        [Description("Set/Get CheckBox 2 Text."), Category("~Custom Data")]
        public string CheckBox2Text
        {
            get { return cbOption2.lblCheckBox.Text; }
            set
            {
                if (cbOption2.lblCheckBox.Text == value)
                    return;
                cbOption2.lblCheckBox.Text = value;
                this.Invalidate();
            }
        }

        [Description("Set/Get CheckBox 3 Text."), Category("~Custom Data")]
        public string CheckBox3Text
        {
            get { return cbOption3.lblCheckBox.Text; }
            set
            {
                if (cbOption3.lblCheckBox.Text == value)
                    return;
                cbOption3.lblCheckBox.Text = value;
                this.Invalidate();
            }
        }

        [Description("Set/Get CheckBox 4 Text."), Category("~Custom Data")]
        public string CheckBox4Text
        {
            get { return cbOption4.lblCheckBox.Text; }
            set
            {
                if (cbOption4.lblCheckBox.Text == value)
                    return;
                cbOption4.lblCheckBox.Text = value;
                this.Invalidate();
            }
        }

        #endregion ----------------------------------- Properties -----------------------------------


        /// <summary>
        /// Clears out all check marks from the check boxes.
        /// </summary>
        public void ClearAllCheckMarks()
        {
            foreach (var checkBox in _checkBoxes)
            {
                checkBox.Checked = false;
                checkBox.CheckBoxEnabled = true;
            }
        }


        private int GetEnabledCheckBox()
        {
            for (int i = 0; i < _checkBoxes.Length; i++)
                if (_checkBoxes[i].CheckBoxEnabled)
                    return i;

            return 99999; // noone sense number.
        }


        #region -------------------------------------- CheckBoxes Events ---------------------------------------
        /// <summary>
        /// When registering this in the desinger file, the c# designer will run into problems at some point.
        /// This is why we do it seperatly for the sub components.
        /// </summary>
        private void RegisterSubEventHandlers()
        {
            this.cbOption1.pbCheckBox.Click += new EventHandler(cbOption1_CheckedChanged);
            this.cbOption2.pbCheckBox.Click += new EventHandler(cbOption2_CheckedChanged);
            this.cbOption3.pbCheckBox.Click += new EventHandler(cbOption3_CheckedChanged);
            this.cbOption4.pbCheckBox.Click += new EventHandler(cbOption4_CheckedChanged);
        }


        private void cbOption1_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption1.CheckBoxEnabled)
                return;

            if (cbOption1.Checked)
            {
                cbOption2.CheckBoxEnabled = false;
                cbOption3.CheckBoxEnabled = false;
                cbOption4.CheckBoxEnabled = false;
            }
            else // when it's unchecked, enable other cb's.
            {
                cbOption2.CheckBoxEnabled = true;
                cbOption3.CheckBoxEnabled = true;
                cbOption4.CheckBoxEnabled = true;
            }
        }

        private void cbOption2_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption2.CheckBoxEnabled)
                return;

            if (cbOption2.Checked)
            {
                cbOption1.CheckBoxEnabled = false;
                cbOption3.CheckBoxEnabled = false;
                cbOption4.CheckBoxEnabled = false;
            }
            else
            {
                cbOption1.CheckBoxEnabled = true;
                cbOption3.CheckBoxEnabled = true;
                cbOption4.CheckBoxEnabled = true;
            }
        }

        private void cbOption3_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption3.CheckBoxEnabled)
                return;

            if (cbOption3.Checked)
            {
                cbOption1.CheckBoxEnabled = false;
                cbOption2.CheckBoxEnabled = false;
                cbOption4.CheckBoxEnabled = false;
            }
            else
            {
                cbOption1.CheckBoxEnabled = true;
                cbOption2.CheckBoxEnabled = true;
                cbOption4.CheckBoxEnabled = true;
            }
        }

        private void cbOption4_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption4.CheckBoxEnabled)
                return;

            if (cbOption4.Checked)
            {
                cbOption1.CheckBoxEnabled = false;
                cbOption2.CheckBoxEnabled = false;
                cbOption3.CheckBoxEnabled = false;
            }
            else
            {
                cbOption1.CheckBoxEnabled = true;
                cbOption2.CheckBoxEnabled = true;
                cbOption3.CheckBoxEnabled = true;
            }
        }
        #endregion -------------------------------------- CheckBoxes Events ---------------------------------------

    }
}
