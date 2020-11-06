namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductInfoes", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.ProductInfoes", "SizeId", "dbo.Sizes");
            DropIndex("dbo.ProductInfoes", new[] { "SizeId" });
            DropIndex("dbo.ProductInfoes", new[] { "ColorId" });
            AlterColumn("dbo.ProductInfoes", "SizeId", c => c.Int());
            AlterColumn("dbo.ProductInfoes", "ColorId", c => c.Int());
            CreateIndex("dbo.ProductInfoes", "SizeId");
            CreateIndex("dbo.ProductInfoes", "ColorId");
            AddForeignKey("dbo.ProductInfoes", "ColorId", "dbo.Colors", "Id");
            AddForeignKey("dbo.ProductInfoes", "SizeId", "dbo.Sizes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInfoes", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.ProductInfoes", "ColorId", "dbo.Colors");
            DropIndex("dbo.ProductInfoes", new[] { "ColorId" });
            DropIndex("dbo.ProductInfoes", new[] { "SizeId" });
            AlterColumn("dbo.ProductInfoes", "ColorId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductInfoes", "SizeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProductInfoes", "ColorId");
            CreateIndex("dbo.ProductInfoes", "SizeId");
            AddForeignKey("dbo.ProductInfoes", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductInfoes", "ColorId", "dbo.Colors", "Id", cascadeDelete: true);
        }
    }
}
