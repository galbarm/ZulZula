using System;
using Microsoft.Practices.Unity;
using NLog;

namespace ZulZula.Log
{
    public class LoggerImpl: ILogger //Cant call this class Logger since Logger is nLog class :+(
    {
        private readonly Logger _logger;

        //Constructors
        [InjectionConstructor]
        public LoggerImpl()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public LoggerImpl(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }


        //DEBUG Prints
        public void Debug(object message, Exception exception)
        {
            _logger.DebugException(message.ToString(), exception);
        }
        public void Debug(object message)
        {
            _logger.Debug(message.ToString());
        }
        public void DebugFormat(string format, params object[] args)
        {
            _logger.Debug(format, args);
        }

        //ERROR Prints
        public void Error(object message, Exception exception)
        {
            _logger.ErrorException(message.ToString(), exception);
        }
        public void Error(object message)
        {
            _logger.Error(message.ToString());
        }
        public void ErrorFormat(string format, params object[] args)
        {
            _logger.Error(format, args);
        }

        //FATAL Prints
        public void Fatal(object message, Exception exception)
        {
            _logger.FatalException(message.ToString(), exception);
        }
        public void Fatal(object message)
        {
            _logger.Fatal(message.ToString());
        }
        public void FatalFormat(string format, params object[] args)
        {
            _logger.Fatal(format, args);
        }

        //INFO Prints
        public void Info(object message, Exception exception)
        {
            _logger.InfoException(message.ToString(), exception);
        }
        public void Info(object message)
        {
            _logger.Info(message.ToString());
        }
        public void InfoFormat(string format, params object[] args)
        {
            _logger.Info(format, args);
        }

        //WARN Prints
        public void Warn(object message, Exception exception)
        {
            _logger.WarnException(message.ToString(), exception);
        }
        public void Warn(object message)
        {
            _logger.Warn(message.ToString());
        }
        public void WarnFormat(string format, params object[] args)
        {
            _logger.Warn(format, args);
        }
    }
    
}
