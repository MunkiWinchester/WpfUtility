using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NLog;
using WpfUtility.LogViewer.Classes;

namespace WpfUtility.LogViewer
{
    /// <summary>
    /// Interaction logic for NlogViewer.xaml
    /// </summary>
    public partial class NlogViewer
    {
        public ObservableCollection<LogEventInfo> ItemSource
        {
            get => (ObservableCollection<LogEventInfo>) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(nameof(ItemSource), typeof(ObservableCollection<LogEventInfo>),
                typeof(NlogViewer),
                new FrameworkPropertyMetadata(new ObservableCollection<LogEventInfo>(), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var nlogViewer = dependencyObject as NlogViewer;
            if (nlogViewer != null)
            {
                var list = dependencyPropertyChangedEventArgs.NewValue as ObservableCollection<LogEventInfo>;
                if (list != null)
                {
                    nlogViewer.ViewModel.LogEntries =
                        new ObservableCollection<LogEvent>(list.Select(x => new LogEvent(x)));
                    _this.ScrollToTop();
                }
            }
        }

        public NlogViewer()
        {
            ViewModel = new NlogViewerViewModel();
            InitializeComponent();
            DataContext = ViewModel;
            _this = this;
        }

        private static NlogViewer _this;
        internal NlogViewerViewModel ViewModel { get; set; }

        private void ScrollToTop()
        {
            if (VisualTreeHelper.GetChildrenCount(DataGrid) > 0)
            {
                var border = VisualTreeHelper.GetChild(DataGrid, 0) as Decorator;
                if (border != null)
                {
                    var scrollViewer = border.Child as ScrollViewer;
                    scrollViewer?.ScrollToTop();
                }
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Use DataGrid_Loaded because UserControl_Loaded bugs..
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var window = Window.GetWindow(this);
                if (window != null)
                    window.Closing += WindowOnClosing;
                ViewModel.ActivateLoggers();
            }
        }

        private void WindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            ViewModel.DeactivateLoggers();
        }
    }
}
