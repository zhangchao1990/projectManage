using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class Employer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserCount { get; set; }

        public string Password { get; set; }

        public string Memo { get; set; }

        public virtual Department Department { get; set; }

        public virtual Role Role { get; set; }


    }
}