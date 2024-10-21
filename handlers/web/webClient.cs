using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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

        /// <param name="address"></param>
        /// <returns>Will return with this structure: {ping} MS if address is valid, if not valid: ping N/A</returns>
        public static async Task<dynamic> getPing(string address)
        {
            using (var ping = new Ping())
            {
                Ping _ping = new Ping();
                PingReply reply = _ping.Send(address);
                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime + " MS";
                }
                else
                {
                    return "ping N/A";
                }
            }
        }
    }
}
