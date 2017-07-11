using System;
using NLog.Common;
using NLog.Targets;

namespace WpfUtility.LogViewer.Classes
{
    [Target(nameof(NlogViewer))]
    internal class NlogViewerTarget : Target
    {
        public event Action<AsyncLogEventInfo> LogReceived;

        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);
            LogReceived?.Invoke(logEvent);
        }
    }
}
