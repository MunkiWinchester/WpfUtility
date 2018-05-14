using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfUtility.GeneralUserControls
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for CircularLoadingAnimation.xaml
    /// </summary>
    public partial class CircularLoadingAnimation
    {
        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(nameof(ForegroundColor), typeof(SolidColorBrush),
                typeof(CircularLoadingAnimation),
                new UIPropertyMetadata(new SolidColorBrush(Colors.Red)));

        /// <summary>
        /// Gets or sets a value indicating whether this animation is shown.
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(CircularLoadingAnimation),
                new UIPropertyMetadata(false, IsLoadingPropertyChangedCallback));

        /// <summary>
        /// Constructor for the CircularLoadingAnimation
        /// </summary>
        public CircularLoadingAnimation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        public SolidColorBrush ForegroundColor
        {
            get => (SolidColorBrush) GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this animation is shown.
        /// </summary>
        public bool IsLoading
        {
            get => (bool) GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        /// <summary>
        /// Method which is invoked trough the dependency
        /// </summary>
        /// <param name="dependencyObject">This contains the CircularLoadingAnimation ("this")</param>
        /// <param name="dependencyPropertyChangedEventArgs">This contains the changed event arguments such as the new value</param>
        private static void IsLoadingPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyObject is CircularLoadingAnimation circularLoadingAnimation)
            {
                var isLoading = (bool) dependencyPropertyChangedEventArgs.NewValue;

                if (circularLoadingAnimation.Resources["Spinner"] is Storyboard spinner)
                    if (isLoading)
                        spinner.Begin(circularLoadingAnimation, true);
                    else
                        spinner.Stop(circularLoadingAnimation);
            }
        }
    }
}