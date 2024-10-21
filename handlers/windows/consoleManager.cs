using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FRamework.handlers.windows
{
    public class consoleManager
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        public static void openConsole(string title, int width, int height)
        {
            AllocConsole();
            Console.Title = title;
            Console.CursorVisible = false;
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = Encoding.GetEncoding(MY_CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.BufferWidth = Console.WindowWidth = width;
            Console.BufferHeight = Console.WindowHeight = height;
            MoveWindowToCenter();
        }

        public static void clearConsole()
        {
            Console.Clear();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;

        private static Size GetScreenSize() => new Size(GetSystemMetrics(0), GetSystemMetrics(1));

        private struct Size
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public Size(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }

        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(HandleRef hWnd, out Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;        //x pos of upper left corner
            public int Top;         //y pos of upper left corner
            public int Right;       //x pos of lower right corner
            public int Bottom;      //y pos of lower right corner
        }

        private static Size GetWindowSize(IntPtr window)
        {
            if (!GetWindowRect(new HandleRef(null, window), out Rect rect))
            {
                throw new Exception("Unable to get window rect");
            }

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            return new Size(width, height);
        }

        public static void MoveWindowToCenter()
        {
            IntPtr window = Process.GetCurrentProcess().MainWindowHandle;

            if (window == IntPtr.Zero)
            {
                throw new Exception("Couldn't find a window to center");
            }

            Size screenSize = GetScreenSize();
            Size windowSize = GetWindowSize(window);

            int x = (screenSize.Width - windowSize.Width) / 2;
            int y = (screenSize.Height - windowSize.Height) / 2;

            SetWindowPos(window, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
        }

        /// <summary>
        /// Put the text in the middle of the console
        /// </summary>
        public static void centerText(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        /// <summary>
        /// Colored logging, some functions take in a position as an int, 1 = colored first, 2 = colored last
        /// </summary>
        public class prettyLog
        {
            public static void normalLog(string message)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("LOG: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(message);
                Console.Write("\n");
            }

            public static void redLog(int position, string first, string rest)
            {
                if (position == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("LOG: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(first);
                    Console.Write($" {rest}");
                    Console.Write("\n");
                }
                if (position == 2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("LOG: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(first);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($" {rest}");
                    Console.Write("\n");
                }
            }

            public static void greenLog(int position, string first, string rest)
            {
                if (position == 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("LOG: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(first);
                    Console.Write($" {rest}");
                    Console.Write("\n");
                }
                if (position == 2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("LOG: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(first);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" {rest}");
                    Console.Write("\n");
                }
            }
        }
    }
}