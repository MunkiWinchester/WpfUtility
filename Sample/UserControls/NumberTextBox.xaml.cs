using System.Web.Security;
using System.Windows;
using System.Windows.Input;

namespace Sample.UserControls
{
    /// <summary>
    /// Interaction logic for NumberTextBox.xaml
    /// </summary>
    public partial class NumberTextBox
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }

        private void NumberTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            _label.Text = Membership.GeneratePassword(12, 2);
        }

        private void Label_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _label.Text =
                Membership.GeneratePassword(128, 24);
        }
    }
}