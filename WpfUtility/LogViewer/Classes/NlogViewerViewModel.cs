﻿using System;
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

        public void ToggleLoggers(bool activate)
        {
            foreach (var target in GetLoggers())
            {
                if(activate)
                    target.LogReceived += LogReceived;
                else
                    target.LogReceived -= LogReceived;
            }
        }

        public IEnumerable<NlogViewerTarget> GetLoggers()
        {
            return LogManager.Configuration.AllTargets.OfType<NlogViewerTarget>().ToList();
        }

        private void LogReceived(NLog.Common.AsyncLogEventInfo log)
        {
            if (Application.Current?.Dispatcher != null)
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    LogEntries.Add(new LogEvent(log.LogEvent));
                }));
        }
    }
}
