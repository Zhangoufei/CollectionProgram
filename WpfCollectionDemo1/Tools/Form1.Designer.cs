namespace Tools
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.ListAdapters = new System.Windows.Forms.ListBox();
            this.TimerCounter = new System.Windows.Forms.Timer(this.components);
            this.LableDownloadValue = new System.Windows.Forms.Label();
            this.LabelUploadValue = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 58);
            this.button1.TabIndex = 0;
            this.button1.Text = "修改本地时间";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(109, 220);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(401, 28);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.Value = new System.DateTime(2018, 8, 10, 0, 0, 0, 0);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(136, 687);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 48);
            this.button2.TabIndex = 2;
            this.button2.Text = "获取网速1";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ListAdapters
            // 
            this.ListAdapters.FormattingEnabled = true;
            this.ListAdapters.ItemHeight = 18;
            this.ListAdapters.Location = new System.Drawing.Point(109, 363);
            this.ListAdapters.Name = "ListAdapters";
            this.ListAdapters.Size = new System.Drawing.Size(296, 292);
            this.ListAdapters.TabIndex = 3;
            this.ListAdapters.SelectedIndexChanged += new System.EventHandler(this.ListAdapters_SelectedIndexChanged);
            // 
            // TimerCounter
            // 
            this.TimerCounter.Tick += new System.EventHandler(this.TimerCounter_Tick);
            // 
            // LableDownloadValue
            // 
            this.LableDownloadValue.AutoSize = true;
            this.LableDownloadValue.Location = new System.Drawing.Point(718, 321);
            this.LableDownloadValue.Name = "LableDownloadValue";
            this.LableDownloadValue.Size = new System.Drawing.Size(62, 18);
            this.LableDownloadValue.TabIndex = 4;
            this.LableDownloadValue.Text = "label1";
            // 
            // LabelUploadValue
            // 
            this.LabelUploadValue.AutoSize = true;
            this.LabelUploadValue.Location = new System.Drawing.Point(718, 414);
            this.LabelUploadValue.Name = "LabelUploadValue";
            this.LabelUploadValue.Size = new System.Drawing.Size(62, 18);
            this.LabelUploadValue.TabIndex = 5;
            this.LabelUploadValue.Text = "label1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(163, 859);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(184, 48);
            this.button3.TabIndex = 6;
            this.button3.Text = "获取网卡信息";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(163, 944);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(184, 48);
            this.button5.TabIndex = 8;
            this.button5.Text = "获取网速2";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1746, 1238);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.LabelUploadValue);
            this.Controls.Add(this.LableDownloadValue);
            this.Controls.Add(this.ListAdapters);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox ListAdapters;
        private System.Windows.Forms.Timer TimerCounter;
        private System.Windows.Forms.Label LableDownloadValue;
        private System.Windows.Forms.Label LabelUploadValue;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
    }
}

