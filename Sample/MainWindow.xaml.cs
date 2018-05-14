using System;
using System.Windows;
using MahApps.Metro;
using Sample.Properties;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Settings.Default.SelectedAccent = string.IsNullOrWhiteSpace(Settings.Default.SelectedAccent)
                ? "Crimson"
                : Settings.Default.SelectedAccent;
            Settings.Default.SelectedTheme = string.IsNullOrWhiteSpace(Settings.Default.SelectedTheme)
                ? "BaseDark"
                : Settings.Default.SelectedTheme;
            Settings.Default.Save();

            try
            {
                ThemeManager.ChangeAppStyle(Application.Current,
                    ThemeManager.GetAccent(Settings.Default.SelectedAccent),
                    ThemeManager.GetAppTheme(Settings.Default.SelectedTheme));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Crimson"),
                    ThemeManager.GetAppTheme("BaseDark"));
            }
        }
    }
}