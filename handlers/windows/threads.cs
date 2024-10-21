using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FRamework.handlers.windows
{
    public class threads
    {
        /// <summary>
        /// Run a function in a new thread, dont add () after is because adding () to a method calls the method instead of referencing the method itself
        /// </summary>
        /// <param name="function">Function you want to run in a new thread. Make sure not to add () after the function name</param>
        public static void createThread(Action function)
        {
            Thread thread = new Thread(new ThreadStart(function));
            thread.Start();
        }


        /// <summary>
        /// Run a function in a new task, dont add () after is because adding () to a method calls the method instead of referencing the method itself
        /// </summary>
        /// <param name="function">Function you want to run in a task. Make sure not to add () after the function name</param>
        public static void createTask(Action function)
        {
            Task.Factory.StartNew(() => function());
            //debugLogger.normalLog("Task started");
        }

        /// <summary>
        /// Return the amount of running threads
        /// </summary>
        public static dynamic getThreads()
        {
            var currentThreads = Process.GetCurrentProcess().Threads.Count;
            return currentThreads.ToString();
        }

        /// <summary>
        /// Get a list of running threads
        /// </summary>
        public static string listThreads()
        {
            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread thread in currentThreads)
            {
                return thread.ToString();
            }
            return "";
        }
    }
}
