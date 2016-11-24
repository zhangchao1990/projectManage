using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProjectManage.Models {
    public class Contract {
        public int Id { get; set; }
        /// <summary>
        /// 销售合同编号
        /// </summary>
        public string ContractNum { get; set; }
        /// <summary>
        /// 销售负责人
        /// </summary>
        public string SalesManagers { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string CustmersDept { get; set; }
        /// <summary>
        /// 签订时间
        /// </summary>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ContractCount { get; set; }
        /// <summary>
        /// 开票时间
        /// </summary>

        public DateTime? BillingTime { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string BillNum { get; set; }

        public decimal RemainCount {
            get {
                if (string.IsNullOrEmpty(this.PayOrders)) {
                    return this.ContractCount;

                }
                else { return this.ContractCount - JsonConvert.DeserializeObject<List<PayOrder>>(this.PayOrders).Sum(a => a.PayCount); }
            }
        }


        /// <summary>
        /// 支付账单
        /// </summary>
        public string PayOrders { get; set; }


        /// <summary>
        /// 项目状态
        /// </summary>
        public string ProjectStatus { get; set; }

        /// <summary>
        /// 预估金额
        /// </summary>
        public decimal EstimatedAmount { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        public virtual ICollection<ContractDetail> ContractDetails { get; set; }

    }

    public class ContractVo {
        public int Id { get; set; }
        /// <summary>
        /// 销售合同编号
        /// </summary>
        public string ContractNum { get; set; }
        /// <summary>
        /// 销售负责人
        /// </summary>
        public string SalesManagers { get; set; }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }
        /// <summary>
        /// 客户单位
        /// </summary>
        public string CustmersDept { get; set; }
        /// <summary>
        /// 签订时间
        /// </summary>
        public DateTime? OrderTime { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal ContractCount { get; set; }
        /// <summary>
        /// 开票时间
        /// </summary>

        public DateTime? BillingTime { get; set; }
        /// <summary>
        /// 票号
        /// </summary>
        public string BillNum { get; set; }

        public decimal RemainCount {
            get;set;
        }

        public decimal ArrivalCount {

            get;
            set;
        }


        /// <summary>
        /// 支付账单
        /// </summary>
        public string PayOrders { get; set; }


        /// <summary>
        /// 项目状态
        /// </summary>
        public string ProjectStatus { get; set; }

        /// <summary>
        /// 预估金额
        /// </summary>
        public decimal EstimatedAmount { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 已发生开支
        /// </summary>
        public decimal ContractedCount { get; set; }

        /// <summary>
        ///  合同状态
        /// </summary>
        public int Status { get; set; }

    }
    public class PayOrder {
        public DateTime PayTime { get; set; }
        public decimal PayCount { get; set; }
    }
}