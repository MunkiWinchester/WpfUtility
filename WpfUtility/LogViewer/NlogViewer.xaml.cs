using System;
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

        public bool ActivateLoggers
        {
            get => (bool) GetValue(ActivateLoggersProperty);
            set => SetValue(ActivateLoggersProperty, value);
        }

        public static readonly DependencyProperty ActivateLoggersProperty =
            DependencyProperty.Register(nameof(ActivateLoggers), typeof(bool),
                typeof(NlogViewer), new FrameworkPropertyMetadata(true, PropertyChangedCallbackActivate));

        private static void PropertyChangedCallbackActivate(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var nlogViewer = dependencyObject as NlogViewer;
            if (nlogViewer != null)
            {
                var activate = dependencyPropertyChangedEventArgs.NewValue != null &&
                               (bool) dependencyPropertyChangedEventArgs.NewValue;
                nlogViewer.ViewModel.ToggleLoggers(activate);
            }
        }

        public bool UseApplicationDispatcher
        {
            get => (bool) GetValue(UseApplicationDispatcherProperty);
            set => SetValue(UseApplicationDispatcherProperty, value);
        }

        public static readonly DependencyProperty UseApplicationDispatcherProperty =
            DependencyProperty.Register(nameof(UseApplicationDispatcher), typeof(bool),
                typeof(NlogViewer),
                new FrameworkPropertyMetadata(true, UseApplicationDispatcherPropertyChangedCallbackActivate));

        private static void UseApplicationDispatcherPropertyChangedCallbackActivate(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var nlogViewer = dependencyObject as NlogViewer;
            if (nlogViewer != null)
            {
                _this.ChooseDispatcherAndToggleLoggers();
            }
        }

        public NlogViewer()
        {
            ViewModel = new NlogViewerViewModel();
            InitializeComponent();
            DataContext = ViewModel;
            _this = this;
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                ChooseDispatcherAndToggleLoggers();
            }
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

        private void ChooseDispatcherAndToggleLoggers()
        {
                if (_this.UseApplicationDispatcher)
                    ViewModel.ToggleLoggers(_this.ActivateLoggers);
                else
                    ToggleLoggers(_this.ActivateLoggers);
        }

        private void ToggleLoggers(bool activate)
        {
            foreach (var target in ViewModel.GetLoggers())
            {
                if (activate)
                    target.LogReceived += LogReceived;
                else
                    target.LogReceived -= LogReceived;
            }
        }

        private void LogReceived(NLog.Common.AsyncLogEventInfo log)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ViewModel.LogEntries.Add(new LogEvent(log.LogEvent));
            }));
        }
    }
}
