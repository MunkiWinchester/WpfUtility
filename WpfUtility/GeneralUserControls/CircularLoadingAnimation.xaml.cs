using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfUtility.GeneralUserControls
{
    /// <summary>
    /// Interaction logic for CircularLoadingAnimation.xaml
    /// </summary>
    public partial class CircularLoadingAnimation
    {
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(int), typeof(CircularLoadingAnimation), new UIPropertyMetadata(1));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(CircularLoadingAnimation), new UIPropertyMetadata(1));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(int), typeof(CircularLoadingAnimation), new UIPropertyMetadata(100));
        
        private readonly DispatcherTimer _animationTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularLoadingAnimation"/> class.
        /// </summary>
        public CircularLoadingAnimation()
        {
            InitializeComponent();

            IsVisibleChanged += OnVisibleChanged;

            _animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
            {
                Interval = new TimeSpan(0, 0, 0, 0, 75)
            };
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
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