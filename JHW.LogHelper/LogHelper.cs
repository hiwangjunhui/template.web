using log4net;
using System;
using System.Reflection;

namespace JHW.LogHelper
{
    public class LogHelper : ILog.ILog
    {
        private readonly log4net.ILog loginfo = null;
        private readonly log4net.ILog logerror = null;

        public LogHelper()
        {
            loginfo = LogManager.GetLogger("loginfo");
            logerror = LogManager.GetLogger("logerror");
        }

        public void Config()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void Write(string message)
        {
            loginfo.Info(message);
        }

        public void Write(Exception exception)
        {
            logerror.Error(exception);
        }
    }
}
