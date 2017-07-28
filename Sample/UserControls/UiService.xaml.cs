using System.Windows;
using WpfUtility.GeneralUserControls;

namespace Sample.UserControls
{
    /// <summary>
    /// Interaction logic for UiService.xaml
    /// </summary>
    public partial class UiService
    {
        public UiService()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            UiServices.ToggleBusyState();
        }
    }
}
