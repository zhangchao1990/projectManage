using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProjectManage.Models;

namespace ProjectManage.DAL
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("ProjectContext") { }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractDetail> ContractDetails { get; set; }
       

        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }

        public DbSet<BusinessCost> BusinessCosts { get; set; }

        public DbSet<OutsourcingCost> OutsourcingCosts { get; set; }

        public DbSet<Reimbursement> Reimbursements { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<Role> Roles { get; set; }


        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<CommModel> CommModels { get; set; }

    }
}