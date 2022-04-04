using NLog;

namespace BF1.ServerAdminTools.Wpf.Helper;

internal static class LoggerHelper
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    static LoggerHelper()
    {
        var config = new NLog.Config.LoggingConfiguration();

        var logfile = new NLog.Targets.FileTarget("logfile")
        {
            FileName = "${specialfolder:folder=MyDocuments}/BF1 Server/Log/InfoLog/${shortdate}.log",
            Layout = "${longdate} ${level:upperCase=true} ${message}",
            MaxArchiveFiles = 10,
            ArchiveAboveSize = 1024 * 1024,
            ArchiveEvery = NLog.Targets.FileArchivePeriod.Day
        };

        config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

        LogManager.Configuration = config;
    }

    #region Debug，调试
    public static void Debug(string msg)
    {
        logger.Debug(msg);
    }

    public static void Debug(string msg, Exception err)
    {
        logger.Debug(err, msg);
    }
    #endregion

    #region Info，信息
    public static void Info(string msg)
    {
        logger.Info(msg);
    }

    public static void Info(string msg, Exception err)
    {
        logger.Info(err, msg);
    }
    #endregion

    #region Warn，警告
    public static void Warn(string msg)
    {
        logger.Warn(msg);
    }

    public static void Warn(string msg, Exception err)
    {
        logger.Warn(err, msg);
    }
    #endregion

    #region Trace，追踪
    public static void Trace(string msg)
    {
        logger.Trace(msg);
    }

    public static void Trace(string msg, Exception err)
    {
        logger.Trace(err, msg);
    }
    #endregion

    #region Error，错误
    public static void Error(string msg)
    {
        logger.Error(msg);
    }

    public static void Error(string msg, Exception err)
    {
        logger.Error(err, msg);
    }
    #endregion

    #region Fatal,致命错误
    public static void Fatal(string msg)
    {
        logger.Fatal(msg);
    }

    public static void Fatal(string msg, Exception err)
    {
        logger.Fatal(err, msg);
    }
    #endregion

    
}