using System.Collections.Specialized;
using System.Net;
using DiscordRPC;

namespace FRamework.handlers.web
{
    public class discord
    {
        /// <summary>
        /// You don't have to use this, use the pre-written functions
        /// </summary>
        public static DiscordRpcClient client;

        /// <summary>
        /// This is the first ID in your Discord application, not the Public Key.
        /// </summary>
        public static string applicationID = "";

        /// <param name="url">url of the webhook</param>
        /// <param name="username">username of the webhook</param>
        /// <param name="content">content you want to send</param>
        public static void sendWebhook(string url, string username, string content)
        {
            WebClient wc = new WebClient();
            wc.UploadValues(url, new NameValueCollection
            {
                {
                    "content", content
                },
                {
                    "username", username
                }
             });
        }

        /// <summary>
        /// Use the updatePresence function after starting the presence
        /// </summary>
        public static void startPresence()
        {
            client = new DiscordRpcClient(applicationID);
            client.Initialize();
        }

        /// <summary>
        /// used after startPresence
        /// </summary>
        /// <param name="details">Top part of presence</param>
        /// <param name="state">Bottom part of presence</param>
        /// <param name="largeImage">Upload your image to Rich Presence Art Assets on your application</param>
        /// <param name="imageName">Set to the name of your application</param>
        public static void updatePresence(string details, string state, string largeImage, string imageName)
        {
            client.SetPresence(new RichPresence()
            {
                Details = details,
                State = state,
                Assets = new Assets()
                {
                    LargeImageKey = largeImage,
                    LargeImageText = imageName
                }
            });
        }

        public static void destroyPresence()
        {
            client.Dispose();
        }

        /// <summary>
        /// Checks if the Client is disposed
        /// </summary>
        public static bool isDestroyed()
        {
            try
            {
                if (client.IsDisposed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                // if the client isnt started at all, it will throw an exception, so return true since its technically destroyed //
                return true;
            }
        }
    }
}