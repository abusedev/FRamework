using FRamework.handlers.windows;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WootingAnalogSDKNET;

namespace FRamework.handlers.input
{
    internal class wootingInterface
    {
        public static bool isInitialized()
        {
            return WootingAnalogSDK.IsInitialised;
        }

        /// <summary>
        /// Check if Wooting SDK is installed correctly. Run this before initSdk
        /// </summary>
        /// <param name="productname">Your project name for the windows popup if the files are not installed</param>
        public static void checkWootingFiles(string productname)
        {
            if (!fstream.fileExists("C:/Program Files/wooting-analog-sdk/wooting_analog_sdk.dll"))
            {
                MessageBox.Show("Please install the Wooting SDK. You can download it from Wootility or through the link copied to your Clipboard. Make sure to install the SDK and Plugin.", productname, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (!fstream.fileExists("C:/Program Files/WootingAnalogPlugins/wooting-analog-plugin/wooting_analog_plugin.dll"))
            {
                MessageBox.Show("Please install the Wooting SDK. You can download it from Wootility or through the link copied to your Clipboard. Make sure to install the SDK and Plugin.", productname, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static dynamic getConnectedDevices()
        {
            var (devices, info) = WootingAnalogSDK.GetConnectedDevicesInfo();
            if (info != WootingAnalogResult.Ok)
            {
                return $"Could not read Wooting device data. Code: {info}";
            }
            foreach (DeviceInfo device in devices)
            {
                variables.devices.Add(device.ToString());
            }
            return "";
        }

        /// <summary>
        /// Initializes the Wooting SDk, only need to run this once
        /// </summary>
        /// <returns>Status of init</returns>
        public static dynamic initSdk()
        {
            if (variables.started) {return "Wooting SDK already started";};
            try
            {
                var (devices, info) = WootingAnalogSDK.Initialise();
                if (devices >= 0)
                {
                    getConnectedDevices();
                    variables.started = true;
                    return "Wooting SDK initialized";
                }
                else
                {
                   return "Wooting SDK did not initialize successfully. Please insure you have a Wooting device plugged in.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <returns>The amount the key is pressed</returns>
        public static dynamic readKeyBuffer()
        {
            if (variables.started) { return "Wooting SDK not started"; };
            var (keys, info) = WootingAnalogSDK.ReadFullBuffer(20);
            if (info == WootingAnalogResult.Ok)
            {
                foreach (var analog in keys)
                {
                    //return $"Keycode: {analog.Item2} | Analog Value: {analog.Item2}";
                    return analog.Item2;
                }
            }
            return "";
        }

        public static class variables
        {
            public static bool started;
            public static List<string> devices = new List<string>();
        }
    }
}
