using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class Reimbursement
    {
        public int Id { get; set; }

        /// <summary>
        /// 报销类型 0 工程报销，1 差旅报销
        /// </summary>
        public int ReimbursementType { get; set; }

        /// <summary>
        /// 报销人
        /// </summary>
        public string ReimbursementPerson { get; set; }

        /// <summary>
        /// 报销时间
        /// </summary>
        public DateTime ReimbursementTime { get; set; }

        /// <summary>
        /// 出发时间
        /// </summary>
        public DateTime StartingTime { get; set; }
        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime ReturnTime { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public string Participant { get; set; }

        /// <summary>
        /// 交通费
        /// </summary>
        public decimal Fare { get; set; }

        /// <summary>
        /// 伙食补助
        /// </summary>
        public decimal FoodAllowance { get; set; }

        /// <summary>
        /// 住宿补助
        /// </summary>
        public decimal AccommodationAllowance { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherExpenses { get; set; }

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

        /// <summary>
        /// 合同
        /// </summary>
        public virtual Contract Contract { get; set; }
    }
}