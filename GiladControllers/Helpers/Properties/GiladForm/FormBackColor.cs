using GiladControllers.Annotations;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using GiladControllers.Helpers.Properties.Events;

namespace GiladControllers.Helpers.Properties.GiladForm
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FormBackColor : RefreshDesignerValues
    {
        private Color _gradientColor1 = Color.Black;
        private Color _gradientColor2 = Color.Brown;
        private LinearGradientMode _gradientMode = LinearGradientMode.Vertical;


        public FormBackColor() { }

        public FormBackColor(LinearGradientMode gradientMode, Color gradColor1, Color gradColor2)
        {
            _gradientMode = gradientMode;
            _gradientColor1 = gradColor1;
            _gradientColor2 = gradColor2;
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        [Description("First color for gradient design will be mixed with Color2.")]
        public Color GradientColor1
        {
            get { return _gradientColor1; }
            set
            {
                if (_gradientColor1 == value) return;
                _gradientColor1 = value;
                OnValueChanged(nameof(GradientColor1));
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        [Description("Second color for gradient design will be mixed with Color1.")]
        public Color GradientColor2
        {
            get { return _gradientColor2; }
            set
            {
                if (_gradientColor2 == value) return;
                _gradientColor2 = value;
                OnValueChanged(nameof(GradientColor2));
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////
        /// 
        [Description("Gradient mode position, the way the gradient is set on the control.")]
        public LinearGradientMode GradientMode
        {
            get { return _gradientMode; }
            set
            {
                if (_gradientMode == value) return;
                _gradientMode = value;
                OnValueChanged(nameof(GradientMode));
            }
        }


        public override string ToString()
        {
            // displays in the property grid.
            return string.Format("{0}; {1}; {2}", GradientMode, GradientColor1.Name, GradientColor2.Name);
        }


        public override event ValueChangedEventHandler ValueChanged;

        [NotifyPropertyChangedInvocator]
        protected override void OnValueChanged([CallerMemberName] string propertyName = null)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(propertyName));
        }
    }
}