using System;
using System.Web;

namespace Lemur
{
    public class LemurModule : IHttpModule
    {
        private static object initLock = new object();
        private static bool initialized = false;

        public void Init(HttpApplication context)
        {
            if(!initialized)
                InitializeLemur();
        }

        private void InitializeLemur()
        {
            lock (initLock)
            {
                //possible we can still get here after initialization
                if (initialized)
                    return;

                new LemurInitializer().Initialize();
            }
        }

        public void Dispose()
        {
        }
    }
}
