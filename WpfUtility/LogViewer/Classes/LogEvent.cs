using System;
using System.Globalization;
using System.Text;
using NLog;

namespace WpfUtility.LogViewer.Classes
{
    /// <summary>
    ///     Class for the LogEvent
    /// </summary>
    internal class LogEvent
    {
        private const int MaxLength = 497;

        /// <summary>
        ///     Constructor for the LogEvent
        /// </summary>
        /// <param name="logEventInfo">LogEventInfo which is transformed to the LogEvent</param>
        public LogEvent(LogEventInfo logEventInfo)
        {
            Level = logEventInfo.Level.ToString();
            FormattedMessageToolTip = logEventInfo.FormattedMessage.Substring(0,
                          logEventInfo.FormattedMessage.Length > MaxLength
                              ? MaxLength
                              : logEventInfo.FormattedMessage.Length) +
                      (logEventInfo.FormattedMessage.Length > MaxLength ? "..." : "");
            FormattedMessage = logEventInfo.FormattedMessage;
            if (logEventInfo.Exception != null)
                ExceptionToolTip = logEventInfo.Exception.ToString().Substring(0,
                                       logEventInfo.Exception.ToString().Length > MaxLength
                                           ? MaxLength
                                           : logEventInfo.Exception.ToString().Length) +
                                   (logEventInfo.Exception.ToString().Length > MaxLength ? "..." : "");
            Exception = logEventInfo.Exception;
            LoggerName = logEventInfo.LoggerName;
            Time = logEventInfo.TimeStamp;
        }

        public DateTime Time { get; }
        public string LoggerName { get; }
        public string Level { get; }
        public string FormattedMessage { get; }
        public Exception Exception { get; }
        public string FormattedMessageToolTip { get; }
        public string ExceptionToolTip { get; }

        /// <summary>
        ///     Converts the LogEventInfo to a readable string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder($"{nameof(Time)}:");
            sb.AppendLine();
            sb.AppendLine(Time.ToString(CultureInfo.CurrentCulture));
            sb.AppendLine();
            sb.AppendLine($"{nameof(Level)}:");
            sb.AppendLine(Level);
            sb.AppendLine();
            sb.AppendLine($"{nameof(FormattedMessage)}:");
            sb.AppendLine(FormattedMessage);
            sb.AppendLine();
            sb.AppendLine($"{nameof(Exception)}:");
            sb.AppendLine(Exception?.ToString());
            return sb.ToString();
        }
    }
}