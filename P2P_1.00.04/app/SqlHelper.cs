using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
/// <summary>
///SqlHelper 的摘要说明
///用于Access数据库
///主要功能：
///         数据的添加、修改、删除、事务调用
/// </summary>
/// 

public class SqlHelper
{
    public static string Flie_Path = System.IO.Directory.GetCurrentDirectory() + "\\Data\\DATA.accdb";//当前路径
    public static string MYPassword = null;//获取数据库密码


    OleDbConnection con;
    DataTable dt;

    [DllImport("kernel32")] //读取文件
    public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    [DllImport("kernel32")]//写文件
    public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    public SqlHelper()
    {
        //if (Flie_Path.IndexOf("DM.accdb") > 0) //查路径中是否包含 DM.accdb
        //    MYPassword = "Q$#fd59Sd424*!d1fl6u";
        //else
        //    MYPassword = "Q$#f5d9Sd4osf*!6dFlu";
        String connStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Jet OLEDB:Database Password ={1}", Flie_Path, MYPassword);//;
        con = new OleDbConnection(connStr);   // TODO: 在此处添加构造函数逻辑
    }

    public bool ExeNoQuery(String sql)//添加、修改、删除
    {
        if (con.State == ConnectionState.Closed)
            con.Open();
        OleDbCommand cmd = new OleDbCommand(sql, con);
        if (cmd.ExecuteNonQuery() > 0)
        {
            con.Close();
            return true;
        }
        else
        {
            con.Close();
            return false;
        }
    }


    public DataTable ExeQuery(String sql)//查询
    {
        //if (con.State == ConnectionState.Closed)
        //    con.Open();
        OleDbDataAdapter oda = new OleDbDataAdapter(sql, con);
        dt = new DataTable();
        oda.Fill(dt);
        con.Close();
        return dt;
    }
    public DataTable ExeQuery1(String sql)//查询
    {
        OleDbDataAdapter oda = new OleDbDataAdapter(sql, con);
        dt = new DataTable();
        oda.Fill(dt);
        con.Close();
        if (dt.Rows[0][2].ToString() == "0")
        {
            dt.Rows[0][0] = "0.00";
            dt.Rows[0][1] = "0.00";
        }
        return dt;
    }

    public bool DiaoYongShiWu(String[] sql)//事务调用
    {
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        OleDbTransaction trans = con.BeginTransaction(IsolationLevel.ReadCommitted);
        OleDbCommand cmd = new OleDbCommand();
        cmd.Transaction = trans;
        cmd.Connection = con;
        try
        {
            foreach (String s in sql)
            {
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }
            trans.Commit();
            con.Close();
            return true;
        }
        catch (Exception e)
        {
            trans.Rollback();
            con.Close();
            return false;
        }
    }
    //
    //SqlHelper sh = new SqlHelper();
    //String[] sql = new String[2];
    //sql[0] = String.Format("insert into [DingDan]([DDID],[Applicant],[Financial],[CommitTime]) values('{0}',{1},{2},#{3}#)", DDID, comboBox2.SelectedValue, Parameter.YGID, CommitTime);
    //sql[1] = String.Format("insert into [DDXQ_Cash] values('{0}',{1},{2},{3},{4})", DDID, Double.Parse(textBox1.Text), Double.Parse(textBox2.Text), comboBox4.SelectedValue, comboBox5.SelectedValue);
    //sh.DiaoYongShiWu(sql)
    //

    /// <summary>
    /// 读取文件的MD5值
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetMD5(string sDataIn)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] bytValue, bytHash;
        bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
        bytHash = md5.ComputeHash(bytValue);
        md5.Clear();
        string sTemp = null;
        for (int i = 0; i < bytHash.Length; i++)
            sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
        return sTemp;
    }
    /// <summary>
    /// 计算日子(参照日子,处理对象, 增减日子）
    /// </summary>
    /// <param name="A"> 参照日子</param>
    /// <param name="B">年 or 月 or 日</param>
    /// <param name="C"> + or -  数字 </param>
    /// <returns></returns>
    public DateTime rq(DateTime A, string B, int C)
    {
        DateTime A1 = A.AddDays(C);
        switch (B)
        {
            case "天": //"天标"
                A1 = A.AddDays(C);//曾减  X 天
                break;
            case "月": //"月标"
                A1 = A.AddMonths(C);//曾减 X 月;    // 算成月息
                break;
            case "年": //"年标"
                A1 = A.AddYears(C);
                break;
                //    //default:
                //    break;
        }
        return A1;
        //dt.AddYears(1); //增加一年
        //            dt.AddMonths(-1);//减少一个月
        //            dt.AddDays(-20d);//减少20天

    }

}

//        DateTime dt = DateTime.Now;
//        Label1.Text = dt.ToString();//2005-11-5 13:21:25
//        Label2.Text = dt.ToFileTime().ToString();//127756416859912816
//        Label3.Text = dt.ToFileTimeUtc().ToString();//127756704859912816
//        Label4.Text = dt.ToLocalTime().ToString();//2005-11-5 21:21:25
//        Label5.Text = dt.ToLongDateString().ToString();//2005年11月5日
//        Label6.Text = dt.ToLongTimeString().ToString();//13:21:25
//        Label7.Text = dt.ToOADate().ToString();//38661.5565508218
//        Label8.Text = dt.ToShortDateString().ToString();//2005-11-5
//        Label9.Text = dt.ToShortTimeString().ToString();//13:21
//        Label10.Text = dt.ToUniversalTime().ToString();//2005-11-5 5:21:25
//          ?2005 - 11 - 5 13:30:28.4412864
//          Label1.Text = dt.Year.ToString();//2005
//        Label2.Text = dt.Date.ToString();//2005-11-5 0:00:00
//        Label3.Text = dt.DayOfWeek.ToString();//Saturday
//        Label4.Text = dt.DayOfYear.ToString();//309
//        Label5.Text = dt.Hour.ToString();//13
//        Label6.Text = dt.Millisecond.ToString();//441
//        Label7.Text = dt.Minute.ToString();//30
//        Label8.Text = dt.Month.ToString();//11
//        Label9.Text = dt.Second.ToString();//28
//        Label10.Text = dt.Ticks.ToString();//632667942284412864
//        Label11.Text = dt.TimeOfDay.ToString();//13:30:28.4412864
//        Label1.Text = dt.ToString();//2005-11-5 13:47:04
//        Label2.Text = dt.AddYears(1).ToString();//2006-11-5 13:47:04
//        Label3.Text = dt.AddDays(1.1).ToString();//2005-11-6 16:11:04
//        Label4.Text = dt.AddHours(1.1).ToString();//2005-11-5 14:53:04
//        Label5.Text = dt.AddMilliseconds(1.1).ToString();//2005-11-5 13:47:04
//        Label6.Text = dt.AddMonths(1).ToString();//2005-12-5 13:47:04
//        Label7.Text = dt.AddSeconds(1.1).ToString();//2005-11-5 13:47:05
//        Label8.Text = dt.AddMinutes(1.1).ToString();//2005-11-5 13:48:10
//        Label9.Text = dt.AddTicks(1000).ToString();//2005-11-5 13:47:04
//        Label10.Text = dt.CompareTo(dt).ToString();//0
//                                                   //Label11.Text = dt.Add(?).ToString();//问号为一个时间段
//        Label1.Text = dt.Equals("2005-11-6 16:11:04").ToString();//False
//        Label2.Text = dt.Equals(dt).ToString();//True
//        Label3.Text = dt.GetHashCode().ToString();//1474088234
//        Label4.Text = dt.GetType().ToString();//System.DateTime
//        Label5.Text = dt.GetTypeCode().ToString();//DateTime
//        Label1.Text = dt.GetDateTimeFormats('s')[0].ToString();//2005-11-05T14:06:25
//        Label2.Text = dt.GetDateTimeFormats('t')[0].ToString();//14:06
//        Label3.Text = dt.GetDateTimeFormats('y')[0].ToString();//2005年11月
//        Label4.Text = dt.GetDateTimeFormats('D')[0].ToString();//2005年11月5日
//        Label5.Text = dt.GetDateTimeFormats('D')[1].ToString();//2005 11 05
//        Label6.Text = dt.GetDateTimeFormats('D')[2].ToString();//星期六 2005 11 05
//        Label7.Text = dt.GetDateTimeFormats('D')[3].ToString();//星期六 2005年11月5日
//        Label8.Text = dt.GetDateTimeFormats('M')[0].ToString();//11月5日
//        Label9.Text = dt.GetDateTimeFormats('f')[0].ToString();//2005年11月5日 14:06
//        Label10.Text = dt.GetDateTimeFormats('g')[0].ToString();//2005-11-5 14:06
//        Label11.Text = dt.GetDateTimeFormats('r')[0].ToString();//Sat, 05 Nov 2005 14:06:25 GMT
//        Label1.Text =? string.Format("{0:d}", dt);//2005-11-5
//        Label2.Text =? string.Format("{0:D}", dt);//2005年11月5日
//        Label3.Text =? string.Format("{0:f}", dt);//2005年11月5日 14:23
//        Label4.Text =? string.Format("{0:F}", dt);//2005年11月5日 14:23:23
//        Label5.Text =? string.Format("{0:g}", dt);//2005-11-5 14:23
//        Label6.Text =? string.Format("{0:G}", dt);//2005-11-5 14:23:23
//        Label7.Text =? string.Format("{0:M}", dt);//11月5日
//        Label8.Text =? string.Format("{0:R}", dt);//Sat, 05 Nov 2005 14:23:23 GMT
//        Label9.Text =? string.Format("{0:s}", dt);//2005-11-05T14:23:23
//        Label10.Text = string.Format("{0:t}", dt);//14:23
//        Label11.Text = string.Format("{0:T}", dt);//14:23:23
//        Label12.Text = string.Format("{0:u}", dt);//2005-11-05 14:23:23Z
//        Label13.Text = string.Format("{0:U}", dt);//2005年11月5日 6:23:23
//        Label14.Text = string.Format("{0:Y}", dt);//2005年11月
//        Label15.Text = string.Format("{0}", dt);//2005-11-5 14:23:23?
//        Label16.Text = string.Format("{0:yyyyMMddHHmmssffff}", dt);
//        yyyymm等可以设置,比如Label16.Text = string.Format("{0:yyyyMMdd}",dt);

//            dt.AddYears(1); //增加一年
//            dt.AddMonths(-1);//减少一个月
//            dt.AddDays(-20d);//减少20天
//            dt.AddHours(+20d);//增加20小时
//            dt.AddMinutes(10d);//增加10分钟
//            dt.AddSeconds();//秒的加减
//            dt.AddMilliseconds();//毫秒的加减