namespace BDAP.WeatherData.WinUI
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnHisWeather = new System.Windows.Forms.Button();
            this.btnLastFFWeather = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHisWeather
            // 
            this.btnHisWeather.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnHisWeather.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnHisWeather.Location = new System.Drawing.Point(25, 73);
            this.btnHisWeather.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnHisWeather.Name = "btnHisWeather";
            this.btnHisWeather.Size = new System.Drawing.Size(215, 85);
            this.btnHisWeather.TabIndex = 0;
            this.btnHisWeather.Text = "截止上月历史天气";
            this.btnHisWeather.UseVisualStyleBackColor = true;
            this.btnHisWeather.Click += new System.EventHandler(this.btnHisWeather_Click);
            // 
            // btnLastFFWeather
            // 
            this.btnLastFFWeather.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLastFFWeather.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnLastFFWeather.Location = new System.Drawing.Point(273, 73);
            this.btnLastFFWeather.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnLastFFWeather.Name = "btnLastFFWeather";
            this.btnLastFFWeather.Size = new System.Drawing.Size(215, 85);
            this.btnLastFFWeather.TabIndex = 1;
            this.btnLastFFWeather.Text = "最近15日天气";
            this.btnLastFFWeather.UseVisualStyleBackColor = true;
            this.btnLastFFWeather.Click += new System.EventHandler(this.btnLastFFWeather_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 255);
            this.Controls.Add(this.btnLastFFWeather);
            this.Controls.Add(this.btnHisWeather);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "抓取天气数据";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHisWeather;
        private System.Windows.Forms.Button btnLastFFWeather;
    }
}