using System.Windows;
using System.Windows.Media;
using WpfUtility.GeneralUserControls;

namespace Sample.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1
    {
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(nameof(ForegroundColor), typeof(Brush),
                typeof(UserControl1), new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.Red),
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        public SolidColorBrush ForegroundColor
        {
            get => (SolidColorBrush)GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        public UserControl1()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
