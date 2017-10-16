using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using NLog;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class LogViewerViewModel : ObservableObject
    {
        private bool _backgroundSending;
        private CancellationTokenSource _cancelLogTask;

        private ObservableCollection<LogEventInfo> _itemSource;
        private Task _logTask;

        private string _text = "This is a total new log file entry. You can change the Text.";
        public int I;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public bool BackgroundSending
        {
            get => _backgroundSending;
            set
            {
                _backgroundSending = value;
                SendLogsFromBackground();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LogEventInfo> ItemSource
        {
            get => _itemSource;
            set => SetField(ref _itemSource, value);
        }

        public ICommand SendLogFromSourceCommand => new DelegateCommand(SendLogFromSource);

        public RelayCommand<string> SendLogCommand => new RelayCommand<string>(SendLog);

        private void SendLog(string logLevel)
        {
            var log = LogManager.GetLogger("button");
            var level = LogLevel.FromString(logLevel);
            if (level == LogLevel.Info || level == LogLevel.Trace || level == LogLevel.Debug)
                log.Log(level, Text);
            else
                log.Log(level, new Exception(Text), Text);
        }

        private void SendLogFromSource()
        {
            ItemSource
                = new ObservableCollection<LogEventInfo>
                {
                    new LogEventInfo(LogLevel.Trace, "ItemSource", "LogfileSource Test"),
                    new LogEventInfo(LogLevel.Debug, "ItemSource", "LogfileSource Test 1"),
                    new LogEventInfo(LogLevel.Warn, "ItemSource", "LogfileSource Test 2"),
                    new LogEventInfo(LogLevel.Error, "ItemSource", "LogfileSource Test 3"),
                    new LogEventInfo(LogLevel.Fatal, "ItemSource", "LogfileSource Test 4"),
                    new LogEventInfo(LogLevel.Fatal, "ItemSource", "LogfileSource Test 5"),
                    new LogEventInfo(LogLevel.Error, "ItemSource", "LogfileSource Test 6"),
                    new LogEventInfo(LogLevel.Warn, "ItemSource", "LogfileSource Test 7"),
                    new LogEventInfo(LogLevel.Debug, "ItemSource", "LogfileSource Test 8"),
                    new LogEventInfo(LogLevel.Info, "ItemSource", "LogfileSource Test 9"),
                    new LogEventInfo(LogLevel.Trace, "ItemSource", "LogfileSource Test 10"),
                    new LogEventInfo(LogLevel.Fatal, "ItemSource", null,
                        "LogfileSource Test 11\r\nWith line breaks to simulate stuff.\r\nMaybe it crashed?",
                        null,
                        new RankException(
                            "We lost a rank!\r\nSo a rank exception occured!\r\nDon't lose ranks and you will be fine!"))
                };
        }

        private void SendLogsFromBackground()
        {
            if (BackgroundSending)
            {
                _cancelLogTask = new CancellationTokenSource();
                var token = _cancelLogTask.Token;
                _logTask = new Task(SendLogs, token, token);
                _logTask.Start();
            }
            else
            {
                _cancelLogTask?.Cancel();
            }
        }

        private static void SendLogs(object obj)
        {
            var ct = (CancellationToken) obj;

            var counter = 0;
            var log = LogManager.GetLogger("task");

            log.Debug("Backgroundtask started.");

            while (!ct.WaitHandle.WaitOne(2000))
                if (counter % 5 == 0)
                    log.Error(new IndexOutOfRangeException($"{counter} % 5 resulted in 0!"),
                        $"Messageno {counter++} from backgroundtask caused an error");
                else
                    log.Trace($"Messageno {counter++} from backgroundtask.");

            log.Debug("Backgroundtask stopped.");
        }
    }
}