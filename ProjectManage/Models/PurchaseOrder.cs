using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        /// <summary>
        /// 采购申请编号
        /// </summary>
        public string PurchaseNum { get; set; }

        /// <summary>
        /// 采购申请人
        /// </summary>
        public string PurchasePerson { get; set; }

        /// <summary>
        /// 采购申请批准
        /// </summary>
        public string PurchaseApproved { get; set; }

        /// <summary>
        /// 采购时间
        /// </summary>
        public DateTime PurchaseTime { get; set; }

        /// <summary>
        /// 项目负责人
        /// </summary>
        public string ProjectManager { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 合同
        /// </summary>
        public virtual Contract Contract { get; set; }

        /// <summary>
        /// 采购明细
        /// </summary>
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }


    }
}