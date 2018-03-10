namespace BDAP.WeatherData.WinUI
{
    partial class FrmHisWeatherCityList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHisWeatherCityList));
            this.btnGetCity = new System.Windows.Forms.Button();
            this.txtRet = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetCity
            // 
            this.btnGetCity.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetCity.Location = new System.Drawing.Point(781, 202);
            this.btnGetCity.Name = "btnGetCity";
            this.btnGetCity.Size = new System.Drawing.Size(213, 82);
            this.btnGetCity.TabIndex = 1;
            this.btnGetCity.Text = "开始获取列表";
            this.btnGetCity.UseVisualStyleBackColor = true;
            this.btnGetCity.Click += new System.EventHandler(this.btnGetCity_Click);
            // 
            // txtRet
            // 
            this.txtRet.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtRet.Location = new System.Drawing.Point(0, 0);
            this.txtRet.Multiline = true;
            this.txtRet.Name = "txtRet";
            this.txtRet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRet.Size = new System.Drawing.Size(703, 567);
            this.txtRet.TabIndex = 2;
            // 
            // FrmHisWeatherCityList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 567);
            this.Controls.Add(this.txtRet);
            this.Controls.Add(this.btnGetCity);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmHisWeatherCityList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史城市天气列表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGetCity;
        private System.Windows.Forms.TextBox txtRet;
    }
}