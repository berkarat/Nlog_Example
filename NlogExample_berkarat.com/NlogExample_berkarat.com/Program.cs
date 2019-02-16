using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace NlogExample_berkarat.com
{
    class Program
    {
        static void Main(string[] args)
        {
            Commander log = new Commander();
            log.WriteLog(LogLevel.Info, "INFO MESSAGE berkarat.com");
            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                log.WriteLog(LogLevel.Error, "Message");
                log.WriteLog(LogLevel.Fatal, "MessageFatal", ex);
            }


        }
    }
    public class Commander
    {

        public Commander()
        {

            try
            {
                Commander.SetEventlogConfig(Properties.Settings.Default.EventLogName);
                Commander.SetDatabaseConfig
            (

                    Properties.Settings.Default.ServerName,
                    Properties.Settings.Default.DatabaseName,
                     Properties.Settings.Default.UserName,
                      Properties.Settings.Default.Password
            );
            }
            catch (Exception ex)
            {
                WriteLog(LogLevel.Error, "Connection Error", ex);
            }
        }
        Logger logger = LogManager.GetCurrentClassLogger();
        public void WriteLog(LogLevel loglevel, string Message)
        {
            logger.Log(loglevel, Message);
        }
        public void WriteLog(LogLevel loglevel, string Message, Exception ex)
        {
            logger.Log(loglevel, Message, ex);
        }


        ///  ***************************************************//
        //public void test()
        //{
        //    logger.Log(LogLevel.Info, "message123");

        //}
        //public void Exc(Exception ex)
        //{
        //    logger.Log(LogLevel.Error, "EX MESSAGE", ex);
        //}
        //public void Information(string Message)
        //{
        //    logger.Info(Message);
        //}
        //public void Warning(string Message)
        //{
        //    logger.Warn(Message);
        //}
        //public void Error(string Message)
        //{
        //    logger.Fatal(Message);
        //}

        //public void start()
        //{
        //    logger.Info("Started");

        //}
        //public void run()
        //{
        //    logger.Warn("Running");
        //}
        //public void stop()
        //{
        //    logger.Fatal("Fatal Error");
        //}
        public static void SetDatabaseConfig(string ServerName, string DatabaseName, string UserId, string Password)
        {
            var databaseTarget = (DatabaseTarget)LogManager.Configuration.FindTargetByName("db");
            databaseTarget.ConnectionString = "Server=" + ServerName + ";Database=" + DatabaseName + ";User id=" + UserId + ";Password=" + Password + ";";
            LogManager.ReconfigExistingLoggers();

        }
        public static void SetEventlogConfig(string EventLogName)
        {
            var eventlogTarget = (EventLogTarget)LogManager.Configuration.FindTargetByName("eventlog");
            eventlogTarget.Log = EventLogName;
            eventlogTarget.Source = EventLogName;
            LogManager.ReconfigExistingLoggers();
        }

    }
}
