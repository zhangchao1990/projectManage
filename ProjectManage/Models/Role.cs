using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}