namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderItems", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Price", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
