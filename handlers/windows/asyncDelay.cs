using System;
using System.Threading.Tasks;

namespace FRamework.handlers.windows
{
    internal class asyncDelay
    {
        /// <summary>
        /// Delay using async, a method can be added to run after delaying
        /// </summary>
        /// <param name="ms">Time to wait</param>
        /// <param name="function">Method to call after delay, can be set to null to not run a function</param>
        /// <param name="functionTask">If set to true, the function will be ran as a Task, if set to false it will run as normal</param>
        public static async void delay(int ms, Action function, bool functionTask)
        {
            await Task.Delay(ms);
            if (function != null)
            {
                if (functionTask)
                {
                    // read threads.cs to understand why () is used and sometimes isnt used //
                    threads.createTask(function);
                }
                else
                {
                    function();
                }
            }
        }
    }
}