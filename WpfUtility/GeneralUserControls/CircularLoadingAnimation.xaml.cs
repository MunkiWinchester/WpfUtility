using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfUtility.GeneralUserControls
{
    /// <summary>
    /// Interaction logic for CircularLoadingAnimation.xaml
    /// </summary>
    public partial class CircularLoadingAnimation
    {
        public SolidColorBrush ForegroundColor
        {
            get => (SolidColorBrush)GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(nameof(ForegroundColor), typeof(Brush),
                typeof(CircularLoadingAnimation), new FrameworkPropertyMetadata(
                    new SolidColorBrush(Colors.Red),
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    ForegroundColorPropertyChangedCallback));

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

        private readonly DispatcherTimer _animationTimer;
        private readonly CircularLoadingAnimationViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularLoadingAnimation"/> class.
        /// </summary>
        public CircularLoadingAnimation()
        {
            InitializeComponent();
            _viewModel = new CircularLoadingAnimationViewModel();
            DataContext = _viewModel;

            IsVisibleChanged += OnVisibleChanged;
            _animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
            {
                Interval = new TimeSpan(0, 0, 0, 0, 75)
            };
        }

        /// <summary>
        /// Sets the position.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="posOffSet">The pos off set.</param>
        /// <param name="step">The step to change.</param>
        private static void SetPosition(DependencyObject ellipse, double offset, double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50 + Math.Sin(offset + posOffSet * step) * 50);
            ellipse.SetValue(Canvas.TopProperty, 50 + Math.Cos(offset + posOffSet * step) * 50);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        private void Start()
        {
            _animationTimer.Tick += OnAnimationTick;
            _animationTimer.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        private void Stop()
        {
            _animationTimer.Stop();
            _animationTimer.Tick -= OnAnimationTick;
        }

        /// <summary>
        /// Handles the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        /// <summary>
        /// Handles the loaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCanvasLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(Circle0, offset, 0.0, step);
            SetPosition(Circle1, offset, 1.0, step);
            SetPosition(Circle2, offset, 2.0, step);
            SetPosition(Circle3, offset, 3.0, step);
            SetPosition(Circle4, offset, 4.0, step);
            SetPosition(Circle5, offset, 5.0, step);
            SetPosition(Circle6, offset, 6.0, step);
            SetPosition(Circle7, offset, 7.0, step);
            SetPosition(Circle8, offset, 8.0, step);
        }

        /// <summary>
        /// Handles the unloaded.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OnCanvasUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// Handles the visible changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }
    }
}