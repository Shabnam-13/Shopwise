namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class j : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleBanners", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.SaleBanners", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleBanners", "EndDate");
            DropColumn("dbo.SaleBanners", "StartDate");
        }
    }
}
