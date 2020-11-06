namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dll : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SaleBanners", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.SaleBanners", "Title", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SaleBanners", "Title", c => c.String(maxLength: 250));
            AlterColumn("dbo.SaleBanners", "Name", c => c.String(maxLength: 150));
        }
    }
}
