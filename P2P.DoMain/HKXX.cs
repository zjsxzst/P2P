using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P.DoMain
{
    public class HKXX
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime GHR { get; set; }
        /// <summary>
        /// 本金
        /// </summary>
        public Decimal BJ { get; set; }
        /// <summary>
        /// 收益
        /// </summary>
        public Decimal SY { get; set; }
        /// <summary>
        /// 回款状态
        /// </summary>
        public int ZTID { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public int GHRCount { get; set; }

    }
    public class HZCL
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 在投
        /// </summary>
        public Decimal ZT { get; set; }
        /// <summary>
        /// 伪利润
        /// </summary>
        public Decimal WLR { get; set; }
        /// <summary>
        /// 坏账
        /// </summary>
        public Decimal HZ { get; set; }
        /// <summary>
        /// 新增坏账（本金）
        /// </summary>
        public Decimal BJ { get; set; }
    }
    public class HKFX
    {
        public String  平台名称 { get; set; }
        public Decimal 提前回款 { get; set; }
        public Decimal 正常回款 { get; set; }
        public Decimal 逾期未收 { get; set; }
        public Decimal 逾期已收 { get; set; }
        public Decimal 坏账 { get; set; }
    }
    public class HKFX1
    {
        public String 平台名称 { get; set; }
        public double 提前回款 { get; set; }
        public double 正常回款 { get; set; }
        public double 逾期未收 { get; set; }
        public double 逾期已收 { get; set; }
        public double 坏账 { get; set; }
    }
}
