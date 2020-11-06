namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "SmallDesc", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Products", "megaDesc", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "megaDesc", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Products", "SmallDesc", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
