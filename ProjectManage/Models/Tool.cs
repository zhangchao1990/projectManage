using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class Tool
    {
        public int Id { get; set; }
        /// <summary>
        /// 工具名称
        /// </summary>
        public string ToolName { get; set; }
        /// <summary>
        /// 工具编号
        /// </summary>
        public string ToolNum { get; set; }
        /// <summary>
        /// 借出人
        /// </summary>
        public string BorrowPerson { get; set; }
        /// <summary>
        /// 使用地点
        /// </summary>
        public string UsedPlace { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime BorrowDate { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime ReturnDate { get; set; }
        /// <summary>
        /// 工具状态
        /// </summary>
        public int ToolStatus  { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

    }
}