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
    public enum CbIndex
    {
        First  = 0,
        Second = 1,
        Third  = 2,
        Fourth = 3
    }

    public partial class Gilad4CheckBox : UserControl
    {
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
            get { return cbOption1.HandCursorHover; }
            set
            {
                if (cbOption1.HandCursorHover == value)
                    return;

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
            get { return cbOption1.LabelForeColor; }
            set
            {
                if (cbOption1.LabelForeColor == value)
                    return;

                foreach (var checkBox in _checkBoxes)
                    checkBox.LabelForeColor = value;
                this.Invalidate();
            }
        }

        [Description("CheckBox Label color when it's being hovered."), Category("~Custom Data")]
        public Color LabelForeColorHover
        {
            get { return cbOption1.LabelForeColorHover; }
            set
            {
                if (cbOption1.LabelForeColorHover == value)
                    return;

                foreach (var checkBox in _checkBoxes)
                    checkBox.LabelForeColorHover = value;
                this.Invalidate();
            }
        }

        [Description("Sets/Gets the color of the label when it's disabled."), Category("~Custom Data")]
        public Color LabelForeColorDisabled
        {
            get { return cbOption1.LabelForeColorDisabled; }
            set
            {
                if (cbOption1.LabelForeColorDisabled == value)
                    return;

                foreach (var checkBox in _checkBoxes)
                    checkBox.LabelForeColorDisabled = value;
            }
        }

        [Description("Returns the selected CheckBox index from the 4 boxes starting from 0 to 3.")]
        public int SelectedCheckBoxIndex => GetEnabledCheckBox();


        [Description("Returns whether one of the CheckBoxes is Checked as true or false for comparison.")]
        public bool Checked
        {
            get
            {
                foreach (var checkBox in _checkBoxes)
                    if (checkBox.Checked)
                        return true;

                return false;
            }
        }

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

        /// <summary>
        /// Lets you select/deselect one of the CheckBoxes with a enum indexer.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="flag"></param>
        public void SelectCheckBox(CbIndex index, bool flag)
        {
            _checkBoxes[(int)index].Checked = flag;

            switch ((int)index)
            {
                case 0:
                    ValidateCheckBox1();
                    break;
                case 1:
                    ValidateCheckBox2();
                    break;
                case 2:
                    ValidateCheckBox3();
                    break;
                case 3:
                    ValidateCheckBox4();
                    break;
            }

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

            ValidateCheckBox1();
        }

        private void cbOption2_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption2.CheckBoxEnabled)
                return;

            ValidateCheckBox2();
        }

        private void cbOption3_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption3.CheckBoxEnabled)
                return;

            ValidateCheckBox3();
        }

        private void cbOption4_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbOption4.CheckBoxEnabled)
                return;

            ValidateCheckBox4();
        }
        #endregion -------------------------------------- CheckBoxes Events ---------------------------------------


        private void ValidateCheckBox1()
        {
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

        private void ValidateCheckBox2()
        {
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

        private void ValidateCheckBox3()
        {
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

        private void ValidateCheckBox4()
        {
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

    }
}
