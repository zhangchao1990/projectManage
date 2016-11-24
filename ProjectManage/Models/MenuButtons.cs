using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class MenuButtons
    {
        public int Id { get; set; }

        public string HtmlId { get; set; }

        public string Name { get; set; }

        public virtual Menu Menu { get; set; }

        public DateTime CreateTime { get; set; }
    }
}