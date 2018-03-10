namespace BDAP.WeatherData.WinUI
{
    partial class FrmLLWeatherData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLLWeatherData));
            this.label1 = new System.Windows.Forms.Label();
            this.lbxErrorCity = new System.Windows.Forms.ListBox();
            this.lbxGetList = new System.Windows.Forms.ListBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(709, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "抓取过程错误列表";
            // 
            // lbxErrorCity
            // 
            this.lbxErrorCity.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbxErrorCity.FormattingEnabled = true;
            this.lbxErrorCity.ItemHeight = 14;
            this.lbxErrorCity.Location = new System.Drawing.Point(709, 268);
            this.lbxErrorCity.Name = "lbxErrorCity";
            this.lbxErrorCity.Size = new System.Drawing.Size(339, 298);
            this.lbxErrorCity.TabIndex = 13;
            // 
            // lbxGetList
            // 
            this.lbxGetList.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbxGetList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbxGetList.FormattingEnabled = true;
            this.lbxGetList.ItemHeight = 16;
            this.lbxGetList.Location = new System.Drawing.Point(0, 0);
            this.lbxGetList.Name = "lbxGetList";
            this.lbxGetList.ScrollAlwaysVisible = true;
            this.lbxGetList.Size = new System.Drawing.Size(703, 567);
            this.lbxGetList.TabIndex = 12;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(765, 89);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(224, 80);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "开始抓取天气";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // FrmLLWeatherData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 567);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxErrorCity);
            this.Controls.Add(this.lbxGetList);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLLWeatherData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "抓取最近15日天气";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLLWeatherData_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbxErrorCity;
        private System.Windows.Forms.ListBox lbxGetList;
        private System.Windows.Forms.Button btnStart;
    }
}