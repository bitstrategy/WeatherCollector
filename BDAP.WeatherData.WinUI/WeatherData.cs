/************************************************************
 * 项目名称：BDAP.WeatherData.WinUI
 * 项目描述：
 * 类名称：WeatherData
 * 版本号：
 * 说明：
 * 作者：Administrator
 * 所在的域：JOHN-PC
 * 命名空间：BDAP.WeatherData.WinUI
 * 注册组织：
 * 机器名称：JOHN-PC
 * CLR版本：4.0.30319.42000
 * .NET Framework版本：4.0
 * 创建时间：2017/9/7 11:30:08
 * 更新时间：2017/9/7 11:30:08
 * *********************************************************
 * Copyright © Administrator 2017. All rights reserved
 * ********************************************************/

namespace BDAP.WeatherData.WinUI
{
    public partial class WeatherData
    {
        public string DateKey { get; set; }

        public string CityName { get; set; }

        public string Weather { get; set; }

        public string MinTemp { get; set; }

        public string MaxTemp { get; set; }

        public WeatherData()
        {

        }
    }
}
