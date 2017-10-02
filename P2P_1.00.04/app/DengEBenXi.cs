using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 还息续投  先息后本 等额本金  等额本息  一次性还本息

//等额本金还款法:每季还款额=贷款本金÷贷款期季数+（本金-已归还本金累计额）×季利率 

//          等额本息法  按月付息到期还本       一次性还本付息

// "还息续投"  "先息后本" "等额本息" "等额本金" "一次性本息":

public class JX
{
    //  本金  年利率  周期  利息费用
    public static List<Bt> TB(double amount, double yearRate, int months, double lx)
    {
        /// <summary>
        /// 天标
        /// </summary>
        /// <param name="amountT">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="monthsx">投资期限，单位：天</param>
        /// <returns></returns>
        /// 

        var a = yearRate;     //年利率转为日利率  
        var b = amount; // 偿还本金
        var c = b * a * months; // 偿还利息

        Bt unit = new Bt();
        var datalist = new List<Bt>();
        unit.QS = 1;// 期数
        unit.BJ = Math.Round(b * 100) / 100;// 偿还本金
        unit.SY = Math.Round(c * 100 * lx) / 100;// 偿还利息
        datalist.Add(unit);
        return datalist;
    }     // 天标
    public static List<Bt> YcxBx(double amount, double yearRate, int months, double lx)
    {
        /// <summary>
        /// 一次性还本付息
        /// </summary>
        /// <param name="amountT">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="monthsx">投资期限，单位：天</param>
        /// <returns></returns>
        /// 
        var a = yearRate;  //年利率 
        var b = amount; // 偿还利息
        var c = b * a * months; // 偿还利息

        Bt unit = new Bt();
        var datalist = new List<Bt>();
        unit.QS = 1;// 期数
        unit.BJ = Math.Round(b * 100) / 100;// 偿还本金
        unit.SY = Math.Round(c * 100 * lx) / 100;// 偿还利息
        datalist.Add(unit);
        return datalist;
    }  // 一次性还本付息
    public static List<Bt> XXHB(double amount, double yearRate, int months, double lx)
    {
        /// <summary>
        /// 按月付息到期还本
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        var datalist = new List<Bt>();  //new Array(Deadline);     //

        double rateIncome = amount * yearRate * months;
        double rateIncomeEve = (rateIncome / months);

        var total = amount + rateIncome;

        for (var i = 1; i < months; i++)
        {
            var unit = new Bt();
            unit.QS = i;// 期数
            unit.SY = Math.Round(rateIncomeEve * 100 * lx) / 100;// 偿还利息
            unit.BJ = 0;// 偿还本金
            datalist.Add(unit);
        }

        datalist.Add(new Bt()
        {
            QS = months,
            SY = Math.Round(rateIncomeEve * 100 * lx) / 100,
            BJ = amount,
        });
        return datalist;
    }  // 先息后本
    public static List<Bt> DEBX(double amount, double yearRate, int months, double lx)
    {
        /// <summary>
        /// 等额本息法
        /// </summary>
        /// <param name="amountT">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="monthsx">投资期限，单位：月</param>
        /// <returns></returns>
        var monthRate = (yearRate);     //年利率转为月利率
        var datalist = new List<Bt>();
        var i = 0;
        var a = 0.0; // 偿还本息
        var b = 0.0; // 偿还利息
        var c = 0.0; // 偿还本金
                     //利息收益
        var totalRateIncome =
            (amount * months * monthRate * Math.Pow((1 + monthRate), months)) / (Math.Pow((1 + monthRate), months) - 1) - amount;
        var totalIncome = totalRateIncome + amount;
        var d = amount + totalRateIncome; // 剩余本金

        totalRateIncome = Math.Round(totalRateIncome * 100) / 100;// 支付总利息
        totalIncome = Math.Round(totalIncome * 100) / 100;
        a = totalIncome / months;    //每月还款本息
        a = Math.Round(a * 100) / 100;//每月还款本息

        for (i = 1; i <= months; i++)
        {
            b = (amount * monthRate * (Math.Pow((1 + monthRate), months) - Math.Pow((1 + monthRate), (i - 1)))) / (Math.Pow((1 + monthRate), months) - 1);
            b = Math.Round(b * 100 * lx) / 100;
            c = a - b;
            c = Math.Round(c * 100) / 100;
            d = d - a;
            d = Math.Round(d * 100) / 100;
            if (i == months)
            {
                c = c + d;
                b = b - d;
                c = Math.Round(c * 100) / 100;
                b = Math.Round(b * 100 * lx) / 100;
                d = 0;
            }

            var unit = new Bt();
            unit.QS = i;// 期数
            unit.SY = b;// 偿还利息
            unit.BJ = c;// 偿还本金
            datalist.Add(unit);
        }
        return datalist;
    }  // 等额本息法

    public static List<Bt> DEBJ(double amount, double yearRate, int months, double lx)
    {
        /// <summary>
        /// 等额本金法
        /// </summary>
        /// <param name="amountT">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="monthsx">投资期限，单位：月</param>
        /// <returns></returns>
        /// 
        var MEIYUEHUAN = amount / months; //每月归还本金

        var monthRate = yearRate;     //年利率转为月利率
        var datalist = new List<Bt>();
        var i = 0;

        var b = 0.0; // 偿还利息
        var c = amount / months; // 偿还本金
                                 //利息收益

        for (i = 1; i <= months; i++)
        {
            b = amount * yearRate * lx;
            b = Math.Round(b * 100) / 100;
            c = Math.Round(c * 100) / 100;
            var unit = new Bt();
            unit.QS = i;// 期数
            unit.SY = b;// 偿还利息
            unit.BJ = c;// 偿还本金
            datalist.Add(unit);
            amount = amount - MEIYUEHUAN;
        }
        return datalist;
    }  // 等额本金法

    public static List<Bt> HXXT(double amount, double yearRate, int months, double lx)
    {
        /// <summary>
        /// 等额本息法
        /// </summary>
        /// <param name="amountT">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="monthsx">投资期限，单位：月</param>
        /// <returns></returns>
        Bt unit = new Bt();
        var datalist = new List<Bt>();
        unit.QS = 1;// 期数
        unit.BJ = amount;// 偿还本金
        unit.SY = 0;// 偿还利息
        datalist.Add(unit);
        return datalist;
    }   // 还息续投法

   
}
public class Bt
{
    public double QS { get; set; } //序列
    public double BJ { get; set; }  //本期应还本金
    public double SY { get; set; } //本期应还利息
}  //定义表格列


public class DengEBenXi
{

    public static List<BackUnit> Compute(double amount, double yearRate, int months)
    {
        /// <summary>
        /// 等额本息法
        /// </summary>
        /// <param name="amountT">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="monthsx">投资期限，单位：月</param>
        /// <returns></returns>
        var monthRate = (yearRate) / 1200.0;     //年利率转为月利率
        var datalist = new List<BackUnit>();
        var i = 0;
        var a = 0.0; // 偿还本息
        var b = 0.0; // 偿还利息
        var c = 0.0; // 偿还本金
                     //利息收益
        var totalRateIncome =
            (amount * months * monthRate * Math.Pow((1 + monthRate), months)) / (Math.Pow((1 + monthRate), months) - 1) - amount;
        var totalIncome = totalRateIncome + amount;
        var d = amount + totalRateIncome; // 剩余本金

        totalRateIncome = Math.Round(totalRateIncome * 100) / 100;// 支付总利息
        totalIncome = Math.Round(totalIncome * 100) / 100;
        a = totalIncome / months;    //每月还款本息
        a = Math.Round(a * 100) / 100;//每月还款本息

        for (i = 1; i <= months; i++)
        {
            b = (amount * monthRate * (Math.Pow((1 + monthRate), months) - Math.Pow((1 + monthRate), (i - 1)))) / (Math.Pow((1 + monthRate), months) - 1);
            b = Math.Round(b * 100) / 100;
            c = a - b;
            c = Math.Round(c * 100) / 100;
            d = d - a;
            d = Math.Round(d * 100) / 100;
            if (i == months)
            {
                c = c + d;
                b = b - d;
                c = Math.Round(c * 100) / 100;
                b = Math.Round(b * 100) / 100;
                d = 0;
            }

            var unit = new BackUnit();
            unit.Number = i;// 期数
            unit.TotalRate = totalRateIncome;// 总利息
            unit.TotalMoney = totalIncome;// 总还款
            unit.A = a;// 偿还本息  someNumber.ToString("N2");
            unit.B = b;// 偿还利息
            unit.C = c;// 偿还本金
            unit.D = d;// 剩余本金
            datalist.Add(unit);
        }

        return datalist;
    }
}
public class AnYueFuxiDaoqiHuanBen
{

    public static List<BackUnit> Compute(double amount, double yearRate, int months)
    {
        /// <summary>
        /// 按月付息到期还本
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        var datalist = new List<BackUnit>();  //new Array(Deadline);     //

        double rateIncome = amount * yearRate / 100 * (months / 12.0);
        double rateIncomeEve = (rateIncome / months);

        var total = amount + rateIncome;

        for (var i = 1; i < months; i++)
        {
            var unit = new BackUnit();
            unit.Number = i;// 期数
            unit.TotalRate = rateIncome;//Math.Round((Amount + TotalRate) * 100) / 100;// 总利息
            unit.TotalMoney = total;//TotalRate;// 总还款
            unit.A = Math.Round(rateIncomeEve * 100) / 100;   // rateIncomeEve; 偿还本息  someNumber.ToString("N2");
            unit.B = Math.Round(rateIncomeEve * 100) / 100;// 偿还利息
            unit.C = 0;// 偿还本金
            unit.D = Math.Round((amount * 1 + rateIncome * 1 - rateIncomeEve * i) * 100) / 100;// 剩余本金
            datalist.Add(unit);
        }

        datalist.Add(new BackUnit()
        {
            Number = months,
            TotalRate = rateIncome,
            TotalMoney = total,
            A = amount + rateIncomeEve,
            B = Math.Round(rateIncomeEve * 100) / 100,
            C = amount,
            D = 0
        });
        return datalist;
    }
}
public class YicixingHuanBenFuxi
{

    public static List<BackUnit> Compute(double amount, double yearRate, int months)
    {
        /// <summary>
        /// 一次性还本付息
        /// </summary>
        /// <param name="amount">投资金额</param>
        /// <param name="yearRate">年利率</param>
        /// <param name="months">投资期限，单位：月</param>
        /// <returns></returns>
        BackUnit unit = new BackUnit();
        var datalist = new List<BackUnit>();
        var rate = yearRate;
        var rateIncome = amount * rate / 100 * (months / 12.0);
        var totalIncome = amount + rateIncome;

        unit.Number = 1;// 期数
        unit.TotalRate = rateIncome;//Math.Round((Amount + TotalRate) * 100) / 100;// 总利息
        unit.TotalMoney = totalIncome;//TotalRate;// 总还款
        unit.A = totalIncome;// 偿还本息  someNumber.ToString("N2");
        unit.B = rateIncome;// 偿还利息
        unit.C = 0;// 偿还本金
        unit.D = 0;// 剩余本金 
        datalist.Add(unit);
        return datalist;
    }
}
public class BackUnit
{
    //当前期数
    public int Number { get; set; }
    //总利息
    public double TotalRate { get; set; }
    //总还款
    public double TotalMoney { get; set; }
    //本期应还全部
    public double A { get; set; }
    //本期应还利息
    public double B { get; set; }
    //本期应还本金
    public double C { get; set; }
    //本期剩余本金
    public double D { get; set; }
}

//            dataGridView1.DataSource = DengEBenXi.Compute(11111, 12, 5);
//              dataGridView2.DataSource = YicixingHuanBenFuxi.Compute(11111, 12, 12);
//              dataGridView3.DataSource = AnYueFuxiDaoqiHuanBen.Compute(11111, 12, 5); 
