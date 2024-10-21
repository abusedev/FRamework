using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FRamework.handlers.windows
{
    internal class processManager
    {
        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Checks if a process exe name is running
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool isRunning(string processName)
        {
            Process[] proc = Process.GetProcessesByName(processName);
            if (proc.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if a process is the current foreground window
        /// </summary>
        public static bool isWindowActive(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                if (GetForegroundWindow() == FindWindow(null, process.MainWindowTitle))
                {
                    return true;
                }
            }
            return false;
        }

        public static void killProcess(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
        }

        public static void restartProcess(string processName)
        {
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == processName)
                {
                    exe.Kill();
                }
            }
            Process.Start($"{processName}.exe");
        }
    }
}
