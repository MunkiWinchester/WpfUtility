using System.Windows.Input;
using CutAndGo.Views;
using WpfUtility.Services;

namespace Sample
{
    public class MainWindowViewModel : ObservableObject
    {
        public ICommand SettingsClickedCommand => new RelayCommand<MainWindow>(OpenSettings);

        /// <summary>
        ///     Opens the settings window centered on the main window
        /// </summary>
        /// <param name="mainWindow"></param>
        private static void OpenSettings(MainWindow mainWindow)
        {
            var settings = new SettingsWindow { Owner = mainWindow };
            settings.ShowDialog();
        }
    }
}