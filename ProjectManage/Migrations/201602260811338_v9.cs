namespace ProjectManage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OutsourcingCosts", "Days", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OutsourcingCosts", "Days", c => c.Int(nullable: false));
        }
    }
}
