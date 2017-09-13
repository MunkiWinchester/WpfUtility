using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using NLog;
using NLog.Common;
using WpfUtility.Services;

namespace WpfUtility.LogViewer.Classes
{
    /// <summary>
    ///     This class is the view model of the NLogViewer
    /// </summary>
    internal class NlogViewerViewModel : ObservableObject
    {
        /// <summary>
        ///     Private property for LogEntries
        /// </summary>
        private ObservableCollection<LogEvent> _logEntries;

        /// <summary>
        ///     Private property for SelectedLogEntry
        /// </summary>
        private LogEvent _selectedLogEntry;

        /// <summary>
        ///     Constructor for the NlogViewerViewModel
        /// </summary>
        public NlogViewerViewModel()
        {
            LogEntries = new ObservableCollection<LogEvent>();
        }

        /// <summary>
        ///     Property for the log entries
        /// </summary>
        public ObservableCollection<LogEvent> LogEntries
        {
            get => _logEntries;
            set => SetField(ref _logEntries, value);
        }

        /// <summary>
        ///     Property for the selected log entry
        /// </summary>
        public LogEvent SelectedLogEntry
        {
            get => _selectedLogEntry;
            set => SetField(ref _selectedLogEntry, value);
        }

        /// <summary>
        ///     Toggles the tracking of the nlog loggers on/off
        /// </summary>
        /// <param name="activate">Should the loggers be tracked?</param>
        public void ToggleLoggers(bool activate)
        {
            foreach (var target in GetLoggers())
                if (activate)
                    target.LogReceived += LogReceived;
                else
                    target.LogReceived -= LogReceived;
        }

        /// <summary>
        ///     Loads all loggers of Nlog
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NlogViewerTarget> GetLoggers()
        {
            return LogManager.Configuration.AllTargets.OfType<NlogViewerTarget>().ToList();
        }

        /// <summary>
        ///     Event which is triggered when a log is received
        /// </summary>
        /// <param name="log">Log entry which was received</param>
        private void LogReceived(AsyncLogEventInfo log)
        {
            if (Application.Current?.Dispatcher != null)
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    LogEntries.Add(new LogEvent(log.LogEvent));
                }));
        }
    }
}