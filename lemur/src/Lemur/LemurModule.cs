using System;
using System.Web;
using log4net;

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

            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            Guid requestId = Guid.NewGuid();            
            var logger = LogManager.GetLogger(GetType());

            LogicalThreadContext.Properties["RequestId"] = requestId;

            logger.InfoFormat("Request: {0}", HttpContext.Current.Request.Url.PathAndQuery);
        }

        private void InitializeLemur()
        {
            lock (initLock)
            {
                //possible we can still get here after initialization
                if (initialized)
                    return;

                new LemurInitializer().Initialize();
                initialized = true;
            }
        }

        public void Dispose()
        {
        }
    }
}
