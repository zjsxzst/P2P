using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using P2P.DoMain;
using P2P;

namespace P2P_1._00._04
{
    public partial class Form1 : Form
    {
        #region  折叠代码 公共变量声明
        public bool KG = false;
        int ys = 0; //延时调用
        //String 临时1 = null;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GBL();
            逾期整理();
            操作日期.TodayDate = DateTime.Now;
            操作日期确认();
        }
        void 逾期整理()
        {
            SqlHelper sh = new SqlHelper();
            DateTime T = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), T1 = sh.rq(T, "月", -3), T2 = sh.rq(T, "日", -1);
            String sql =  string.Format("UPDATE [HSQD] SET ZTID=4 where ZTID=1 AND  GHR  < #{0}#", T ); // between = 日期之间 改为逾期
            //String sql = string.Format("UPDATE [HSQD] SET ZTID=4 where ZTID=1 AND  GHR  between #{0}# and #{1}#  ", T1, T2); // between = 日期之间 改为逾期
            ////sql[1] = string.Format("UPDATE [HSQD] SET ZTID=5 where ZTID=4 AND  GHR < #{0}# ", T1); //改为坏账
            //sh = new SqlHelper();
            DataTable dt   = sh.ExeQuery(sql);

            //if (sh.DiaoYongShiWu(sql)) MessageBox.Show("如你所愿");
            //else MessageBox.Show("输入失败"); //    事物调用
        }
        private void 坏账整理_Click(object sender, EventArgs e)
        {
            SqlHelper sh = new SqlHelper();
            DateTime T = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), T1 = sh.rq(T, "月", -3), T2 = sh.rq(T, "日", -1);
            String sql = string.Format("UPDATE [HSQD] SET ZTID=5 where ZTID=4 AND  GHR < #{0}# ", T1); //  //改为坏账
            DataTable dt = sh.ExeQuery(sql);
            定时1.Enabled = true ;
        }
        void GBL()
        {
            收益预览表.AllowUserToAddRows = false;
            收益预览表.AutoGenerateColumns = false;

            账户列表.AutoGenerateColumns = false;  //关闭自动产生列
            平台列表.AutoGenerateColumns = false;

            ////列表名称.AutoGenerateColumns = false;  //关闭自动产生列
            ////  列表名称.AllowUserToAddRows = false;  //关闭自动产生行
        } //关闭自动产生列
        void 收益预览()
        {
            SqlHelper sh = new SqlHelper();
            int GHRCount = 0;
            decimal SY = 0, BJ = 0;
            while (收益预览表.Rows.Count > 0) 收益预览表.Rows.RemoveAt(0); //逐条删除第一行
            DateTime T = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), T1 = sh.rq(T, "月", -6), T2 = sh.rq(T, "月", 6);

            //String sql = "SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD GROUP BY HSQD.GHR, HSQD.ZTID;";
            String sql = String.Format("SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD where  GHR>=#{0}# and GHR<=#{1}#   GROUP BY HSQD.GHR, HSQD.ZTID   order by GHR desc ",
                           T1.ToShortDateString().ToString(), T2.ToShortDateString().ToString());
            DataTable dt = sh.ExeQuery(sql);
            IList<HKXX> LHKXX = TableProcessing<HKXX>.DataTableToList(dt);
            List<HKXX> LHKXX_Bat = LHKXX.Where(m => m.GHR == T && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
                收益预览表.Rows.Add("今 日 应 收", LHKXX_Bat[0].GHRCount, LHKXX_Bat[0].BJ, LHKXX_Bat[0].SY, LHKXX_Bat[0].BJ + LHKXX_Bat[0].SY);

            T1 = sh.rq(T, "日", 1);
            LHKXX_Bat = LHKXX.Where(m => m.GHR == T1 && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
                收益预览表.Rows.Add("明 日 应 收", LHKXX_Bat[0].GHRCount, LHKXX_Bat[0].BJ, LHKXX_Bat[0].SY, LHKXX_Bat[0].BJ + LHKXX_Bat[0].SY);

            T1 = sh.rq(T, "日", 7);
            LHKXX_Bat = LHKXX.Where(m => m.GHR <= T1 && m.GHR >= T &&  m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("一周内  应收", GHRCount, BJ, SY, BJ + SY);
            }
            T1 = sh.rq(T, "月", 3);
            LHKXX_Bat = LHKXX.Where(m => m.GHR <= T1 && m.GHR >= T && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("未来3月应收", GHRCount, BJ, SY, BJ + SY);
            }
            T1 = sh.rq(T, "月", 6);
            LHKXX_Bat = LHKXX.Where(m => m.GHR <= T1 && m.GHR >= T && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("未来6月应收", GHRCount, BJ, SY, BJ + SY);
            }

            //收益预览表.Rows.Add(" ", " ", " ", " ", " ");

            T1 = sh.rq(T, "日", -1);
            LHKXX_Bat = LHKXX.Where(m => m.GHR == T1 && m.ZTID < 4 && m.ZTID > 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                  GHRCount = 0;  SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("昨 日 已 收", GHRCount, BJ, SY, BJ + SY);
            }

            T1 = sh.rq(T, "日", -7);
            LHKXX_Bat = LHKXX.Where(m => m.GHR >= T1 && m.GHR < T && m.ZTID < 4 && m.ZTID > 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("一周内  已收", GHRCount, BJ, SY, BJ + SY);
            }

            T1 = sh.rq(T, "月", -3);
            LHKXX_Bat = LHKXX.Where(m => m.GHR >= T1 && m.GHR <= T && m.ZTID < 4 && m.ZTID > 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("过去3月已收", GHRCount, BJ, SY, BJ + SY);
            }
            T1 = sh.rq(T, "月", -6);
            LHKXX_Bat = LHKXX.Where(m => m.GHR >= T1 && m.GHR <= T && m.ZTID < 4 && m.ZTID > 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("过去6月已收", GHRCount, BJ, SY, BJ + SY);
            }
            //收益预览表.Rows.Add(" ", " ", " ", " ", " ");
            T1 = sh.rq(T, "月", -3);
            LHKXX_Bat = LHKXX.Where(m => m.GHR >= T1 && m.GHR <= T && m.ZTID == 4).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                } 
                收益预览表.Rows.Add("逾   期   中", GHRCount, BJ, SY, BJ + SY);
            }

           sql = "SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD  where ZTID=5  GROUP BY HSQD.GHR, HSQD.ZTID ;";

            //sql = "SELECT sum(BJ) AS A, SUM(SY) AS B ,count(*) as  cnt FROM  HSQD  ";
            dt = sh.ExeQuery(sql);
            LHKXX = TableProcessing<HKXX>.DataTableToList(dt);
            LHKXX_Bat = LHKXX.Where(m =>  m.ZTID == 5).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                if (LHKXX_Bat.Count > 0)
                {
                    GHRCount = 0; SY = 0; BJ = 0;
                    foreach (var item in LHKXX_Bat)
                    {
                        BJ += item.BJ;
                        SY += item.SY;
                        GHRCount += item.GHRCount;
                    }
                    收益预览表.Rows.Add("坏       账", GHRCount, BJ, SY, BJ + SY);
                }
            }
        }
        #region  折叠代码 网页浏览
        private void WY1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY1.Tag.ToString()); //("iexplore.exe", WY1.Tag.ToString());
        }
        private void WY2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY2.Tag.ToString());
        }

        private void WY3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY3.Tag.ToString());
        }
        private void WY4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY4.Tag.ToString());
        }
        private void WY5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY5.Tag.ToString());
        }
        private void WY6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY6.Tag.ToString());
        }
        private void WY7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY7.Tag.ToString());
        }

        private void WY8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY8.Tag.ToString());
        }
        private void WY9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY9.Tag.ToString());
        }
        private void WY10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY10.Tag.ToString());
        }
        private void WY11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", WY11.Tag.ToString());
        }

        #endregion
        private void 操作日期_DateSelected(object sender, DateRangeEventArgs e)
        {
            操作日期确认();
        }
        void 操作日期确认()
        {
            //DateTime dt = DateTime.Now;
            //dt = 操作日期.SelectionStart;
            预览日期.Text = 操作日期.SelectionStart.ToShortDateString().ToString();
        }
        private void 多页_Selecting(object sender, TabControlCancelEventArgs e)
        {
            KG = false;
            String sql = "";
            SqlHelper sh = new SqlHelper();
            DataTable dt = new DataTable();
            switch (e.TabPage.Name)
            {
                case "关注信息":
                    收益预览();
                    break;
            }
        }
        private void 定时1_Tick(object sender, EventArgs e)
        {
            定时1.Enabled = false;
            收益预览();
        }


    }
}