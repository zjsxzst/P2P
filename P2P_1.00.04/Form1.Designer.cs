namespace P2P_1._00._04
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.多页 = new System.Windows.Forms.TabControl();
            this.关注信息 = new System.Windows.Forms.TabPage();
            this.操作日期选择 = new System.Windows.Forms.GroupBox();
            this.预览日期 = new System.Windows.Forms.Label();
            this.预览操作日期 = new System.Windows.Forms.Label();
            this.操作日期 = new System.Windows.Forms.MonthCalendar();
            this.预览回款 = new System.Windows.Forms.GroupBox();
            this.收益预览表 = new System.Windows.Forms.DataGridView();
            this.时间段 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预览笔数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预览本金 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预览收益 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.预览合计 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.安全信息 = new System.Windows.Forms.GroupBox();
            this.预览警告 = new System.Windows.Forms.Label();
            this.WY1 = new System.Windows.Forms.Label();
            this.WY10 = new System.Windows.Forms.Label();
            this.WY11 = new System.Windows.Forms.Label();
            this.WY9 = new System.Windows.Forms.Label();
            this.WY4 = new System.Windows.Forms.Label();
            this.WY8 = new System.Windows.Forms.Label();
            this.WY5 = new System.Windows.Forms.Label();
            this.WY7 = new System.Windows.Forms.Label();
            this.WY2 = new System.Windows.Forms.Label();
            this.WY6 = new System.Windows.Forms.Label();
            this.WY3 = new System.Windows.Forms.Label();
            this.平台信息 = new System.Windows.Forms.TabPage();
            this.平台列表 = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZT = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.账户列表 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.定时1 = new System.Windows.Forms.Timer(this.components);
            this.坏账整理 = new System.Windows.Forms.Button();
            this.多页.SuspendLayout();
            this.关注信息.SuspendLayout();
            this.操作日期选择.SuspendLayout();
            this.预览回款.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.收益预览表)).BeginInit();
            this.安全信息.SuspendLayout();
            this.平台信息.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.平台列表)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.账户列表)).BeginInit();
            this.SuspendLayout();
            // 
            // 多页
            // 
            this.多页.Controls.Add(this.关注信息);
            this.多页.Controls.Add(this.平台信息);
            this.多页.Location = new System.Drawing.Point(2, 2);
            this.多页.Name = "多页";
            this.多页.SelectedIndex = 0;
            this.多页.Size = new System.Drawing.Size(1340, 540);
            this.多页.TabIndex = 0;
            this.多页.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.多页_Selecting);
            // 
            // 关注信息
            // 
            this.关注信息.Controls.Add(this.操作日期选择);
            this.关注信息.Controls.Add(this.预览回款);
            this.关注信息.Controls.Add(this.安全信息);
            this.关注信息.Location = new System.Drawing.Point(4, 22);
            this.关注信息.Name = "关注信息";
            this.关注信息.Padding = new System.Windows.Forms.Padding(3);
            this.关注信息.Size = new System.Drawing.Size(1332, 514);
            this.关注信息.TabIndex = 0;
            this.关注信息.Text = "关注信息";
            this.关注信息.UseVisualStyleBackColor = true;
            // 
            // 操作日期选择
            // 
            this.操作日期选择.Controls.Add(this.坏账整理);
            this.操作日期选择.Controls.Add(this.预览日期);
            this.操作日期选择.Controls.Add(this.预览操作日期);
            this.操作日期选择.Controls.Add(this.操作日期);
            this.操作日期选择.Location = new System.Drawing.Point(1048, 79);
            this.操作日期选择.Name = "操作日期选择";
            this.操作日期选择.Size = new System.Drawing.Size(247, 360);
            this.操作日期选择.TabIndex = 38;
            this.操作日期选择.TabStop = false;
            this.操作日期选择.Text = "操作日期选择";
            // 
            // 预览日期
            // 
            this.预览日期.AutoSize = true;
            this.预览日期.Cursor = System.Windows.Forms.Cursors.Hand;
            this.预览日期.Font = new System.Drawing.Font("宋体", 13F);
            this.预览日期.Location = new System.Drawing.Point(84, 263);
            this.预览日期.Name = "预览日期";
            this.预览日期.Size = new System.Drawing.Size(98, 18);
            this.预览日期.TabIndex = 39;
            this.预览日期.Tag = "";
            this.预览日期.Text = "2017-10-11";
            // 
            // 预览操作日期
            // 
            this.预览操作日期.AutoSize = true;
            this.预览操作日期.Cursor = System.Windows.Forms.Cursors.Hand;
            this.预览操作日期.Font = new System.Drawing.Font("宋体", 13F);
            this.预览操作日期.Location = new System.Drawing.Point(48, 235);
            this.预览操作日期.Name = "预览操作日期";
            this.预览操作日期.Size = new System.Drawing.Size(134, 18);
            this.预览操作日期.TabIndex = 38;
            this.预览操作日期.Tag = "";
            this.预览操作日期.Text = "操作日期均为：";
            // 
            // 操作日期
            // 
            this.操作日期.Location = new System.Drawing.Point(12, 33);
            this.操作日期.MaxSelectionCount = 1;
            this.操作日期.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.操作日期.Name = "操作日期";
            this.操作日期.TabIndex = 37;
            this.操作日期.TodayDate = new System.DateTime(2017, 9, 2, 0, 0, 0, 0);
            this.操作日期.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.操作日期_DateSelected);
            // 
            // 预览回款
            // 
            this.预览回款.Controls.Add(this.收益预览表);
            this.预览回款.Location = new System.Drawing.Point(599, 79);
            this.预览回款.Name = "预览回款";
            this.预览回款.Size = new System.Drawing.Size(424, 360);
            this.预览回款.TabIndex = 36;
            this.预览回款.TabStop = false;
            this.预览回款.Text = "回款信息";
            // 
            // 收益预览表
            // 
            this.收益预览表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.收益预览表.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.时间段,
            this.预览笔数,
            this.预览本金,
            this.预览收益,
            this.预览合计});
            this.收益预览表.Location = new System.Drawing.Point(10, 36);
            this.收益预览表.Name = "收益预览表";
            this.收益预览表.RowTemplate.Height = 23;
            this.收益预览表.Size = new System.Drawing.Size(403, 299);
            this.收益预览表.TabIndex = 33;
            // 
            // 时间段
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.时间段.DefaultCellStyle = dataGridViewCellStyle6;
            this.时间段.Frozen = true;
            this.时间段.HeaderText = "时间段";
            this.时间段.MaxInputLength = 10;
            this.时间段.Name = "时间段";
            this.时间段.ReadOnly = true;
            this.时间段.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.时间段.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // 预览笔数
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            this.预览笔数.DefaultCellStyle = dataGridViewCellStyle7;
            this.预览笔数.Frozen = true;
            this.预览笔数.HeaderText = "笔数";
            this.预览笔数.MaxInputLength = 10;
            this.预览笔数.Name = "预览笔数";
            this.预览笔数.ReadOnly = true;
            this.预览笔数.Width = 60;
            // 
            // 预览本金
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.预览本金.DefaultCellStyle = dataGridViewCellStyle8;
            this.预览本金.Frozen = true;
            this.预览本金.HeaderText = "本金";
            this.预览本金.MaxInputLength = 10;
            this.预览本金.Name = "预览本金";
            this.预览本金.ReadOnly = true;
            this.预览本金.Width = 60;
            // 
            // 预览收益
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.预览收益.DefaultCellStyle = dataGridViewCellStyle9;
            this.预览收益.Frozen = true;
            this.预览收益.HeaderText = "收益";
            this.预览收益.MaxInputLength = 10;
            this.预览收益.Name = "预览收益";
            this.预览收益.ReadOnly = true;
            this.预览收益.Width = 60;
            // 
            // 预览合计
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Format = "N2";
            this.预览合计.DefaultCellStyle = dataGridViewCellStyle10;
            this.预览合计.Frozen = true;
            this.预览合计.HeaderText = "合计";
            this.预览合计.MaxInputLength = 10;
            this.预览合计.Name = "预览合计";
            this.预览合计.ReadOnly = true;
            this.预览合计.Width = 60;
            // 
            // 安全信息
            // 
            this.安全信息.Controls.Add(this.预览警告);
            this.安全信息.Controls.Add(this.WY1);
            this.安全信息.Controls.Add(this.WY10);
            this.安全信息.Controls.Add(this.WY11);
            this.安全信息.Controls.Add(this.WY9);
            this.安全信息.Controls.Add(this.WY4);
            this.安全信息.Controls.Add(this.WY8);
            this.安全信息.Controls.Add(this.WY5);
            this.安全信息.Controls.Add(this.WY7);
            this.安全信息.Controls.Add(this.WY2);
            this.安全信息.Controls.Add(this.WY6);
            this.安全信息.Controls.Add(this.WY3);
            this.安全信息.Location = new System.Drawing.Point(33, 79);
            this.安全信息.Name = "安全信息";
            this.安全信息.Size = new System.Drawing.Size(535, 360);
            this.安全信息.TabIndex = 35;
            this.安全信息.TabStop = false;
            this.安全信息.Text = "安全信息";
            // 
            // 预览警告
            // 
            this.预览警告.AutoSize = true;
            this.预览警告.Cursor = System.Windows.Forms.Cursors.Hand;
            this.预览警告.Font = new System.Drawing.Font("隶书", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.预览警告.ForeColor = System.Drawing.Color.Red;
            this.预览警告.Location = new System.Drawing.Point(18, 33);
            this.预览警告.Name = "预览警告";
            this.预览警告.Size = new System.Drawing.Size(511, 40);
            this.预览警告.TabIndex = 33;
            this.预览警告.Tag = "";
            this.预览警告.Text = "投资有风险    操作需谨慎";
            // 
            // WY1
            // 
            this.WY1.AutoSize = true;
            this.WY1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY1.Font = new System.Drawing.Font("宋体", 13F);
            this.WY1.Location = new System.Drawing.Point(222, 85);
            this.WY1.Name = "WY1";
            this.WY1.Size = new System.Drawing.Size(71, 18);
            this.WY1.TabIndex = 22;
            this.WY1.Tag = "http://jq.qq.com/?_wv=1027&k=eND3Qt";
            this.WY1.Text = "作者Q群";
            this.WY1.Click += new System.EventHandler(this.WY1_Click);
            // 
            // WY10
            // 
            this.WY10.AutoSize = true;
            this.WY10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY10.Font = new System.Drawing.Font("宋体", 13F);
            this.WY10.Location = new System.Drawing.Point(359, 261);
            this.WY10.Name = "WY10";
            this.WY10.Size = new System.Drawing.Size(80, 18);
            this.WY10.TabIndex = 23;
            this.WY10.Tag = "http://www.76676.com/html/product/product_p2p/";
            this.WY10.Text = "平台点评";
            this.WY10.Click += new System.EventHandler(this.WY10_Click);
            // 
            // WY11
            // 
            this.WY11.AutoSize = true;
            this.WY11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY11.Font = new System.Drawing.Font("宋体", 13F);
            this.WY11.Location = new System.Drawing.Point(359, 301);
            this.WY11.Name = "WY11";
            this.WY11.Size = new System.Drawing.Size(98, 18);
            this.WY11.TabIndex = 24;
            this.WY11.Tag = "http://www.76676.com/html/product/listhome/rq/";
            this.WY11.Text = "平台排行榜";
            this.WY11.Click += new System.EventHandler(this.WY11_Click);
            // 
            // WY9
            // 
            this.WY9.AutoSize = true;
            this.WY9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY9.Font = new System.Drawing.Font("宋体", 13F);
            this.WY9.Location = new System.Drawing.Point(359, 220);
            this.WY9.Name = "WY9";
            this.WY9.Size = new System.Drawing.Size(80, 18);
            this.WY9.TabIndex = 32;
            this.WY9.Tag = "http://www.dailuopan.com/";
            this.WY9.Text = "网贷数据";
            this.WY9.Click += new System.EventHandler(this.WY9_Click);
            // 
            // WY4
            // 
            this.WY4.AutoSize = true;
            this.WY4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY4.Font = new System.Drawing.Font("宋体", 13F);
            this.WY4.Location = new System.Drawing.Point(88, 221);
            this.WY4.Name = "WY4";
            this.WY4.Size = new System.Drawing.Size(116, 18);
            this.WY4.TabIndex = 25;
            this.WY4.Tag = "http://gsxt.saic.gov.cn/";
            this.WY4.Text = "工商信息查询";
            this.WY4.Click += new System.EventHandler(this.WY4_Click);
            // 
            // WY8
            // 
            this.WY8.AutoSize = true;
            this.WY8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY8.Font = new System.Drawing.Font("宋体", 13F);
            this.WY8.Location = new System.Drawing.Point(359, 177);
            this.WY8.Name = "WY8";
            this.WY8.Size = new System.Drawing.Size(80, 18);
            this.WY8.TabIndex = 31;
            this.WY8.Tag = "http://icp.chinaz.com";
            this.WY8.Text = "域名查询";
            this.WY8.Click += new System.EventHandler(this.WY8_Click);
            // 
            // WY5
            // 
            this.WY5.AutoSize = true;
            this.WY5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY5.Font = new System.Drawing.Font("宋体", 13F);
            this.WY5.Location = new System.Drawing.Point(88, 261);
            this.WY5.Name = "WY5";
            this.WY5.Size = new System.Drawing.Size(152, 18);
            this.WY5.TabIndex = 26;
            this.WY5.Tag = "http://zhixing.court.gov.cn/search/";
            this.WY5.Text = "被执行人信息查询";
            this.WY5.Click += new System.EventHandler(this.WY5_Click);
            // 
            // WY7
            // 
            this.WY7.AutoSize = true;
            this.WY7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY7.Font = new System.Drawing.Font("宋体", 13F);
            this.WY7.Location = new System.Drawing.Point(359, 136);
            this.WY7.Name = "WY7";
            this.WY7.Size = new System.Drawing.Size(80, 18);
            this.WY7.TabIndex = 30;
            this.WY7.Tag = "http://zhixing.court.gov.cn/search/";
            this.WY7.Text = "法院执行";
            this.WY7.Click += new System.EventHandler(this.WY7_Click);
            // 
            // WY2
            // 
            this.WY2.AutoSize = true;
            this.WY2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY2.Font = new System.Drawing.Font("宋体", 13F);
            this.WY2.Location = new System.Drawing.Point(106, 137);
            this.WY2.Name = "WY2";
            this.WY2.Size = new System.Drawing.Size(80, 18);
            this.WY2.TabIndex = 27;
            this.WY2.Tag = "http://www.zhongdengwang.org.cn/zhongdeng/index.shtml";
            this.WY2.Text = "征信系统";
            this.WY2.Click += new System.EventHandler(this.WY2_Click);
            // 
            // WY6
            // 
            this.WY6.AutoSize = true;
            this.WY6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY6.Font = new System.Drawing.Font("宋体", 13F);
            this.WY6.Location = new System.Drawing.Point(88, 302);
            this.WY6.Name = "WY6";
            this.WY6.Size = new System.Drawing.Size(98, 18);
            this.WY6.TabIndex = 29;
            this.WY6.Tag = "http://shixin.court.gov.cn/";
            this.WY6.Text = "老赖黑名单";
            this.WY6.Click += new System.EventHandler(this.WY6_Click);
            // 
            // WY3
            // 
            this.WY3.AutoSize = true;
            this.WY3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.WY3.Font = new System.Drawing.Font("宋体", 13F);
            this.WY3.Location = new System.Drawing.Point(106, 178);
            this.WY3.Name = "WY3";
            this.WY3.Size = new System.Drawing.Size(80, 18);
            this.WY3.TabIndex = 28;
            this.WY3.Tag = "http://gsxt.saic.gov.cn";
            this.WY3.Text = "企业信用";
            this.WY3.Click += new System.EventHandler(this.WY3_Click);
            // 
            // 平台信息
            // 
            this.平台信息.Controls.Add(this.平台列表);
            this.平台信息.Controls.Add(this.账户列表);
            this.平台信息.Location = new System.Drawing.Point(4, 22);
            this.平台信息.Name = "平台信息";
            this.平台信息.Padding = new System.Windows.Forms.Padding(3);
            this.平台信息.Size = new System.Drawing.Size(1332, 514);
            this.平台信息.TabIndex = 1;
            this.平台信息.Text = "平台信息";
            this.平台信息.UseVisualStyleBackColor = true;
            // 
            // 平台列表
            // 
            this.平台列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.平台列表.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.PT,
            this.ZT,
            this.Column6,
            this.Column7,
            this.Column8});
            this.平台列表.Location = new System.Drawing.Point(239, 18);
            this.平台列表.Name = "平台列表";
            this.平台列表.RowTemplate.Height = 23;
            this.平台列表.Size = new System.Drawing.Size(1097, 489);
            this.平台列表.TabIndex = 6;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "PTID";
            this.Column5.HeaderText = "ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 30;
            // 
            // PT
            // 
            this.PT.DataPropertyName = "PTMC";
            this.PT.FillWeight = 120F;
            this.PT.HeaderText = "平台";
            this.PT.Name = "PT";
            this.PT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // ZT
            // 
            this.ZT.DataPropertyName = "PTID";
            this.ZT.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ZT.HeaderText = "状态";
            this.ZT.Name = "ZT";
            this.ZT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ZT.Width = 70;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "PTQT";
            this.Column6.HeaderText = "起投";
            this.Column6.Name = "Column6";
            this.Column6.Width = 70;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "FWF";
            this.Column7.HeaderText = "费用(%)";
            this.Column7.Name = "Column7";
            this.Column7.Width = 70;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "PTWZ";
            this.Column8.HeaderText = "平台官网（点击无ID行显示全部，双击进入官网）";
            this.Column8.Name = "Column8";
            this.Column8.Width = 695;
            // 
            // 账户列表
            // 
            this.账户列表.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.账户列表.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.账户列表.Location = new System.Drawing.Point(5, 18);
            this.账户列表.Name = "账户列表";
            this.账户列表.RowTemplate.Height = 23;
            this.账户列表.Size = new System.Drawing.Size(220, 489);
            this.账户列表.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ZHID";
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ZHMC";
            this.Column2.HeaderText = "账户名称";
            this.Column2.Name = "Column2";
            // 
            // 定时1
            // 
            this.定时1.Enabled = true;
            this.定时1.Interval = 500;
            this.定时1.Tick += new System.EventHandler(this.定时1_Tick);
            // 
            // 坏账整理
            // 
            this.坏账整理.Location = new System.Drawing.Point(74, 292);
            this.坏账整理.Name = "坏账整理";
            this.坏账整理.Size = new System.Drawing.Size(108, 52);
            this.坏账整理.TabIndex = 40;
            this.坏账整理.Text = "按系统日期\r\n\r\n整理坏账";
            this.坏账整理.UseVisualStyleBackColor = true;
            this.坏账整理.Click += new System.EventHandler(this.坏账整理_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 543);
            this.Controls.Add(this.多页);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.多页.ResumeLayout(false);
            this.关注信息.ResumeLayout(false);
            this.操作日期选择.ResumeLayout(false);
            this.操作日期选择.PerformLayout();
            this.预览回款.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.收益预览表)).EndInit();
            this.安全信息.ResumeLayout(false);
            this.安全信息.PerformLayout();
            this.平台信息.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.平台列表)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.账户列表)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl 多页;
        private System.Windows.Forms.TabPage 关注信息;
        private System.Windows.Forms.TabPage 平台信息;
        private System.Windows.Forms.Label WY9;
        private System.Windows.Forms.Label WY8;
        private System.Windows.Forms.Label WY7;
        private System.Windows.Forms.Label WY6;
        private System.Windows.Forms.Label WY3;
        private System.Windows.Forms.Label WY2;
        private System.Windows.Forms.Label WY5;
        private System.Windows.Forms.Label WY4;
        private System.Windows.Forms.Label WY11;
        private System.Windows.Forms.Label WY10;
        private System.Windows.Forms.Label WY1;
        private System.Windows.Forms.DataGridView 收益预览表;
        private System.Windows.Forms.GroupBox 安全信息;
        private System.Windows.Forms.Label 预览警告;
        private System.Windows.Forms.MonthCalendar 操作日期;
        private System.Windows.Forms.GroupBox 预览回款;
        private System.Windows.Forms.GroupBox 操作日期选择;
        private System.Windows.Forms.Label 预览操作日期;
        private System.Windows.Forms.DataGridView 平台列表;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn PT;
        private System.Windows.Forms.DataGridViewComboBoxColumn ZT;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridView 账户列表;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label 预览日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间段;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预览笔数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预览本金;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预览收益;
        private System.Windows.Forms.DataGridViewTextBoxColumn 预览合计;
        private System.Windows.Forms.Timer 定时1;
        private System.Windows.Forms.Button 坏账整理;
    }
}

