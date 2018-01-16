using ReichUI.Annotations;

namespace ReichUI.Helpers.Properties.Events
{

    /// <summary>
    /// Used for deriving this class to another class in order to implement the events behavior.
    /// </summary>
    public abstract class RefreshDesignerValues
    {
        public abstract event ValueChangedEventHandler ValueChanged;

        [NotifyPropertyChangedInvocator]
        protected abstract void OnValueChanged(string propertyName);
    }
}