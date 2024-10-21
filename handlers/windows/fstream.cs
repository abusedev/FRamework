using System;
using System.IO;

namespace FRamework.handlers.windows
{
    public class fstream
    {
        public static TextWriter writer;

        /// <param name="path">Path to folder</param>
        public static bool folderExists(string path)
        {

            bool exists = Directory.Exists(path);

            if (exists)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <param name="path">Path to file</param>
        public static bool fileExists(string path)
        {
            bool exists = File.Exists(path);

            if (exists)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <param name="path">Path to file</param>
        public static void deleteFile(string path)
        {
            if (fileExists(path))
            {
                File.Delete(path);
            }
        }

        /// <param name="path">Path to folder</param>
        public static void createFolder(string path)
        {
            if (!folderExists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <param name="path">Path to folder</param>
        public static void deleteFolder(string path)
        {
            if (folderExists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// Checks if the executable is packaged with winrar or zipped
        /// </summary>
        public static bool isPackaged()
        {
            if (Path.GetFileName(Path.GetDirectoryName(Environment.CurrentDirectory)) == "Temp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <param name="appname">Name of your app</param>
        /// <param name="folder">Path where you want to save a log</param>
        /// <param name="appversion">Version of your app</param>
        /// <param name="exception">Exception message</param>
        /// <param name="function">Name of function that threw an error</param>
        public static void writeLog(dynamic appname, string folder, dynamic appversion, dynamic exception, string function)
        {
            createFolder(folder);

            writer = File.CreateText(folder + @"\log.txt");
            writer.WriteLine($"# {appname} log");
            writer.WriteLine($"# Caught exception at: {DateTime.Now}");
            writer.WriteLine($"# {appname} version: {appversion}");
            writer.WriteLine();
            writer.WriteLine($"# Function: {function}");
            writer.WriteLine($"# Exception: {exception}");
            writer.Flush();
            writer.Close();
        }
    }
}
