using System;
using System.Windows.Forms;

namespace BDAP.WeatherData.WinUI
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnHisWeather_Click(object sender, EventArgs e)
        {
            FrmHisWeatherData frm = new FrmHisWeatherData();
            frm.ShowDialog();
        }

        private void btnLastFFWeather_Click(object sender, EventArgs e)
        {
            FrmLLWeatherData frm = new FrmLLWeatherData();
            frm.ShowDialog();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
