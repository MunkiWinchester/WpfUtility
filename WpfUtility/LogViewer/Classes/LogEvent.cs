using System;
using System.Windows.Media;
using NLog;

namespace WpfUtility.LogViewer.Classes
{
    internal class LogEvent
    {
        private const int MaxLength = 497;

        public LogEvent(LogEventInfo logEventInfo)
        {
            ToolTip = logEventInfo.FormattedMessage.Substring(0,
                          logEventInfo.FormattedMessage.Length > MaxLength
                              ? MaxLength
                              : logEventInfo.FormattedMessage.Length) +
                      (logEventInfo.FormattedMessage.Length > MaxLength ? "..." : "");
            Level = logEventInfo.Level.ToString();
            FormattedMessage = logEventInfo.FormattedMessage;
            Exception = logEventInfo.Exception;
            LoggerName = logEventInfo.LoggerName;
            Time = logEventInfo.TimeStamp;

            SetupColors(logEventInfo);
        }

        public DateTime Time { get; }
        public string LoggerName { get; }
        public string Level { get; }
        public string FormattedMessage { get; }
        public Exception Exception { get; }
        public string ToolTip { get; }
        public SolidColorBrush Background { get; private set; }
        public SolidColorBrush Foreground { get; private set; }
        public SolidColorBrush BackgroundMouseOver { get; private set; }
        public SolidColorBrush ForegroundMouseOver { get; private set; }

        public override string ToString()
        {
            return Time.ToString("dd.MM.yyyy HH:mm:ss") + "\t" + LoggerName + "\t" + Level + "\t" + FormattedMessage +
                   "\t" + Exception;
        }

        private void SetupColors(LogEventInfo logEventInfo)
        {
            if (logEventInfo.Level == LogLevel.Warn)
            {
                Background = Brushes.Yellow;
                BackgroundMouseOver = Brushes.GreenYellow;
            }
            else if (logEventInfo.Level == LogLevel.Error)
            {
                Background = Brushes.Tomato;
                BackgroundMouseOver = Brushes.IndianRed;
            }
            else if (logEventInfo.Level == LogLevel.Fatal)
            {
                Background = Brushes.Red;
                BackgroundMouseOver = Brushes.OrangeRed;
            }
            else
            {
                Background = Brushes.White;
                BackgroundMouseOver = Brushes.LightGray;
            }
            Foreground = Brushes.Black;
            ForegroundMouseOver = Brushes.Black;
        }
    }
}