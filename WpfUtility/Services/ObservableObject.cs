using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        /// The different property access types
        /// </summary>
        [Flags]
        public enum PropertyAccessType
        {
            /// <summary>
            /// All properties
            /// </summary>
            Any = 0,
            /// <summary>
            /// Only properties which are readable
            /// </summary>
            Read = 1,
            /// <summary>
            /// Only properties which are writeable
            /// </summary>
            Write = 2
        }

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
        /// Raises the OnPropertyChanged event for a list of properties
        /// </summary>
        /// <param name="properties">The list of properties</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(IEnumerable<string> properties)
        {
            foreach (var property in properties)
            {
                OnPropertyChanged(property);
            }
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

        /// <summary>
        /// Gets all properties of the deriving class
        /// </summary>
        /// <param name="accessType">The <see cref="PropertyAccessType"/> of the properties (optional, default = Read | Write)</param>
        /// <param name="onlyPublic"><see langword="true"/> to get only public properties, otherwise <see langword="false"/> (optional, default = <see langword="true"/></param>
        /// <returns></returns>
        protected List<string> GetProperties(PropertyAccessType accessType = PropertyAccessType.Read | PropertyAccessType.Write, bool onlyPublic = true)
        {
            if (!GetType().IsClass)
                return new List<string>();

            List<PropertyInfo> properties;
            switch ((int)accessType)
            {
                case 1:
                    properties = GetType().GetProperties().Where(w => w.CanRead).ToList();
                    break;
                case 2:
                    properties = GetType().GetProperties().Where(w => w.CanWrite).ToList();
                    break;
                case 3:
                    properties = GetType().GetProperties().Where(w => w.CanRead && w.CanWrite).ToList();
                    break;
                default:
                    properties = GetType().GetProperties().ToList();
                    break;
            }

            return onlyPublic
                ? properties.Where(w => w.GetMethod.IsPublic).Select(s => s.Name).ToList()
                : properties.Select(s => s.Name).ToList();
        }
    }
}