using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro;
using Sample.Properties;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class SettingsWindowViewModel : ObservableObject
    {
        private readonly bool _initial;

        private ObservableCollection<string> _accents;

        private string _selectedAccent;

        private string _selectedTheme;

        private ObservableCollection<string> _themes;

        /// <summary>
        ///     Initializes a settings window view model
        /// </summary>
        public SettingsWindowViewModel()
        {
            Themes = new ObservableCollection<string>(ThemeManager.AppThemes.Select(x => x.Name));
            Accents = new ObservableCollection<string>(ThemeManager.Accents.Select(x => x.Name));
            _initial = true;
            SelectedAccent = Accents.First();
            SelectedTheme = Themes.First();
            _initial = false;
        }

        /// <summary>
        ///     Command to save the settings
        /// </summary>
        public ICommand SaveCommand => new DelegateCommand(SaveSettings);

        /// <summary>
        ///     Command to cancle the settings
        /// </summary>
        public ICommand CancelCommand => new DelegateCommand(ResetSettings);

        /// <summary>
        ///     Command to switch the style of the app
        /// </summary>
        public ICommand SwitchCommand => new DelegateCommand(SwitchAppStyle);

        /// <summary>
        ///     Collection with the different accents
        /// </summary>
        public ObservableCollection<string> Accents
        {
            get => _accents;
            set => SetField(ref _accents, value);
        }

        /// <summary>
        ///     Collection with the different themes
        /// </summary>
        public ObservableCollection<string> Themes
        {
            get => _themes;
            set => SetField(ref _themes, value);
        }

        /// <summary>
        ///     Value of the selected accent
        /// </summary>
        public string SelectedAccent
        {
            get => _selectedAccent;
            set
            {
                _selectedAccent = value;
                SwitchAppStyle();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Value of the selected theme
        /// </summary>
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                SwitchAppStyle();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Switches the theme and/or accent of the app
        /// </summary>
        private void SwitchAppStyle()
        {
            if (!_initial)
                ThemeManager.ChangeAppStyle(Application.Current,
                    ThemeManager.GetAccent(_selectedAccent),
                    ThemeManager.GetAppTheme(_selectedTheme));
        }

        /// <summary>
        ///     Saves the selected accent and theme to the settings
        /// </summary>
        private static void ResetSettings()
        {
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(Settings.Default.SelectedAccent),
                ThemeManager.GetAppTheme(Settings.Default.SelectedTheme));

        }

        /// <summary>
        /// Resets the settings to the previous ones
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.SelectedAccent = SelectedAccent;
            Settings.Default.SelectedTheme = SelectedTheme;
            Settings.Default.Save();
        }
    }
}