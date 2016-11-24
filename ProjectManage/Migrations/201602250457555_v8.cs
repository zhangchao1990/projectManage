namespace ProjectManage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommModels", "Memo", c => c.String());
            AddColumn("dbo.CommModels", "SortNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommModels", "SortNum");
            DropColumn("dbo.CommModels", "Memo");
        }
    }
}
