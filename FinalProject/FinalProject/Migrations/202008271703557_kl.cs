namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ProductNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductNumber", c => c.String());
        }
    }
}
