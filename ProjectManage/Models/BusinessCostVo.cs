using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class BusinessCostVo
    {
        public int Id { get; set; }

        /// <summary>
        /// 开支名称
        /// </summary>
        public string CostName { get; set; }

        /// <summary>
        /// 开支金额
        /// </summary>
        public decimal CostAmount { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        public int ContractId { get; set; }

        
    }
}