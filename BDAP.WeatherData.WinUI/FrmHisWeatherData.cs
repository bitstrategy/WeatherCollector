using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDAP.WeatherData.WinUI
{
    public partial class FrmHisWeatherData : Form
    {
        bool taskRuned = false;
        DataTable weatherdt = new DataTable();
        IList<WeatherData> weatherList = new List<WeatherData>();
        string connstring = ConfigurationManager.ConnectionStrings["weatherdb"].ToString();
        private Log4netHelper logger = new Log4netHelper("logerror");
        int maxcpu = 2;
        public FrmHisWeatherData()
        {
            InitializeComponent();

            string cpus = ConfigurationManager.AppSettings["MaxDegreeOfParallelism"].ToString();
            try
            {
                if (cpus != null && cpus != "")
                {
                    maxcpu = Convert.ToInt32(cpus);
                }
            }
            catch
            {
                maxcpu = 2;
            }

            maxcpu = maxcpu > Environment.ProcessorCount / 2 ? Environment.ProcessorCount / 2 : maxcpu;
            //MessageBox.Show(maxcpu.ToString());

            this.lbxGetList.Items.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            /*
            WeatherItem ff = new WeatherItem();
            ff.CityName = "杭州市";
            ff.CityName_EN = "hangzhou";
            ff.CityShortName = "杭州";
            ff.Url = "http://lishi.tianqi.com/hangzhou/201712.html";
            GetData(ff);
            */

            bool isGetAll = this.rdbGetAll.Checked ? true : false;
            string cmdstr = "SELECT * FROM [dbo].[V_BI_HP_GetWeatherList]";
            //string cmdstr = "SELECT * FROM [dbo].[V_BI_HP_GetWeatherList] WHERE [V_BI_HP_GetWeatherList].[DateKey] >= 201712";
            //string cmdstrdel = "TRUNCATE TABLE [dbo].[DER_WeatherData]";

            bool isaggren = false;

            if (!isGetAll)
            {
                cmdstr = "SELECT * FROM [dbo].[V_BI_HP_GetWeatherList] WHERE CONVERT(INT, [DateKey]) >= CONVERT(INT, LEFT(CONVERT(NVARCHAR(8), DATEADD(dd, -DAY(GETDATE() - 1), GETDATE() - 1), 112), 6))";
                //cmdstrdel = "DELETE FROM [dbo].[DER_WeatherData] WHERE [DateKey] BETWEEN CONVERT(INT, LEFT(CONVERT(NVARCHAR(8), DATEADD(dd, -DAY(GETDATE() - 1), GETDATE() - 1), 112), 6) + '01') AND CONVERT(INT, CONVERT(NVARCHAR(8), DATEADD(dd, -DAY(GETDATE() - 1), GETDATE() - 1), 112))";
                isaggren = true;
            }
            else
            {
                if (MessageBox.Show("1、抓取全部的数据耗时很久而且可能会被封IP，如果已经抓取过历史数据，请选择第2种选项方式。\n\n确定要抓取全部吗？(Y/N)", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    isaggren = true;
                }
            }

            if (isaggren)
            {
                taskRuned = true;
                try
                {
                    //throw new Exception("故意引发的错误");
                    DataSet ds = new DataSet();
                    using (SqlConnection connection = new SqlConnection(connstring))
                    {
                        using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                        {
                            using (SqlDataAdapter adp = new SqlDataAdapter())
                            {
                                adp.SelectCommand = cmd;
                                adp.Fill(ds, "weatherList");
                            }
                        }

                        //using (SqlCommand cmd = new SqlCommand(cmdstrdel, connection))
                        //{
                        //    connection.Open();
                        //    cmd.ExecuteNonQuery();
                        //}
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //界面调整，比如 button1.Enabled = false; 以防止重入
                        this.btnStart.Text = "正在抓取......";
                        this.btnStart.Enabled = false;
                        IList<WeatherItem> list = DataTableHelper.ConvertTo<WeatherItem>(ds.Tables[0]);
                        //利用线程池安排一个线程执行并行循环
                        ThreadPool.QueueUserWorkItem(w =>
                        {
                            Parallel.ForEach(list, new ParallelOptions { MaxDegreeOfParallelism = maxcpu }, (index) => { GetData(index); });

                            this.Invoke(new MethodInvoker(() => ShowSuss()));
                        }, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("抓取数据发生错误：" + ex.StackTrace);
                }
            }
            else
            {
                MessageBox.Show("抓取任务取消");
            }
        }

        private void ShowSuss()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    string cmdstr = "TRUNCATE TABLE [dbo].[DER_WeatherData_Getdate_His]";
                    using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                weatherdt = DataTableHelper.ConvertTo<WeatherData>(weatherList);
                using (SqlBulkCopy sbc = new SqlBulkCopy(connstring))
                {
                    sbc.DestinationTableName = "DER_WeatherData_Getdate_His";
                    sbc.ColumnMappings.Add("DateKey", "DateKey");
                    sbc.ColumnMappings.Add("CityName", "CityName");
                    sbc.ColumnMappings.Add("Weather", "Weather");
                    sbc.ColumnMappings.Add("MinTemp", "MinTemp");
                    sbc.ColumnMappings.Add("MaxTemp", "MaxTemp");
                    sbc.WriteToServer(weatherdt);
                }

                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    string cmdstr = "EXEC [dbo].[usp_UpdateGetdateWeather_His];";
                    using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                this.btnStart.Text = "开始抓取天气";
                this.btnStart.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                MessageBox.Show("数据插入数据库发生错误，请查看日志！");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    string cmdstr = "EXEC [msdb].[dbo].[sp_start_job] @job_name = '301-天气Cube处理'";
                    using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("数据成功抓取完毕，cube处理大概需要10余秒，稍等下即可查阅天气报表！");
            }
            catch (Exception ex)
            {

                logger.Error(ex.Message, ex);
                MessageBox.Show("调用处理天气Cube发生错误，Cube需要手工处理\n\n详细错误请查看日志！");
            }
        }

        private void GetData(WeatherItem index)
        {
            //this.lbxGetList.Items.Add(string.Format("{0},{1},{2},{3},{4}", index.CityName, index.DateKey, index.CityShortName, index.CityName_EN, index.Url));

            try
            {
                string urlPat = @"((ht|f)tps?):\/\/([\w\-]+(\.[\w\-]+)*\/)*[\w\-]+(\.[\w\-]+)*\/?(\?([\w\-\.,@?^=%&:\/~\+#]*)+)?";
                Regex urlPg = new Regex(urlPat, RegexOptions.Multiline);
                //this.dgvGetList.DataSource = ds.Tables[0];
                //DataTable urldt = ds.Tables[0];
                //int count = 0;
                if (urlPg.IsMatch(index.Url))
                {
                    HttpWebRequest request = WebRequest.Create(new Uri(index.Url)) as HttpWebRequest;
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36";
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("GB2312"));
                        string text2 = sr2.ReadToEnd();

                        string patten = @"<div\s*class=\""tqtongji2\"">(.|\s)*?</div>";
                        Regex rg = new Regex(patten, RegexOptions.Multiline);
                        //this.textBox1.Text = rg.Match(text2).Value;
                        string pat2 = @"<ul>(.|\s)*?</ul>";
                        Regex rg2 = new Regex(pat2, RegexOptions.Multiline);

                        MatchCollection mchisList = rg2.Matches(rg.Match(text2).Value);

                        string datePat = @"<a[\s\S]*href=\""[\s\S]*\"">(.|\s)*?</a>";
                        Regex dateRg = new Regex(datePat, RegexOptions.Multiline);

                        string datePat2 = @">(.|\s)*?<";
                        Regex dateRg2 = new Regex(datePat2, RegexOptions.Multiline);

                        string tempPat = @"<li>(\-|\+)?\d+(\.\d+)?</li>";
                        Regex tempRg = new Regex(tempPat, RegexOptions.Multiline);

                        string weatherPat = @"<li>[\u4e00-\u9fa5]*</li>";
                        Regex weatherRg = new Regex(weatherPat, RegexOptions.Multiline);

                        string chinesePat = @"[\u4e00-\u9fa5]+";
                        Regex chineseRg = new Regex(chinesePat, RegexOptions.Multiline);

                        string num = @"(\-|\+)?\d+(\.\d+)?";
                        Regex numRg = new Regex(num, RegexOptions.Multiline);
                        //MessageBox.Show(numRg.Match("123").Value);

                        this.lbxGetList.Items.Add(string.Format("{0},{1},{2},{3},{4}", index.CityName, index.DateKey, index.CityShortName, index.CityName_EN, index.Url));
                        this.lbxGetList.TopIndex = this.lbxGetList.Items.Count - 1;

                        if (mchisList != null && mchisList.Count > 0)
                        {
                            for (int i = 0; i < mchisList.Count; i++)
                            {
                                //DataRow dr = weatherdt.NewRow();
                                if (tempRg.Matches(mchisList[i].Value).Count >= 2)
                                {
                                    WeatherData item = new WeatherData();
                                    item.DateKey = dateRg2.Match(dateRg.Match(mchisList[i].Value).Value).Value.Replace("-", "").Replace("<", "").Replace(">", "");
                                    item.CityName = index.CityName;
                                    item.MaxTemp = numRg.Match(tempRg.Matches(mchisList[i].Value)[0].Value).Value;
                                    item.MinTemp = numRg.Match(tempRg.Matches(mchisList[i].Value)[1].Value).Value;
                                    if (weatherRg.Matches(mchisList[i].Value).Count > 0)
                                    {
                                        item.Weather = chineseRg.Match(weatherRg.Matches(mchisList[i].Value)[0].Value).Value;
                                    }
                                    weatherList.Add(item);
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.lbxErrorCity.Items.Add(string.Format("抓取[{0}]的网址是错误", index.CityName));
                    this.lbxErrorCity.TopIndex = this.lbxErrorCity.Items.Count - 1;
                }
            }
            catch (Exception ex)
            {
                logger.Error("抓取[" + index.CityName + "]数据发生错误", ex);
                this.lbxErrorCity.Items.Add(string.Format("抓取[{0}]、[{1}]数据发生错误", index.CityName, index.DateKey));
                this.lbxErrorCity.TopIndex = this.lbxErrorCity.Items.Count - 1;
            }
            Thread.Sleep(1000);
        }

        private void FrmHisWeatherData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (taskRuned)
            {
                Environment.Exit(0);
            }
        }
    }
}
