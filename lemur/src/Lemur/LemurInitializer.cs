using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;
using log4net.Appender;

namespace Lemur
{
    public class LemurInitializer
    {
        public void Initialize()
        {
            Stream configStream = GetDefaultConfigStream();

            if (configStream == null)
                throw new Exception("Error loading default lemur config");

            log4net.Config.XmlConfigurator.Configure(configStream);
            
            ResetAppenderConnectionString();

            LogManager.GetLogger(GetType()).Info("Lemur initialized.");
        }

        private Stream GetDefaultConfigStream()
        {
            Assembly asm = GetType().Assembly;
            return asm.GetManifestResourceStream("Lemur.default.config");
        }

        private void ResetAppenderConnectionString()
        {
            var connStringSettings = ConfigurationManager.ConnectionStrings["Lemur"];
            if (connStringSettings != null)
            {
                var loggerHierarchy = LogManager.GetRepository();
                if (loggerHierarchy != null)
                {
                    var appender =
                        (AdoNetAppender) loggerHierarchy.GetAppenders().Single(app => app.Name == "AdoNetAppender");
                    appender.ConnectionString = connStringSettings.ConnectionString;
                    appender.ActivateOptions();
                }
            }
        }
    } 
}