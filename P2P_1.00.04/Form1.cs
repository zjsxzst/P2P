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
            收益预览();
            操作日期.TodayDate = DateTime.Now;
            操作日期确认();
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
            while (收益预览表.Rows.Count > 0) 收益预览表.Rows.RemoveAt(0); //逐条删除第一行
            DateTime T = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), T1 = T;
            SqlHelper sh = new SqlHelper();
            String sql = "SELECT HSQD.GHR, Sum(HSQD.BJ) AS BJ, Sum(HSQD.SY) AS SY, Count(HSQD.GHR) AS GHRCount, HSQD.ZTID FROM HSQD GROUP BY HSQD.GHR, HSQD.ZTID;";
            DataTable dt = sh.ExeQuery1(sql);
            IList<HKXX> LHKXX = TableProcessing<HKXX>.ConvertToModel(dt);

            List<HKXX> LHKXX_Bat = LHKXX.Where(m => m.GHR ==T && m.ZTID == 1).ToList();
            if(LHKXX_Bat.Count>0)
                收益预览表.Rows.Add("今日", LHKXX_Bat[0].GHRCount, LHKXX_Bat[0].BJ, LHKXX_Bat[0].SY, LHKXX_Bat[0].BJ + LHKXX_Bat[0].SY);

            T1 = sh.rq(T, "1",- 1);
            LHKXX_Bat = LHKXX.Where(m => m.GHR == T1 && m.ZTID == 1).ToList();
            if (LHKXX_Bat.Count > 0)
                收益预览表.Rows.Add("今  日", LHKXX_Bat[0].GHRCount, LHKXX_Bat[0].BJ, LHKXX_Bat[0].SY, LHKXX_Bat[0].BJ + LHKXX_Bat[0].SY);

            int a = 0;
            ////double A = 0, B = 0, C = 0, D = 0, E = 0, F = 0;
            ////int Z = 0, G = 0;

            ////String sql = String.Format("SELECT sum(BJ) AS A, SUM(SY) AS B ,count(*) as  cnt FROM  HSQD  where  GHR  between  #{0}# and #{1}#  AND  ZTID=1",
            ////     T.ToShortDateString().ToString(), T.ToShortDateString().ToString());
            ////DataTable dt = sh.ExeQuery1(sql);
            ////Z = int.Parse(dt.Rows[0][2].ToString());
            ////A = double.Parse(dt.Rows[0][0].ToString());
            ////B = double.Parse(dt.Rows[0][1].ToString());
            ////C = A + B; G = G + Z; D = D + A; E = E + B; F = F + C;  收益预览表.Rows.Add("今    日", Z, A, B, C);

            ////T1 = sh.rq(T, "1", 1);
            ////sql = String.Format("SELECT sum(BJ) AS A, SUM(SY) AS B ,count(*) as  cnt FROM  HSQD  where  GHR  between  #{0}# and #{1}#  AND  ZTID=1 ",
            ////                T1.ToShortDateString().ToString(), T1.ToShortDateString().ToString());
            ////dt = sh.ExeQuery1(sql);
            ////Z = int.Parse(dt.Rows[0][2].ToString());
            ////A = double.Parse(dt.Rows[0][0].ToString());
            ////B = double.Parse(dt.Rows[0][1].ToString());
            ////C = A + B; G = G + Z; D = D + A; E = E + B; F = F + C;    收益预览表.Rows.Add("明    日",Z, A, B, C);

            //////收益预览表.Rows.Add("未来一周", 1, 2, 3, 5);
            //////收益预览表.Rows.Add("未来一月", 1, 2, 3, 5);
            //////收益预览表.Rows.Add("未来三月", 1, 2, 3, 5);
            //////收益预览表.Rows.Add("未来六月", 1, 2, 3, 5);

            ////T1 = sh.rq(T, "1", -1);
            ////sql = String.Format("SELECT sum(BJ) AS A, SUM(SY) AS B ,count(*) as  cnt FROM  HSQD  where  GHR  between  #{0}# and #{1}#   AND  ZTID=2",
            ////               T1.ToShortDateString().ToString(), T1.ToShortDateString().ToString());
            ////dt = sh.ExeQuery1(sql);
            ////Z = int.Parse(dt.Rows[0][2].ToString());
            ////A = double.Parse(dt.Rows[0][0].ToString());
            ////B = double.Parse(dt.Rows[0][1].ToString());
            ////C = A + B; G = G + Z; D = D + A; E = E + B; F = F + C; 收益预览表.Rows.Add("昨    日", Z, A, B, C);
            ////List<testc> ss = new List<testc>();
            ////ss.Where(m => m.列明 == "x");

            //////收益预览表.Rows.Add("过去一周", 1, 2, 3, 5);
            //////收益预览表.Rows.Add("过去一月", 1, 2, 3, 5);
            //////收益预览表.Rows.Add("过去三月", 1, 2, 3, 5);
            //////收益预览表.Rows.Add("过去六月", 1, 2, 3, 5);
            ////收益预览表.Rows.Add("总    计", G, D, E, F);
        }
        #region  折叠代码 网页浏览
        private void WY1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY1.Tag.ToString());
        }
        private void WY2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY2.Tag.ToString());
        }

        private void WY3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY3.Tag.ToString());
        }
        private void WY4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY4.Tag.ToString());
        }
        private void WY5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY5.Tag.ToString());
        }
        private void WY6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY6.Tag.ToString());
        }
        private void WY7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY7.Tag.ToString());
        }

        private void WY8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY8.Tag.ToString());
        }
        private void WY9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY9.Tag.ToString());
        }
        private void WY10_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY10.Tag.ToString());
        }
        private void WY11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", WY11.Tag.ToString());
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

        private void 收益预览表_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}