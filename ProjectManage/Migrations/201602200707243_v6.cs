namespace ProjectManage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PurchaseDetails", "Count", c => c.Int());
            AlterColumn("dbo.PurchaseDetails", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseDetails", "TaxRate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseDetails", "PriceSum", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseDetails", "PriceSum", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseDetails", "TaxRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseDetails", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PurchaseDetails", "Count", c => c.Int(nullable: false));
        }
    }
}
