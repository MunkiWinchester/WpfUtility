using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfUtility.GeneralUserControls
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for LoadingPanel.xaml
    /// </summary>
    public partial class LoadingPanel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(LoadingPanel),
                new UIPropertyMetadata(false));

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register(nameof(ForegroundColor), typeof(SolidColorBrush), typeof(LoadingPanel),
                new UIPropertyMetadata(new SolidColorBrush(Colors.Red)));

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        public static readonly DependencyProperty MessageForegroundColorProperty =
            DependencyProperty.Register(nameof(MessageForegroundColor), typeof(SolidColorBrush), typeof(LoadingPanel),
                new UIPropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        public static readonly DependencyProperty SubMessageForegroundColorProperty =
            DependencyProperty.Register(nameof(SubMessageForegroundColor), typeof(SolidColorBrush),
                typeof(LoadingPanel),
                new UIPropertyMetadata(new SolidColorBrush(Colors.DimGray)));

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(LoadingPanel),
                new UIPropertyMetadata("Loading..."));

        /// <summary>
        /// Gets or sets the sub message.
        /// </summary>
        public static readonly DependencyProperty SubMessageProperty =
            DependencyProperty.Register(nameof(SubMessage), typeof(string), typeof(LoadingPanel),
                new UIPropertyMetadata(string.Empty));

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WpfUtility.GeneralUserControls.LoadingPanel" /> class.
        /// </summary>
        public LoadingPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoading
        {
            get => (bool) GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        /// <value> The (solid color brush) color. </value>
        public SolidColorBrush ForegroundColor
        {
            get => (SolidColorBrush) GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        /// <value> The (solid color brush) color. </value>
        public SolidColorBrush MessageForegroundColor
        {
            get => (SolidColorBrush) GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the circular loading animation.
        /// </summary>
        /// <value> The (solid color brush) color. </value>
        public SolidColorBrush SubMessageForegroundColor
        {
            get => (SolidColorBrush) GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// Gets or sets the sub message.
        /// </summary>
        /// <value>The sub message.</value>
        public string SubMessage
        {
            get => (string) GetValue(SubMessageProperty);
            set => SetValue(SubMessageProperty, value);
        }
    }
}