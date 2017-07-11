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


        public void ActivateLoggers()
        {
            SwitchLoggerEvents(true);
        }

        public void DeactivateLoggers()
        {
            SwitchLoggerEvents(false);
        }

        private void SwitchLoggerEvents(bool activate)
        {
            foreach (var target in GetLoggers())
            {
                if(activate)
                    target.LogReceived += LogReceived;
                else
                    target.LogReceived -= LogReceived;
            }
        }

        private static IEnumerable<NlogViewerTarget> GetLoggers()
        {
            return LogManager.Configuration.AllTargets.OfType<NlogViewerTarget>().ToList();
        }

        private void LogReceived(NLog.Common.AsyncLogEventInfo log)
        {
            var vm = new LogEvent(log.LogEvent);
            AddLog(vm);
        }

        private void AddLog(LogEvent entry)
        {
            if (Application.Current != null)
                Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        LogEntries.Add(entry);
                    });
        }
    }
}
