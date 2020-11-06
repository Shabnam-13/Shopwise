namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingBags", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.ShoppingBags", "UserId", "dbo.Users");
            DropForeignKey("dbo.Wishlists", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.Wishlists", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "ProductInfo_Id", "dbo.ProductInfoes");
            DropIndex("dbo.OrderItems", new[] { "ProductInfo_Id" });
            DropIndex("dbo.ShoppingBags", new[] { "UserId" });
            DropIndex("dbo.ShoppingBags", new[] { "ProductInfoId" });
            DropIndex("dbo.Wishlists", new[] { "UserId" });
            DropIndex("dbo.Wishlists", new[] { "ProductInfoId" });
            RenameColumn(table: "dbo.OrderItems", name: "ProductInfo_Id", newName: "ProductInfoId");
            AlterColumn("dbo.OrderItems", "ProductInfoId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "ProductInfoId");
            AddForeignKey("dbo.OrderItems", "ProductInfoId", "dbo.ProductInfoes", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderItems", "ProductId");
            DropTable("dbo.ShoppingBags");
            DropTable("dbo.Wishlists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductInfoId = c.Int(nullable: false),
                        ProQuantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingBags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductInfoId = c.Int(nullable: false),
                        ProQuantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OrderItems", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderItems", "ProductInfoId", "dbo.ProductInfoes");
            DropIndex("dbo.OrderItems", new[] { "ProductInfoId" });
            AlterColumn("dbo.OrderItems", "ProductInfoId", c => c.Int());
            RenameColumn(table: "dbo.OrderItems", name: "ProductInfoId", newName: "ProductInfo_Id");
            CreateIndex("dbo.Wishlists", "ProductInfoId");
            CreateIndex("dbo.Wishlists", "UserId");
            CreateIndex("dbo.ShoppingBags", "ProductInfoId");
            CreateIndex("dbo.ShoppingBags", "UserId");
            CreateIndex("dbo.OrderItems", "ProductInfo_Id");
            AddForeignKey("dbo.OrderItems", "ProductInfo_Id", "dbo.ProductInfoes", "Id");
            AddForeignKey("dbo.Wishlists", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Wishlists", "ProductInfoId", "dbo.ProductInfoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingBags", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingBags", "ProductInfoId", "dbo.ProductInfoes", "Id", cascadeDelete: true);
        }
    }
}
