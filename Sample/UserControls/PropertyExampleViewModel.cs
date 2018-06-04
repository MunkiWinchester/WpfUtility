using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class PropertyExampleViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the first value
        /// </summary>
        public string ValueOne { get; set; } = "The first value";

        /// <summary>
        /// Gets or sets the second value
        /// </summary>
        public string ValueTwo { get; set; } = "Value two";

        /// <summary>
        /// Backing field for <see cref="Info"/>
        /// </summary>
        private string _info = "";
        /// <summary>
        /// Gets or sets the info
        /// </summary>
        public string Info
        {
            get => _info;
            set => SetField(ref _info, value);
        }

        /// <summary>
        /// Command to show the properties
        /// </summary>
        public ICommand ShowAllPropertyCommand => new DelegateCommand(() => WriteProperties(PropertyAccessType.Any));
        /// <summary>
        /// Command to show readable properties
        /// </summary>
        public ICommand ShowReadPropertyCommand => new DelegateCommand(() => WriteProperties(PropertyAccessType.Read));
        /// <summary>
        /// Command to show writeable properties
        /// </summary>
        public ICommand ShowWritePropertyCommand => new DelegateCommand(() => WriteProperties(PropertyAccessType.Write));
        /// <summary>
        /// Command to show read / write able properties
        /// </summary>
        public ICommand ShowReadWritePropertyCommand => new DelegateCommand(() => WriteProperties(PropertyAccessType.Read | PropertyAccessType.Write));
        /// <summary>
        /// Command to raise the change event
        /// </summary>
        public ICommand RaiseChangeEventCommand => new DelegateCommand(() =>
        {
            OnPropertyChanged(GetProperties(PropertyAccessType.Write));
            Info = "'OnPropertyChanged' raised.";
        });
        /// <summary>
        /// Changes the values without a change event
        /// </summary>
        public ICommand ChangeValuesCommand => new DelegateCommand(() =>
        {
            ValueOne = $"Value one. Time: {DateTime.Now:HH:mm:ss}";
            ValueTwo = $"Value two. Time: {DateTime.Now:HH:mm:ss}";

            Info = "Values changed.";
        });

        /// <summary>
        /// Changes the values and raises the change event
        /// </summary>
        public ICommand ChangeRaiseCommand => new DelegateCommand(() =>
        {
            ValueOne = $"Value one. Time: {DateTime.Now:HH:mm:ss}";
            ValueTwo = $"Value two. Time: {DateTime.Now:HH:mm:ss}";

            OnPropertyChanged(GetProperties(PropertyAccessType.Write));

            Info = "Values changed and 'OnPropertyChanged' event raised.";
        });

        /// <summary>
        /// Writes the properties
        /// </summary>
        private void WriteProperties(PropertyAccessType accessType)
        {
            var properties = GetProperties(accessType);

            Info = $"Properties (parameter: {accessType}):";
            foreach (var property in properties)
            {
                Info += $"\r\n- {property}";
            }
        }
    }
}
