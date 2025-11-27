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
        /// Queue a task to the ThreadPool which automatically uses a background thread
        /// </summary>
        /// <returns></returns>
        public static async Task runInNewThread(Func<Task> code)
        {
            await Task.Run(code);
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

        /// <returns><The current thread ID/returns>
        public static int getThreadID()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }
    }
}
