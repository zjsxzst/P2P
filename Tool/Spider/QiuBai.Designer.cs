namespace QiuBai
{
    partial class QiuBaiForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusButtom = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.AutoWorker = new System.ComponentModel.BackgroundWorker();
            this.lbltime = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.statusButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusButtom
            // 
            this.statusButtom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusButtom.Location = new System.Drawing.Point(0, 536);
            this.statusButtom.Name = "statusButtom";
            this.statusButtom.Size = new System.Drawing.Size(1000, 22);
            this.statusButtom.TabIndex = 32;
            this.statusButtom.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // AutoWorker
            // 
            this.AutoWorker.WorkerReportsProgress = true;
            this.AutoWorker.WorkerSupportsCancellation = true;
            this.AutoWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.AutoWorker_DoWork);
            this.AutoWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.AutoWorker_ProgressChanged);
            this.AutoWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.AutoWorker_RunWorkerCompleted);
            // 
            // lbltime
            // 
            this.lbltime.AutoSize = true;
            this.lbltime.Location = new System.Drawing.Point(734, 74);
            this.lbltime.Name = "lbltime";
            this.lbltime.Size = new System.Drawing.Size(53, 12);
            this.lbltime.TabIndex = 39;
            this.lbltime.Text = "运行时间";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(417, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 41);
            this.btnStart.TabIndex = 38;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 37;
            this.label4.Text = "执行日志";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 90);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(976, 443);
            this.txtLog.TabIndex = 36;
            // 
            // QiuBaiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 558);
            this.Controls.Add(this.lbltime);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.statusButtom);
            this.Name = "QiuBaiForm";
            this.Text = "学习装置_糗百";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusButtom.ResumeLayout(false);
            this.statusButtom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusButtom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.ComponentModel.BackgroundWorker AutoWorker;
        private System.Windows.Forms.Label lbltime;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLog;
    }
}

