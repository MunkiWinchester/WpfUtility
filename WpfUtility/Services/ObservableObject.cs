using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NlogViewer.Properties;

namespace WpfUtility.Services
{
    /// <summary>
    /// This class allows simpler handling of the INotifyPropertyChanged
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the OnPropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property (if none is given CallerMemberName will be used)</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a field to the given value
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="field">Reference of the given field</param>
        /// <param name="value">Value which will be set to the field</param>
        /// <param name="propertyName">Name of the property (if none is given CallerMemberName will be used)</param>
        /// <returns>True if the value has changed or false if it is equal</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            // Don't remove propertyName! CallerMemberName would be SetField and not the property!
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Sets a field of the given class to the given value
        /// </summary>
        /// <typeparam name="T">The type of the class</typeparam>
        /// <param name="parent">The class which contains the field</param>
        /// <param name="property">The name of the field</param>
        /// <param name="value">The new value</param>
        /// <param name="propertyName">Name of the property (if none is given CallerMemberName will be used)</param>
        /// <returns>True if the value has changed or false if it is equal</returns>
        protected bool SetField<T>(ref T parent, string property, object value,
            [CallerMemberName] string propertyName = null)
        {
            if (parent == null)
                return false;

            if (string.IsNullOrEmpty(property))
                throw new ArgumentNullException(nameof(property));

            var propertyInfo = parent.GetType().GetProperty(property);
            if (propertyInfo == null)
                throw new NullReferenceException($"The property with the name \"{property}\" does not exist.");

            var currentValue = propertyInfo.GetValue(parent, null);
            if (currentValue.Equals(value))
                return false;

            propertyInfo.SetValue(parent, Convert.ChangeType(value, propertyInfo.PropertyType), null);
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}