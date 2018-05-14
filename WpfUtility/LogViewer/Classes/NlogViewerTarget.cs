using System;
using NLog.Common;
using NLog.Targets;

namespace WpfUtility.LogViewer.Classes
{
    /// <summary>
    /// Class to help with the receiving of log entries
    /// </summary>
    [Target(nameof(NlogViewer))]
    internal class NlogViewerTarget : Target
    {
        /// <summary>
        /// Event which is triggered when a new log entry was written
        /// </summary>
        public event Action<AsyncLogEventInfo> LogReceived;

        /// <summary>
        /// Invokes the LogReceived event
        /// </summary>
        /// <param name="logEvent"></param>
        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);
            LogReceived?.Invoke(logEvent);
        }
    }
}