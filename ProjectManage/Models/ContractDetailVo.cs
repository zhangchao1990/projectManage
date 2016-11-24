using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class ContractDetailVo
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
        public int Count { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal PriceSum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        public int ContranctId { get; set; }
    }
}