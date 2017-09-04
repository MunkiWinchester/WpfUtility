using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NLog;
using NLog.Common;
using WpfUtility.LogViewer.Classes;

namespace WpfUtility.LogViewer
{
    /// <summary>
    ///     Interaction logic for NlogViewer.xaml
    /// </summary>
    public partial class NlogViewer
    {
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(nameof(ItemSource), typeof(ObservableCollection<LogEventInfo>),
                typeof(NlogViewer),
                new FrameworkPropertyMetadata(new ObservableCollection<LogEventInfo>(), PropertyChangedCallback));

        public static readonly DependencyProperty ActivateLoggersProperty =
            DependencyProperty.Register(nameof(ActivateLoggers), typeof(bool),
                typeof(NlogViewer), new FrameworkPropertyMetadata(true, PropertyChangedCallbackActivate));

        public static readonly DependencyProperty UseApplicationDispatcherProperty =
            DependencyProperty.Register(nameof(UseApplicationDispatcher), typeof(bool),
                typeof(NlogViewer),
                new FrameworkPropertyMetadata(true, UseApplicationDispatcherPropertyChangedCallbackActivate));

        /// <summary>
        ///     Variable with itself in it, to use it in static content
        /// </summary>
        private static NlogViewer _this;

        /// <summary>
        ///     Constructor for the NlogViewer
        /// </summary>
        public NlogViewer()
        {
            ViewModel = new NlogViewerViewModel();
            InitializeComponent();
            DataContext = ViewModel;
            _this = this;
            if (!DesignerProperties.GetIsInDesignMode(this))
                ChooseDispatcherAndToggleLoggers();
        }

        /// <summary>
        ///     Gets or sets the value of the item source.
        /// </summary>
        public ObservableCollection<LogEventInfo> ItemSource
        {
            get => (ObservableCollection<LogEventInfo>)GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value if the loggers should be active
        /// </summary>
        public bool ActivateLoggers
        {
            get
            {
                var value = GetValue(ActivateLoggersProperty);
                return value != null && (bool) value;
            }
            set => SetValue(ActivateLoggersProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value if the application dispatcher should be used or the threading dispatcher
        /// </summary>
        public bool UseApplicationDispatcher
        {
            get
            {
                var value = GetValue(UseApplicationDispatcherProperty);
                return value != null && (bool) value;
            }
            set => SetValue(UseApplicationDispatcherProperty, value);
        }

        /// <summary>
        ///     Gets or sets the view model of the nlog viewer
        /// </summary>
        internal NlogViewerViewModel ViewModel { get; set; }

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

        /// <summary>
        ///     Method which is invoked trough the dependency
        /// </summary>
        /// <param name="dependencyObject">This contains the NlogViewer ("this")</param>
        /// <param name="dependencyPropertyChangedEventArgs">This contains the changed event arguments such as the new value</param>
        private static void PropertyChangedCallbackActivate(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var nlogViewer = dependencyObject as NlogViewer;
            if (nlogViewer != null)
            {
                var activate = dependencyPropertyChangedEventArgs.NewValue != null &&
                               (bool)dependencyPropertyChangedEventArgs.NewValue;
                nlogViewer.ViewModel.ToggleLoggers(activate);
            }
        }

        /// <summary>
        ///     Method which is invoked trough the dependency
        /// </summary>
        /// <param name="dependencyObject">This contains the NlogViewer ("this")</param>
        /// <param name="dependencyPropertyChangedEventArgs">This contains the changed event arguments such as the new value</param>
        private static void UseApplicationDispatcherPropertyChangedCallbackActivate(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var nlogViewer = dependencyObject as NlogViewer;
            if (nlogViewer != null)
                _this.ChooseDispatcherAndToggleLoggers();
        }

        /// <summary>
        ///     Method to set the datagrid to the top entry
        /// </summary>
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

        /// <summary>
        ///     Method to choose which dispatcher is used and toogles the loggers
        /// </summary>
        private void ChooseDispatcherAndToggleLoggers()
        {
            // Activate the loggers of the ApplicationDispatcher
            // Deactivate the other loggers (if running)
            if (_this.UseApplicationDispatcher)
            {
                ViewModel.ToggleLoggers(_this.ActivateLoggers);
                ToggleLoggers(!_this.ActivateLoggers);
            }
            else
            {
                ViewModel.ToggleLoggers(!_this.ActivateLoggers);
                ToggleLoggers(_this.ActivateLoggers);
            }
        }

        /// <summary>
        ///     Method to toggle the loggers on or off
        /// </summary>
        /// <param name="activate">Bool indicating if the loggers should be active</param>
        private void ToggleLoggers(bool activate)
        {
            foreach (var target in ViewModel.GetLoggers())
                if (activate)
                    target.LogReceived += LogReceived;
                else
                    target.LogReceived -= LogReceived;
        }

        /// <summary>
        ///     Method which is called when a ne wlog entry is written
        /// </summary>
        /// <param name="log">Log entry which will be added to the display</param>
        private void LogReceived(AsyncLogEventInfo log)
        {
            Dispatcher.BeginInvoke(new Action(() => { ViewModel.LogEntries.Add(new LogEvent(log.LogEvent)); }));
        }
    }
}