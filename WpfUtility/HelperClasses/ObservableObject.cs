using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NlogViewer.Properties;

namespace WpfUtility
{
    /// <summary>
    ///     This class allows simpler handling of the INotifyPropertyChanged
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises the OnPropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property (if none is given CallerMemberName will be used)</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Sets a field to the given value
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="field">Reference of the given field</param>
        /// <param name="value">Value which will be set to the field</param>
        /// <param name="propertyName">Name of the property (if none is given CallerMemberName will be used)</param>
        /// <returns>True if the value has changed or false if it equal</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            // ReSharper disable once ExplicitCallerInfoArgument
            // Don't remove propertyName! CallerMemberName would be SetField and not the property!
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}