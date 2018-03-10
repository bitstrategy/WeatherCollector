using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BDAP.WeatherData.WinUI
{
    public partial class FrmHisWeatherCityList : Form
    {
        public FrmHisWeatherCityList()
        {
            InitializeComponent();
        }

        private void btnGetCity_Click(object sender, EventArgs e)
        {
            string url = "http://lishi.tianqi.com/";
            HttpWebRequest request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB2312"));
            string text2 = sr2.ReadToEnd();

            string patten = @"<ul\s*class=\""bcity\""(.|\s)*?>(.|\s)*?</ul>";
            Regex rg = new Regex(patten, RegexOptions.Multiline);
            MatchCollection mclist = rg.Matches(text2);
            string str = string.Empty;
            string ulPat = @"<li><a href=\""http://lishi.tianqi.com/(.|\s)*?/index.html\"" title=\""(.|\s)*?\"" target=\""_blank\"">(.|\s)*?</a></li>";
            string hrefPat = @"href=\""(.|\s)*?\""";
            Regex hrefRg = new Regex(hrefPat, RegexOptions.Multiline);
            string namePat = @"target=\""_blank\"">(.|\s)*?</a>";
            Regex nameRg = new Regex(namePat, RegexOptions.Multiline);
            if (mclist != null && mclist.Count > 0)
            {
                for (int i = 0; i < mclist.Count; i++)
                {
                    Regex rgli = new Regex(ulPat, RegexOptions.Multiline);
                    //string ss = rgli.Match(mclist[i].Value).Value;
                    MatchCollection refMcList = rgli.Matches(mclist[i].Value);
                    if (refMcList != null && refMcList.Count > 0)
                    {
                        for (int j = 0; j < refMcList.Count; j++)
                        {
                            string rf = hrefRg.Match(refMcList[j].Value).Value;
                            string na = nameRg.Match(refMcList[j].Value).Value;
                            str += na + ";" + rf + "\n";
                        }
                    }
                }
            }
            //text2 = rg.Match(text2).Value;
            this.txtRet.Text = str;
        }
    }
}
