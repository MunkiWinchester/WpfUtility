using System.Web.Security;
using System.Windows;

namespace Sample.UserControls
{
    /// <summary>
    ///     Interaction logic for NumberTextBox.xaml
    /// </summary>
    public partial class NumberTextBox
    {
        public NumberTextBox()
        {
            InitializeComponent();
        }

        private void NumberTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            Label.Text = Membership.GeneratePassword(12, 2);
        }

        private void Label_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Label.Text =
                Membership.GeneratePassword(128, 24);
        }
    }
}
