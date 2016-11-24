using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string DeptName { get; set; }

        public string Memo { get; set; }

        public DateTime CreateTime { get; set; }

        public int SortId { get; set; }

        public int ParentId { get; set; }

        public virtual ICollection<Employer> Employers { get; set; }
    }
}