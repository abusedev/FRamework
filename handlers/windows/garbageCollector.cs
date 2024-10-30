using System;

namespace FRamework.handlers.windows
{
    internal class garbageCollector
    {
        public static void collectGarbage()
        {
            GC.Collect(0);
        }

        public static dynamic getMemory()
        {
            return GC.GetTotalMemory(false);
        }

        /// <returns>The maximum number of generations that are supported by the system</returns>
        public static dynamic getMaxGeneration()
        {
            return GC.MaxGeneration;
        }

        public static dynamic getObjectGeneration(object obj)
        {
            return GC.GetGeneration(obj);
        }
    }
}
