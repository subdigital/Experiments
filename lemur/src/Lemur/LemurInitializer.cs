using System;
using System.Configuration;

namespace Lemur
{
    public class LemurInitializer
    {
        public void Initialize()
        {
            var appender = new log4net.Appender.AdoNetAppender();
            appender.Name = "LemurAppender";
            appender.ConnectionString = ConfigurationManager.ConnectionStrings["lemur"].ConnectionString;

            log4net.Config.BasicConfigurator.Configure(appender);
        }
    }
}