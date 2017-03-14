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
        private GiladCheckBox[] _checkBoxes;
        private Color _labelForeColor          = Color.Black;
        private Color _labelForeColorDisabled  = Color.Gray;


        public Gilad4CheckBox()
        {
            InitializeComponent();
            this.cbOption1.pbCheckBox.Click += new EventHandler(cbOption1_CheckedChanged);
            this.cbOption2.pbCheckBox.Click += new EventHandler(cbOption2_CheckedChanged);
            this.cbOption3.pbCheckBox.Click += new EventHandler(cbOption3_CheckedChanged);
            this.cbOption4.pbCheckBox.Click += new EventHandler(cbOption4_CheckedChanged);
            _checkBoxes = new GiladCheckBox[4]{ cbOption1, cbOption2, cbOption3, cbOption4 };
        }

        #region ----------------------------------- Properties -----------------------------------
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

        [Description("Set/Get CheckBox 1 Text."), Category("~Custom Data")]
        public string CheckBox1Text
        {
            get { return cbOption1.Text; }
            set
            {
                if (cbOption1.Text == value)
                    return;
                cbOption1.Text = value;
            }
        }


        [Description("Set/Get CheckBox 2 Text."), Category("~Custom Data")]
        public string CheckBox2Text
        {
            get { return cbOption2.Text; }
            set
            {
                if (cbOption2.Text == value)
                    return;
                cbOption2.Text = value;
            }
        }

        [Description("Set/Get CheckBox 3 Text."), Category("~Custom Data")]
        public string CheckBox3Text
        {
            get { return cbOption3.Text; }
            set
            {
                if (cbOption3.Text == value)
                    return;
                cbOption3.Text = value;
            }
        }

        [Description("Set/Get CheckBox 4 Text."), Category("~Custom Data")]
        public string CheckBox4Text
        {
            get { return cbOption4.Text; }
            set
            {
                if (cbOption4.Text == value)
                    return;
                cbOption4.Text = value;
            }
        }

        #endregion ----------------------------------- Properties -----------------------------------



        private int GetEnabledCheckBox()
        {
            for (int i = 0; i < _checkBoxes.Length; i++)
                if (_checkBoxes[i].CheckBoxEnabled)
                    return i;

            return 99999; // noone sense number.
        }



        #region -------------------------------------- CheckBoxes Events ---------------------------------------
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
