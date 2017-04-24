using System;
using GiladControllers.Annotations;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GiladControllers.Helpers.Properties.Events;

namespace GiladControllers.Helpers.Properties.GiladForm
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class AppTitle : RefreshDesignerValues
    {

        // Registering listerners to the properties.
        public override event ValueChangedEventHandler ValueChanged;

        [NotifyPropertyChangedInvocator]
        protected override void OnValueChanged([CallerMemberName] string propertyName = null)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(propertyName));
        }



        private Icon _icon = GiladControllers.Properties.Resources.app_icon;
        private Size _iconSize = new Size(32, 32);
        private Point _iconLocation = new Point(10, 10);
        private bool _showIcon = true;
        private bool _showTextTitle = true;
        private SolidBrush _brush = new SolidBrush(Color.Red); // depend on TextColor property.
        private Font _font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
        private Point _textLocation = new Point(50, 15);

        public AppTitle() { }

        [Description("Application title font.")]
        public Icon Icon
        {
            get { return ShowIcon ? _icon : null; }
            set
            {
                if (_icon == value || !ShowIcon) return;
                _icon = value;
                OnValueChanged(nameof(Icon));
            }
        }

        [Description("Application title font.")]
        public Size IconSize
        {
            get { return ShowIcon ? _iconSize : new Size(0,0); }
            set
            {
                if (_iconSize == value || !ShowIcon) return;
                _iconSize = value;
                OnValueChanged(nameof(IconSize));
            }
        }

        [Description("Application title font.")]
        public Point IconLocation
        {
            get { return ShowIcon ? _iconLocation : new Point(0,0); }
            set
            {
                if (_iconLocation == value || !ShowIcon) return;
                _iconLocation = value;
                OnValueChanged(nameof(IconLocation));
            }
        }

        [Description("Displays the application icon.")]
        public bool ShowIcon
        {
            get { return _showIcon; }
            set
            {
                _showIcon = value;
                if (ShowTextTitle)
                    _textLocation.X = _showIcon
                        ? TextLocation.X + 40
                        : TextLocation.X - 40;
                OnValueChanged(nameof(ShowIcon));
            }
        }

        [Description("Displays the application title text.")]
        public bool ShowTextTitle
        {
            get { return _showTextTitle; }
            set
            {
                _showTextTitle = value;
                OnValueChanged(nameof(ShowTextTitle));
            }
        }



        [Browsable(false)]
        public SolidBrush Brush => _brush;

        [Description("Application title color.")]
        public Color TextColor
        {
            get { return ShowTextTitle ? _brush.Color : Color.Empty; }
            set
            {
                if (_brush.Color == value || !ShowTextTitle) return;
                _brush = null;
                _brush = new SolidBrush(value);
                OnValueChanged(nameof(TextColor));
            }
        }

        [Description("Application title font.")]
        public Font TextFont
        {
            get { return ShowTextTitle ? _font : null; }
            set
            {
                if (_font.Equals(value) || !ShowTextTitle) return;
                _font = value;
                OnValueChanged(nameof(TextFont));
            }
        }


        [Description("Application title font.")]
        public Point TextLocation
        {
            get { return ShowTextTitle ? _textLocation : new Point(0,0); }
            set
            {
                if (_textLocation == value || !ShowTextTitle) return;
                _textLocation = value;
                OnValueChanged(nameof(TextLocation));
            }
        }


        public override string ToString()
        {
            return "Icons & Text Properties";
        }


    }
}
