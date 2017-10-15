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
        String 临时 = null;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            GBL();
            操作日期.TodayDate = DateTime.Now;
            操作日期确认();
            收益预览();
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            KG = true;
        }
        private void 逾期整理_Click(object sender, EventArgs e)
        {
            SqlHelper sh = new SqlHelper();
            DateTime T = Convert.ToDateTime(预览日期.Text);
            String sql = string.Format("UPDATE [HSQD] SET ZTID=4 where ZTID=1 AND  GHR  < #{0}#", T); // between = 日期之间 改为逾期
            DataTable dt = sh.ExeQuery(sql);
            ys = 3;
            定时1.Enabled = true;
        }
        private void 坏账整理_Click(object sender, EventArgs e)
        {
            SqlHelper sh = new SqlHelper();
            DateTime T = Convert.ToDateTime(预览日期.Text), T1 = sh.rq(T, "月标", -3);

            String sql = string.Format("SELECT ZCXX.ID, ZCXX.ZT, ZCXX.WLR, ZCXX.HZ, Sum(HSQD.BJ) AS BJ FROM (ZCXX LEFT JOIN TZLB ON ZCXX.ID = TZLB.ID) LEFT JOIN HSQD ON TZLB.DH = HSQD.DH WHERE (HSQD.ZTID)=4 AND (HSQD.GHR) < #{0}#   GROUP BY ZCXX.ID, ZCXX.ZT, ZCXX.WLR, ZCXX.HZ", T1);
            DataTable dt = sh.ExeQuery(sql);
            IList<HZCL> HZCL = TableProcessing<HZCL>.DataTableToList(dt);

            if (HZCL.Count > 0)
            {
                int X = 1;
                String[] sqL = new String[HZCL.Count + 1];
                foreach (var item in HZCL)
                {
                    item.ZT -= item.BJ;
                    item.WLR -= item.BJ;
                    item.HZ += item.BJ;
                    sqL[X] = string.Format("UPDATE [ZCXX] SET ZT={0},WLR={1},HZ={2}  where ID={3}", item.ZT, item.WLR, item.HZ, item.ID);
                    X++;
                }
                sqL[0] = string.Format("UPDATE [HSQD] SET ZTID=6  where GHR<#{0}# AND ZTID=4", T1);
                if (sh.DiaoYongShiWu(sqL)) MessageBox.Show("如你所愿");
                else MessageBox.Show("输入失败"); //    事物调用
            }
            ys = 3;
            定时1.Enabled = true;
        }
        void GBL()
        {

            收益预览表.AutoGenerateColumns = false;
            账户列表.AutoGenerateColumns = false;  //关闭自动产生列
            平台列表.AutoGenerateColumns = false;
            注册列表.AutoGenerateColumns = false;
            账户概况.AutoGenerateColumns = false;
            管理投资.AutoGenerateColumns = false;
            投资列表.AutoGenerateColumns = false;
            账户流水.AutoGenerateColumns = false;
            投资概况.AutoGenerateColumns = false;

            账户概况.AllowUserToAddRows = false;
            投资概况.AllowUserToAddRows = false;
            收益预览表.AllowUserToAddRows = false;
            ////列表名称.AutoGenerateColumns = false;  //关闭自动产生列
            ////  列表名称.AllowUserToAddRows = false;  //关闭自动产生行
        } //关闭自动产生列
        void 收益预览()
        {
            SqlHelper sh = new SqlHelper();
            int GHRCount = 0;
            decimal SY = 0, BJ = 0;
            while (收益预览表.Rows.Count > 0) 收益预览表.Rows.RemoveAt(0); //逐条删除第一行
            DateTime T = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), T1 = sh.rq(T, "月标", -6), T2 = sh.rq(T, "月标", 6);

            //String sql = "SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD GROUP BY HSQD.GHR, HSQD.ZTID;";
            String sql = String.Format("SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD where  GHR>=#{0}# and GHR<=#{1}#   GROUP BY HSQD.GHR, HSQD.ZTID   order by GHR desc ",
                           T1.ToShortDateString().ToString(), T2.ToShortDateString().ToString());
            DataTable dt = sh.ExeQuery(sql);
            IList<HKXX> LHKXX = TableProcessing<HKXX>.DataTableToList(dt);
            List<HKXX> LHKXX_Bat = LHKXX.Where(m => m.GHR == T && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
                收益预览表.Rows.Add("今 日 应 收", LHKXX_Bat[0].GHRCount, LHKXX_Bat[0].BJ, LHKXX_Bat[0].SY, LHKXX_Bat[0].BJ + LHKXX_Bat[0].SY);

            T1 = sh.rq(T, "日标", 1);
            LHKXX_Bat = LHKXX.Where(m => m.GHR == T1 && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
                收益预览表.Rows.Add("明 日 应 收", LHKXX_Bat[0].GHRCount, LHKXX_Bat[0].BJ, LHKXX_Bat[0].SY, LHKXX_Bat[0].BJ + LHKXX_Bat[0].SY);

            T1 = sh.rq(T, "日标", 7);
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
                收益预览表.Rows.Add("一周内  应收", GHRCount, BJ, SY, BJ + SY);
            }
            T1 = sh.rq(T, "月标", 1);
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
                收益预览表.Rows.Add("未来1月应收", GHRCount, BJ, SY, BJ + SY);
            }
            T1 = sh.rq(T, "月标", 3);
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
            T1 = sh.rq(T, "月标", 6);
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

            T1 = sh.rq(T, "日标", -1);
            LHKXX_Bat = LHKXX.Where(m => m.GHR == T1 && m.ZTID < 4 && m.ZTID > 1).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                GHRCount = 0; SY = 0; BJ = 0;
                foreach (var item in LHKXX_Bat)
                {
                    BJ += item.BJ;
                    SY += item.SY;
                    GHRCount += item.GHRCount;
                }
                收益预览表.Rows.Add("昨 日 已 收", GHRCount, BJ, SY, BJ + SY);
            }

            T1 = sh.rq(T, "日标", -7);
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
            T1 = sh.rq(T, "月标", -1);
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
                收益预览表.Rows.Add("过去1月已收", GHRCount, BJ, SY, BJ + SY);
            }
            T1 = sh.rq(T, "月标", -3);
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
            T1 = sh.rq(T, "月标", -6);
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
            T1 = sh.rq(T, "月标", -3);
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

            sql = "SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD  where ZTID=6  GROUP BY HSQD.GHR, HSQD.ZTID ;";

            //sql = "SELECT sum(BJ) AS A, SUM(SY) AS B ,count(*) as  cnt FROM  HSQD  ";
            dt = sh.ExeQuery(sql);
            LHKXX = TableProcessing<HKXX>.DataTableToList(dt);
            LHKXX_Bat = LHKXX.Where(m => m.ZTID == 6).ToList();
            if (LHKXX_Bat.Count > 0)
            {
                if (LHKXX_Bat.Count > 0)
                {
                    GHRCount = 0; SY = 0; BJ = 0;
                    foreach (var item in LHKXX_Bat)
                    {
                        BJ += item.BJ;
                        //SY += item.SY;
                        GHRCount += item.GHRCount;
                    }
                    收益预览表.Rows.Add("坏   账   中", GHRCount, BJ, 0, BJ);
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
        private void 平台列表_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", 平台列表.CurrentRow.Cells[5].Value.ToString());
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
                case "账户平台":
                    #region     
                    sql = "SELECT * FROM  ZHML Order by ZHID asc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                    SHUAXING(sql, 账户列表);
                    sql = "SELECT * FROM  PTZT Order by ZTID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ZT.DataSource = dt;
                        ZT.ValueMember = "ZTID";
                        ZT.DisplayMember = "PTZT";
                        ZT.DataPropertyName = "ZTID";
                    }
                    sql = "SELECT * FROM   PTXX  Order by PTID asc";
                    SHUAXING(sql, 平台列表);
                    #endregion
                    break;
                case "注册信息":
                    #region
                    sql = "SELECT * FROM  ZHML Order by ZHID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ZCZH.DataSource = dt;
                        ZCZH.ValueMember = "ZHID";
                        ZCZH.DisplayMember = "ZHMC";
                        ZCZH.DataPropertyName = "ZHID";
                    }
                    sql = "SELECT * FROM  PTXX Order by PTID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ZCPT.DataSource = dt;
                        ZCPT.ValueMember = "PTID";
                        ZCPT.DisplayMember = "PTMC";
                        ZCPT.DataPropertyName = "PTID";
                    }
                    sql = "SELECT * FROM  PTZT Order by ZTID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ZCZT.DataSource = dt;
                        ZCZT.ValueMember = "ZTID";
                        ZCZT.DisplayMember = "PTZT";
                        ZCZT.DataPropertyName = "ZTID";
                    }
                    sql = "SELECT * FROM  ZCXX  Order by ID asc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                    SHUAXING(sql, 注册列表);
                    #endregion
                    break;
                case "款项进出":
                    #region 
                    sql = "SELECT * FROM  ZHML Order by ZHID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        KXZH.DataSource = dt;
                        KXZH.ValueMember = "ZHID";
                        KXZH.DisplayMember = "ZHMC";
                        KXZH.DataPropertyName = "ZHID";
                    }
                    sql = "SELECT * FROM  PTXX Order by PTID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        KXPT.DataSource = dt;
                        KXPT.ValueMember = "PTID";
                        KXPT.DisplayMember = "PTMC";
                        KXPT.DataPropertyName = "PTID";
                    }
                    sql = "SELECT * FROM  ZCXX  Order by ID asc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                    SHUAXING(sql, 账户概况);
                    #endregion
                    break;
                case "投资管理":
                    #region  折叠代码 
                    //sql = "SELECT * FROM  PTZT Order by ZTID asc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                    //XULIE(sql, 管理方向, "ZTID", "PTZT");

                    sql = "SELECT * FROM  ZHML Order by ZHID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        GLZH.DataSource = dt;
                        GLZH.ValueMember = "ZHID";
                        GLZH.DisplayMember = "ZHMC";
                        GLZH.DataPropertyName = "ZHID";
                    }
                    sql = "SELECT * FROM  PTXX Order by PTID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        GLPT.DataSource = dt;
                        GLPT.ValueMember = "PTID";
                        GLPT.DisplayMember = "PTMC";
                        GLPT.DataPropertyName = "PTID";
                    }
                    sql = "SELECT ZCXX.ID, ZCXX.ZHID, ZCXX.PTID, ZCXX.ZG, ZCXX.ZT, ZCXX.HZ, PTXX.PTQT, PTXX.FWF, ZCXX.ZTID FROM ZCXX INNER JOIN PTXX ON ZCXX.PTID = PTXX.PTID";
                    sql = "  SELECT *   FROM  (" + sql + ") where ZTID=1";
                    SHUAXING(sql, 投资概况);

                    sql = "SELECT * FROM  QXFL Order by QXID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        GLQX.DataSource = dt;
                        GLQX.ValueMember = "QXID";
                        GLQX.DisplayMember = "QXFL";
                        GLQX.DataPropertyName = "QXID";
                    }

                    sql = "SELECT * FROM  BDFL Order by BDID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        GLBD.DataSource = dt;
                        GLBD.ValueMember = "BDID";
                        GLBD.DisplayMember = "BDFL";
                        GLBD.DataPropertyName = "BDID";
                    }
                    sql = "SELECT * FROM  HKFL Order by HKID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        GLHK.DataSource = dt;
                        GLHK.ValueMember = "HKID";
                        GLHK.DisplayMember = "HKFL";
                        GLHK.DataPropertyName = "HKID";
                    }
                    #endregion
                    break;
                case "赎回管理":
                    #region  折叠代码  
                    sql = "SELECT * FROM  BDFL Order by BDID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        HSLX.DataSource = dt;
                        HSLX.ValueMember = "BDID";
                        HSLX.DisplayMember = "BDFL";
                        HSLX.DataPropertyName = "BDID";
                    }
                    sql = "SELECT * FROM  HKFL Order by HKID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        HSFS.DataSource = dt;
                        HSFS.ValueMember = "HKID";
                        HSFS.DisplayMember = "HKFL";
                        HSFS.DataPropertyName = "HKID";
                    }
                    sql = "SELECT * FROM  QXFL Order by QXID asc";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        HSDW.DataSource = dt;
                        HSDW.ValueMember = "QXID";
                        HSDW.DisplayMember = "QXFL";
                        HSDW.DataPropertyName = "QXID";
                    }
                    sql = "SELECT * FROM  HKZT Order by ZTID asc";
                    XULIE(sql, GL_L_1, "ZTID", "HKZT");  //已移植到窗体载入时

                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        HSZT.DataSource = dt;
                        HSZT.ValueMember = "ZTID";
                        HSZT.DisplayMember = "HKZT";
                        HSZT.DataPropertyName = "ZTID";
                        QDZT.DataSource = dt;
                        QDZT.ValueMember = "ZTID";
                        QDZT.DisplayMember = "HKZT";
                        QDZT.DataPropertyName = "ZTID";
                    }
                    sql = "SELECT * FROM  TZLB where ZTID=1  Order by ID asc";
                    SHUAXING(sql, 管理投资);
                    #endregion
                    break;
                case "平台比重":
                    #region  折叠代码  
                    //      Sum(DSBJ) as abc    新字段定义   =   sum（原字段）AS 新字段
                    sql = "SELECT PTXX.PTMC, ZCXX.ZG, ZCXX.ZT, ZCXX.WLR, ZCXX.HZ FROM ZCXX INNER JOIN PTXX ON ZCXX.PTID = PTXX.PTID";

                    sql = "SELECT PTMC, Sum(ZG) as A,Sum(ZT) as B, Sum(WLR) as C, Sum(HZ) as D  FROM (" + sql + ")where ZG > 0  OR ZT > 0  GROUP BY PTMC ";
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //chart2
                        double[] yValues = new double[dt.Rows.Count], y1Values = new double[dt.Rows.Count], y2Values = new double[dt.Rows.Count],
                            y3Values = new double[dt.Rows.Count], y4Values = new double[dt.Rows.Count];
                        string[] xValues = new string[dt.Rows.Count];
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            yValues[i] = double.Parse(dr["A"].ToString()) + double.Parse(dr["B"].ToString());   // yValues[i] = double.Parse(dr["abc"].ToString());    //原句
                            y1Values[i] = double.Parse(dr["C"].ToString());
                            y2Values[i] = double.Parse(dr["D"].ToString());
                            y3Values[i] = double.Parse(dr["B"].ToString()) / 100;
                            y4Values[i] = double.Parse(dr["A"].ToString()) / 100;
                            xValues[i] = dr["PTMC"].ToString();
                            i++;
                        }
                        chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
                        chart2.Series["伪利润"].Points.DataBindXY(xValues, y1Values);
                        chart2.Series["坏账"].Points.DataBindXY(xValues, y2Values);
                        chart2.Series["在投资金"].Points.DataBindXY(xValues, y3Values);
                        chart2.Series["站岗资金"].Points.DataBindXY(xValues, y4Values);
                        //chart2.Series["Series1"].LegendText = "#VALX";
                        //chart2.Series["Series1"].Label = "#PERCENT{P}";
                    }
                    #endregion
                    break;
                case "回款分析":
                    分析();
                    break;
            }
            KG = true;
        }
        private void 收款金额_CheckedChanged(object sender, EventArgs e)
        {
            分析();
        }

        private void 收款次数_CheckedChanged(object sender, EventArgs e)
        {
            分析1();
        }
        void 分析()
        {
            SqlHelper sh = new SqlHelper();
            String sql = "";
            if (收款金额.Checked) { sql = "SELECT * FROM  C_JE";
            DataTable dt = sh.ExeQuery(sql);
            IList<HKFX> HKFX = TableProcessing<HKFX>.DataTableToList(dt);
                //List<HKXX> LHKXX_Bat = LHKXX.Where(m => m.GHR == T && m.ZTID == 1).ToList();
                if (HKFX.Count > 0)
                {
                    Decimal A = 0;
                    int i = 0;
                    //string[] xValues = new string[HKFX.Count];
                    //Decimal[] yValues = new Decimal[HKFX.Count], y1Values = new Decimal[HKFX.Count], y2Values = new Decimal[HKFX.Count], y3Values = new Decimal[HKFX.Count], y4Values = new Decimal[HKFX.Count];
                    List<string> xValues = new List<string>();
                    List<Decimal> yValues = new List<decimal>();
                    List<Decimal> y1Values = new List<decimal>();
                    List<Decimal> y2Values = new List<decimal>();
                    List<Decimal> y3Values = new List<decimal>();
                    List<Decimal> y4Values = new List<decimal>();
                    foreach (var item in HKFX)
                    {
                        A = item.提前回款 + item.正常回款 + item.逾期未收 + item.逾期已收 + item.坏账;
                        if (A > 0)
                        {
                            yValues.Add(decimal.Round(item.提前回款 / A * 100, 0));

                            y1Values.Add(decimal.Round(item.正常回款 / A * 100, 0));
                            y2Values.Add(decimal.Round(item.逾期未收 / A * 100, 0));
                            y3Values.Add(decimal.Round(item.逾期已收 / A * 100, 0));
                            y4Values.Add(decimal.Round(item.坏账 / A * 100, 0));
                            xValues.Add(item.平台名称);
                        }
                    }
                    chart3.Series["提前回款"].Points.DataBindXY(xValues, yValues);
                    chart3.Series["正常回款"].Points.DataBindXY(xValues, y1Values);
                    chart3.Series["逾期未收"].Points.DataBindXY(xValues, y2Values);
                    chart3.Series["逾期已收"].Points.DataBindXY(xValues, y3Values);
                    chart3.Series["坏账"].Points.DataBindXY(xValues, y4Values);
                }
            }
        }
        void 分析1()
        {
            SqlHelper sh = new SqlHelper();
            String sql = "";
            if (收款次数.Checked)
            {
                sql = "SELECT * FROM C_CS ";
                DataTable dt = sh.ExeQuery(sql);
                IList<HKFX1> HKFX = TableProcessing<HKFX1>.DataTableToList(dt);
                //List<HKXX> LHKXX_Bat = LHKXX.Where(m => m.GHR == T && m.ZTID == 1).ToList();
                if (HKFX.Count > 0)
                {
                    double A = 0;
                    int i = 0;
                    //string[] xValues = new string[HKFX.Count];
                    //Decimal[] yValues = new Decimal[HKFX.Count], y1Values = new Decimal[HKFX.Count], y2Values = new Decimal[HKFX.Count], y3Values = new Decimal[HKFX.Count], y4Values = new Decimal[HKFX.Count];
                    List<string> xValues = new List<string>();
                    List<double> yValues = new List<double>();
                    List<double> y1Values = new List<double>();
                    List<double> y2Values = new List<double>();
                    List<double> y3Values = new List<double>();
                    List<double> y4Values = new List<double>();
                    foreach (var item in HKFX)
                    {
                        A = item.提前回款 + item.正常回款 + item.逾期未收 + item.逾期已收 + item.坏账;
                        if (A > 0)
                        {

                            yValues.Add(Math.Round(item.提前回款 / A * 100, 0));

                            y1Values.Add(Math.Round(item.正常回款 / A * 100, 0));
                            y2Values.Add(Math.Round(item.逾期未收 / A * 100, 0));
                            y3Values.Add(Math.Round(item.逾期已收 / A * 100, 0));
                            y4Values.Add(Math.Round(item.坏账 / A * 100, 0));
                            xValues.Add(item.平台名称);
                        }
                    }
                    chart3.Series["提前回款"].Points.DataBindXY(xValues, yValues);
                    chart3.Series["正常回款"].Points.DataBindXY(xValues, y1Values);
                    chart3.Series["逾期未收"].Points.DataBindXY(xValues, y2Values);
                    chart3.Series["逾期已收"].Points.DataBindXY(xValues, y3Values);
                    chart3.Series["坏账"].Points.DataBindXY(xValues, y4Values);
                }
               
            }
        }
        void XULIE(String A, ComboBox C, String D, String D1)
        {
            SqlHelper sh = new SqlHelper();
            DataTable dt = sh.ExeQuery(A);
            if (dt.Rows.Count > 0)
            {
                C.DataSource = dt;
                C.ValueMember = D;
                C.DisplayMember = D1;
            }
        }
        private void 定时1_Tick(object sender, EventArgs e)
        {
            String sql = "";
            switch (ys)
            {
                case 1:
                    sql = "SELECT * FROM   PTXX  Order by PTID asc";
                    SHUAXING(sql, 平台列表);
                    break;
                case 2:
                    sql = "SELECT * FROM  ZCXX  Order by ID asc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                    SHUAXING(sql, 注册列表);
                    break;
                case 3:
                    收益预览();
                    break;
            }
            ys = 0;
            定时1.Enabled = false;
        }
        void SHUAXING(String A, DataGridView B)
        {
            SqlHelper sh = new SqlHelper();
            DataTable dt = sh.ExeQuery(A);

            if (dt.Rows.Count > 0) B.DataSource = dt;
            else
            {
                if (B.AllowUserToAddRows == false)   //关闭自动产生行
                    while (B.Rows.Count > 0) B.Rows.RemoveAt(0); //逐条删除第一行
                else
                    while (B.Rows.Count > 1) B.Rows.RemoveAt(0); //逐条删除第一行
            }
        }   //表格控件更新
        private void 账户列表_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (账户列表.CurrentRow.Cells[1].Value == null || 账户列表.CurrentRow.Cells[1].Value.ToString() == "") return;
            信息处理(账户列表, e, "ZHML");
        }
        void 信息处理(DataGridView A, DataGridViewCellEventArgs B, String ACC)
        {
            String Zi = null, Zi1 = A.CurrentRow.Cells[B.ColumnIndex].Value.ToString(), BT = null, BT1 = null;
            if (Zi1.Length > 0) Zi1 = Zi1.Replace(" ", "");
            if (Zi1.Length == 0)
            {
                MessageBox.Show("该活动单元格为空");
                return;
            }
            if (A.CurrentRow.Cells[1].Value == null || A.CurrentRow.Cells[1].Value.ToString() == "")
            {
                MessageBox.Show("该表的第一列为空禁止操作");
                return;   //停止执行代码
            }
   ;
            if (B.ColumnIndex == 1)
            {
                if (A.Name == "平台列表") A.CurrentRow.Cells[2].Value = 1;
                BT = A.Columns[1].DataPropertyName;
                if (临时 == "") Zi = A.CurrentRow.Cells[1].Value.ToString();
                else Zi = 临时;
                BT1 = BT;
            }
            else if (B.ColumnIndex > 1)
            {
                BT = A.Columns[1].DataPropertyName;
                BT1 = A.Columns[B.ColumnIndex].DataPropertyName;
                Zi = A.CurrentRow.Cells[1].Value.ToString();
            }
            String sql = "SELECT * FROM [" + ACC + "]  where " + BT + "='" + Zi + "'";
            SqlHelper sh = new SqlHelper();
            DataTable dt = sh.ExeQuery(sql);
            if (dt.Rows.Count > 0)
                sql = "UPDATE [" + ACC + "] SET " + BT1 + "='" + Zi1 + "'  where " + BT + "='" + Zi + "'";
            else
                sql = "insert into [" + ACC + "] (" + BT + ")values('" + Zi + "')";
            sh = new SqlHelper();
            sh.ExeNoQuery(sql);
            if (B.ColumnIndex == 1)
            {
                ys = 1;
                定时1.Enabled = true;
            }
        }
        private void 账户列表_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try { 临时 = 账户列表.CurrentRow.Cells[1].Value.ToString(); }
            catch { }
        }
        private void 投资列表_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try { 临时 = 投资列表.CurrentRow.Cells[e.ColumnIndex].Value.ToString(); }
            catch { }
        }

        private void 投资列表_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String sql = null;
                SqlHelper sh = new SqlHelper();
                //DataTable dt = sh.ExeQuery(sql);
                DataTable dt = new DataTable();
                DialogResult result = new DialogResult();
                #region  折叠代码  修改本金及收益
                if (e.ColumnIndex > 2 && e.ColumnIndex < 5)
                {
                    double X3 = Convert.ToDouble(投资列表.CurrentRow.Cells[e.ColumnIndex].Value.ToString());
                    X3 = Convert.ToDouble(X3.ToString("f2"));
                    投资列表.CurrentRow.Cells[e.ColumnIndex].Value = X3;
                    if (投资列表.AllowUserToAddRows == true)
                    {
                        #region  折叠代码  为活期时
                        if (投资列表.CurrentRow.Cells[0].Value.ToString() == "" || 投资列表.CurrentRow.Cells[0].Value.ToString() == null)
                        {
                            投资列表.CurrentRow.Cells[0].Value = 投资列表.Rows[e.RowIndex - 1].Cells[0].Value.ToString();
                            投资列表.CurrentRow.Cells[1].Value = 投资列表.Rows.Count - 1;
                            DateTime T = DateTime.Now;
                            投资列表.CurrentRow.Cells[2].Value = T.ToShortDateString().ToString();
                            投资列表.CurrentRow.Cells[5].Value = 1;
                            if (e.ColumnIndex == 3 && 投资列表.CurrentRow.Cells[4].Value.ToString() == "")
                                投资列表.CurrentRow.Cells[4].Value = 0;
                            else if (e.ColumnIndex == 4 && 投资列表.CurrentRow.Cells[3].Value.ToString() == "")
                                投资列表.CurrentRow.Cells[3].Value = 0;

                            string zi = "SELECT TZLB.DH, TZLB.ID, ZCXX.ZG, ZCXX.ZT, ZCXX.WLR, TZLB.SY, TZLB.BJ FROM TZLB INNER JOIN ZCXX ON TZLB.ID = ZCXX.ID",
                                zi1 = 投资列表.Rows[e.RowIndex - 1].Cells[0].Value.ToString();
                            sql = String.Format("SELECT *  FROM ({0})  where DH='{1}' ", zi, zi1);
                            sh = new SqlHelper();
                            dt = sh.ExeQuery(sql);
                            if (Convert.ToDouble(dt.Rows[0][2].ToString()) < Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString()))
                            {
                                result = MessageBox.Show("最多只能投资： " + dt.Rows[0][2].ToString(), "是否按站岗资金投资", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (result == DialogResult.Yes)     //如果点的是OK
                                {
                                    投资列表.CurrentRow.Cells[3].Value = dt.Rows[0][2].ToString();
                                }
                                else if (result == DialogResult.No)
                                {
                                    投资列表.CurrentRow.Cells[0].Value = "";
                                    //sql = String.Format("SELECT * FROM  HSQD  where DH='{0}'  ", zi1);
                                    //SHUAXING(sql, 投资列表); //  
                                    return;
                                }
                            }


                            X3 = Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            dt.Rows[0][2] = Convert.ToDouble(dt.Rows[0][2].ToString()) - X3;
                            dt.Rows[0][3] = Convert.ToDouble(dt.Rows[0][3].ToString()) + X3;
                            dt.Rows[0][6] = Convert.ToDouble(dt.Rows[0][6].ToString()) + X3;
                            X3 = Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString());
                            if (X3 < 0)
                            {
                                MessageBox.Show("收益为负数");
                                //投资列表.CurrentRow.Cells[0].Value = "";
                                投资列表.Rows.RemoveAt(e.RowIndex);
                                return;
                            }
                            dt.Rows[0][4] = Convert.ToDouble(dt.Rows[0][4].ToString()) + X3;
                            dt.Rows[0][5] = Convert.ToDouble(dt.Rows[0][5].ToString()) + X3;

                            X3 = Convert.ToDouble(dt.Rows[0][5].ToString()) + Convert.ToDouble(dt.Rows[0][6].ToString());
                            if (X3 < 0)
                            {
                                MessageBox.Show("取太多款了！ 最多只能在现有基础上：" + X3.ToString());
                                投资列表.CurrentRow.Cells[0].Value = "";
                                return;
                            }
                            String[] sql1 = new String[3];
                            //TZLB.DH, TZLB.ID, ZCXX.ZG, ZCXX.ZT, ZCXX.WLR, TZLB.SY, TZLB.BJ 
                            //   0        1        2         3         4         5        6
                            sql1[0] = String.Format("insert into [HSQD] values('{0}',{1},#{2}# ,{3},{4},{5})",
                                投资列表.CurrentRow.Cells[0].Value.ToString(), 投资列表.CurrentRow.Cells[1].Value.ToString(),
                                投资列表.CurrentRow.Cells[2].Value.ToString(), 投资列表.CurrentRow.Cells[3].Value.ToString(),
                                投资列表.CurrentRow.Cells[4].Value.ToString(), 1);

                            sql1[1] = String.Format("UPDATE[ZCXX] SET ZG ={0},ZT ={1},WLR={2}  where ID = {3}", dt.Rows[0][2].ToString(),
                                dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][1].ToString());
                            sql1[2] = String.Format("UPDATE[TZLB] SET  SY={0}, BJ={1}  where DH = '{2}'",
                                dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString(), dt.Rows[0][0].ToString());
                            result = MessageBox.Show("确定投资", "确定信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)     //如果点的是OK
                            {
                                if (sh.DiaoYongShiWu(sql1)) MessageBox.Show("如你所愿");
                                else MessageBox.Show("输入失败"); // 
                            }
                            else if (result == DialogResult.No)
                            {
                                投资列表.CurrentRow.Cells[0].Value = "";
                                return;
                            }

                        }
                        else
                            MessageBox.Show("活期信息只允许添加信息");
                        #endregion
                    }
                    else
                    {
                        if (投资列表.CurrentRow.Cells[5].Value.ToString() != "2")
                        {
                            //投资列表.CurrentRow.Cells[3].Value.ToString();
                            //投资列表.CurrentRow.Cells[4].Value.ToString();
                            sql = String.Format("UPDATE [HSQD] SET BJ={0},SY={1} where DH='{2}' AND QS={3}",
                                   投资列表.CurrentRow.Cells[3].Value.ToString(), 投资列表.CurrentRow.Cells[4].Value.ToString(),
                                   投资列表.CurrentRow.Cells[0].Value.ToString(), 投资列表.CurrentRow.Cells[1].Value.ToString());
                            sh = new SqlHelper();
                            dt = sh.ExeQuery(sql);
                        }
                        else
                        {
                            投资列表.CurrentRow.Cells[e.ColumnIndex].Value = 临时;
                            MessageBox.Show("已回收状态禁止修改");
                        }
                        return;
                    }
                }
                #endregion
                #region  折叠代码  修改收款状态
                else if (e.ColumnIndex == 5)
                {
                    if (投资列表.AllowUserToAddRows == true)
                    {
                        MessageBox.Show("活期直接加减");
                        return;
                    }
                    String zi = "SELECT HSQD.DH, HSQD.QS, TZLB.ZQ, TZLB.FQ, TZLB.HB, TZLB.SY, TZLB.ZTID, ZCXX.ZG, ZCXX.ZT, ZCXX.WLR, ZCXX.HZ,TZLB.ID FROM HSQD INNER JOIN (TZLB INNER JOIN ZCXX ON TZLB.ID = ZCXX.ID) ON HSQD.DH = TZLB.DH";
                    sql = String.Format("SELECT *  FROM  ({0})  where DH='{1}' AND QS={2}",
                                   zi, 投资列表.CurrentRow.Cells[0].Value.ToString(), 投资列表.CurrentRow.Cells[1].Value.ToString());
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    // HSQD.DH =单号 TZLB.ID= 账户&平台 
                    // 1=未回收 2=已回收 3=逾期 4=坏账 5=提前回收
                    // 1=未回收 2=提前收 3=正常期 4=逾期 5=逾期收 6=坏账
                    //zi = 临时 + "TO" + 投资列表.CurrentRow.Cells[5].Value;
                    switch (临时 + "TO" + 投资列表.CurrentRow.Cells[5].Value)
                    {
                        case "1TO2":
                            投资列表.CurrentRow.Cells[5].Value = 日期计算(投资列表.CurrentRow.Cells[2].Value.ToString()); //收款状态监测
                            zi = "1TO2";
                            break;
                        case "1TO3":
                            投资列表.CurrentRow.Cells[5].Value = 日期计算(投资列表.CurrentRow.Cells[2].Value.ToString());//收款状态监测
                            zi = "1TO2";
                            break;
                        case "1TO5":
                            投资列表.CurrentRow.Cells[5].Value = 日期计算(投资列表.CurrentRow.Cells[2].Value.ToString());//收款状态监测
                            zi = "1TO2";
                            break;

                        case "4TO2":
                            投资列表.CurrentRow.Cells[5].Value = 日期计算(投资列表.CurrentRow.Cells[2].Value.ToString()); //收款状态监测
                            break;
                        case "4TO3":
                            投资列表.CurrentRow.Cells[5].Value = 日期计算(投资列表.CurrentRow.Cells[2].Value.ToString());//收款状态监测
                            zi = "1TO2";
                            break;
                        case "4TO5":
                            投资列表.CurrentRow.Cells[5].Value = 日期计算(投资列表.CurrentRow.Cells[2].Value.ToString());//收款状态监测
                            zi = "1TO2";
                            break;

                        case "2TO1":
                            zi = "2TO1";
                            break;
                        case "3TO1":
                            zi = "2TO1";
                            break;
                        case "5TO1":
                            zi = "2TO1";
                            break;
                        case "6TO1":
                            zi = "6TO1";
                            break;
                            //case "3TO4":
                            //    zi = "1TO4";
                            //    break;
                    }

                    switch (zi)
                    {
                        case "1TO2": //收款处理
                            if (投资列表.CurrentRow.Cells[1].Value.ToString() != "0") dt.Rows[0][3] = Convert.ToInt32(dt.Rows[0][3].ToString()) + 1;
                            if (dt.Rows[0][3].ToString() == dt.Rows[0][2].ToString()) dt.Rows[0][6] = 3;
                            //MessageBox.Show(dt.Rows[0][3].ToString() + "|" +dt.Rows[0][2].ToString());
                            dt.Rows[0][4] = Convert.ToDouble(dt.Rows[0][4].ToString()) + Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //已归还本金
                            dt.Rows[0][5] = Convert.ToDouble(dt.Rows[0][5].ToString()) + Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString());
                            //本次投资毛收益
                            dt.Rows[0][7] = Convert.ToDouble(dt.Rows[0][7].ToString()) + Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString())
                                + Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //站岗资金
                            dt.Rows[0][8] = Convert.ToDouble(dt.Rows[0][8].ToString()) - Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //在投资金
                            dt.Rows[0][9] = Convert.ToDouble(dt.Rows[0][9].ToString()) + Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString());
                            //伪利润
                            break;
                        case "2TO1": //收款反处理
                            if (投资列表.CurrentRow.Cells[5].Value.ToString() != "0") dt.Rows[0][3] = Convert.ToInt32(dt.Rows[0][3].ToString()) - 1;
                            dt.Rows[0][6] = 1;
                            dt.Rows[0][4] = Convert.ToDouble(dt.Rows[0][4].ToString()) - Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //已归还本金
                            dt.Rows[0][5] = Convert.ToDouble(dt.Rows[0][5].ToString()) - Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString());
                            //本次投资毛收益
                            dt.Rows[0][7] = Convert.ToDouble(dt.Rows[0][7].ToString()) - Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString())
                                - Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //站岗资金
                            dt.Rows[0][8] = Convert.ToDouble(dt.Rows[0][8].ToString()) + Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //在投资金
                            dt.Rows[0][9] = Convert.ToDouble(dt.Rows[0][9].ToString()) - Convert.ToDouble(投资列表.CurrentRow.Cells[4].Value.ToString());
                            break;
                        case "6TO1": //坏账反处理
                            dt.Rows[0][6] = 1;
                            dt.Rows[0][8] = Convert.ToDouble(dt.Rows[0][8].ToString()) + Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            //在投资金

                            dt.Rows[0][10] = Convert.ToDouble(dt.Rows[0][10].ToString()) - Convert.ToDouble(投资列表.CurrentRow.Cells[3].Value.ToString());
                            // 坏账

                            break;
                        default:
                            投资列表.CurrentRow.Cells[5].Value = 临时;
                            MessageBox.Show("逾期  &坏账 在首页整理！");
                            return;
                    }
                    String[] sql1 = new String[3];
                    sql1[0] = String.Format("UPDATE [ZCXX] SET ZG={0},ZT={1}, WLR={2},HZ={3} where ID={4} ",
 dt.Rows[0][7].ToString(), dt.Rows[0][8].ToString(), dt.Rows[0][9].ToString(), dt.Rows[0][10].ToString(), dt.Rows[0][11].ToString());
                    sql1[1] = String.Format("UPDATE [TZLB] SET FQ={0},HB={1}, SY={2},ZTID={3} where DH='{4}' ",
 dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString(), dt.Rows[0][0].ToString());
                    sql1[2] = String.Format("UPDATE [HSQD] SET ZTID={0} where DH='{1}' AND QS={2}",
 投资列表.CurrentRow.Cells[5].Value, dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString());

                    sh = new SqlHelper();
                    if (sh.DiaoYongShiWu(sql1)) MessageBox.Show("如你所愿");
                    else MessageBox.Show("输入失败"); //    事物调用
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex);
                return;   //停止执行代码
            }
        }
        int 日期计算(String A)
        {
            DateTime T = Convert.ToDateTime(预览日期.Text);
            DateTime T1 = Convert.ToDateTime(A);
            if (T > T1) return 5;
            else if (T < T1) return 2;
            else
                return 3;
            // 1=未回收 2=提前收 3=正常期 4=逾期 5=逾期收 6=坏账
        }

        private void 平台列表_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try { 临时 = 平台列表.CurrentRow.Cells[1].Value.ToString(); }
            catch { }
        }
        private void 平台列表_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            信息处理(平台列表, e, "PTXX");
        }

        private void 注册列表_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (注册列表.CurrentRow.Cells[e.ColumnIndex].Value == null || 注册列表.CurrentRow.Cells[e.ColumnIndex].Value.ToString() == "") return;
            String sql = 注册列表.CurrentRow.Cells[e.ColumnIndex].Value.ToString();
            if (sql.Length > 0) sql = sql.Replace(" ", "");
            if (sql.Length == 0)
            {
                MessageBox.Show("该活动单元格为空");
                return;
            }
            if (注册列表.CurrentRow.Cells[1].Value == null || 注册列表.CurrentRow.Cells[1].Value.ToString() == "") return;
            if (注册列表.CurrentRow.Cells[2].Value == null || 注册列表.CurrentRow.Cells[2].Value.ToString() == "") return;

            SqlHelper sh = new SqlHelper();
            DataTable dt = new DataTable();

            //String sql = "SELECT * FROM  PTZT Order by ZTID asc";
            //SqlHelper sh = new SqlHelper();
            //DataTable dt = sh.ExeQuery(sql);
            if (e.ColumnIndex > 2)
            {
                // MessageBox.Show(账户列表.Columns[e.ColumnIndex].DataPropertyName); //获取数据库内部表头
                sql = "UPDATE [zcxx] SET " + 注册列表.Columns[e.ColumnIndex].DataPropertyName + "= '" + 注册列表.CurrentRow.Cells[e.ColumnIndex].Value.ToString() + "'  where id=" + 注册列表.CurrentRow.Cells[0].Value;
                sh = new SqlHelper();
                sh.ExeNoQuery(sql);
            }
            else
            {
                if (注册列表.CurrentRow.Cells[0].Value == null || 注册列表.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    sql = "SELECT * FROM  ZCXX  where ZHID=" + 注册列表.CurrentRow.Cells[1].Value + " AND PTID=" + 注册列表.CurrentRow.Cells[2].Value;  // Order by ZTID asc
                    sh = new SqlHelper();
                    dt = sh.ExeQuery(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = string.Format("insert into [ZCXX](ZHID, PTID)values({0},{1} )", 注册列表.CurrentRow.Cells[1].Value, 注册列表.CurrentRow.Cells[2].Value);
                        sh = new SqlHelper();
                        sh.ExeNoQuery(sql);
                    }
                    ys = 2;
                    定时1.Enabled = true;
                }
            }
        }

        private void 账户概况_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (账户流水.Rows[0].Cells[0].Value != 账户概况.CurrentRow.Cells[0].Value)
            {
                string sql = "SELECT * FROM  XJLS where ID=" + 账户概况.CurrentRow.Cells[0].Value + "  Order by RQ desc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                SHUAXING(sql, 账户流水);
            }
        }
        private void 投资概况_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //MessageBox.Show(投资概况.CurrentRow.Cells[10].Value.ToString());
            //MessageBox.Show(投资概况.CurrentRow.Cells[10].FormattedValue.ToString());
            if (e.ColumnIndex > 11)
            {
                Double A = 0;
                try
                {
                    A = Convert.ToDouble(投资概况.CurrentRow.Cells[e.ColumnIndex].Value);
                    A = Convert.ToDouble(A.ToString("f2"));
                    投资概况.CurrentRow.Cells[e.ColumnIndex].Value = A.ToString();
                    if (A == 0 && e.ColumnIndex < 15)
                    {
                        MessageBox.Show("0元？ 开玩笑？");
                        return;   //停止执行代码
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误" + ex);
                    投资概况.CurrentRow.Cells[e.ColumnIndex].Value = 1;
                    return;   //停止执行代码
                }
                A = 0;
                for (int i = 8; i < 16; i++)
                {
                    if (投资概况.CurrentRow.Cells[i].Value != null)
                        if (投资概况.CurrentRow.Cells[i].Value.ToString().Length > 0) A++;
                }
                if (A == 8)
                {
                    DialogResult result;
                    if (Convert.ToDouble(投资概况.CurrentRow.Cells[7].Value.ToString()) > Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()))
                    {
                        //  MessageBox.Show("未到起投条件");
                        result = MessageBox.Show("是否强投", "未到起投条件", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No) return;
                    }
                    if (Convert.ToDouble(投资概况.CurrentRow.Cells[3].Value.ToString()) < Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()))
                    {
                        MessageBox.Show("站岗资金不够");
                        return;
                    }


                    result = MessageBox.Show("是否确定投资", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)     //如果点的是OK
                    {
                        List<Bt> FH = null;
                        var LI = Convert.ToDouble(投资概况.CurrentRow.Cells[12].Value.ToString()) / 100; //利率
                        int QI = int.Parse(投资概况.CurrentRow.Cells[14].Value.ToString());  //期限
                        var FEI = (100 - Convert.ToDouble(投资概况.CurrentRow.Cells[6].Value.ToString())) / 100;    //折算减去利息费后的百分比
                        switch (投资概况.CurrentRow.Cells[10].Value.ToString())
                        {
                            case "1":
                                LI = LI / 360;           //换算成日利息
                                FH = JX.TB(Convert.ToDouble(投资概况.CurrentRow.Cells[14].Value.ToString()), LI, 1, FEI);
                                break;
                            case "2":
                                LI = LI / 12.0;    // 算成月息
                                break;
                                //case "3":
                                //    break;
                                //default:
                                //    break;
                        }
                        //TZCL(投资概况.CurrentRow.Cells[9].Value.ToString(), 投资概况.CurrentRow.Cells[13].Value.ToString(), LI, QI, FEI);
                        if (投资概况.CurrentRow.Cells[10].Value.ToString() != "1")
                        {
                            switch (投资概况.CurrentRow.Cells[9].Value.ToString())
                            {
                                case "1":  //"还息续投"
                                    FH = JX.HXXT(Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()), LI, QI, FEI);
                                    break;
                                case "2":  // "一次性本息":
                                    FH = JX.YcxBx(Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()), LI, QI, FEI);
                                    break;
                                case "3":  //"先息后本":
                                    FH = JX.XXHB(Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()), LI, QI, FEI);
                                    break;
                                case "4":  // "等额本金":
                                    FH = JX.DEBJ(Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()), LI, QI, FEI);
                                    break;
                                case "5":  //"等额本息":
                                    FH = JX.DEBX(Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString()), LI, QI, FEI);
                                    break;
                            }
                        }
                        //MessageBox.Show(FH[0].BJ.ToString());    //读取列表的例子  //Convert.ToDouble(dt.Rows[0][7].ToString());
                        投资概况.CurrentRow.Cells[4].Value = Convert.ToDouble(投资概况.CurrentRow.Cells[4].Value.ToString()) + Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString());
                        投资概况.CurrentRow.Cells[3].Value = Convert.ToDouble(投资概况.CurrentRow.Cells[3].Value.ToString()) - Convert.ToDouble(投资概况.CurrentRow.Cells[13].Value.ToString());

                        DateTime T = DateTime.Now, T1;

                        String DH = "D" + T.ToFileTimeUtc(), BIAO = 投资概况.CurrentRow.Cells[10].Value.ToString();

                        DH = DH.Remove(0, DH.Length - 10);
                        T = 操作日期.SelectionStart;



                        DH = "D(" + T.ToShortDateString() + ")" + DH;
                        String[] sql = new String[FH.Count + 2];

                        SqlHelper sh = new SqlHelper();
                        if (Convert.ToDouble(投资概况.CurrentRow.Cells[15].Value.ToString()) > 0)
                        {
                            sql = new String[FH.Count + 3];
                            sql[FH.Count + 2] = String.Format("insert into [HSQD] values('{0}',{1},#{2}# ,{3},{4},{5})",
                           DH, 0, T.ToShortDateString().ToString(), 0, 投资概况.CurrentRow.Cells[15].Value.ToString(), 1);
                            //          DH  QS  GHR   BJ   SY   ZT
                        }
                        sql[FH.Count + 1] = String.Format("UPDATE [ZCXX] SET ZG={0},ZT={1} where ID={2} ",
                            投资概况.CurrentRow.Cells[3].Value.ToString(), 投资概况.CurrentRow.Cells[4].Value.ToString(), 投资概况.CurrentRow.Cells[0].Value.ToString());

                        if (投资概况.CurrentRow.Cells[9].Value.ToString() != "1")

                            sql[FH.Count] = String.Format("insert into [TZLB] values({0},'{1}',{2} ,{3},{4},'{5}',{6},{7},{8} ,{9},{10},{11})",
                                                           投资概况.CurrentRow.Cells[0].Value.ToString(), DH, 投资概况.CurrentRow.Cells[8].Value.ToString(),
                                                           投资概况.CurrentRow.Cells[9].Value.ToString(), BIAO,
                                                           投资概况.CurrentRow.Cells[11].Value.ToString(), 投资概况.CurrentRow.Cells[14].Value.ToString(),
                                                           0, 投资概况.CurrentRow.Cells[13].Value.ToString(), 0, 0, 1);

                        else
                            sql[FH.Count] = String.Format("insert into [TZLB] values({0},'{1}',{2} ,{3},{4},'{5}',{6},{7},{8} ,{9},{10},{11})",
                                                           投资概况.CurrentRow.Cells[0].Value.ToString(), DH, 投资概况.CurrentRow.Cells[8].Value.ToString(),
                                                           投资概况.CurrentRow.Cells[9].Value.ToString(), BIAO,
                                                           投资概况.CurrentRow.Cells[11].Value.ToString() + "(" + T.ToShortDateString().ToString() + ")", 0,
                                                           0, 投资概况.CurrentRow.Cells[13].Value.ToString(), 0, 0, 1);

                        BIAO = 投资概况.CurrentRow.Cells[10].FormattedValue.ToString();
                        for (int X = 0; X < FH.Count; X++)
                        {

                            T1 = sh.rq(T, BIAO, X + 1);
                            sql[X] = String.Format("insert into [HSQD] values('{0}',{1},#{2}# ,{3},{4},{5})",
                                DH, FH[X].QS.ToString(), T1.ToShortDateString().ToString(), FH[X].BJ.ToString(), FH[X].SY.ToString(), 1);
                        }
                        if (sh.DiaoYongShiWu(sql)) MessageBox.Show("如你所愿");
                        else MessageBox.Show("输入失败"); //    事物调用
                        for (int X = 11; X < 16; X++) 投资概况.CurrentRow.Cells[X].Value = "";
                    }
                }
            }

        }

        private void 管理投资_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (管理投资.CurrentRow.Cells[1].Value == null || 管理投资.CurrentRow.Cells[1].Value.ToString() == "") return;
            if (gbzn.Checked == false) return;
            String sql = String.Format("SELECT * FROM  HSQD  where DH='{0}'  Order by QS asc", 管理投资.CurrentRow.Cells[1].Value.ToString());
            SHUAXING(sql, 投资列表);

            if (管理投资.CurrentRow.Cells[3].Value.ToString() == "1")
            {
                投资列表.AllowUserToAddRows = true;
                投资列表.Rows[投资列表.Rows.Count - 1].Selected = true;   // 选中最后一行
            }
            else 投资列表.AllowUserToAddRows = false;
        }

        private void 管理投资_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string hhh = "";
            if (e.ColumnIndex == 1)
            {
                hhh = Microsoft.VisualBasic.Interaction.InputBox("请输入查找【单号】", "查找");
                if (hhh.Length > 0)
                {
                    String sql = String.Format("SELECT * FROM  TZLB where DH  LIKE  '%{0}%'", hhh);  //模糊查找 表格 TZLB 中，字段位DH中带 “xxx”
                    SHUAXING(sql, 管理投资);
                }
            }

            if (e.ColumnIndex == 5)
            {
                hhh = Microsoft.VisualBasic.Interaction.InputBox("请输入查找【项目】", "查找");
                if (hhh.Length > 0)
                {
                    String sql = String.Format("SELECT * FROM  TZLB where XM  LIKE  '%{0}%'", hhh);
                    SHUAXING(sql, 管理投资);
                }
            }
            bool KG1 = true; //调整位置
            gbzn.Checked = true; //调整位置
            调整位置(KG1); //调整位置
        }
        void 调整位置(bool KG1)
        {
            if (KG1)
            {
                //this.管理投资.Visible = true;
                this.管理投资.Size = new System.Drawing.Size(942, 471);
                this.投资列表.Size = new System.Drawing.Size(375, 471);//大小
                this.投资列表.Location = new System.Drawing.Point(954, 6);//位置
                this.Column37.Visible = false;
            }
            else
            {
                //this.管理投资.Visible = false;

                this.管理投资.Size = new System.Drawing.Size(780, 471);
                this.投资列表.Size = new System.Drawing.Size(530, 471);
                this.投资列表.Location = new System.Drawing.Point(800, 6);
                this.Column37.Visible = true;

            }

        }

        private void zjys_Enter(object sender, EventArgs e)
        {
            DateTime T = DateTime.Now, T1 = T;
            SqlHelper sh = new SqlHelper();
            T1 = sh.rq(T, "月标", -2);
            // String sql = String.Format("SELECT top 100 * FROM  HSQD  where ZTID=2 AND GHR  between  #{0}# and #{1}#   Order by GHR desc ",  T.ToShortDateString().ToString(), T1.ToShortDateString().ToString());
            String sql = String.Format("SELECT top 100 * FROM  HSQD  where   ZTID>1 AND ZTID<4  OR ZTID=5  Order by GHR desc ");
            SHUAXING(sql, 投资列表); // 
            bool KG1 = false;
            调整位置(KG1);
        }//已收

        private void zjws_Enter(object sender, EventArgs e)
        {
            DateTime T = DateTime.Now, T1 = T;
            SqlHelper sh = new SqlHelper();
            T1 = sh.rq(T, "月标", 2);
            //String sql = String.Format("SELECT * FROM  HSQD  where ZTID=1 AND GHR  between #{0}# and #{1}#  Order by GHR asc ",  T.ToShortDateString().ToString(), T1.ToShortDateString().ToString());
            String sql = String.Format("SELECT top 100 * FROM  HSQD  where ZTID=1   Order by GHR asc ");
            SHUAXING(sql, 投资列表); //                                        出库日期 between c Order by GHR as ZT=1 AND
            bool KG1 = false;
            调整位置(KG1);
        } //未收
        private void radioButton1_Enter(object sender, EventArgs e)
        {
            DateTime T = DateTime.Now, T1 = T;
            SqlHelper sh = new SqlHelper();
            T1 = sh.rq(T, "月标", 2);
            //String sql = String.Format("SELECT * FROM  HSQD  where ZTID=1 AND GHR  between #{0}# and #{1}#  Order by GHR asc ",  T.ToShortDateString().ToString(), T1.ToShortDateString().ToString());
            String sql = String.Format("SELECT top 100 * FROM  HSQD  where ZTID=4   Order by GHR asc ");
            SHUAXING(sql, 投资列表); //                                        出库日期 between c Order by GHR as ZT=1 AND
            bool KG1 = false;
            调整位置(KG1);
        }//逾期
        private void radioButton2_Enter(object sender, EventArgs e)
        {
            DateTime T = DateTime.Now, T1 = T;
            SqlHelper sh = new SqlHelper();
            T1 = sh.rq(T, "月标", 2);
            //String sql = String.Format("SELECT * FROM  HSQD  where ZTID=1 AND GHR  between #{0}# and #{1}#  Order by GHR asc ",  T.ToShortDateString().ToString(), T1.ToShortDateString().ToString());
            String sql = String.Format("SELECT top 100 * FROM  HSQD  where ZTID=5   Order by GHR asc ");
            SHUAXING(sql, 投资列表); //                                        出库日期 between c Order by GHR as ZT=1 AND
            bool KG1 = false;
            调整位置(KG1);
        }
        private void gbzn_Enter(object sender, EventArgs e)
        {
            bool KG1 = true;
            调整位置(KG1);
            //this.管理投资.Size = new System.Drawing.Size(839, 402);
            //原始大小 942, 402//this.管理投资.Size = new System.Drawing.Size(942, 402);
        }

        private void GL_L_1_DropDownClosed(object sender, EventArgs e)
        {
            if (KG)
            {
                String sql = String.Format("SELECT * FROM  TZLB where ZTID={0}  Order by ID asc", GL_L_1.SelectedValue.ToString());
                SHUAXING(sql, 管理投资);
                if (投资列表.AllowUserToAddRows == false)   //关闭自动产生行
                    while (投资列表.Rows.Count > 0) 投资列表.Rows.RemoveAt(0); //逐条删除第一行
                else
                    while (投资列表.Rows.Count > 1) 投资列表.Rows.RemoveAt(0); //逐条删除第一行
            }
        }

        private void 账户概况_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex > 8)
            {
                Double A = 0;
                try
                {
                    A = Convert.ToDouble(账户概况.CurrentRow.Cells[e.ColumnIndex].Value);
                    A = Convert.ToDouble(A.ToString("f2"));
                    账户概况.CurrentRow.Cells[e.ColumnIndex].Value = A.ToString();
                    if (A == 0)
                    {
                        MessageBox.Show("0元？ 开玩笑？");
                        return;   //停止执行代码
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误" + ex);
                    return;   //停止执行代码
                }
                SqlHelper sh = new SqlHelper();
                DialogResult result = MessageBox.Show("是否过账", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)     //如果点的是OK
                {
                    DateTime T = DateTime.Now;
                    //  DateTime T = 操作日期.SelectionStart;
                    String[] sql = new String[2];
                    String DH = T.ToFileTimeUtc().ToString();

                    //  DH = DH.Remove(0, DH.Length - 10);
                    //T = 操作日期.SelectionStart;



                    if (e.ColumnIndex == 9)
                    {
                        账户概况.CurrentRow.Cells[3].Value = A + Convert.ToDouble(账户概况.CurrentRow.Cells[3].Value);
                        账户概况.CurrentRow.Cells[5].Value = A + Convert.ToDouble(账户概况.CurrentRow.Cells[5].Value);
                        sql[0] = string.Format("insert into [XJLS](ID, DH, RQ, BJ, SM)values({0},'{1}',#{2}#,{3},'{4}' )",
                            账户概况.CurrentRow.Cells[0].Value, "C" + DH, 操作日期.SelectionStart.ToShortDateString(),
                            账户概况.CurrentRow.Cells[9].Value, "现金充值");
                        sql[1] = string.Format("UPDATE [ZCXX] SET CZ={0}, ZG={1} where ID={2} ", 账户概况.CurrentRow.Cells[3].Value, 账户概况.CurrentRow.Cells[5].Value, 账户概况.CurrentRow.Cells[0].Value);
                    }
                    else if (e.ColumnIndex == 10)
                    {
                        if (Convert.ToDouble(账户概况.CurrentRow.Cells[5].Value) - A < 0)
                        {
                            MessageBox.Show("错误: 站岗资金不够！无法提现");
                            return;   //停止执行代码
                        }
                        账户概况.CurrentRow.Cells[4].Value = A + Convert.ToDouble(账户概况.CurrentRow.Cells[4].Value);
                        账户概况.CurrentRow.Cells[5].Value = Convert.ToDouble(账户概况.CurrentRow.Cells[5].Value) - A;
                        sql[0] = string.Format("insert into [XJLS](ID, DH, RQ, BJ, SM)values({0},'{1}',#{2}#,{3},'{4}' )",
                            账户概况.CurrentRow.Cells[0].Value, "T" + DH, 操作日期.SelectionStart.ToShortDateString(),
                            "-" + 账户概况.CurrentRow.Cells[10].Value, "提现金");
                        sql[1] = string.Format("UPDATE [ZCXX] SET TK={0}, ZG={1}  where ID={2} ", 账户概况.CurrentRow.Cells[4].Value, 账户概况.CurrentRow.Cells[5].Value, 账户概况.CurrentRow.Cells[0].Value);
                    }
                    sh.DiaoYongShiWu(sql);
                    账户概况.CurrentRow.Cells[e.ColumnIndex].Value = 0;
                    //MessageBox.Show("如你所愿");
                    //"SELECT * FROM  XJLS where ID=" + 账户概况.CurrentRow.Cells[0].Value + "  Order by RQ desc"; // Order by sl desc  asc 排序=从小到大，desc 排序=从大到小
                    SHUAXING("SELECT * FROM  XJLS where ID=" + 账户概况.CurrentRow.Cells[0].Value + "  Order by RQ desc", 账户流水);
                }
                else if (result == DialogResult.No)
                {
                    账户概况.CurrentRow.Cells[e.ColumnIndex].Value = 0;
                }
            }
        }//编辑停止时发生 

        private void 账户流水_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SqlHelper sh = new SqlHelper();
            String sql = string.Format("UPDATE [XJLS] SET SM='{0}' where DH='{1}'    ", 账户流水.CurrentRow.Cells[4].Value, 账户流水.CurrentRow.Cells[1].Value.ToString()); //  //改为坏账
            DataTable dt = sh.ExeQuery(sql);
        }


    }
}
