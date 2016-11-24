using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManage.Models
{
    public class Menu
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int PId { get; set; }

        public DateTime CreatTime { get; set; }

        public int OrderNum { get; set; }

        public virtual ICollection<MenuButtons> MenuButtons { get; set; }
    }
}