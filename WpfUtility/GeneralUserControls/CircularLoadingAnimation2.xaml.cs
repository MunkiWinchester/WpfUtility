using System.Windows;
using System.Windows.Media;

namespace WpfUtility.GeneralUserControls
{
    /// <summary>
    /// Interaction logic for CircularLoadingAnimation2.xaml
    /// </summary>
    public partial class CircularLoadingAnimation2
    {
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(nameof(ForegroundColor), typeof(Brush),
                typeof(CircularLoadingAnimation2), new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.Red),
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    ForegroundColorPropertyChangedCallback));

        public SolidColorBrush ForegroundColor
        {
            get => (SolidColorBrush)GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        private static void ForegroundColorPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var circularLoadingAnimation = dependencyObject as CircularLoadingAnimation2;
            if (circularLoadingAnimation != null)
            {
                var color = dependencyPropertyChangedEventArgs.NewValue as SolidColorBrush;
                if (color != null)
                    circularLoadingAnimation._viewModel.ForegroundColor = color;
            }
        }

        private readonly CircularLoadingAnimationViewModel _viewModel;

        public CircularLoadingAnimation2()
        {
            InitializeComponent();
            _viewModel = new CircularLoadingAnimationViewModel();
            DataContext = _viewModel;
        }
    }
}
