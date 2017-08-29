using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfUtility.GeneralUserControls
{
    /// <summary>
    /// Interaction logic for CircularLoadingAnimation.xaml
    /// </summary>
    public partial class CircularLoadingAnimation
    {
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(nameof(ForegroundColor), typeof(Brush),
                typeof(CircularLoadingAnimation), new FrameworkPropertyMetadata(
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
            var circularLoadingAnimation = dependencyObject as CircularLoadingAnimation;
            if (circularLoadingAnimation != null)
            {
                var color = dependencyPropertyChangedEventArgs.NewValue as SolidColorBrush;
                if (color != null)
                    circularLoadingAnimation._viewModel.ForegroundColor = color;
            }
        }

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(CircularLoadingAnimation),
                new UIPropertyMetadata(false, IsLoadingPropertyChangedCallback));

        private static void IsLoadingPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var circularLoadingAnimation = dependencyObject as CircularLoadingAnimation;
            if (circularLoadingAnimation != null)
            {
                var isLoading = (bool) dependencyPropertyChangedEventArgs.NewValue;
                circularLoadingAnimation._viewModel.IsLoading = isLoading;

                var spinner = circularLoadingAnimation.Resources["Spinner"] as Storyboard;
                if (spinner != null)
                {
                    if (isLoading)
                        spinner.Begin(circularLoadingAnimation, true);
                    else
                        spinner.Stop(circularLoadingAnimation);
                }
            }
        }


        /// <summary>
        ///     Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        private readonly CircularLoadingAnimationViewModel _viewModel;

        public CircularLoadingAnimation()
        {
            InitializeComponent();
            _viewModel = new CircularLoadingAnimationViewModel();
            DataContext = _viewModel;
        }
    }
}
