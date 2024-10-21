using System.Drawing;

namespace FRamework.handlers.windows
{
    internal class fontChecker
    {
        public static bool isInstalled(string font)
        {
            float fontSize = 12;

            using (Font fontTester = new Font(font, fontSize, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                if (fontTester.Name != font)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}