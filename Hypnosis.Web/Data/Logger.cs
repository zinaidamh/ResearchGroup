using log4net;
using log4net.Appender;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hypnosis.Web.Data
{
    public static class Logger
    {

        private static log4net.ILog log = null;

        public static log4net.ILog Log
        {
            get
            {
                if (log == null)
                {
                    log4net.Config.XmlConfigurator.Configure();
                    log = log4net.LogManager.GetLogger("root");
                }
                return log;
            }
        }
        public static void Flush()
        {
            ILoggerRepository rep = LogManager.GetRepository();
            foreach (IAppender appender in rep.GetAppenders())
            {
                var buffered = appender as BufferingAppenderSkeleton;
                if (buffered != null)
                {
                    buffered.Flush();
                }
            }
        }

    }

}