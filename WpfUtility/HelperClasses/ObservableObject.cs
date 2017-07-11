using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NlogViewer.Properties;

namespace WpfUtility
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
