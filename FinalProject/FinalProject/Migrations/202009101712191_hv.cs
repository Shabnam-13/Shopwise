namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hv : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "ShipDate");
            DropColumn("dbo.Orders", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Code", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Orders", "ShipDate", c => c.DateTime(nullable: false));
        }
    }
}
