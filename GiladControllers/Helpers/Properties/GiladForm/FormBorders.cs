using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GiladControllers.Annotations;
using GiladControllers.Helpers.Properties.Events;

namespace GiladControllers.Helpers.Properties.GiladForm
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FormBorders : RefreshDesignerValues
    {

        private bool _drawBorder = true;
        private int _width = 8;
        private Color _color = Color.AntiqueWhite;


        public FormBorders() { }
        public FormBorders(bool drawBorders, int width, Color color)
        {
            this._drawBorder = drawBorders;
            this._width = width;
            this._color = color;
        }

        [Description("You can enable and disable the border drawing.")]
        public bool DrawBorders
        {
            get { return _drawBorder; }
            set
            {
                _drawBorder = value;
                OnValueChanged(nameof(DrawBorders));
            }
        }

        [Description("Modify the width of the border in the form.")]
        public int Width
        {
            get { return DrawBorders ? _width : 0; }
            set
            {
                if (!DrawBorders) return;
                _width = value;
                OnValueChanged(nameof(Width));
            }
        }

        [Description("Sets the color of the border.")]
        public Color Color
        {
            get { return DrawBorders ? _color : Color.Empty; }
            set
            {
                if (!DrawBorders) return;
                _color = value;
                OnValueChanged(nameof(Color));
            }
        }


        public override string ToString()
        {
            // displays in the property grid.
            return string.Format("{0}; {1}; {2}", DrawBorders, Width, Color.Name);
        }


        public override event ValueChangedEventHandler ValueChanged;

        [NotifyPropertyChangedInvocator]
        protected override void OnValueChanged([CallerMemberName] string propertyName = null)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(propertyName));
        }
    }



    internal class FormBordersConverter : TypeConverter
    {
        /// <summary>
        /// Let's the client know if it can convert the string back to the original source type (FormBorders)
        /// and then continues to the ConvertFrom function.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            // We can convert from a string to a FormBorders type
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }


        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            // We can be converted to an InstanceDescriptor
            if (destinationType == typeof(InstanceDescriptor)) return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the string from the desginer back to the original type that we use.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="info"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
        {
            // If converting from a string
            if (value is string)
            {
                // Build a FormBorders type
                try
                {
                    // Get FormBorders properties
                    var propertyList = (string) value;
                    var properties = propertyList.Split(';');
                    return new FormBorders(
                        bool.Parse(properties[0].Trim()), // Trim() - trims the white spaces.
                        int.Parse(properties[1].Trim()),
                        Color.FromName(properties[2])
                    );
                }
                catch
                {
                    throw new ArgumentException("Invalid arguments for the target property -> "
                                                + value.ToString());
                }
            }
            return base.ConvertFrom(context, info, value);
        }


        /// <summary>
        /// Converts the type values that we have in our custom object into a string that 
        /// will display in the designer property grid.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
                                         object value, Type destinationType)
        {
            // If source value is a Hand type
            if (value is FormBorders)

                // Convert to InstanceDescriptor
                if (destinationType == typeof(InstanceDescriptor))
                {
                    FormBorders formBorders = (FormBorders) value;
                    object[] properties = new object[3];
                    Type[] types = new Type[3];

                    // DrawBorders?
                    types[0] = typeof(bool);
                    properties[0] = formBorders.DrawBorders;

                    // Width
                    types[1] = typeof(int);
                    properties[1] = formBorders.Width;

                    // Color
                    types[2] = typeof(Color);
                    properties[2] = formBorders.Color;

                    // Build constructor
                    ConstructorInfo ci = typeof(FormBorders).GetConstructor(types);
                    return new InstanceDescriptor(ci, properties);
                }
            #region another way of convert it:
            //if (destinationType == typeof(string))
            //{
            //    try
            //    {
            //        FormBorders formBorders = (FormBorders)value;

            //        // checking wether the color is rgb or normal color and returns a proper string.
            //        string borderColor = formBorders.Color.IsNamedColor
            //                        ? formBorders.Color.Name
            //                        : formBorders.Color.R + ", " +
            //                          formBorders.Color.G + ", " +
            //                          formBorders.Color.B;

            //        string results = string.Format("{0}; {1}; {2}", formBorders.DrawBorders, formBorders.Width, borderColor); // displays the property as string.
            //        return results;

            //    }
            //    catch (Exception)
            //    {

            //        throw new Exception("Failed to convert property values to a string.");
            //    }
            //}
            #endregion another way of convert it:
            return base.ConvertTo(context, culture, value, destinationType);
        }


        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            // Always force a new instance
            return true;
        }


        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            // Use the dictionary to create a new instance
            return new FormBorders(
                (bool)propertyValues["DrawBorders"],
                (int)propertyValues["Width"],
                (Color)propertyValues["Color"]);
        }

    }
}
