using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using NLog;

namespace WpfUtility.LogViewer.Classes
{
    internal class NlogViewerViewModel : ObservableObject
    {
        private ObservableCollection<LogEvent> _logEntries;
        public ObservableCollection<LogEvent> LogEntries
        {
            get => _logEntries;
            set => SetField(ref _logEntries, value);
        }

        private LogEvent _selectedLogEntry;
        public LogEvent SelectedLogEntry
        {
            get => _selectedLogEntry;
            set => SetField(ref _selectedLogEntry, value);
        }

        public NlogViewerViewModel()
        {
            LogEntries = new ObservableCollection<LogEvent>();
        }

        public void ActivateLoggers()
        {
            // TODO: Make this chooseable
            foreach (var target in GetLoggers())
            {
                target.LogReceived += LogReceived;
            }
        }

        private static IEnumerable<NlogViewerTarget> GetLoggers()
        {
            return LogManager.Configuration.AllTargets.OfType<NlogViewerTarget>().ToList();
        }

        private void LogReceived(NLog.Common.AsyncLogEventInfo log)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                LogEntries.Add(new LogEvent(log.LogEvent));
            }));
        }
    }
}
