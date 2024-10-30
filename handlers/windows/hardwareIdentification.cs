using System.Management;

namespace FRamework.handlers.windows
{
    internal class hardwareIdentification
    {
        public static string Wmi(string wmiClass, string wmiProperty)
        {
            var result = "";
            var mc = new ManagementClass(wmiClass);
            var moc = mc.GetInstances();

            foreach (var o in moc)
            {
                var mo = (ManagementObject)o;

                if (result != "")
                {
                    continue;
                }

                try
                {
                    result = mo[wmiProperty].ToString();

                    break;
                }
                catch
                {
                    // ignored
                }
            }

            return result;
        }

        private static string formatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return System.String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        public static string biosId()
        {
            return Wmi("Win32_BIOS", "ReleaseDate") + Wmi("Win32_BIOS", "SMBIOSBIOSVersion");
        }

        public static string cpuId()
        {
            string result = Wmi("Win32_Processor", "UniqueId");
            if (result == "")
            {
                result = Wmi("Win32_Processor", "ProcessorId");
                if (result == "")
                {
                    result = Wmi("Win32_Processor", "Name");
                    if (result == "")
                    {
                        result = Wmi("Win32_Processor", "Manufacturer");
                    }
                    result += Wmi("Win32_Processor", "MaxClockSpeed");
                }
            }
            return result;
        }

        public static string diskId()
        {
            return Wmi("Win32_DiskDrive", "Model");
        }

        public static string videoId()
        {
            return Wmi("Win32_VideoController", "Name");
        }

        public static string getHwid()
        {
            return $"{cpuId()} - {diskId()} - {biosId()} - {videoId()}";
        }
    }
}
