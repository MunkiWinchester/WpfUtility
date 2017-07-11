using System;
using NLog;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Sample.Properties;

namespace Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        Task _logTask;
        CancellationTokenSource _cancelLogTask;
        public int I;

        private ObservableCollection<LogEventInfo> _itemSource;

        public ObservableCollection<LogEventInfo> ItemSource
        {
            get => _itemSource;
            set
            {
                _itemSource = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            BtnItemSource_OnClick(null, null);
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var log = LogManager.GetLogger("button");

            var level = LogLevel.Trace;
            if (sender.Equals(BtnDebug)) level = LogLevel.Debug;
            if (sender.Equals(BtnWarning)) level = LogLevel.Warn;
            if (sender.Equals(BtnError)) level = LogLevel.Error;
            if (sender.Equals(BtnFatal)) level = LogLevel.Fatal;

            log.Log(level, TbLogText.Text);
            I++;
        }

        private void BackgroundSending_Checked(object sender, RoutedEventArgs e)
        {
            _cancelLogTask = new CancellationTokenSource();
            var token = _cancelLogTask.Token;
            _logTask = new Task(SendLogs, token, token);
            _logTask.Start();
        }

        private void BackgroundSending_Unchecked(object sender, RoutedEventArgs e)
        {
            _cancelLogTask?.Cancel();
        }

        private static void SendLogs(object obj)
        {
            var ct = (CancellationToken) obj;

            var counter = 0;
            var log = LogManager.GetLogger("task");

            log.Debug("Backgroundtask started.");

            while (!ct.WaitHandle.WaitOne(2000))
            {
                log.Trace($"Messageno {counter++} from backgroudtask.");
            }

            log.Debug("Backgroundtask stopped.");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnItemSource_OnClick(object sender, RoutedEventArgs e)
        {
            ItemSource
                = new ObservableCollection<LogEventInfo>
                {
                    new LogEventInfo(LogLevel.Trace, "Collection", $"LogfileSource Test {I}0"),
                    new LogEventInfo(LogLevel.Debug, "Collection", $"LogfileSource Test {I}1"),
                    new LogEventInfo(LogLevel.Warn, "Collection", $"LogfileSource Test  {I}"),
                    new LogEventInfo(LogLevel.Error, "Collection", $"LogfileSource Test {I}3"),
                    new LogEventInfo(LogLevel.Fatal, "Collection", $"LogfileSource Test {I}3"),
                    new LogEventInfo(LogLevel.Fatal, "Logger", "Lalalalala"),
                    new LogEventInfo(LogLevel.Error, "Logger", "Lalalalala"),
                    new LogEventInfo(LogLevel.Warn, "Logger", "Lalalalala"),
                    new LogEventInfo(LogLevel.Debug, "Logger", "Lalalalala"),
                    new LogEventInfo(LogLevel.Info, "Logger", "Lalalalala"),
                    new LogEventInfo(LogLevel.Trace, "Logger", "Lalalalala"),
                    new LogEventInfo(LogLevel.Fatal, "Logger", null,
                        "LalalalalaAufgrund vielfältiger Verkaufskanäle und übergreifender Sortimente ist die klassische Verteilung von Plan- und Ist-Mengen auf Vertriebswege und Werbeträger oft nicht mehr zeitgemäß.\r\nMithilfe der „Kollektionen“ kann ein zeitlich begrenztes Sortiment vertriebsweg- und kanalunabhängig geplant werden.\r\nAuf den Planungsebenen Warenbereich, Produktobergruppe und Markenkategorien können zurzeit folgende Zielwerte definiert werden: EK-Budget, Umsatz, Durchschn. VK-Preis, Anzahl Modelle (Ausprägungen)\r\nBasierend auf den Planwerten können die konkreten Ausprägungen geplant werden. Die Planwerte (Menge, VK-Preis, EK-Preis, berechnete Werte) werden aggregiert und mit den Zielwerten je Planungsebene verglichen.\r\nNach Abschluss der Planung können die Planmengen über einen Größenschlüssel auf die einzelnen Artikel verteilt und der Einkauf bei den Lieferanten ausgelöst werden.\r\n",
                        null,
                        new Exception("alles scheieg0eghweguibweguwiegbweuibgweuiwe weui gbweguwe bgwuegbi " +
                                      Environment.NewLine + "weubuiweg wbeug wbieugb weuibg weug "))
                };
        }
    }
}
