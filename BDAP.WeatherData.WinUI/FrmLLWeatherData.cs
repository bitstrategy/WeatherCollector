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
    public partial class FrmLLWeatherData : Form
    {
        bool taskRuned = false;
        DataTable weatherdt = new DataTable();
        IList<WeatherData> weatherList = new List<WeatherData>();
        string connstring = ConfigurationManager.ConnectionStrings["weatherdb"].ToString();
        private Log4netHelper logger = new Log4netHelper("logerror");
        int maxcpu = 2;

        public FrmLLWeatherData()
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
             * 测试案例
            WeatherItemFF ff = new WeatherItemFF();
            ff.CityName = "杭州市";
            ff.CityName_EN = "hangzhou";
            ff.CityShortName = "杭州";
            ff.Url = "https://www.tianqi.com/hangzhou/15/";
            GetData(ff);
            */

            bool isaggren = false;

            if (MessageBox.Show("确定开始抓取数据吗？(Y/N)", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                isaggren = true;
            }

            if (isaggren)
            {
                string cmdstr = "SELECT * FROM [dbo].[V_BI_HP_GetFFWeatherList]";
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
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //界面调整，比如 button1.Enabled = false; 以防止重入
                        this.btnStart.Text = "正在抓取......";
                        this.btnStart.Enabled = false;
                        IList<WeatherItemFF> list = DataTableHelper.ConvertTo<WeatherItemFF>(ds.Tables[0]);

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
                    logger.Error("抓取数据发生错误：" + ex.StackTrace);
                    MessageBox.Show("抓取数据发生错误：" + ex.StackTrace);
                }
            }
            else
            {
                MessageBox.Show("抓取任务取消");
            }
        }

        private void GetData(WeatherItemFF index)
        {
            try
            {
                string urlPat = @"((ht|f)tps?):\/\/([\w\-]+(\.[\w\-]+)*\/)*[\w\-]+(\.[\w\-]+)*\/?(\?([\w\-\.,@?^=%&:\/~\+#]*)+)?";
                Regex urlPg = new Regex(urlPat, RegexOptions.Multiline);
                //this.dgvGetList.DataSource = ds.Tables[0];
                //DataTable urldt = ds.Tables[0];
                //int count = 0;
                int indexM = 0;
                int indexD = 0;
                string ret = "";
                if (urlPg.IsMatch(index.Url))
                {
                    HttpWebRequest request = WebRequest.Create(new Uri(index.Url)) as HttpWebRequest;
                    request.Method = "GET";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36";
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader sr2 = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                        string text2 = sr2.ReadToEnd();

                        string patten = @"<div[\s]*class=\""table_day(.|\s)*?\"">(.|\s)*?</div>";
                        Regex rg = new Regex(patten, RegexOptions.Multiline);

                        MatchCollection mchisList = rg.Matches(text2);

                        string datePat = @"<h3><b>(.|\s)*?</b>";
                        Regex dateRg = new Regex(datePat, RegexOptions.Multiline);

                        string tempPat = @"<li class=\""temp\"">(.|\s)*?<\/li>";
                        Regex tempRg = new Regex(tempPat, RegexOptions.Multiline);

                        string weatherPat = @"[\u4e00-\u9fa5]+";
                        Regex weatherRg = new Regex(weatherPat, RegexOptions.Singleline);

                        string num = @"(\-|\+)?\d+(\.\d+)?";
                        Regex numRg = new Regex(num, RegexOptions.Multiline);
                        //MessageBox.Show(numRg.Match("123").Value);

                        this.lbxGetList.Items.Add(string.Format("{0},{1},{2},{3}", index.CityName, index.CityShortName, index.CityName_EN, index.Url));
                        this.lbxGetList.TopIndex = this.lbxGetList.Items.Count - 1;

                        if (mchisList != null && mchisList.Count > 0)
                        {
                            for (int i = 0; i < mchisList.Count; i++)
                            {
                                try
                                {
                                    WeatherData item = new WeatherData();
                                    ret = dateRg.Match(dateRg.Match(mchisList[i].Value).Value).Value;
                                    indexM = ret.IndexOf("月");
                                    indexD = ret.IndexOf("日");

                                    item.DateKey = DateTime.Now.AddDays(i).Year.ToString() + ret.Substring(indexM - 2, 2) + ret.Substring(indexD - 2, 2);
                                    item.CityName = index.CityName;
                                    item.Weather = weatherRg.Match(tempRg.Match(mchisList[i].Value).Value).Value;
                                    item.MinTemp = "0";
                                    item.MaxTemp = "0";
                                    if (numRg.Matches(tempRg.Match(mchisList[i].Value).Value).Count == 1)
                                    {
                                        item.MinTemp = numRg.Matches(tempRg.Match(mchisList[i].Value).Value)[0].Value;
                                    }
                                    if (numRg.Matches(tempRg.Match(mchisList[i].Value).Value).Count == 2)
                                    {
                                        item.MinTemp = numRg.Matches(tempRg.Match(mchisList[i].Value).Value)[0].Value;
                                        item.MaxTemp = numRg.Matches(tempRg.Match(mchisList[i].Value).Value)[1].Value;
                                    }
                                    weatherList.Add(item);
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(index.CityName + "-" + ex.Message);
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
                logger.Error("抓取[" + index.CityName + "]数据发生错误，" + index.Url, ex);
                this.lbxErrorCity.Items.Add(string.Format("抓取[{0}]数据发生错误", index.CityName));
                this.lbxErrorCity.TopIndex = this.lbxErrorCity.Items.Count - 1;
            }
            Thread.Sleep(1000);
        }

        private void ShowSuss()
        {
            /*
             将当天抓取的数据写入到数据库
             */
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    string cmdstr = "TRUNCATE TABLE [dbo].[DER_WeatherData_Getdate]";
                    using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                weatherdt = DataTableHelper.ConvertTo<WeatherData>(weatherList);
                using (SqlBulkCopy sbc = new SqlBulkCopy(connstring))
                {
                    sbc.DestinationTableName = "DER_WeatherData_Getdate";
                    sbc.ColumnMappings.Add("DateKey", "DateKey");
                    sbc.ColumnMappings.Add("CityName", "CityName");
                    sbc.ColumnMappings.Add("Weather", "Weather");
                    sbc.ColumnMappings.Add("MinTemp", "MinTemp");
                    sbc.ColumnMappings.Add("MaxTemp", "MaxTemp");
                    sbc.WriteToServer(weatherdt);
                }

                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    string cmdstr = "EXEC [dbo].[usp_UpdateGetdateWeather]";
                    using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                this.btnStart.Text = "开始抓取天气";
                this.btnStart.Enabled = true;

                DelWeatherCurrOvertime();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                MessageBox.Show("数据插入数据库发生错误，请查看日志！");
            }

            /*
             处理cube
             */
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

        /// <summary>
        /// 清理curr表过期数据
        /// </summary>
        private void DelWeatherCurrOvertime()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    string cmdstr = "EXEC [dbo].[usp_DelWeatherCurrOvertime]";
                    using (SqlCommand cmd = new SqlCommand(cmdstr, connection))
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                MessageBox.Show("清理curr表过期数据发生错误！\n\n" + ex.StackTrace);
            }
        }

        private void FrmLLWeatherData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (taskRuned)
            {
                Environment.Exit(0);
            }
        }
    }
}
