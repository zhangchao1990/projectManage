namespace ProjectManage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseDetails", "TaxUnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PurchaseDetails", "TaxRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseDetails", "TaxRate");
            DropColumn("dbo.PurchaseDetails", "TaxUnitPrice");
        }
    }
}
