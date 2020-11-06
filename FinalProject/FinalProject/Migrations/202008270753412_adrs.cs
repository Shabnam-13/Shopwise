namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adrs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "DetailedAddress", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "DetailedAddress", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
