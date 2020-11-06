namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductNumber", c => c.String(nullable: false));
        }
    }
}
