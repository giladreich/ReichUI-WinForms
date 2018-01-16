using System;

namespace ReichUI.Helpers.Properties.Events
{
    public class ValueChangedEventArgs : EventArgs
    {
        private readonly string propertyName;


        /// <summary>
        /// Stores the name of the property.
        /// </summary>
        /// <param name="propertyName"></param>
        public ValueChangedEventArgs(string propertyName)
        {
            this.propertyName = propertyName;
        }


        /// <summary>
        /// Simply returns the property name.
        /// </summary>
        public virtual string PropertyName
        {
            get { return propertyName; }
        }
    }
}