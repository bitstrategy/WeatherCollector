/************************************************************
 * 项目名称：BDAP.WeatherData.WinUI
 * 项目描述：
 * 类名称：Log4netHelper
 * 版本号：
 * 说明：
 * 作者：Administrator
 * 所在的域：JOHN-PC
 * 命名空间：BDAP.WeatherData.WinUI
 * 注册组织：
 * 机器名称：JOHN-PC
 * CLR版本：4.0.30319.42000
 * .NET Framework版本：4.0
 * 创建时间：2017/9/7 10:30:01
 * 更新时间：2017/9/7 10:30:01
 * *********************************************************
 * Copyright © Administrator 2017. All rights reserved
 * ********************************************************/
using log4net;
using System;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace BDAP.WeatherData.WinUI
{
    /// <summary>
    /// Log4net日志辅助类
    /// </summary>
    public partial class Log4netHelper : ILogger
    {
        private readonly ILog logger = null;

        public Log4netHelper()
        {
            logger = LogManager.GetLogger(typeof(Log4netHelper));
        }

        /// <summary>
        /// 实例化log4net相应的logger
        /// </summary>
        /// <param name="t">相同类型的logger</param>
        public Log4netHelper(Type t)
        {
            logger = LogManager.GetLogger(t);
        }

        /// <summary>
        /// 实例化log4net相应的logger
        /// </summary>
        /// <param name="name">相同名称的logger</param>
        public Log4netHelper(string name)
        {
            logger = LogManager.GetLogger(name);
        }

        /// <summary>
        /// web.config 默认配置
        /// </summary>
        public static void LogInit()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// log4net独立文件配置
        /// </summary>
        /// <param name="filePath">log4net配置文件路径</param>
        public static void LogInit(string filePath)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(filePath));
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        public void Info(object message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        public void Info(object message, Exception e)
        {
            logger.Info(message, e);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        public void Debug(object message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        public void Debug(object message, Exception e)
        {
            logger.Debug(message, e);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">要记录的信息</param>
        public void Warn(object message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        public void Warn(object message, Exception e)
        {
            logger.Warn(message, e);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        public void Error(object message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        public void Error(object message, Exception e)
        {
            logger.Error(message, e);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        public void Fatal(object message)
        {
            logger.Fatal(message);
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        public void Fatal(object message, Exception e)
        {
            logger.Fatal(message, e);
        }
    }
}
