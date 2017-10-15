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
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 坏账
        /// </summary>
        public Decimal HZ { get; set; }
    }
}
