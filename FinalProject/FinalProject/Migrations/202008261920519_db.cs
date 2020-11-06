namespace FinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Street = c.String(nullable: false, maxLength: 250),
                        DetailedAddress = c.String(nullable: false, maxLength: 500),
                        Zipcode = c.String(nullable: false, maxLength: 10),
                        IsPrimary = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                        Username = c.String(maxLength: 50),
                        Password = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(maxLength: 15),
                        isRegistered = c.Boolean(nullable: false),
                        Image = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 1000),
                        Createdate = c.DateTime(nullable: false),
                        ParentCommentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BlogId)
                .Index(t => t.UserId)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Quote = c.String(maxLength: 1000),
                        Content = c.String(nullable: false),
                        Read = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .ForeignKey("dbo.BlogCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AdminId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Surname = c.String(nullable: false, maxLength: 20),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Image = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogId = c.Int(nullable: false),
                        BlogTagsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .ForeignKey("dbo.BlogTags", t => t.BlogTagsId, cascadeDelete: true)
                .Index(t => t.BlogId)
                .Index(t => t.BlogTagsId);
            
            CreateTable(
                "dbo.BlogTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Comment = c.String(maxLength: 500),
                        Rating = c.Byte(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SmallDesc = c.String(nullable: false, maxLength: 250),
                        megaDesc = c.String(nullable: false, maxLength: 500),
                        ProductNumber = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SubOfSubcategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SubOfSubcategories", t => t.SubOfSubcategoryId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SubOfSubcategoryId);
            
            CreateTable(
                "dbo.SubOfSubcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 250),
                        IconClass = c.String(maxLength: 50),
                        SubcategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subcategories", t => t.SubcategoryId, cascadeDelete: true)
                .Index(t => t.SubcategoryId);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 250),
                        IconClass = c.String(maxLength: 50),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 250),
                        IconClass = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        SalePercent = c.Decimal(storeType: "money"),
                        SaleBannerId = c.Int(),
                        Quantity = c.Int(nullable: false),
                        isNew = c.Boolean(nullable: false),
                        isTopSelling = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        SizeId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SaleBanners", t => t.SaleBannerId)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.SaleBannerId)
                .Index(t => t.SizeId)
                .Index(t => t.ColorId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        ProductInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .Index(t => t.ProductInfoId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        ProductInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfo_Id)
                .Index(t => t.OrderId)
                .Index(t => t.ProductInfo_Id);
            
            CreateTable(
                "dbo.SaleBanners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 250),
                        Name = c.String(maxLength: 150),
                        Title = c.String(maxLength: 250),
                        Content = c.String(maxLength: 500),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductInfoId);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SizeOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 50),
                        Value = c.String(nullable: false, maxLength: 50),
                        productInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.productInfoId, cascadeDelete: true)
                .Index(t => t.productInfoId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInfoes", t => t.ProductInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProductInfoId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 250),
                        IframeLink = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Subject = c.String(nullable: false, maxLength: 250),
                        Messages = c.String(nullable: false, maxLength: 1000),
                        SendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Partners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Questions = c.String(nullable: false, maxLength: 250),
                        Reply = c.String(nullable: false, maxLength: 500),
                        isGeneral = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogoDark = c.String(maxLength: 250),
                        LogoLight = c.String(maxLength: 250),
                        VideoLink = c.String(maxLength: 250),
                        AboutImg = c.String(maxLength: 250),
                        HomeImg = c.String(maxLength: 250),
                        AboutTitle = c.String(maxLength: 250),
                        AboutSubtitle = c.String(maxLength: 250),
                        AboutContent = c.String(maxLength: 1000),
                        Copyright = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialNets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Link = c.String(nullable: false, maxLength: 250),
                        IconClass = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 250),
                        SendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Terms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 250),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Position = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Wishlists", "UserId", "dbo.Users");
            DropForeignKey("dbo.Wishlists", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.SizeOptions", "productInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.ProductInfoes", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.ShoppingBags", "UserId", "dbo.Users");
            DropForeignKey("dbo.ShoppingBags", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.ProductInfoes", "SaleBannerId", "dbo.SaleBanners");
            DropForeignKey("dbo.ProductInfoes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "ProductInfo_Id", "dbo.ProductInfoes");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ProductImages", "ProductInfoId", "dbo.ProductInfoes");
            DropForeignKey("dbo.ProductInfoes", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.SubOfSubcategories", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Subcategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProCategories", "SubOfSubcategoryId", "dbo.SubOfSubcategories");
            DropForeignKey("dbo.ProCategories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Tags", "BlogTagsId", "dbo.BlogTags");
            DropForeignKey("dbo.Tags", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.BlogImages", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Blogs", "CategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.Blogs", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropIndex("dbo.Wishlists", new[] { "ProductInfoId" });
            DropIndex("dbo.Wishlists", new[] { "UserId" });
            DropIndex("dbo.SizeOptions", new[] { "productInfoId" });
            DropIndex("dbo.ShoppingBags", new[] { "ProductInfoId" });
            DropIndex("dbo.ShoppingBags", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "ProductInfo_Id" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.ProductImages", new[] { "ProductInfoId" });
            DropIndex("dbo.ProductInfoes", new[] { "ProductId" });
            DropIndex("dbo.ProductInfoes", new[] { "ColorId" });
            DropIndex("dbo.ProductInfoes", new[] { "SizeId" });
            DropIndex("dbo.ProductInfoes", new[] { "SaleBannerId" });
            DropIndex("dbo.Subcategories", new[] { "CategoryId" });
            DropIndex("dbo.SubOfSubcategories", new[] { "SubcategoryId" });
            DropIndex("dbo.ProCategories", new[] { "SubOfSubcategoryId" });
            DropIndex("dbo.ProCategories", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Tags", new[] { "BlogTagsId" });
            DropIndex("dbo.Tags", new[] { "BlogId" });
            DropIndex("dbo.BlogImages", new[] { "BlogId" });
            DropIndex("dbo.Blogs", new[] { "CategoryId" });
            DropIndex("dbo.Blogs", new[] { "AdminId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.Testimonials");
            DropTable("dbo.Terms");
            DropTable("dbo.Subscribes");
            DropTable("dbo.SocialNets");
            DropTable("dbo.Settings");
            DropTable("dbo.Questions");
            DropTable("dbo.Partners");
            DropTable("dbo.Messages");
            DropTable("dbo.Contacts");
            DropTable("dbo.Wishlists");
            DropTable("dbo.SizeOptions");
            DropTable("dbo.Sizes");
            DropTable("dbo.ShoppingBags");
            DropTable("dbo.SaleBanners");
            DropTable("dbo.OrderItems");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Colors");
            DropTable("dbo.ProductInfoes");
            DropTable("dbo.Categories");
            DropTable("dbo.Subcategories");
            DropTable("dbo.SubOfSubcategories");
            DropTable("dbo.ProCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Reviews");
            DropTable("dbo.Orders");
            DropTable("dbo.BlogTags");
            DropTable("dbo.Tags");
            DropTable("dbo.BlogImages");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.Admins");
            DropTable("dbo.Blogs");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Addresses");
        }
    }
}
