using System;
using System.Windows.Forms;

namespace BDAP.WeatherData.WinUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //log4net初始化
            //Log4netHelper.LogInit();
            Application.Run(new FrmMain());
        }
    }
}
