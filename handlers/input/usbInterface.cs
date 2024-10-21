using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace FRamework.handlers.input
{
    internal class usbInterface
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr SecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile
       );

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped
        );

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            byte[] lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);


        public static void getDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Removable)
                {
                    variables.drives.Add(variables.drives[0]);
                }
            }
        }

        public static bool isOnDrive(string file)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Removable && file == d.RootDirectory.ToString())
                {
                    variables.path = d.RootDirectory;
                    variables.drivename = d.Name;
                    return true;
                }
            }
            return false;
        }

        public IntPtr USBEject(string driveLetter)
        {
            string filename = @"\\.\" + driveLetter[0] + ":";
            return CreateFile(filename, variables.usbTypes.read | variables.usbTypes.write, variables.usbTypes.shareRead | variables.usbTypes.shareWrite, IntPtr.Zero, 0x3, 0, IntPtr.Zero);
        }

        public static bool Eject(IntPtr handle)
        {
            bool result = false;

            if (helpers.lockVolume(handle) && helpers.dismountVolume(handle))
            {
                helpers.preventRemoval(handle, false);
                result = helpers.autoEject(handle);
            }
            CloseHandle(handle);
            return result;
        }

        public static class helpers
        {
            public static bool lockVolume(IntPtr handle)
            {
                uint byteReturned;

                for (int i = 0; i < 10; i++)
                {
                    if (DeviceIoControl(handle, variables.usbTypes.lockDrive, IntPtr.Zero, 0, IntPtr.Zero, 0, out byteReturned, IntPtr.Zero))
                    {
                        return true;
                    }
                    Thread.Sleep(500);
                }
                return false;
            }

            public static bool dismountVolume(IntPtr handle)
            {
                uint byteReturned;
                return DeviceIoControl(handle, variables.usbTypes.dismountDrive, IntPtr.Zero, 0, IntPtr.Zero, 0, out byteReturned, IntPtr.Zero);
            }

            public static bool autoEject(IntPtr handle)
            {
                uint byteReturned;
                return DeviceIoControl(handle, variables.usbTypes.ejectDrive, IntPtr.Zero, 0, IntPtr.Zero, 0, out byteReturned, IntPtr.Zero);
            }

            public static bool preventRemoval(IntPtr handle, bool prevent)
            {
                byte[] buf = new byte[1];
                uint retVal;

                buf[0] = (prevent) ? (byte)1 : (byte)0;
                return DeviceIoControl(handle, variables.usbTypes.removeDrive, buf, 1, IntPtr.Zero, 0, out retVal, IntPtr.Zero);
            }
        }

        public static class variables
        {
            public static dynamic path;
            public static dynamic drivename;
            public static List<string> drives = new List<string> { };

            public static class usbTypes
            {
                public static dynamic read = 0x80000000;
                public static dynamic write = 0x40000000;
                public static dynamic shareRead = 0x1;
                public static dynamic shareWrite = 0x2;
                public static dynamic lockDrive = 0x00090018;
                public static dynamic dismountDrive = 0x00090020;
                public static dynamic ejectDrive = 0x2D4808;
                public static dynamic removeDrive = 0x002D4804;
            }
        }
    }
}
