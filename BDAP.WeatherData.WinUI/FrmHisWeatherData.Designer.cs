namespace BDAP.WeatherData.WinUI
{
    partial class FrmHisWeatherData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHisWeatherData));
            this.btnStart = new System.Windows.Forms.Button();
            this.lbxGetList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbGetAll = new System.Windows.Forms.RadioButton();
            this.rdbGetLastMonth = new System.Windows.Forms.RadioButton();
            this.lbxErrorCity = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(766, 139);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(224, 80);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始抓取天气";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
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
            this.lbxGetList.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbGetAll);
            this.groupBox1.Controls.Add(this.rdbGetLastMonth);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(766, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抓取范围选择";
            // 
            // rdbGetAll
            // 
            this.rdbGetAll.AutoSize = true;
            this.rdbGetAll.Location = new System.Drawing.Point(23, 29);
            this.rdbGetAll.Name = "rdbGetAll";
            this.rdbGetAll.Size = new System.Drawing.Size(191, 25);
            this.rdbGetAll.TabIndex = 10;
            this.rdbGetAll.Text = "全部重新抓取(最近2年)";
            this.rdbGetAll.UseVisualStyleBackColor = true;
            // 
            // rdbGetLastMonth
            // 
            this.rdbGetLastMonth.AutoSize = true;
            this.rdbGetLastMonth.Checked = true;
            this.rdbGetLastMonth.Location = new System.Drawing.Point(23, 58);
            this.rdbGetLastMonth.Name = "rdbGetLastMonth";
            this.rdbGetLastMonth.Size = new System.Drawing.Size(147, 25);
            this.rdbGetLastMonth.TabIndex = 9;
            this.rdbGetLastMonth.TabStop = true;
            this.rdbGetLastMonth.Text = "抓取/更新上个月";
            this.rdbGetLastMonth.UseVisualStyleBackColor = true;
            // 
            // lbxErrorCity
            // 
            this.lbxErrorCity.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbxErrorCity.FormattingEnabled = true;
            this.lbxErrorCity.ItemHeight = 14;
            this.lbxErrorCity.Location = new System.Drawing.Point(709, 315);
            this.lbxErrorCity.Name = "lbxErrorCity";
            this.lbxErrorCity.Size = new System.Drawing.Size(339, 256);
            this.lbxErrorCity.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(709, 297);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "抓取过程错误列表";
            // 
            // FrmHisWeatherData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 567);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxErrorCity);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbxGetList);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHisWeatherData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "历史每日天气获取";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmHisWeatherData_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lbxGetList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbGetAll;
        private System.Windows.Forms.RadioButton rdbGetLastMonth;
        private System.Windows.Forms.ListBox lbxErrorCity;
        private System.Windows.Forms.Label label1;
    }
}