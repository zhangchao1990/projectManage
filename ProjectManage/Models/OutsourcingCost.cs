using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class OutsourcingCost
    {
        public int Id { get; set; }

        /// <summary>
        /// 外包工人名称
        /// </summary>
        public string OutsourcingPeopleName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public decimal Days { get; set; }

        /// <summary>
        /// 工资
        /// </summary>
        public decimal Pay { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// 审核金额
        /// </summary>
        public decimal AuditAmount { get; set; } 

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        public virtual Contract Contract { get; set; }
    }
}