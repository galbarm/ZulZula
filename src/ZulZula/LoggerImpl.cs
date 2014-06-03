using Microsoft.Practices.Unity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZulZula
{
    public class LoggerImpl: ILogger //Cant call this class Logger since Logger is nLog class :+(
    {
        private Logger logger;

        //Constructors
        [InjectionConstructor]
        public LoggerImpl()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public LoggerImpl(string loggerName)
        {
            logger = LogManager.GetLogger(loggerName);
        }


        //DEBUG Prints
        public void Debug(object message, Exception exception)
        {
            logger.DebugException(message.ToString(), exception);
        }
        public void Debug(object message)
        {
            logger.Debug(message.ToString());
        }
        public void DebugFormat(string format, params object[] args)
        {
            logger.Debug(format, args);
        }

        //ERROR Prints
        public void Error(object message, Exception exception)
        {
            logger.ErrorException(message.ToString(), exception);
        }
        public void Error(object message)
        {
            logger.Error(message.ToString());
        }
        public void ErrorFormat(string format, params object[] args)
        {
            logger.Error(format, args);
        }

        //FATAL Prints
        public void Fatal(object message, Exception exception)
        {
            logger.FatalException(message.ToString(), exception);
        }
        public void Fatal(object message)
        {
            logger.Fatal(message.ToString());
        }
        public void FatalFormat(string format, params object[] args)
        {
            logger.Fatal(format.ToString(), args);
        }

        //INFO Prints
        public void Info(object message, Exception exception)
        {
            logger.InfoException(message.ToString(), exception);
        }
        public void Info(object message)
        {
            logger.Info(message.ToString());
        }
        public void InfoFormat(string format, params object[] args)
        {
            logger.Info(format.ToString(), args);
        }

        //WARN Prints
        public void Warn(object message, Exception exception)
        {
            logger.WarnException(message.ToString(), exception);
        }
        public void Warn(object message)
        {
            logger.Warn(message.ToString());
        }
        public void WarnFormat(string format, params object[] args)
        {
            logger.Warn(format.ToString(), args);
        }
    }
    
}
