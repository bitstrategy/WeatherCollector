/************************************************************
 * 项目名称：BDAP.WeatherData.WinUI
 * 项目描述：
 * 类名称：ILogger
 * 版本号：
 * 说明：
 * 作者：Administrator
 * 所在的域：JOHN-PC
 * 命名空间：BDAP.WeatherData.WinUI
 * 注册组织：
 * 机器名称：JOHN-PC
 * CLR版本：4.0.30319.42000
 * .NET Framework版本：4.0
 * 创建时间：2017/9/7 10:29:12
 * 更新时间：2017/9/7 10:29:12
 * *********************************************************
 * Copyright © Administrator 2017. All rights reserved
 * ********************************************************/
using System;

namespace BDAP.WeatherData.WinUI
{
    /// <summary>
    /// 日志记录接口
    /// </summary>
    public partial interface ILogger
    {
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        void Info(object message);

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        void Info(object message, Exception e);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        void Debug(object message);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        void Debug(object message, Exception e);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">要记录的信息</param>
        void Warn(object message);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        void Warn(object message, Exception e);

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        void Error(object message);

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        void Error(object message, Exception e);

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        void Fatal(object message);

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="message">要记录的信息</param>
        /// <param name="e">异常</param>
        void Fatal(object message, Exception e);
    }
}
