using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FRamework.handlers.input
{
    internal class keyboardInterface
    {
        [DllImport("user32.dll")]
        private static extern ushort GetAsyncKeyState(int vKey);
        public static bool isKeyDown(Keys key)
        {
            return 0 != (GetAsyncKeyState((int)key) & 0x8000);
        }

        public static void pressKey(string key)
        {
            SendKeys.Send(($"({key})"));
        }
    }
}