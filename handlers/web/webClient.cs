using System;
using System.Collections.Specialized;
using System.Net;

namespace FRamework.handlers.web
{
    public class webClient
    {
        public static WebClient client = new WebClient();

        /// <param name="link">Direct link to download</param>
        /// <param name="path">Where to save the file to</param>
        ///  <param name="fileName">Name to save the file as</param>
        /// <param name="extensionType">Type of file</param>
        public static void downloadFile(string link, string path, string fileName, string extensionType)
        {
            client.DownloadFileAsync(new Uri(link), path + $"/{fileName}.{extensionType}");
        }

        /// <param name="link">Link  to page to scrape</param>
        public static string readString(string link)
        {
            return client.DownloadString(link);
        }
    }
}
