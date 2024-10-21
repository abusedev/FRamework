using Microsoft.Win32;

namespace FRamework.handlers.windows
{
    public class windowsVersion
    {
        /// <summary>
        /// Checks Windows Registry to get the CurrentBuild value
        /// </summary>
        public static bool isWin11()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            var currentBuildStr = (string)reg.GetValue("CurrentBuild");
            var currentBuild = int.Parse(currentBuildStr);

            if (currentBuild >= 22000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
