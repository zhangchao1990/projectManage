namespace ProjectManage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contracts", "ProjectStatus", c => c.String());
            AddColumn("dbo.Contracts", "EstimatedAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Contracts", "TaxRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Contracts", "OrderNum", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contracts", "OrderNum");
            DropColumn("dbo.Contracts", "TaxRate");
            DropColumn("dbo.Contracts", "EstimatedAmount");
            DropColumn("dbo.Contracts", "ProjectStatus");
        }
    }
}
