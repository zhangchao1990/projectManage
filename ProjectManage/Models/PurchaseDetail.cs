using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class PurchaseDetail
    {
        public int Id { get; set; }

        /// <summary>
        /// 设备材料名称
        /// </summary>
        public string EquipmentMaterialName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specifications { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Count { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// 含税单价
        /// </summary>
        public decimal TaxUnitPrice { get; set; }

        /// <summary>
        /// 税率
        /// </summary>
        public decimal? TaxRate { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal? PriceSum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 到货地点
        /// </summary>
        public string ArrivalAddress { get; set; }

        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime? ArrivalTime { get; set; }

        /// <summary>
        /// 签收人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime? ReceiveTime { get; set; }

        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialNum { get; set; } 

        /// <summary>
        /// 采购明细
        /// </summary>
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        /// <summary>
        /// 1、采购申请 红色 2、采购过程 蓝色 3、采购统计 黑色
        /// </summary>
        public int Status { get; set; }
        

    }
}