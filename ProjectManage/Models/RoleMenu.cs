using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class RoleMenu
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        public string MenuButtons { get; set; }

        public virtual Role Role { get; set; }

        public virtual Menu Menu { get; set; }
    }
}