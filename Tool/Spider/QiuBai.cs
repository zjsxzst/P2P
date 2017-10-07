
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QiuBai
{
    public partial class QiuBaiForm : Form
    {
        public int SleepTime = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["SleepTime"].ToString());
        //public int startid = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["startid"].ToString());
        //public int endid = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["endid"].ToString());
        //ini文件读写操作
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        public QiuBaiForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString();
        }
        private void LogTxt(string mes)
        {

            try
            {
                if (!File.Exists(@"日志"))
                    Directory.CreateDirectory(@"日志");
                FileStream stream = new FileStream("日志/Log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("gb2312"));
                writer.WriteLine("-----------------------------------");
                writer.WriteLine(DateTime.Now.ToString() + "," + mes);
                writer.Close();
                stream.Close();
            }
            catch
            {
                LogTxt(mes);
            }
        }
        private string ReadTxt(string filename = "columnid_unitid")
        {


            try
            {
                string strLine = "";

                using (StreamReader sr = new StreamReader("" + filename + ".txt", Encoding.UTF8, true))
                {
                    while (!sr.EndOfStream)
                    {
                        strLine += sr.ReadLine() + "\n";
                    }
                }

                return strLine;
            }
            catch (Exception ex)
            {
                return "err";
            }
        }
        WebUtils web = new WebUtils();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="argument"></param>
        private void DoWork(BackgroundWorker worker, object argument)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            int startid = 0;
            #region 爬取糗百
            try
            {
                worker.ReportProgress(10, "准备爬取");
                string domain = "http://www.qiushibaike.com";
                string DataId = null;
                //string[] columnid_unitid = ReadTxt().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                DataTable dtZcomChannel = SQLHelper.Query("select * from Collect_Channel where SiteId=1").Tables[0];

                worker.ReportProgress(10, "待爬取列表：" + dtZcomChannel.Rows.Count + "个");
                for (int i = 0; i < dtZcomChannel.Rows.Count; i++)
                {
                    try
                    {
                        string columnid = dtZcomChannel.Rows[i]["columnid"].ToString();
                        string pageUrl = dtZcomChannel.Rows[i]["pageUrl"].ToString();
                        string chnlcatalog = dtZcomChannel.Rows[i]["chnlcatalog"].ToString();
                        worker.ReportProgress(10, "开始爬取【站点栏目：" + chnlcatalog + "，栏目编号：" + columnid + "】");
                        int num = 0;//记录存在数据条数
                        for (int j = 1; j <= 35; j++)
                        { 
                            if (num > 50)
                                break;
                            string myHtml = web.DoGet(pageUrl +  + j + "/", null);
                            string regString = string.Format(@"{0}(?<getcontent>[\s|\S]+?){1}", "<span>", "</span>");
                            MatchCollection matchs = Regex.Matches(myHtml, regString);
                            worker.ReportProgress(10, "站点" + chnlcatalog + "栏目，找到" + matchs.Count + "个链接,页码:"+j);
                           
                            foreach (Match m in matchs)
                            {
                                #region 根据新闻url获取具体信息
                                try
                                {
                                    string Contents = m.Groups["getcontent"].Value;

                                    if (!string.IsNullOrEmpty(Contents))
                                    {//根据页面url 判断唯一性
                                        num = 0;
                                        String regexstr = @"<.*?>";//!/p去除所有标签，只剩img,br,p
                                        Contents = Regex.Replace(Contents, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                                        Contents = Regex.Replace(Contents, @"<!--.*?-->", "", RegexOptions.IgnoreCase);
                                        Contents = Regex.Replace(Contents, regexstr, string.Empty, RegexOptions.IgnoreCase);
                                        Contents = Contents.Replace("&nbsp;", "");
                                        Contents = Contents.Replace("&quot;", "");
                                        Contents = Contents.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ");
                                        if (Contents.Length < 100|| Contents.Contains("公网安备")|| Contents.Contains("邮箱：")|| Contents.Contains("互联网违法和不良信息举报电话：") )
                                            {
                                                worker.ReportProgress(10, "无效数据，跳过！");
                                                continue;
                                            }
                                        if (Convert.ToInt32(SQLHelper.Query("select count(*) from Common_QA where [Contents]='" + Contents + "'").Tables[0].Rows[0][0])
                                         > 0)
                                        {
                                            worker.ReportProgress(10, "已存在，跳过：" + Contents);
                                            continue;
                                        }
                                        worker.ReportProgress(10, "获取到【" + chnlcatalog + "】栏目的笑话");

                                            #region 数据存储操作
                                            StringBuilder sb = new StringBuilder();
                                            sb.Append("insert into Common_QA (");
                                            sb.Append("Title,");
                                            sb.Append("Contents,");
                                            sb.Append("TypeId,");
                                            sb.Append("displaying");
                                            sb.Append(")values (");

                                            sb.Append("@Title,");
                                            sb.Append("@Contents,");
                                            sb.Append("@TypeId,");
                                            sb.Append("@displaying");
                                            sb.Append(")");

                                            SqlParameter[] paras = new SqlParameter[]{
                                        new SqlParameter("@Title", SqlDbType.NVarChar, 500),
                                        new SqlParameter("@Contents",SqlDbType.NVarChar,int.MaxValue),
                                        new SqlParameter("@TypeId",SqlDbType.Int),
                                        new SqlParameter("@displaying",SqlDbType.Int)
                                        };
                                            paras[0].Value = "笑话";
                                            paras[1].Value = Contents;
                                            paras[2].Value = 4;
                                            paras[3].Value = 0;

                                            int i_insert = SQLHelper.ExecuteSql(sb.ToString(), paras);
                                            //lblAllAdd.Text = (Convert.ToInt32(lblAllAdd.Text) + i_insert).ToString();
                                            worker.ReportProgress(10, "新增完成，新增条数：" + i_insert);


                                        }
                                        #endregion
                                        Thread.Sleep(1000);
                                    
                                }
                                catch (Exception ex)
                                {
                                    worker.ReportProgress(10, "新闻异常:" + ex.Message);

                                }
                                #endregion

                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        worker.ReportProgress(10, "主异常:" + ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                worker.ReportProgress(10, "主异常:" + ex.Message);
            }
            #endregion
            worker.ReportProgress(10, "一轮回处理完成，休息" + SleepTime + "秒");

        }
        private void AutoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            WorkArgs args = e.Argument as WorkArgs;
            e.Result = args.Type;

            switch (args.Type)
            {
                case "开始":
                    DoWork(worker, args.Item);

                    break;

            }

            if (worker.CancellationPending)
                e.Cancel = true;


        }
        private void AutoWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                if (txtLog.Text.Length > 10000)
                {
                    txtLog.Text = "";
                    System.GC.Collect();
                }
                LogTxt(e.UserState.ToString());
                switch (e.ProgressPercentage)
                {
                    case 0:
                        MessageBox.Show(e.UserState.ToString());
                        break;
                    case 10:
                        txtLog.Text += DateTime.Now.ToString() + " " + e.UserState.ToString() + "\r\n";

                        txtLog.Select();
                        txtLog.Select(txtLog.TextLength, 0);
                        txtLog.ScrollToCaret();

                        toolStripStatusLabel1.Text = e.UserState.ToString();
                        break;
                    case 30:
                        toolStripStatusLabel1.Text = e.UserState.ToString();
                        break;
                    default:
                        MessageBox.Show(e.UserState.ToString());
                        toolStripStatusLabel1.Text = e.UserState.ToString();
                        break;
                }
            }
        }

        private void AutoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                toolStripStatusLabel1.Text = e.Error.Message;
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("操作取消。");
                toolStripStatusLabel1.Text = "您取消了操作";
            }
            else
            {
                toolStripStatusLabel1.Text = "";
            }

            //btnStart.Enabled = true;

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            btnStart.Enabled = false;
            count = 99999;
        }
        int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToString();

            if (!AutoWorker.IsBusy)
            {
                count++;
                if (count > SleepTime)
                {
                    count = 0;
                    btnStart.Enabled = false;
                    WorkArgs args = new WorkArgs() { Type = "开始", Item = null };
                    AutoWorker.RunWorkerAsync(args);
                }

            }
            else
            {
                //MessageBox.Show("后台线程正在工作，请等待上一个工作完成");
            }
        }
    }
}
